function fbAction(callback, failCallback) {
    FB.getLoginStatus(function (response) {
        if (response.status === 'connected') {
            if (typeof callback == "function") {
                callback();
            }
        } else {
            FB.login(function (response) {
                if (response.authResponse) {
                    if (typeof callback == "function") {
                        callback();
                    }
                } else {
                    if (typeof failCallback == "function") {
                        failCallback();
                    }
                }
            }, { /*scope: 'read_stream'*/ });
        }
    });
}

function fbShare(fbcallback) {
    if (isIOS == false) {
        $("meta[name=viewport]").attr("content", "width=device-width; initial-scale=1");
    }
    FB.api('/me', function (responseMe) {
        var facebookMe = responseMe;
        FB.ui({
            method: 'feed',
            name: "[킨더조이] TVCF 퀴즈 이벤트",
            link: "http://www.kinderjoy.co.kr/Event/Word/Track/facebook/share",
            picture: "https://slocal101.cdnworks.com/FerreroRocher/KinderJoy/Word/facebookThum1.jpg",
            display: 'iframe',
            description: "빈칸의 정답을 입력하고 킨더조이 받자!"
        }, function (response) {
            if (isIOS == false) {
                var scale = $(window).width() / 640;
                $("meta[name=viewport]").attr("content", "width=640; initial-scale=" + scale);
            }
            if (response && response.post_id) {
                $(document).data("SnsType", "facebook");
                $(document).data("SnsId", facebookMe.id);
                $(document).data("PostId", response.post_id);
                saveSns(fbcallback);
            } else {
                //alert('Post was not published.');
            }
        });
    });
}

function saveSns(fbcallback) {
    $.ajax({
        url: contentUrl + 'Event/Word/Share',
        dataType: 'json',
        async: false,
        method: 'post',
        cache: false,
        data: {
            SnsType: $(document).data("SnsType"),
            SnsId: $(document).data("SnsId"),
            PostId: $(document).data("PostId"),
            Channel: channel
        },
        success: function (data) {
            if (data.Result) {
                if (typeof fbcallback == "function") {
                    fbcallback();
                }
            } else {
                alert(data.Message);
            }
        },
        error: function () {
        }
    });
}

function InitSnsInfo() {
    $(document).data("SnsType", "");
    $(document).data("SnsId", "");
    $(document).data("PostId", "");
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
            url: contentUrl + 'Event/Word/TwitterShare',
            dataType: 'json',
            async: false,
            method: 'post',
            cache: false,
            success: function (data) {
                if (data.Result) {
                    $(document).data("SnsType", "twitter");
                    $(document).data("SnsId", data.SnsId);
                    $(document).data("PostId", data.PostId);
                    saveSns(function () {
                        alert("트위터 공유가 완료 되었습니다.");
                        //$(".popup-sns").removeClass('on');
                    });
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

function IsWinnerResultSt(St,okCallback, failCallback) {
    $.ajax({
        url: contentUrl + 'Event/Word/IsWinnerResultSt',
        dataType: 'json',
        async: false,
        method: 'post',
        cache: false,
        data: {
            St: St
        },
        success: function (data) {
            if (data.Result) {
                if (typeof okCallback == "function") {
                    okCallback();
                }
            } else {
                if (typeof failCallback == "function") {
                    failCallback();
                }
            }
        },
        error: function () {
        }
    });
}

function OpenWinnerResult(St) {
    WinnerResult(St);
    $(".dang_selector1").removeClass("on");
    $(".dang_selector2").removeClass("on");
    $(".dang_selector3").removeClass("on");
    $(".dang_selector4").removeClass("on");
    $(".dang_selector" + St).addClass("on");

    $(".dang_selector").removeClass("on");
    $(".dang_selector").eq(St).addClass("on");

    $(".popup-winner").addClass("on");

    $('#dang_1, #dang_2, #dang_3, #dang_4').hide();
    $('#dang_' + St).show();

    if (St == 4) {
        $("#dang_txt_1").attr("src", "/Images/event/giftTitle_popup_winner1_1.png");
        $("#dang_txt_2").attr("src", "/Images/event/giftTitle_popup_winner2_1.png");
    } else {
        $("#dang_txt_1").attr("src", "/Images/event/giftTitle_popup_winner1.png");
        $("#dang_txt_2").attr("src", "/Images/event/giftTitle_popup_winner2.png");
    }

    goPosition(0);
}

function WinnerResult(St) {
    $.ajax({
        url: contentUrl + 'Event/Word/WinnerResult',
        dataType: 'html',
        method: 'post',
        cache: false,
        data: {
            St: St,
            GiftType: 'M'
        },
        success: function (data) {
            $("#WinnerListM").html(data);
        },
        error: function () {
        }
    });
    $.ajax({
        url: contentUrl + 'Event/Word/WinnerResult',
        dataType: 'html',
        method: 'post',
        cache: false,
        data: {
            St: St,
            GiftType: 'F'
        },
        success: function (data) {
            $("#WinnerListF").html(data);
        },
        error: function () {
        }
    });
}