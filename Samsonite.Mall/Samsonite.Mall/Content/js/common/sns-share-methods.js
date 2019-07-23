var fbMove;
var fbCount = 0;
function facebookShare(data, callback, failCallback) {
    FB.getLoginStatus(function (response) {
        if (response.status === 'connected') {

            if (typeof callback == 'function') {
                FB.api('/me', function (res) {
                    FB.ui({
                        method: 'feed',
                        link: data.link,
                        picture: data.picture,
                        name: data.name,
                        description: data.description,
                        caption: '쌤소나이트 백투더퓨처',
                        display: 'iframe'
                    }, function (resp) {
                        if (resp && resp.post_id) {
                            var postId = resp.post_id;
                            postId = postId.replace("_", "/posts/");
                            scrapURL = 'https://www.facebook.com/' + postId;
                            if (typeof callback == 'function') {
                                callback(response.authResponse.userID, res.name, resp.post_id, scrapURL);
                            }
                        } else {
                            if (resp.error_code == null) {
                                if (typeof callback == 'function') {
                                    callback(response.authResponse.userID, res.name, null, null);
                                }
                            } else {
                                if (typeof failCallback == 'function') {
                                    failCallback();
                                }
                            }
                        }
                    });
                    fbMove = setInterval(function () {
                        $('.fb_dialog').each(function (index) {
                            if (!checkMobile.any()) {
                                if ($(this).css('top') != '-10000px') {
                                    var top = data.dialogStyleTop || 100;
                                    $(this).css('top', top + 'px');
                                    window.scrollTo(0, top);
                                }
                            }
                            fbCount = fbCount + 1;
                            if (fbCount >= 100) {
                                fbCount = 0;
                                clearInterval(fbMove);
                            }
                        });
                    }, 1000);
                });
            }
        } else {
            FB.login(function (response) {
                if (response.authResponse) {
                    facebookShare(data, callback, failCallback);
                } else {
                    if (typeof failCallback == 'function') {
                        failCallback();
                    }
                }
            });
        }
    });
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
                    callback(res.id, res.properties.nickname, postId);
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
function kakaotalkShare(data, callback, failCallback) {
    if (!checkMobile) {
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
            if (typeof callback == 'function') {
                callback();
            }
        }, 3000);
    }
}