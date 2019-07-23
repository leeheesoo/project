$(function () {
    if (isMobile) {
        // inapp 팝업처리
        if (isInApp) {
            openPop('popInapp');
        }
    }
});

// 팝업별 공유 구별
var toyFlag;
function showToyDetailPop(popId) {
    toyFlag = popId;

    // 장난감 자세히 보기 팝업 호출
    // toy.pange.js
    openToyDetailArticle(popId);    
    /*
     * 2017.10.19 추가
     * 한승철
     * 팝업 오픈할때 화면크기를 팝업 크기로 조정
     */
    resizeVieHeight(popId);
}

// 페이스북 스크랩
function toyFacebookScrap() {
    var url = 'http://' + location.host + '/Image/Track/toyPage/facebook/' + toyFlag;
    window.open('https://www.facebook.com/sharer/sharer.php?u=' + encodeURIComponent(url), "fbPop", "menubar=false, scrollbars=no,width=600px,height=450px");
}

// 카카오스토리 스크랩
function toyKakaoStoryScrap() {
    var url = 'http://' + location.host + '/Image/Track/toyPage/kakaostory/' + toyFlag + "?_=" + new Date().getTime();
    var scrapImg = '';
    var scrapContent = '#킨더조이 #상상하는즐거움\r\n\r\n우리아이에게 필요한 서프라이즈는 무엇일까요?\r\n\r\n킨더조이는 우리 아이의 행복한 미래를 위한\r\n엄마와의 특별한 시간을 선사합니다!\r\n\r\n확인하러가기 > https://www.kinderjoy.co.kr/Image/Track/toyPage/kakaostory/' + toyFlag;

    Kakao.Auth.login({
        success: function (authObj) {
            Kakao.API.request({
                url: '/v1/api/story/linkinfo',
                data: {
                    url: url
                }
            }).then(function (res) {
                Kakao.API.request({
                    url: '/v1/api/story/post/link',
                    data: {
                        link_info: res,
                        content: scrapContent
                    }
                }).then(function (res) {                    
                    alert("카카오스토리 공유 완료 되었습니다.");
                    if (isMobile) {
                        location.reload();
                    }                    
                    
                }, function (err) {
                    alert(JSON.stringify(err));
                });
            });
        }
    });
}

// 카카오톡 스크랩
function toyKakaoTalkScrap() {
    var scrapUrl = 'http://'+location.host+'/Image/Track/toyPage/kakaostory/'+toyFlag;
    var scrapImg = 'http://' + location.host + '/Images/toy/thumbnail/kakaotalk/kakaotalk_' + toyFlag + '.jpg';
    var scrapTitle = '#킨더조이 #상상하는즐거움';
    var scrapContent = '우리아이에게 필요한 서프라이즈는 무엇일까요?\r\n\r\n킨더조이는 우리 아이의 행복한 미래를 위한\r\n엄마와의 특별한 시간을 선사합니다!';

    Kakao.Link.sendDefault({
        objectType: 'feed',
        content: {
            title: scrapTitle,
            description: scrapContent,
            imageUrl: scrapImg,
            imageWidth:300,
            imageHeight:200,
            link: {
                mobileWebUrl: scrapUrl,
                webUrl: scrapUrl
            },
            
        },
        buttonTitle: '지금 확인하러 가기'
    });
    setTimeout(function () {
        if (typeof callback == 'function') {
            callback();
        }
    }, 3000);
    //Kakao.Link.sendDefault{
    //    objectType:'feed',
    //    label: scrapContent,
    //    image: {
    //        src: scrapImg,
    //        width: '300',
    //        height: '200'
    //    },
    //    webButton: {
    //        text: '지금 확인하러 가기',
    //        url: 'https://www.kinderjoy.co.kr/Image/Track/toyPage/kakaotalk/' + toyFlag + "?_=" + new Date().getTime()
    //    }
    //});
}



function TwitterLogin() {
    var pop = window.open(contentUrl + 'Twitter/LoginCheck', 'twitterJoin', 'width=1024, height=600');
    if (pop == null) {
        alert('팝업이 차단되어 있어서 정상적인 진행이 어렵습니다. \n[도구 > 팝업차단] 에서 \'팝업 차단 사용 안 함\' 으로 설정해 주시기 바랍니다. ');
        return;
    }
}

function twitterAfterLogin(response) {
    if (response) {
        // 공유로직 들어가기
        var tmpReturn = false;
        $.ajax({
            url: contentUrl + 'Image/TwitterShare/' + $(document).data('toyFlag'),
            dataType: 'json',
            async: false,
            method: 'post',
            cache: false,
            success: function (data) {
                if (data.Result) {
                    alert("트위터 공유가 완료 되었습니다.");
                } else {
                    alert(data.Message);
                }
            },
            error: function () {
            }
        });
        return tmpReturn;
    } else {
        alert('트위터 로그인 하신 후 다시 참여해 주세요.(인증된 계정으로 로그인하셔야 합니다)');
    }
}
// 트위터 스크랩
function toyTwitterScrap() {
    var scrapURL = 'https://www.kinderjoy.co.kr/Image/Track/toyPage/twitter/' + toyFlag + "?_=" + new Date().getTime();
    var scrapContent = '#킨더조이 상상하는 즐거움 킨더조이는 아이들의 창의성과 동심을 자극하는 상상 이상의 즐거움을 선사합니다!\r\n지금, 아이와 함께 킨더조이 속 장난감을 확인해보세요!\r\n';
    if (toyFlag == 'new') {
        scrapContent = '#킨더조이 아이스에이지 출시! 지금 바로 확인해보세요!\r\n7월부터 한정기간 동안 판매 \r\n';
    }
    var href = "http://twitter.com/intent/tweet?text=" + encodeURIComponent(scrapContent) + "&url=" + encodeURIComponent(scrapURL);
    var pop = window.open(href, "twitter", "width=600px,height=500px");
    if (pop) {
        pop.focus();
    }
    //TwitterLogin();
}