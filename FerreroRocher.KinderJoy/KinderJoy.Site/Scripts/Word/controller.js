$(function () {
    $("#btnEntry").click(function (e) {
        e.preventDefault();
        if (isEventDay == false) {
            alert("이벤트가 종료 되었습니다.");
            return;
        }

        var $word1 = $("#Word1");
        if ($word1.val() == "") {
            alert("빈칸에 단어를 입력 하세요");
            $word1.focus();
            return;
        }
        var $word2 = $("#Word2");
        if ($word2.val() == "") {
            alert("빈칸에 단어를 입력 하세요");
            $word2.focus();
            return;
        }
        var $word3 = $("#Word3");
        if ($word3.val() == "") {
            alert("빈칸에 단어를 입력 하세요");
            $word3.focus();
            return;
        }
        var $word4 = $("#Word4");
        if ($word4.val() == "") {
            alert("빈칸에 단어를 입력 하세요");
            $word4.focus();
            return;
        }
        var word = $word1.val() + $word2.val() + $word3.val() + $word4.val();
        if (word != "킨더조이") {
            alert("틀렸습니다! 다시 한 번 정확히 입력해주세요 ^^!");
            return;
        }
        openPop('userInfo');
        $word1.val("");
        $word2.val("");
        $word3.val("");
        $word4.val("");
    });

    $("#btnMobileEntry").click(function (e) {
        e.preventDefault();
        if (isEventDay == false) {
            alert("이벤트가 종료 되었습니다.");
            return;
        }

        var $word = $("#word");
        if ($word.val() == "") {
            alert("빈칸에 단어를 입력 하세요");
            $word.focus();
            return;
        }
        if ($word.val() != "킨더조이") {
            alert("틀렸습니다! 다시 한 번 정확히 입력해주세요 ^^!");
            return;
        }
        openPop('userInfo');
        $word.val("");
    });
    
    $("form[name=frm]").data("validator").settings.submitHandler = function (form) {
        var $form = $(form);
        $form.ajaxSubmit({
            dataType: 'json',
            cache: false,
            async: false,
            data: {
                Channel: channel
            },
            beforeSubmit: function () {
                $(window).data('isLoading', true);
                $(".loading").show();
            },
            complete: function () {
                $(window).data('isLoading', false);
                $(".loading").hide();
            },
            success: function (data) {
                if (data.Result) {
                    $(".popup-userInfo").removeClass('on');
                    openPop('sns');
                } else {
                    alert(data.Message);
                }
                $form.clearForm();
            },
            error: function () {
                alert('일시적인 오류가 발생하였습니다. 페이지 새로고침 후 다시 시도해 주세요');
                //window.location.reload();
            }
        });
    }

    $("#btnFacebook").click(function (e) {
        e.preventDefault();
        InitSnsInfo();
        $(document).data("SnsType", "facebook");
        saveSns(null);
        var url = "http://www.kinderjoy.co.kr/Event/Word/Track/facebook/share";
        window.open('https://www.facebook.com/sharer/sharer.php?u=' + encodeURIComponent(url), "fbPop", "menubar=false, scrollbars=no,width=600px,height=450px");
        /*
        fbAction(function () {
            fbShare(function () {
                alert("페이스북 공유가 완료 되었습니다.");
                //$(".popup-sns").removeClass('on');
            });
        }, function () {
            alert("페이스북 인증 후 사용 가능합니다.");
        });
        */
    });

    $("#btnTwitter").click(function (e) {
        e.preventDefault();
        InitSnsInfo();
        TwitterLogin();
    });

    $("#btnKakaostory").click(function (e) {
        e.preventDefault();
        InitSnsInfo();

        var kakaoMe;
        Kakao.Auth.login({
            success: function (authObj) {
                Kakao.API.request({
                    url: '/v1/user/me'
                }).then(function (res) {
                    kakaoMe = res;
                    Kakao.API.request({
                        url: '/v1/api/story/post/photo',
                        data: {
                            image_url_list: ["/blQ7UD/hyf8GyaatT/HgtpAFygObXPMTt7x8fcwK/img.jpg?width=632&height=300"],
                            content: "[킨더조이] TVCF 퀴즈 이벤트\n#킨더조이 #세가지즐거움\n세가지 즐거움이 하나에! 새로 나온 킨더조이 TV CF보고 빈칸에 들어갈  단어를 입력해주세요! 2,000명에게 킨더조이를 선물로 드립니다!\n\n지금참여하기>> http://goo.gl/g7Oq8E"
                        }
                    }).then(function (res) {

                        $(document).data("SnsType", "kakaostory");
                        $(document).data("SnsId", kakaoMe.id);
                        $(document).data("PostId", res.id);
                        saveSns(function () {
                            alert("카카오스토리 공유 완료 되었습니다.");
                            //$(".popup-sns").removeClass('on');
                        });
                    }, function (err) {
                        alert(JSON.stringify(err));
                    });
                });
            }
        });
    });

    $("#btnKakaoTalk").click(function (e) {
        e.preventDefault();
        InitSnsInfo();
        if (channel != "Mobile") {
            alert("카카오톡은 모바일만 응모 가능합니다.");
            return;
        }
        $(document).data("SnsType", "kakaotalk");
        saveSns(null);
        Kakao.Link.sendTalkLink({
            label: "[킨더조이]  TVCF 퀴즈 이벤트\n\n▶ 세가지 즐거움이 하나에! 새로 나온 킨더조이!\n\n▶ 킨더조이 TV CF보고 빈칸에 들어갈  단어를 입력해주세요!\n\n▶ 2,000명에게 킨더조이 당첨의 기회가!",
            image: {
                src: 'https://slocal101.cdnworks.com/FerreroRocher/KinderJoy/Word/kakaotalkThum1.jpg',
                width: '300',
                height: '200'
            },
            webButton: {
                text: '이벤트 참여하기',
                url: 'http://www.kinderjoy.co.kr/event/Word/Track/kakaotalk/sharee'
            }
        });
    });

    $("#btnWinnerResult").click(function (e) {
        e.preventDefault();
        IsWinnerResultSt(winnerLastSt, function () {
            OpenWinnerResult(winnerLastSt);
        }, function () {
            alert('당첨자는 매주 금요일 확인 가능 합니다.');
        });
    });

    $(".btnDang").click(function (e) {
        e.preventDefault();
        var St = $(this).attr("href");
        IsWinnerResultSt(St, function () {
            OpenWinnerResult(St);
        }, function () {
            alert("당첨자가 아직 발표되지 않았습니다.");
        });
    });
});