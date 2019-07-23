var isInapp = (navigator.userAgent.toUpperCase().match(/KAKAOTALK|KAKAOSTORY|KAKAO|TWITTER|FB|NAVER|CriOS|GSA/i) ? true : false);
var eventModule = angular.module('app-finding-dory', []);

eventModule.controller('ctrl-finding-dory', function ($scope, $http) {
    var self = $scope;

    self.findingDoryUser = {
        name: null,
        age: null,
        mobile: null,
        zipCode: null,
        address: null,
        addressDetail: null,
        agree: null
    }

    self.findingDorySNSShare = {
        userId: null,
        snsType: null,
        snsId: null,
        snsName: null,
        post: null
    }

    self.startFindingDory = function () {
        if (isClose == 'True') {
            alert('3월 10일 이벤트가 종료되었습니다.');
            return;
        }

        if (isInapp) {
            openPop('pop_inapp');
        } else {
            gameStageControl();
        }
    }

    //개인정보 저장
    self.saveFindingDoryUser = function () {
        if ($('#frmFindingDory2017').validate().errorList.length <= 0) {
            $('.page_loading').show();
            $http({
                method: 'POST',
                url: '/api/2017-findingdory/create-user',
                data: self.findingDoryUser
            }).then(function successCallback(response) {
                self.findingDorySNSShare.userId = response.data.id;
                gameStageControl();
                $('.page_loading').hide();
            }, function errorCallback(response) {
                alert(response.data.message);
                $('.page_loading').hide();
            });
        }
    }

    // SNS 공유
    self.snsShare = function (snsType) {
        //$('.page_loading').show();
        self.findingDorySNSShare.snsType = snsType;
        switch (snsType) {
            case 'Facebook': {
                var data = {};
                data.link = 'http://' + location.host + '/event/2017-findingdory?utm_source=facebook&utm_campaign=findingdory&utm_medium=share';
                data.picture = 'http://' + location.host + '/Images/FindingDory2017/sns/facebook.jpg';
                data.name = '킨더조이 도리를 찾아서 장난감 출시 기념 이벤트';
                data.description = '킨더조이 1,000개 증정!';
                data.top = 2300;
                facebookShare(data, function (userId, userName, is_silhouette, pictureDataUrl, scrapURL) {
                    self.findingDorySNSShare.snsId = userId;
                    self.findingDorySNSShare.snsName = userName;
                    self.findingDorySNSShare.post = scrapURL;
                    self.saveSNSShare();
                }, function (error) {
                    //$('.page_loading').hide();
                });
                break;
            }
            case 'Kakaostory': {
                var data = {};
                data.link = 'http://' + location.host + '/event/2017-findingdory?utm_source=kakaostory&utm_campaign=findingdory&utm_medium=share';
                data.description = '[킨더조이 한정판 도리를 찾아서 장난감 출시 기념 이벤트]\r\n\r\n새로 나온 6가지 도리를 찾아서 서프라이즈를 만나보세요!\r\n\r\n도리와 함께 떠나는 서프라이즈 모험 이벤트에 참여하시면 추첨을 통해 1,000분께 #킨더조이를 드립니다.\r\n\r\n구경하기 > https://goo.gl/mGVGlm';
                kakaostoryShareLink(data, function (userId, nickName, postId) {
                    var scrapURL = 'https://story.kakao.com/' + postId.replace('.', '/');
                    self.findingDorySNSShare.snsId = userId;
                    self.findingDorySNSShare.snsName = nickName;
                    self.findingDorySNSShare.post = scrapURL;
                    self.saveSNSShare();
                }, function (e) {
                    //$('.page_loading').hide();
                    alert(e.responseJSON.message);
                });
                break;
            }
            case 'Kakaotalk': {
                var data = {};
                data.description = '[킨더조이 한정판 도리를 찾아서 장난감 출시 기념 이벤트]\r\n\r\n새로 나온 6가지 도리를 찾아서 서프라이즈를 만나보세요!\r\n\r\n도리와 함께 떠나는 서프라이즈 모험 이벤트에 참여하시면 추첨을 통해 1,000분께 #킨더조이를 드립니다.';
                data.btnText = '킨더조이 받으러 가기';
                data.link = 'http://' + location.host + '/event/2017-findingdory?utm_source=kakaotalk&utm_campaign=findingdory&utm_medium=share';
                data.picture = 'http://' + location.host + '/Images/FindingDory2017/sns/kakaotalk.jpg';
                kakaotalkShare(data, function () {
                    self.findingDorySNSShare.snsId = null;
                    self.findingDorySNSShare.snsName = null;
                    self.findingDorySNSShare.post = null;
                    self.saveSNSShare();
                }, function (error) {
                    alert("카카오톡 설치 가능한 모바일기기에서만 사용 가능합니다.");
                });
                break;
            }
        }
    }

    // SNS정보 저장
    self.saveSNSShare = function () {
        $http({
            method: 'POST',
            url: '/api/2017-findingdory/create-sns',
            data: self.findingDorySNSShare
        }).then(function successCallback(response) {
            if (response.data.snsType == 1 || response.data.snsType == 3) {
                alert('공유가 완료되었습니다\r\n다른 SNS로도 공유하시면 당첨확률이 높아집니다!^^');
            }
        }, function errorCallback(response) {
            alert(response.data.message);
        });
    }

    //우편번호 찾기
    self.openPostCode = function () {
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
                self.findingDoryUser.zipCode = data.zonecode;
                self.findingDoryUser.address = fullAddr;
                self.$apply();

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
}).directive('krInput', ['$parse', function ($parse) { // 한글 입력시 바인딩 오류
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