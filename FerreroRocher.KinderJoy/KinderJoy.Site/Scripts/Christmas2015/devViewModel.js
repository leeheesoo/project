var isInapp = (navigator.userAgent.toUpperCase().match(/KAKAOTALK|KAKAOSTORY|KAKAO|TWITTER|FB|NAVER|CriOS|GSA/i) ? true : false);
function makeChristmasViewModel(isEvent1) {
    var self = this;

    self.isLoading = ko.observable(false);
    /////////////////////////////////////////////////////////////////////
    // EVENT1 : 즉석당첨
    /////////////////////////////////////////////////////////////////////
    self.prizeType = ko.observable('Loser');
    self.realPrizeType = ko.observable('');
    self.event1Id = ko.observable(0);

    /* 즉석당첨 참여 기록 저장 */
    self.saveNaverSearching = function () {
        $.ajax({
            url: '/event/2015-christmas/save-naver-searching',
            type: 'post',
            success: function (data) {
                self.event1Id(data.Id);
                switch (data.PrizeType) {
                    //망토
                    case 1:
                        self.prizeType('Cloak');
                        self.realPrizeType(1);
                        openPop('.pop-prize');
                        break;
                        //킨더조이 1개입(남아용/여아용 랜덤)
                    case 2:
                        self.prizeType('Kinder');
                        self.realPrizeType(2);
                        openPop('.pop-prize');
                        $('.show-addr').removeClass('on');
                        break;
                        //꽝
                    case 0:
                    default:
                        openPop('.pop-bang');
                        break;
                }
            },
            error: function (req) {
                alert(req.responseJSON.message);
            }
        });
    }

    //올바른 경로로 즉당 이벤트 참여했을 경우
    if (isEvent1 == 'True') {
        self.saveNaverSearching();
    }
    self.commonBegin = function () {
        self.isLoading(true);
    }
    self.commonComplete = function () {
        self.isLoading(false);
    }
    self.failureEntryByEvent1 = function (err) {
        alert(err.responseJSON.message);
        return;
    }

    self.successEntryByEvent1 = function () {
        alert('완료되었습니다.');

        closePop('.pop-prize');
        closePop('.pop-bang');
    }

    /////////////////////////////////////////////////////////////////////
    // EVENT2 : 트리만들기
    /////////////////////////////////////////////////////////////////////
    self.treeId = ko.observable(-1);
    self.selectImgs = ko.observable('');
    self.successEntryByEvent2 = function (treeId) {
        self.treeId(treeId);

        alert('트리를 꾸며주세요 :)');
        closePop('.pop-userInfo');
    }

    self.startOpenChristmasTreeCard = function () {
        openChristmasTreeCard();
    }
    self.startMakeTree = function () {
        drawTree(self.treeId(), self.selectImgs());
    }
    $('.toys-list img').click(function (e) {
        var currentImg = e.currentTarget.attributes["data-type"].value;
        if (self.selectImgs().split(',').length < 8) {
            self.selectImgs(self.selectImgs() + currentImg + ',');
        }
    });

    /////////////////////////////////////////////////////////////////////
    // EVENT2 : 트리만들기 (SNS공유)
    /////////////////////////////////////////////////////////////////////
    self.snsResult = ko.observableArray();

    /* sns 1일 공유 가능 횟수 check*/
    self.checkSNSShare = function (snsType, callback, failCallback) {
        $.ajax({
            type: 'post',
            cache: false,
            async: false,
            dataType: 'json',
            data: {
                SNSType: snsType,
                Mobile: $('#hidMakeTreeMobile').val()
            },
            url: '/api/2015-christmas/sns-share-count',
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

    /* 페이스북 공유 */
    self.shareFacebook = function () {
        self.isLoading(true);
        /*   
        self.checkSNSShare('facebook', function () {
            var url = 'http://www.kinderjoy.co.kr/event/2015-christmas?utm_source=facebook&utm_medium=share&utm_campaign=2015-christmas&mtk=' + $('#hidMakeTreeId').val();
            var shareUrl = 'https://www.facebook.com/sharer/sharer.php?u=' + encodeURIComponent(url);
            var pop = window.open(shareUrl, 'shareFacebook', 'width=575, height=315');
            pop.focus();
            self.snsResult.push({ snsType: 'facebook', userId: null, userName: null, nickName: null, scrapURL: null });
            self.saveSnsShare();
        }, function (e) {
            alert(e.responseJSON.message);
            self.isLoading(false);
        });
        */     
        var data = {};
        data.picture = $('#hidMakeTreeImage').val();
        data.link = 'https://www.kinderjoy.co.kr/event/2015-christmas?utm_source=facebook&utm_medium=share&utm_campaign=2015-christmas';
        data.description = '[킨더조이] 크리스마스 트리 만들기 이벤트 Merry Christmas! 맛있는 즐거움, 상상하는 즐거움, 함께하는 즐거움 크리스마스는 킨더조이의 세 가지 즐거움과 함께!';

        self.checkSNSShare('facebook', function () {
            facebookShare(data, function (userId, userName, isDefault, userImage, scrapURL) {
                if (isMobile.any()) {
                    $('meta[name=viewport]').attr('content', 'width=640, user-scalable=yes');
                }
                window.scrollTo(0, 1000);
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
                window.scrollTo(0, 1000);
            });
        }, function (e) {
            alert(e.responseJSON.message);
            self.isLoading(false);
        });
    }
    /* 카카오스토리 공유 */
    self.shareKakaoStory = function () {
        self.isLoading(true);
        var data = {};
        data.link = 'https://www.kinderjoy.co.kr/event/2015-christmas?utm_source=kakaostory&utm_medium=share&utm_campaign=2015-christmas';
        var resultCanvas = document.getElementById('resultCanvas');
        data.canvas = resultCanvas.toDataURL('image/png');
        data.description = '[킨더조이] 크리스마스 트리 만들기 이벤트\r\n\r\nMerry Christmas!\r\n\r\n맛있는 즐거움, 상상하는 즐거움, 함께하는 즐거움\r\n크리스마스는 킨더조이의 세 가지 즐거움과 함께~~!!\r\n\r\n이벤트 참여하고 킨더조이 받으러가기 > https://goo.gl/KWBhwP';
        self.checkSNSShare('kakaostory', function () {
            KakaostoryShareUseMulti(data, function (userId, nickName, scrapURL) {
                self.snsResult.removeAll();
                self.snsResult.push({ snsType: 'kakaostory', snsId: userId, snsName: nickName, nickName: nickName, scrapURL: scrapURL });
                self.saveSnsShare();
                self.isLoading(false);
            }, function (e) {
                alert(e.responseJSON.message);
                self.isLoading(false);
            });
        }, function (e) {
            alert(e.responseJSON.message);
            self.isLoading(false);
        });
    }
    /* 카카오톡 공유 */
    self.shareKakaoTalk = function () {
        self.isLoading(true);
        var data = {};
        data.btnText = '지금 참여하러가기';
        data.picture = $('#hidMakeTreeImage').val();
        data.link = 'https://www.kinderjoy.co.kr/event/2015-christmas?utm_source=kakaotalk&utm_medium=share&utm_campaign=2015-christmas';
        data.description = '[킨더조이] 크리스마스 트리 만들기 이벤트\r\n\r\nMerry Christmas!\r\n\r\n맛있는 즐거움,\r\n상상하는 즐거움,\r\n함께하는 즐거움\r\n\r\n크리스마스는 킨더조이의\r\n세 가지 즐거움과 함께~~!!';
        self.checkSNSShare('kakaotalk', function () {
            kakaotalkShare(data, function () {
                self.snsResult.removeAll();
                self.snsResult.push({ snsType: 'kakaotalk', userId: null, userName: null, nickName: null, scrapURL: null });
                self.saveSnsShare();
                self.isLoading(false);
            }, function () {
                alert('모바일에서 참여가 가능합니다 ^^');
                self.isLoading(false);
            });
        }, function (e) {
            alert(e.responseJSON.message);
            self.isLoading(false);
        });
    }
    /* sns 공유 저장 */
    self.saveSnsShare = function () {
        var result = self.snsResult()[0];
        $.ajax({
            type: 'post',
            cache: false,
            async: false,
            dataType: 'json',
            url: '/api/2015-christmas/save-sns',
            data: {
                SNSType: result.snsType,
                SNSId: result.snsId,
                SNSName: result.snsName,
                SNSNickName: result.nickName,
                ScrapURL: result.scrapURL,
                Christmas2015MakeTreeId: $('#hidMakeTreeId').val()
            },
            success: function () {
               // if (result.snsType != 'facebook') {
                    alert('완료되었습니다!\n다른채널도 이용해주세요^^');
              //  }
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

    self.treeReset = function () {
        $('.tree-area').find('img').remove();
        $('.toys-list').find('img').css('visibility', 'visible');
        self.selectImgs('');
    }

    self.finishSnsShare = function () {
        /*
        //트리그리기 reset
        self.treeReset();
        $('.pop-tree').addClass('on');
        //sns공유팝업 닫기
        closePop('.pop-sns');
        //개인정보 reset
        $('.pop-userInfo__form').clearForm();
        $('.js-phone').placeholder({
            text: '-없이 입력해주세요'
        });
        //크리스마스 카드 reset
        $('#txtCard').val('');
        */
        location.href = location.pathname;
    }
}

/////////////////////////////////////////////////////////////////////
// 우편번호 검색 팝업
/////////////////////////////////////////////////////////////////////
function openPostCode(eventId) {
    $('.js-chDim').addClass('on');
    openDaumPostCode(eventId);
}

function openInapp(openUrl) {

    alert('이벤트가 종료되었습니다. 감사합니다:)');
    return;
    /*
    if (isInapp) {
        openPop('.popup-inapp');
    } else {
        var openNewWindow = window.open(openUrl);
    }
    */
}
function startMakeTreePersonalInfo() {
    alert('이벤트가 종료되었습니다. 감사합니다:)');
    return;
    /*
    if (isInapp) {
        openPop('.popup-inapp');
    } else {
        closePop('.pop-tree');
        openPop('.pop-userInfo');
    }
    */
}