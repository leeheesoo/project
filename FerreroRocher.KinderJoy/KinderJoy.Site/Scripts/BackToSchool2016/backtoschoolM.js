//레이어팝업 열기
function openPopup(param) {
    var wrapH = $('#wrap').height();
    $('#png_bg').css('height', wrapH);
    $('#png_bg').show();
    $('#' + param).show();
    $('html,body').animate({ scrollTop: 130 });
};


//레이어팝업 닫기
function closePop(param) {
    $('#png_bg').hide();
    $('#png_bg2').hide();
    $('#' + param).hide();
}


//우편번호팝업 닫기
function closePop2(param) {
    $('#' + param).hide();
}


// 인앱 팝업 열기
function openInapp(param) {
    var wrapH = $('#wrap').height();
    $('#png_bg2').css('height', wrapH);
    $('#png_bg2').show();
    $('#' + param).show();
    $('html, body').animate({ scrollTop: 0 });
}


// 모바일 메인 탭 제어
function mobTab(tab) {
    $('#mobTab01').attr('src', "/Images/BackToSchool2016/m/mob_tab01.png");
    $('#mobTab02').attr('src', "/Images/BackToSchool2016/m/mob_tab02.png");
    $('#mobTab03').attr('src', "/Images/BackToSchool2016/m/mob_tab03.png");

    var src = $('#mobTab0' + tab).attr('src');
    $('#mobTab0' + tab).attr('src', src.replace('.png', 'on.png'));

    if (tab == 1) {
        $('.notice_wrap').show();
        $('.bingo_wrap').show();
        $('.toy_detail_wrap').hide();
        $('.package_wrap').hide();
    } else if (tab == 2) {
        $('.toy_detail_wrap').show();
        $('.notice_wrap').hide();
        $('.bingo_wrap').hide();
        $('.package_wrap').hide();
    } else if (tab == 3) {
        $('.package_wrap').show();
        $('.toy_detail_wrap').hide();
        $('.notice_wrap').hide();
        $('.bingo_wrap').hide();
    }
}


$(document).ready(function () {
    /* 라디오버튼 클릭시 이미지 변경 */
    var $rdArea = $('.js-rd-area'); // 라디오버튼 영역
    $rdArea.each(function () {
        $(this).on('click', function () {
            var agree_id = $(this).attr("id");
            var radiobox_id = "radio" + agree_id;
            var answer = $(this).attr('data-value');
            $('.rdImg').attr("src", "/Images/BackToSchool2016/m/rd_off.png");
            $('.inputRadio').prop("checked", false);

            $('#rd_agree' + answer).attr("src", "/Images/BackToSchool2016/m/rd_on.png");
            $('#select' + answer).prop("checked", true);

            $('#hid_Answer').val(answer);

        });
    });
});


$(function () {
//연락처 placeholder
    $('.ph_phone').placeholder({
        type: 'background',
        background: '/Images/BackToSchool2016/pop/ph_phone.png'
    });
    $('.ph_phone').css('background-position', '15px 15px');
});


//경품 및 당첨자 팝업 탭 제어
function giftTab(tab) {
    $('#tab01').attr('src', "/Images/BackToSchool2016/pop/gift_tab01.png");
    $('#tab02').attr('src', "/Images/BackToSchool2016/pop/gift_tab02.png");

    var src = $('#tab0' + tab).attr('src');
    $('#tab0' + tab).attr('src', src.replace('.png', 'on.png'));

    if (tab == 1) {
        $('.gift_area').show();
        $('.dang_area').hide();
    } else if (tab == 2) {
        $('.dang_area').show();
        $('.gift_area').hide();
    }
}