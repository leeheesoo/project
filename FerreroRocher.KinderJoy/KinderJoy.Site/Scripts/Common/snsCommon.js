/* Twitter Login */
function twitterLogin(contentUrl) {
    //var pop = window.open('https://' + location.host + '/Twitter/LoginCheck', 'twitterLogin', 'width=720, height=540');    
    var pop = window.open(location.protocol + '//' + location.host + '/Twitter/LoginCheck', 'twitterLogin', 'width=720, height=540');
    if (pop == null) {
        alert('팝업이 차단되어 있어서 정상적인 진행이 어렵습니다. \n[도구 > 팝업차단] 에서 \'팝업 차단 사용 안 함\' 으로 설정해 주시기 바랍니다. ');
        return;
    }
}

/* Facebook Share */
var fbMove;
function facebookShare(data, callback, failCallback) {
    FB.getLoginStatus(function (response) {
        if (response.status === 'connected') {
            if (typeof callback == "function") {
                FB.api('/me?fields=name,picture', function (res) {
                    FB.ui({
                        method: 'feed',
                        link: data.link,
                        picture: data.picture,
                        name: data.name,
                        description: data.description,
                        display: 'iframe'
                    }, function (resp) {
                        var scrapURL = '';
                        if (resp && resp.post_id) {
                            var postId = resp.post_id;
                            postId = postId.replace("_", "/posts/");
                            scrapURL = 'https://www.facebook.com/' + postId;
                            if (typeof callback == "function") {
                                //res.picture.data.is_silhouette : 프로필이미지 기본값 여부
                                callback(response.authResponse.userID, res.name, res.picture.data.is_silhouette, res.picture.data.url, scrapURL);
                            }
                        } else {
                            if (typeof failCallback == "function") {
                                failCallback();
                            }
                        }
                    });
                    if (!isMobile.any()) {
                        if (!data.top) {
                            data.top = 1000;
                        }
                        fbMove = setInterval(function () {
                            $('.fb_dialog').each(function (index) {
                                var top = data.top + 'px';
                                var windowScrollTop = data.top + 10;
                                if ($(this).css('top') != top) {
                                    $(this).css('top', top);
                                    window.scrollTo(0, windowScrollTop);
                                } else {
                                    clearInterval(fbMove);
                                }
                            });
                        }, 500);
                    }
                });
            }
        } else {
            FB.login(function (response) {
                if (response.authResponse) {
                    facebookShare(data, callback, failCallback);
                } else {
                    if (typeof failCallback == "function") {
                        failCallback();
                    }
                }
            });
        }
    });
}
/* Kakaostory Image Share */
function kakaostoryShareImage(data, callback, failCallback) {
    var scrapURL = null;
    var userId = -1;
    var snsImage = '';
    Kakao.Auth.login({
        success: function (authObj) {            
            Kakao.API.request({
                url: '/v1/api/story/post/photo',
                data: {
                    image_url_list: [data.picture],
                    content: data.description
                }
            }).then(function (res) {
                scrapURL = 'https://story.kakao.com/' + res.id.replace('.', '/');
                return Kakao.API.request({
                    url: '/v1/user/me'
                });
            }).then(function (res) {
                userId = res.id;
                return Kakao.API.request({
                    url: '/v1/api/story/profile',
                    data: {
                        secure_resource: true
                    }
                });
            }).then(function (res) {
                snsImage = res.thumbnailURL;
                if (typeof callback == "function") {
                    callback(userId, res.nickName, snsImage, scrapURL);
                }
            }, function (err) {
                if (typeof failCallback == "function") {
                    failCallback(JSON.stringify(err));
                }
            });
        },
        fail: function (res) {
            alert(res.error);
            alert(res.error_description);
        }
    });
}

/* Kakaostory Video link Share */
function kakaostoryShareVideo(data, callback, failCallback) {
    var scrapURL = null;
    var userId = -1;
    Kakao.Auth.login({
        success: function (authObj) {
            Kakao.API.request({
                url: '/v1/api/story/linkinfo',
                data: {
                    url: data.link
                }
            }).then(function (res) {
                return Kakao.API.request({
                    url: '/v1/api/story/post/link',
                    data: {
                        link_info: res,
                        content: data.description
                    }
                });
            }).then(function (res) {
                scrapURL = 'https://story.kakao.com/' + res.id.replace('.', '/');
                return Kakao.API.request({
                    url: '/v1/user/me'
                });
            }).then(function (res) {
                userId = res.id;
                return Kakao.API.request({
                    url: '/v1/api/story/profile'
                });
            }).then(function (res) {
                if (typeof callback == "function") {
                    callback(userId, res.nickName, scrapURL);
                }
            }, function (err) {
                if (typeof failCallback == "function") {
                    failCallback(JSON.stringify(err));
                }
            });
        },
        fail: function (res) {
            alert(res.error);
            alert(res.error_description);
        }
    });
}

/* Kakaotalk Share */
function kakaotalkShare(data, callback, failCallback) {
    if (!isMobile.any()) {
        if (typeof failCallback == "function") {
            failCallback();
        }
    } else {
        Kakao.Link.sendTalkLink({
            label: data.description,
            webButton: {
                text: data.btnText,
                url: data.link
            },
            image: {
                src: data.picture,
                width: 300,
                height: 200
            },
            fail: function () {
                alert('카카오톡 공유는 모바일로만 가능합니다.');
            }
        });
        setTimeout(function () {
            if (typeof callback == "function") {
                callback();
            }
        }, 5000);
    }
}

/* Kakaostory link Share */
function kakaostoryShareLink(data, callback, failCallback) {
    var postId = null;
    Kakao.Auth.getStatus(function (statusObj) {
        if (statusObj.status == 'connected') {
            Kakao.API.request({
                url: '/v1/api/story/linkinfo',
                data: {
                    url: data.link
                }
            }).then(function (res) {
                // 이전 API 호출이 성공한 경우 다음 API를 호출합니다.
                return Kakao.API.request({
                    url: '/v1/api/story/post/link',
                    data: {
                        link_info: res,
                        content: data.description
                    }
                });
            }).then(function (res) {
                postId = res.id

                return Kakao.API.request({
                    url: '/v1/user/me'
                });
            }).then(function (res) {
                if (typeof callback == "function") {
                    callback(res.id, res.nickname, postId);
                }
            }, function (err) {
                if (typeof failCallback == "function") {
                    failCallback(JSON.stringify(err));
                }
            });
        } else {
            Kakao.Auth.login({
                success: function (authObj) {
                    kakaostoryShareLink(data, callback, failCallback);
                },
                fail: function (err) {
                    if (typeof failCallback == "function") {
                        failCallback(JSON.stringify(err));
                    }
                }
            });
        }
    });
}

/* Kakaostory muilti part image Share */
function KakaostoryShareUseMulti(data, callback, failCallback) {
    var scrapURL = null;
    Kakao.Auth.login({
        success: function (authObj) {
            Kakao.API.request({
                url: '/v1/api/story/linkinfo',
                data: {
                    url: data.link
                }
            }).then(function (res) {
                return Kakao.API.request({
                    url: '/v1/api/story/profile'
                });
            }).then(function (res) {
                var byteString = window.atob(data.canvas.split(',')[1]);    
                var mimeString = data.canvas.split(',')[0].split(':')[1].split(';')[0]
                var arrBuffer = new ArrayBuffer(byteString.length);
                var uint8Arr = new Uint8Array(arrBuffer);
                for (var i = 0; i < byteString.length; i++) {
                    uint8Arr[i] = byteString.charCodeAt(i);
                }    
                var blob = new Blob([arrBuffer], { "type": mimeString });
                var formData = new FormData();
                formData.append("file", blob);
                
                return Kakao.API.request({
                    url: '/v1/api/story/upload/multi',
                    files: [blob]
                });
            }).then(function (res) {
                
                return Kakao.API.request({
                    url: '/v1/api/story/post/photo',
                    data: {
                        image_url_list: res,
                        content: data.description
                    }
                })
            }).then(function (res) {
                scrapURL = 'https://story.kakao.com/' + res.id.replace('.', '/');
                return Kakao.API.request({
                    url: '/v1/user/me'
                });
            }).then(function (res) {
                if (typeof callback == "function") {
                    callback(res.id, res.properties.nickname, scrapURL);
                }
            }, function (err) {
                if (typeof failCallback == "function") {
                    failCallback(JSON.stringify(err));
                }
            });
        },
        fail: function (res) {
            alert(res.error);
            alert(res.error_description);
        }
    });
}