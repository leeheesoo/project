var isInapp =(navigator.userAgent.toUpperCase().match(/KAKAOTALK|KAKAOSTORY|KAKAO|TWITTER|FB|NAVER|CriOS|GSA/i) ? true: false);
function makeChildrensViewModel() {
    self = this;

    // 장남감 성별
    self.isGender = ko.observable();
    // 로딩바 visible 처리
    self.isLoading = ko.observable(false);
    // 숨은그림찾기아이디
    self.HiddenPictureId = ko.observable(-1);
    // 공유데이터
    self.snsResult = ko.observableArray();

    self.gender = function (gender) {
        self.isGender(gender);
    }
    self.beginEntry = function () {
        self.isLoading(true);
    }

    self.completeEntry = function () {
        self.isLoading(false);
    }
    self.successEntry = function (data) {
        self.HiddenPictureId(data.id);

        closePop('pop_entry');
        $('#frmHiddenPicture').clearForm();
        openPopup('pop_sns');
    }

    self.failureEntry = function (err) {
        alert(err.responseJSON.message);
        return;
    }


    // 페이스북 공유
    self.shareFacebook = function () {
        var data = {};
        data.name = '[킨더조이] 어린이날 숨은그림찾기 이벤트';
        data.picture = 'https://www.kinderjoy.co.kr/Images/ChildrensDay/sns/facebook.jpg';
        data.link = 'https://www.kinderjoy.co.kr/event/2016-childrens-day?utm_source=facebook&utm_medium=share&utm_campaign=childrensday';
        data.description = '킨더조이 Cars & Minnie 한정판 장난감 출시! 숨은그림 찾고 총 1,010개의 킨더조이 받자!';
        facebookShare(data, function (userId, userName, isDefault, userImage, scrapURL) {
            if (isMobile.any()) {
                $('meta[name=viewport]').attr('content', 'width=640, user-scalable=yes');
            }
            self.snsResult.removeAll();
            self.snsResult.push({ snsType: 'facebook', snsId: userId, snsName: userName, nickName: null, scrapURL: scrapURL });
            self.saveSnsShare();

            self.isLoading(false);
        }, function () {
            alert("페이스북 인증 후 사용 가능합니다.");
            self.isLoading(false);
            if (isMobile.any()) {
                $('meta[name=viewport]').attr('content', 'width=640, user-scalable=yes');
            }
        });
    }

    // 카카오스토리 공유
    self.shareKakaoStory = function () {
        var data = {};
        data.link = 'https://www.kinderjoy.co.kr/event/2016-childrens-day?utm_source=kakaostory&utm_medium=share&utm_campaign=childrensday';
        data.description = '[킨더조이] 어린이날 숨은그림찾기 이벤트\r\n#킨더조이 #어린이날\r\n숨은그림 찾고 총 1,010개의 킨더조이를 받아가세요!\r\n지금 참여하기 >> https://goo.gl/oOIZmL';

        kakaostoryShareVideo(data, function (userId, nickName, scrapURL) {
            self.snsResult.removeAll();
            self.snsResult.push({ snsType: 'kakaostory', snsId: userId, snsName: nickName, nickName: nickName, scrapURL: scrapURL });
            self.saveSnsShare();
        }, function (e) {
            alert(e.responseJSON.message);
            self.isLoading(false);
        });
    }
   
    // 카카오톡 공유
    self.shareKakaoTalk = function () {
        self.isLoading(true);
        var data = {};
        data.btnText = '이벤트 참여하기';
        data.picture = 'https://www.kinderjoy.co.kr/Images/ChildrensDay/sns/kakaotalk.jpg';
        data.link = 'https://www.kinderjoy.co.kr/event/2016-childrens-day?utm_source=kakaotalk&utm_medium=share&utm_campaign=childrensday';
        data.description = '[킨더조이]어린이날 숨은그림찾기 이벤트 #킨더조이 #어린이날\r\n\r\n킨더조이 Cars&Minnie 한정판장난감 출시!\r\n\r\n숨은그림을 찾으시면 총 1,010명에게 킨더조이를 선물로 드립니다!';

        kakaotalkShare(data, function () {
            self.snsResult.removeAll();
            self.snsResult.push({ snsType: 'kakaotalk', userId: null, userName: null, nickName: null, scrapURL: null });
            self.saveSnsShare();
        }, function () {
            alert('모바일에서 참여가 가능합니다 ^^');
            self.isLoading(false);
        });        
    }

    // sns 공유 저장
    self.saveSnsShare = function () {
        var result = self.snsResult()[0];
        $.ajax({
            type: 'post',
            cache: false,
            async: false,
            dataType: 'json',
            url: '/api/childrens-day/create-sns',
            data: {
                ChildrensDayHiddenPictureId: self.HiddenPictureId(),
                SNSType: result.snsType,
                SNSId: result.snsId,
                SNSName: result.snsName,
                SNSNickName: result.nickName,
                ScrapURL: result.scrapURL
            },
            beforeSend: function () {
                self.isLoading(true);
            },
            complete:function(){
                self.isLoading(false);
            },
            success: function () {
                alert('완료되었습니다!\n다른채널도 이용해주세요.');
            },
            error: function (data, textStatus, jqXHR) {
                if (textStatus != 'abort') {
                    alert(data.responseJSON.message);
                }
            }
        });
    }
}

function openPostCode() {
    openPopup('pop_post');

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
            $('#ZipCode').val(data.zonecode); //5자리 새우편번호 사용
            $('#Address1').val(fullAddr);

            // 커서를 상세주소 필드로 이동한다.
            $('#Address2').focus();

            // iframe을 넣은 element를 안보이게 한다.
            closePop('pop_post');
        },
        /*
        // 우편번호 찾기 화면 크기가 조정되었을때 실행할 코드를 작성하는 부분. iframe을 넣은 element의 높이값을 조정한다.
        onresize: function (size) {
            element_wrap.style.height = size.height + 'px';
        },
        */
        width: '536px',
        height: '590px'
    }).embed(document.getElementById('searchPostCode'));
    return false;
}
