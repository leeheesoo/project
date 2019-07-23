$(function () {
    var hash = location.hash;
    if (hash == '#ninjabarbieevent') {
        window.scrollTo(0, $('#ninjabarbieevent').offset().top);
    }
})

var eventModule = angular.module('event', [])
.run(function ($rootScope, $http) {
    $rootScope.user = {
        surprizeType: null,
        name: null,
        mobile: null,
        zipcode: null,
        address: null,
        addressDetail: null,
        age: null
    };
    $rootScope.surprize = {};
    $rootScope.sharing = {};

    //처음화면으로
    $rootScope.reply = function () {
        location.href = '/event/2016-ninjabarbie?_= ' + new Date().getTime() + ' #ninjabarbieevent';
    }

    //서프라이즈 선택
    $rootScope.selectType = function (type) {
        $rootScope.user.surprizeType = type;
        if (type == 'BARBIE') {
            selectChar('bb');
        } else if (type == 'NINJA') {
            selectChar('nj');
        }
    }

    //개인정보 저장
    $rootScope.save = function () {
        if ($('form[name=saveEvent1Step1]').validate().errorList.length <= 0) {
            $('.page_loading').show();
            $http({
                method: 'POST',
                url: '/api/2016-ninjabarbie/save-user',
                data: $rootScope.user
            }).then(function successCallback(response) {
                $rootScope.surprize = response.data;
                playGame();
                $rootScope.user = {};
                $('.page_loading').hide();
            }, function errorCallback(response) {
                alert(response.responseJSON.message);
                $('.page_loading').hide();
            });
        }
    }

    //합성이미지 저장
    $rootScope.canvasSave = function (canvas) {
        var imagePath = null;
        if ($rootScope.surprize.surprizeType == 1) {
            imagePath = '/Images/NinjaBarbie2016/canvas/barbie/';            
        } else if ($rootScope.surprize.surprizeType == 0) {
            imagePath = '/Images/NinjaBarbie2016/canvas/ninja/';
        } else {
            alert('새로고침 후 다시 이용해주세요.');
            return;
        }

        //sharing image canvas setting
        var fb_canvas = document.getElementById('sharing_canvas_facebook');
        var fb_ctx = fb_canvas.getContext('2d');
        var kt_canvas = document.getElementById('sharing_canvas_kakaotalk');
        var kt_ctx = kt_canvas.getContext('2d');
        var ks_canvas = document.getElementById('sharing_canvas_kakaostory');
        var ks_ctx = ks_canvas.getContext('2d');

        var src = canvas.toDataURL('image/png');
        $('#canvas_result').attr('src', src);
        var canvasImage = document.getElementById('canvas_result');

        $('.page_loading').show();
        //페이스북
        var fb_background = new Image();
        fb_background.onload = function () {
            fb_ctx.drawImage(fb_background, 0, 0);
            fb_ctx.drawImage(canvasImage, 350, 50);
            $rootScope.surprize.facebookImage = fb_canvas.toDataURL('image/png').replace('data:image/png;base64,', '');
            
            //카카오톡
            var kt_background = new Image();
            kt_background.onload = function () {
                kt_ctx.drawImage(kt_background, 0, 0);
                kt_ctx.drawImage(canvasImage, 50, -25);
                $rootScope.surprize.kakaotalkImage = kt_canvas.toDataURL('image/png').replace('data:image/png;base64,', '');
                
                //카카오스토리
                var ks_background = new Image();
                ks_background.onload = function () {
                    ks_ctx.drawImage(ks_background, 0, 0);
                    ks_ctx.drawImage(canvasImage, 370, 40);
                    $rootScope.surprize.kakaostoryImage = ks_canvas.toDataURL('image/png').replace('data:image/png;base64,', '');
                    
                    //합성이미지 생성
                    $http({
                        method: 'POST',
                        url: '/api/2016-ninjabarbie/save-surprize',
                        data: $rootScope.surprize
                    }).then(function successCallback(response) {
                        $rootScope.sharing = response.data;
                        $rootScope.sharing.surprizeType = $rootScope.surprize.surprizeType;
                        $rootScope.surprize = {};

                        //결과화면 노출
                        goResult();

                        $('.page_loading').hide();
                    }, function errorCallback(response) {
                        alert(response.responseJSON.message);
                        $('.page_loading').hide();
                    });
                };
                ks_background.src = imagePath + 'kakaostory.jpg';
            };
            kt_background.src = imagePath + 'kakaotalk.jpg';
        };
        fb_background.src = imagePath + 'facebook.jpg';
    }

    //sharing sns
    $rootScope.shareOnSnsComplete = function () {
        $http({
            method: 'POST',
            url: '/api/2016-ninjabarbie/save-sharing',
            data: $rootScope.sharing
        }).then(function successCallback(response) {
            if (response.data.snsType == 1 || response.data.snsType == 3) {
                alert('공유가 완료되었습니다\r\n다른 SNS로도 공유하시면 당첨확률이 높아집니다!^^');
            }
        }, function errorCallback(response) {
            alert(response.responseJSON.message);
        });
    }
    $rootScope.shareOnSns = function (snsType) {
        var postUrl = 'http://' + location.host + '/event/2016-ninjabarbie/' + $rootScope.sharing.userId + '?utm_source=' + snsType + '&utm_medium=sharing&utm_campaign=ninjabarbie2016';

        $rootScope.sharing.snsType = snsType;
        switch (snsType) {
            case 'Facebook': {
                var data = {};
                data.link = postUrl;
                data.picture = $rootScope.sharing.facebookImage;
                data.name = 'NEW 서프라이즈 출시 기념 이벤트';
                data.description = '킨더조이 1,000개 증정!';
                data.top = $('#ninjabarbieevent').offset().top;
                facebookShare(data, function (userId, userName, is_silhouette, pictureDataUrl, scrapURL) {
                    $rootScope.sharing.snsId = userId;
                    $rootScope.sharing.snsName = userName;
                    $rootScope.sharing.post = scrapURL;
                    $rootScope.shareOnSnsComplete();
                }, function (error) {
                    alert('공유하셔야 참여가 완료됩니다');
                });
                break;
            }
            case 'Kakaotalk': {
                if (isMobile.any()) {
                    alert('공유 후 다시 브라우저로 돌아오셔야 공유가 완료됩니다!^^');
                    var data = {};
                    data.description = '[킨더조이 NEW 서프라이즈 출시 기념 이벤트]\r\n\r\n새로 나온 16가지의 바비 & 닌자 서프라이즈를 만나보세요!\r\n\r\n아이와 함께 나만의 바비 & 닌자를 만들기 이벤트에 참여하면, 추첨을 통해 1,040분께 킨더조이 선물을 드립니다.';
                    data.btnText = '킨더조이 받으러 가기';
                    data.link = postUrl;
                    data.picture = $rootScope.sharing.kakaotalkImage;
                    kakaotalkShare(data, function () {
                        $rootScope.shareOnSnsComplete();
                    }, function (error) {
                        alert("카카오톡 설치 가능한 모바일기기에서만 사용 가능합니다.");
                    });
                } else {
                    alert('카카오톡 설치 가능한 모바일기기에서만 사용 가능합니다.\r\n다른 SNS로 참여해주세요^^')
                }
                break;
            }
            case 'Kakaostory': {
                var data = {};
                data.link = postUrl;
                data.description = '[킨더조이 NEW 서프라이즈 출시 기념 이벤트]\r\n\r\n새로 나온 16가지의 바비 & 닌자 서프라이즈를 만나보세요!\r\n\r\n아이와 함께 나만의 바비 & 닌자를 만들기 이벤트에 참여하면, 추첨을 통해 1,040분께 #킨더조이 선물을 드립니다.';
                kakaostoryShareLink(data, function (userId, nickName, postId) {
                    var scrapURL = 'https://story.kakao.com/' + postId.replace('.', '/');
                    $rootScope.sharing.snsId = userId;
                    $rootScope.sharing.snsName = nickName;
                    $rootScope.sharing.post = scrapURL;
                    $rootScope.shareOnSnsComplete();
                }, function (e) {
                    alert(e.responseJSON.message);
                    self.isLoading(false);
                });
                break;
            }
        }
    }

    //우편번호 찾기
    $rootScope.openPostCode = function () {
        openPop('pop_post');

        new daum.Postcode({
            oncomplete: function (data) {
                // 검색결과 항목을 클릭했을때 실행할 코드를 작성하는 부분.

                // 각 주소의 노출 규칙에 따라 주소를 조합한다.
                // 내려오는 변수가 값이 없는 경우엔 공백('')값을 가지므로, 이를 참고하여 분기 한다.
                var fullAddr = data.address; // 최종 주소 변수
                var extraAddr = ''; // 조합형 주소 변수

                // 기본 주소가 도로명 타입일때 조합한다.
                if (data.addressType === 'R') {
                    //법정동명이 있을 경우 추가한다.
                    if (data.bname !== '') {
                        extraAddr += data.bname;
                    }
                    // 건물명이 있을 경우 추가한다.
                    if (data.buildingName !== '') {
                        extraAddr += (extraAddr !== '' ? ', ' + data.buildingName : data.buildingName);
                    }
                    // 조합형주소의 유무에 따라 양쪽에 괄호를 추가하여 최종 주소를 만든다.
                    fullAddr += (extraAddr !== '' ? ' (' + extraAddr + ')' : '');
                }

                // 우편번호와 주소 정보를 해당 필드에 넣는다.
                $rootScope.user.zipcode = data.zonecode;
                $rootScope.user.address = fullAddr;
                $rootScope.$apply();

                // 커서를 상세주소 필드로 이동한다.
                $('#AddressDetail').focus();

                // iframe을 넣은 element를 안보이게 한다.
                closePop('pop_post');
            },
            /*
            // 우편번호 찾기 화면 크기가 조정되었을때 실행할 코드를 작성하는 부분. iframe을 넣은 element의 높이값을 조정한다.
            onresize: function (size) {
                element_wrap.style.height = size.height + 'px';
            },
            */
            width: '640px',
            height: '590px'
        }).embed(document.getElementById('searchPostCode'));
        return false;
    }
}).directive('krInput', ['$parse', function ($parse) {// 한글 입력시 바인딩 오류
    return {
        priority: 2,
        restrict: 'A',
        compile: function (element) {
            element.on('compositionstart', function (e) {
                e.stopImmediatePropagation();
            });
        },
    };
}]);
//바비
eventModule.controller('Barbie', function ($scope, $rootScope) {
    var canvas = document.getElementById('barbie_canvas');
    var ctx = canvas.getContext('2d');

    //캔버스 안에서 악세사리 이동
    var acc_canvasX = 150;
    var acc_canvasY = 110;
    $('#barbie_acc').draggable({
        containment: '#barbie_canvas',
        stop: function (e) {
            acc_canvasX = parseInt($(this).offset().left - $("#barbie_canvas").offset().left);
            acc_canvasY = parseInt($(this).offset().top - $("#barbie_canvas").offset().top);
        }
    });
    
    //이미지 선택시 캔버스에 노출
    $scope.setDraw = function (num) {
        $scope.surprize.top = num;
        $scope.surprize.bottom = num;
        $scope.surprize.accessory = num;
        $scope.cavasDraw();

        $('.stage_cover').fadeOut(); // 세트를 먼저 선택해야 슬라이더 선택 가능
    }
    $scope.setTop = function (top) {
        if (top) {
            $scope.surprize.top = top;
            $scope.cavasDraw();
        } else {
            //top
            var img_top = new Image();
            img_top.onload = function () {
                ctx.drawImage(img_top, 0, 0);
                $scope.setBottom();
            };
            img_top.src = '/Images/NinjaBarbie2016/canvas/barbie/top_' + $scope.surprize.top + '.png';
        }
    };
    $scope.setBottom = function (bottom) {
        if (bottom) {
            $scope.surprize.bottom = bottom;
            $scope.cavasDraw();
        } else {
            //bottom
            var img_bottom = new Image();
            img_bottom.onload = function () {
                ctx.drawImage(img_bottom, 0, 0);
                $scope.setAcc();
            }
            img_bottom.src = '/Images/NinjaBarbie2016/canvas/barbie/bottom_' + $scope.surprize.bottom + '.png';
        }
    };
    $scope.setAcc = function (acc) {
        if (acc) {
            $scope.surprize.accessory = acc;
            $scope.cavasDraw();
        } else {
            //acc
            $('#barbie_acc').attr('src', '/Images/NinjaBarbie2016/canvas/barbie/acc_' + $scope.surprize.accessory + '.png');
        }
    };
    $scope.cavasDraw = function () {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        $scope.setTop();
    }    
    //나만의 바비 완성
    $scope.goResult = function () {
        if ($scope.surprize.top == 0 || $scope.surprize.bottom == 0 || $scope.surprize.accessory == 0) {
            alert('좌측 하단에서 원하는 바비를 선택해주세요!');
        } else {
            ctx.drawImage(document.getElementById('barbie_acc'), acc_canvasX, acc_canvasY);
            $('#barbie_acc').hide();
            $rootScope.canvasSave(canvas);
        }
    }
    //되돌리기
    $scope.canvasRevert = function () {
        $('.stage_cover').show();
        $('input:radio[name=bb_set]').prop('checked', false);
        $rootScope.surprize.top = null;
        $rootScope.surprize.bottom = null;
        $rootScope.surprize.accessory = null;
        $('#barbie_acc').attr('src', '/Images/NinjaBarbie2016/w/blank.gif');
        ctx.clearRect(0, 0, canvas.width, canvas.height);
    }
});
//닌자
eventModule.controller('Ninja', function ($scope, $rootScope) {
    var canvas = document.getElementById('ninja_canvas');
    var ctx = canvas.getContext('2d');

    //캔버스 안에서 악세사리 이동
    var acc_canvasX = 150;
    var acc_canvasY = 110;
    $('#ninja_acc').draggable({
        containment: '#ninja_canvas',
        stop: function (e) {
            acc_canvasX = parseInt($(this).offset().left - $("#ninja_canvas").offset().left);
            acc_canvasY = parseInt($(this).offset().top - $("#ninja_canvas").offset().top);
        }
    });

    //이미지 선택시 캔버스에 노출
    $scope.setDraw = function (num) {
        $scope.surprize.top = num;
        $scope.surprize.bottom = num;
        $scope.surprize.accessory = num;
        $scope.cavasDraw();

        $('.stage_cover').fadeOut(); // 세트를 먼저 선택해야 슬라이더 선택 가능
    }
    $scope.setTop = function (top) {
        if (top) {
            if ($scope.surprize.top == 8) {
                alert('선택하신 닌자 서프라이즈의 경우 무기만 선택 가능합니다!');
                return;
            }
            $scope.surprize.top = top;
            $scope.cavasDraw();
        } else {
            //top
            var img_top = new Image();
            img_top.onload = function () {
                ctx.drawImage(img_top, 0, 0);
                if ($scope.surprize.top == 8) {
                    $scope.setAcc();
                } else {
                    $scope.setBottom();
                }
            };
            img_top.src = '/Images/NinjaBarbie2016/canvas/ninja/top_' + $scope.surprize.top + '.png';
        }
    };
    $scope.setBottom = function (bottom) {
        if (bottom) {
            if ($scope.surprize.top == 8) {
                alert('선택하신 닌자 서프라이즈의 경우 무기만 선택 가능합니다!');
                return;
            }
            $scope.surprize.bottom = bottom;
            $scope.cavasDraw();
        } else {
            //bottom
            var img_bottom = new Image();
            img_bottom.onload = function () {
                ctx.drawImage(img_bottom, 0, 0);
                $scope.setAcc();
            }
            img_bottom.src = '/Images/NinjaBarbie2016/canvas/ninja/bottom_' + $scope.surprize.bottom + '.png';
        }
    };
    $scope.setAcc = function (acc) {
        if (acc) {
            $scope.surprize.accessory = acc;
            $scope.cavasDraw();
        } else {
            //acc
            $('#ninja_acc').attr('src', '/Images/NinjaBarbie2016/canvas/ninja/acc_' + $scope.surprize.accessory + '.png');
        }
    };
    $scope.cavasDraw = function () {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        $scope.setTop();
    }
    //나만의 닌자 완성
    $scope.goResult = function () {
        if ($scope.surprize.top == 0 || $scope.surprize.bottom == 0 || $scope.surprize.accessory == 0) {
            alert('좌측 하단에서 원하는 닌자를 선택해주세요!');
        } else {
            ctx.drawImage(document.getElementById('ninja_acc'), acc_canvasX, acc_canvasY);
            $('#ninja_acc').hide();
            $rootScope.canvasSave(canvas);
        }
    }
    //되돌리기
    $scope.canvasRevert = function () {
        $('.stage_cover').show();
        $('input:radio[name=nj_set]').prop('checked', false);
        $rootScope.surprize.top = null;
        $rootScope.surprize.bottom = null;
        $rootScope.surprize.accessory = null;
        $('#ninja_acc').attr('src', '/Images/NinjaBarbie2016/w/blank.gif');
        ctx.clearRect(0, 0, canvas.width, canvas.height);
    }
});