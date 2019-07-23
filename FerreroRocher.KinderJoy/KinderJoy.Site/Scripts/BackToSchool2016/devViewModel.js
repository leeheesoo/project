var isInapp = (navigator.userAgent.toUpperCase().match(/KAKAOTALK|KAKAOSTORY|KAKAO|TWITTER|FB|NAVER|CriOS|GSA/i) ? true : false);
function makeBacktoSchoolViewModel(isClosed) {

    var self = this;

    //빙고퀴즈이벤트 아이디
    self.bingoQuizId = ko.observable(-1);    
    //로딩바 visible 처리
    self.isLoading = ko.observable(false);
    
    //sns공유 결과
    self.snsResult = ko.observableArray();

    //빙고 완성하기
    self.checkBingo = function () {
        if (isClosed == 'True') {
            alert('이벤트가 종료되었습니다.\n감사합니다.');
            return;
        }
        if (isInapp) {
            openInapp('pop_inapp');
        } else {
            var checkedVal = $("input[name='select']:checked").val();
            if (checkedVal == undefined) {
                alert('정답을 선택해주세요~');
            } else if (checkedVal != undefined && checkedVal == 1) {
                //개인정보입력팝업 open
                $('#frmBingoQuiz').clearForm();
                openPopup('pop_entry');
            } else {
                alert('오답입니다! 다시 도전해보세요!');
            }
        }
    }

    self.beginEntry = function () {
        self.isLoading(true);
    }

    self.completeEntry = function () {
        self.isLoading(false);
    }

    self.successEntry = function (data) {
        self.bingoQuizId(data.id);

        //개인정보팝업 닫기
        closePop('pop_entry');
        $('#frmBingoQuiz').clearForm();

        //sns공유팝업 오픈
        openPopup('pop_sns');
    }

    self.failureEntry = function (err) {
        alert(err.responseJSON.message);
        return;
    }

    //페이스북 공유
    self.shareFacebook = function () {
        //self.checkSNSShareCount('facebook', function () {
        if (confirm('페이스북 공유 참여해주신 후에,\n 다른 채널 이용해주세요^^')) {
            //빙고퀴즈아이디 암호화
            var bParam = jQuery.jCryption.encrypt(self.bingoQuizId().toString(), 'base64');
            bParam = bParam.substr(0, bParam.length - 1);
            //공유링크
            var url = 'https://www.kinderjoy.co.kr/event/2016-backtoschool?utm_source=facebook&utm_medium=share&utm_campaign=backtoschool';
            //리다이렉트uri
            var redirect_uri = 'https://www.kinderjoy.co.kr/event/2016-backtoschool?utm_source=facebook&utm_medium=share&utm_campaign=backtoschool&bid=' + bParam +'&';
            var shareUrl = 'https://www.facebook.com/dialog/share?app_id=' + fbAppId + '&display=popup&href=' + encodeURIComponent(url);
            var fbUserID = FB.getUserID();
            if (isMobile.any()) {
                //모바일 참여시에만 redirect uri 추가
                shareUrl = shareUrl + '&redirect_uri=' + encodeURIComponent(redirect_uri);
            }

            var pop = window.open(shareUrl, 'shareFacebook', 'width=575, height=315');
            pop.focus();

            self.snsResult.removeAll();
            self.snsResult.push({ snsType: 'facebook', snsId: fbUserID, userName: null, nickName: null, scrapURL: null });
            self.saveSnsShare();
        }
        /* 
        //페이스북 api가 변경되어 share처리로 변경함
        var data = {};
        data.name = '킨더조이 새학기 이벤트';
        data.picture = 'https://www.kinderjoy.co.kr/Images/items/flavor-content.png';//'http://dev.www.kinderjoy.co.kr/Images/BackToSchool2016/sns/facebook.jpg';
        data.link = 'https://www.kinderjoy.co.kr/';
        data.description = '[킨더조이]새학기 맞이 킨더조이 빙고 퀴즈 이벤트\r\n\r\n우리 아이의 새학기를 킨더조이 쿵푸 팬더와 함께 응원하세요!\r\n킨더조이!세가지 즐거움이 가득! https://www.kinderjoy.co.kr';
        facebookShare(data, function (userId, userName, isDefault, userImage, scrapURL) {
            if (isMobile.any()) {
                $('meta[name=viewport]').attr('content', 'width=640, user-scalable=yes');
            }
            window.scrollTo(0, 1000);
            self.snsResult.removeAll();
            self.snsResult.push({ snsType: 'facebook', snsId: userId, snsName: userName, nickName: null, scrapURL: scrapURL });
            //sns테이블에 저장
            self.saveSnsShare();

            self.isLoading(false);
        }, function () {
            alert("페이스북 인증 후 사용 가능합니다.");
            self.isLoading(false);
            if (isMobile.any()) {
                $('meta[name=viewport]').attr('content', 'width=640, user-scalable=yes');
            }
            window.scrollTo(0, 1000);
        });
        */
        //}, function (e) {
        //    alert(e.responseJSON.message);
        //    self.isLoading(false);
        //});        
    }

    /* 카카오스토리 공유 */
    self.shareKakaoStory = function () {
        var data = {};
        data.link = 'https://www.kinderjoy.co.kr/event/2016-backtoschool?utm_source=kakaostory&utm_medium=share&utm_campaign=backtoschool';
        data.picture = '/fwojk/hyiC2Y4OkO/ix5yjY0wffC1UowvFB7X6K/img.jpg?width=632&height=300';
        data.description = '[킨더조이]새학기 맞이 킨더조이 빙고 퀴즈 이벤트\r\n\r\n우리 아이의 새학기를 킨더조이 쿵푸 팬더와 함께 응원하세요!\r\n킨더조이!세가지 즐거움이 가득!\r\n\r\n이벤트 참여하고 킨더조이 받으러가기> https://goo.gl/46TprT';
        //self.checkSNSShareCount('kakaostory', function () {
        kakaostoryShareImage(data, function (userId, nickName, imaage, scrapURL) {
                self.isLoading(true);
                self.snsResult.removeAll();
                self.snsResult.push({ snsType: 'kakaostory', snsId: userId, snsName: nickName, nickName: nickName, scrapURL: scrapURL });
                self.saveSnsShare();
            }, function (e) {
                alert(e.responseJSON.message);
                self.isLoading(false);
            });
        //}, function (e) {
        //    alert(e.responseJSON.message);
        //    self.isLoading(false);
        //});
    }
    /* 카카오톡 공유 */
    self.shareKakaoTalk = function () {
        self.isLoading(true);
        var data = {};
        data.btnText = '지금 참여하러가기';
        data.picture = 'https://www.kinderjoy.co.kr/Images/BackToSchool2016/sns/kakaotalk.jpg';
        data.link = 'https://www.kinderjoy.co.kr/event/2016-backtoschool?utm_source=kakaotalk&utm_medium=share&utm_campaign=backtoschool';
        data.description = '[킨더조이]\r\n새학기 맞이 킨더조이\r\n빙고 퀴즈 이벤트\r\n\r\n우리 아이의 새학기를\r\n킨더조이 쿵푸 팬더와 함께 응원하세요!\r\n\r\n킨더조이!\r\n세가지 즐거움이 가득!\r\n\r\n';
        //self.checkSNSShareCount('kakaotalk', function () {
            kakaotalkShare(data, function () {
                self.snsResult.removeAll();
                self.snsResult.push({ snsType: 'kakaotalk', userId: null, userName: null, nickName: null, scrapURL: null });
                self.saveSnsShare();
            }, function () {
                alert('모바일에서 참여가 가능합니다 ^^');
                self.isLoading(false);
            });
        //}, function (e) {
        //    alert(e.responseJSON.message);
        //    self.isLoading(false);
        //});
    }
    /* sns 공유 저장 */
    self.saveSnsShare = function () {
        var result = self.snsResult()[0];
        $.ajax({
            type: 'post',
            cache: false,
            async: false,
            dataType: 'json',
            url: '/api/2016-backtoschool/create-sns',
            data: {
                SNSType: result.snsType,
                SNSId: result.snsId,
                SNSName: result.snsName,
                SNSNickName: result.nickName,
                ScrapURL: result.scrapURL,
                BackToSchool2016BingoQuizId: self.bingoQuizId()
            },
            success: function () {
                if (result.snsType != 'facebook') {
                    alert('완료되었습니다!\n다른채널도 이용해주세요^^');
                } 
                self.isLoading(false);
            },
            error: function (data, textStatus, jqXHR) {
                if (textStatus != 'abort') {
                    alert(data.responseJSON.message);
                }
                self.isLoading(false);
            }
        });
    }

    /* 
        sns 공유 횟수 체크
        snsType: facebook / kakaostory / kakaotalk / twitter / ...
    */
    self.checkSNSShareCount = function (snsType, callback, failCallback) {
        $.ajax({
            type: 'post',
            cache: false,
            async: false,
            dataType: 'json',
            data: {
                SNSType: snsType,
                BingoQuizId: self.bingoQuizId()
            },
            url: '/api/2016-backtoschool/sns-share-check',
            dataType: 'json',
            success: function (data) {
                if (typeof callback == "function") {
                    callback(data);
                }
            },
            error: function (req) {;
                if (typeof failCallback == "function") {
                    failCallback(req);
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

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2]);
}