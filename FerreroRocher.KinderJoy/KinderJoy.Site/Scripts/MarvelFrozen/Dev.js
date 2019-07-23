var isInapp = (navigator.userAgent.toUpperCase().match(/KAKAOTALK|KAKAOSTORY|KAKAO|TWITTER|FB|NAVER|CriOS|GSA/i) ? true : false);
var eventModule = angular.module('app-marvel-frozen', []);

eventModule.controller('ctrl-marvel-frozen', function ($scope, $http) {
    var self = $scope;
    self.isLoading = false;

    self.marvelFrozenUser = {
        name: null,
        age: null,
        mobile: null,
        zipCode: null,
        address: null,
        addressDetail: null,
        childGender: null,
        agree: null
    }

    self.marvelFrozenSNSShare = {
        userId: null,
        snsType: null,
        snsId: null,
        snsName: null,
        post: null
    }

    self.openMarvelFrozen = function () {
        if (isOpen == "False") {
            alert('4월 24일 월요일 오전 9시부터 참여가능합니다 :)');
            return;
        }
        if (isClose == "True") {
            alert('5월 19일 이벤트가 종료되었습니다.');
            return;
        }

        if (isInapp) {
            openPopup('pop-inapp');
        } else {
            game.setStep(2);
        }
    }

    //개인정보 저장
    self.saveMarvelFrozenUser = function () {
        if ($('#frmMarvelFrozen').validate().errorList.length <= 0) {
            if (self.marvelFrozenUser.childGender == null) {
                alert('자녀의 성별을 선택해주세요.');
                $('#boy').focus();
                return;
            }
            self.isLoading = true;
            $http({
                method: 'POST',
                url: '/api/2017-marvel-frozen/create-user',
                data: self.marvelFrozenUser
            }).then(function successCallback(response) {
                alert(stReady);
                game.setStep(3);
                $('#frmMarvelFrozen').clearForm();
                self.marvelFrozenSNSShare.userId = response.data.id;
                self.isLoading = false;
            }, function errorCallback(response) {
                alert(response.data.message);
                self.isLoading = false;
            });
        }
    }
    

    //우편번호 찾기
    self.openPostCode = function () {
        openPopup('pop-post');

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
                self.marvelFrozenUser.zipCode = data.zonecode;
                self.marvelFrozenUser.address = fullAddr;
                self.$apply();

                // 커서를 상세주소 필드로 이동한다.
                $('#AddressDetail').focus();

                // iframe을 넣은 element를 안보이게 한다.
                closePopup('pop-post');
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

    // SNS 공유
    self.snsShare = function (snsType) {

        //self.isLoading = true;
        self.marvelFrozenSNSShare.snsType = snsType;
        switch (snsType) {
            case 'Facebook': {
                var data = {};
                data.link = 'http://' + location.host + '/event/2017-Marvel_frozen?utm_source=facebook&utm_campaign=marvel-frozen&utm_medium=share';
                data.picture = 'http://' + location.host + '/Images/MarvelFrozen/sns/facebook.jpg';
                data.name = '이벤트 참여하고 킨더조이 받자!';
                data.description = '킨더조이 1,020개 증정!';
                data.top = 1800;
                facebookShare(data, function (userId, userName, is_silhouette, pictureDataUrl, scrapURL) {
                    self.marvelFrozenSNSShare.snsId = userId;
                    self.marvelFrozenSNSShare.snsName = userName;
                    self.marvelFrozenSNSShare.post = scrapURL;
                    self.saveSNSShare();
                }, function (error) {
                    self.isLoading = false;
                });
                break;
            }
            case 'Kakaostory': {
                var data = {};
                data.link = 'http://' + location.host + '/event/2017-Marvel_frozen?utm_source=kakaostory&utm_campaign=marvel-frozen&utm_medium=share';
                data.description = '[킨더조이 NEW 서프라이즈 런칭 이벤트]\r\n\r\n새로나온 16가지의 마블 히어로 & 디즈니 프린세스 & 겨울왕국 서프라이즈를 만나보세요!\r\n\r\n아이와 함께 NEW 서프라이즈 런칭 이벤트에 참여하면, 추첨을 통해 1,020분께 #킨더조이 선물을 드립니다.';
                kakaostoryShareLink(data, function (userId, nickName, postId) {
                    var scrapURL = 'https://story.kakao.com/' + postId.replace('.', '/');
                    self.marvelFrozenSNSShare.snsId = userId;
                    self.marvelFrozenSNSShare.snsName = nickName;
                    self.marvelFrozenSNSShare.post = scrapURL;
                    self.saveSNSShare();
                }, function (e) {
                    self.isLoading = false;
                    alert(e.responseJSON.message);
                });
                break;
            }
            case 'Kakaotalk': {
                var data = {};
                data.description = '[킨더조이 NEW 서프라이즈 런칭 이벤트]\r\n\r\n새로나온 16가지의 마블 히어로 & 디즈니 프린세스 & 겨울왕국 서프라이즈를 만나보세요!\r\n\r\n아이와 함께 NEW 서프라이즈 런칭 이벤트에 참여하면, 추첨을 통해 1,020분께 #킨더조이 선물을 드립니다.';
                data.btnText = '킨더조이 받으러 가기';
                data.link = 'http://' + location.host + '/event/2017-Marvel_frozen?utm_source=kakaotalk&utm_campaign=marvel-frozen&utm_medium=share';
                data.picture = 'http://' + location.host + '/Images/MarvelFrozen/sns/kakaotalk.jpg';
                kakaotalkShare(data, function () {
                    self.marvelFrozenSNSShare.snsId = null;
                    self.marvelFrozenSNSShare.snsName = null;
                    self.marvelFrozenSNSShare.post = null;
                    self.saveSNSShare();
                }, function (error) {
                    self.isLoading = false;
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
            url: '/api/2017-marvel-frozen/create-sns',
            data: self.marvelFrozenSNSShare
        }).then(function successCallback(response) {
            if (response.data.snsType == 1 || response.data.snsType == 3) {
                alert('공유가 완료되었습니다\r\n다른 SNS로도 공유하시면 당첨확률이 높아집니다!^^');
            }
            self.isLoading = false;
        }, function errorCallback(response) {
            alert(response.data.message);
            self.isLoading = false;
        });
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