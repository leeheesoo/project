var orange4050EventModel = new Vue({
    el: '#orange4050-event',
    data: {
        closeCheck: false
    },
    methods: {
        beginEntry: function () {
            $('.dimm__loading').show();
        },
        completeEntry: function () {
            $('.dimm__loading').hide();
        },
        saveEntry: function (res) {
            $('.dimm__loading').hide();
            if (res.Result === true) {
                alert(res.Message);
                closePop('personal');
                $('#frmorange4050EventModelEntry').clearForm();
                $('.dimm__block').attr('onclick', 'alert("휴대폰 인증을 진행해 주세요");');

            } else {
                //if (!res.IsRequiredAllCheck) {
                //    openPopup('Alert');
                //} else {
                    alert(res.Message);
                //}
            }
        },
        failureEntry: function (xhr, err) {
            $('.dimm__loading').hide();
            alert('다시 시도해주세요.');
        },
        isCloseCheck: function () {
            if (eventClose == 'True') {
                alert("이벤트가 종료되었습니다.");
                this.closeCheck = false;
                return false;
            }
            this.closeCheck = true;
        },
        openPlusFriendPop: function () {
            this.isCloseCheck();
            if (this.closeCheck) {
                $('.btn-marketing').addClass('on');
                openPop('confirm'); return false;
            }
        },
        openMarketingPop: function () {
            this.isCloseCheck();
            if (this.closeCheck) {
                if ($('.btn-marketing').hasClass('on')) {
                    openPop('personal');
                } else {
                    alert('플러스친구를 추가를 해주세요');
                }
            }
        },
        shareFacebook: function () {
            var url = 'https://' + window.location.host + window.location.pathname + '?utm_source=facebook&utm_medium=share&utm_campaign=emoticon';
            window.open('https://www.facebook.com/sharer/sharer.php?u=' + encodeURIComponent(url), "fbPop", "menubar=false, scrollbars=no,width=600px,height=450px");
        },
        shareKakao: function () {
            Kakao.Link.sendCustom({
                templateId: 12065
            });
        },
        shareBand: function () {
            var url = 'http://bit.ly/2wEjWiq';
            if (isMobile) {
                if (navigator.userAgent.match(/android/i)) {
                    window.location.href = 'intent:bandapp://create/post?text=' + encodeURIComponent(url) + '%0A' + encodeURIComponent('4050도 갖고 싶은 카카오이모티콘!\r\n선착순 2만명! 100%증정! 단 2분 소요! 이벤트에 지금 참여하세요.') + '%0A' + '&route=https://www.orange-event.kr' + '#Intent;package=com.nhn.android.band;end';
                } else if (navigator.userAgent.match(/(iphone)|(ipod)|(ipad)/i)) {
                    setTimeout(function () { location.href = 'itms-apps://itunes.apple.com/app/id542613198'; }, 200);
                    location.href = 'bandapp://' + 'create/post?text=' + encodeURIComponent(url) + '%0A' + encodeURIComponent('4050도 갖고 싶은 카카오이모티콘!\r\n선착순 2만명! 100%증정! 단 2분 소요! 이벤트에 지금 참여하세요.') + '%0A' + '&route=https://www.orange-event.kr';
                }
            } else {
                window.open('http://band.us/plugin/share?body=' + encodeURIComponent(url) + '%0A' + encodeURIComponent('4050도 갖고 싶은 카카오이모티콘!\r\n선착순 2만명! 100%증정! 단 2분 소요! 이벤트에 지금 참여하세요.') + '%0A' + '&route=https://www.orange-event.kr', '', 'scrollbars=no, width=584, height=635');
            }
        }
    }
});

function setKmcData(data) {
    if (data == null) {
        closePop('personal');
    } else {
        $('#OverFortyFiveDbModel_Name').val(data.Name);
        $('#OverFortyFiveDbModel_Mobile').val(data.Mobile);
        $('#OverFortyFiveDbModel_BirthDay').val(data.BirthDay);
        if (data.Gender === "0") {
            $("#male").prop("checked", true);
        } else {
            $("#female").prop("checked", true);
        }
        $('.dimm__block').attr('onclick', '');
    }

    //if ($('#OverFortyFiveDbModel_BirthDay').val().substring(0, 4) >= 1990) {
    //    closePop('personal');
    //}
}
