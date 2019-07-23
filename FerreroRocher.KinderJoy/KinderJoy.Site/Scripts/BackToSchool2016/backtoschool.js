//레이어팝업 열기
function openPopup(param) {
    var wrapH = $('#wrap').height();
    $('#png_bg').css('height', wrapH);
    $('#png_bg').show();
    $('#' + param).show();
    $('html,body').animate({ scrollTop: 120 });
};


//레이어팝업 닫기
function closePop(param) {
    $('#png_bg').hide();
    $('#' + param).hide();
}


//우편번호팝업 닫기
function closePop2(param) {
    $('#' + param).hide();
}


$(document).ready(function () {
    /* 라디오버튼 클릭시 이미지 변경 */
    var $rdArea = $('.js-rd-area'); // 라디오버튼 영역
    $rdArea.each(function () {
        $(this).on('click', function () {
            var agree_id = $(this).attr("id");
            var radiobox_id = "radio" + agree_id;
            var answer = $(this).attr('data-value');
            $('.rdImg').attr("src", "/Images/BackToSchool2016/rd_off.png");
            $('.inputRadio').prop("checked", false);

            $('#rd_agree' + answer).attr("src", "/Images/BackToSchool2016/rd_on.png");
            $('#select' + answer).prop("checked", true);

            $('#hid_Answer').val(answer);

        });
    });
});



/////////////////////////메인 스크롤 제어 시작/////////////////////////

//전역변수
var scrollReady = true;

//현재영역의 번호저장 전역변수
var thisParam = 0;

window.onload = function () {
    $('#navi li.navion img').each(function () {
        $(this).mouseover(function () {
            $(this).attr('src', "/Images/BackToSchool2016/" + this.id + "on.png");
        });
        $(this).mouseout(function () {
            menuOut();
        });
    });
}

function menuOut() {
    $('#navi li.navion img').each(function () {
        this.src = this.src.replace('on.png', '.png');
    });
    $('#menu0' + thisParam).attr('src', "/Images/BackToSchool2016/menu0" + thisParam + "on.png");
}

function scrollMov() {
    var scrollTop = $(window).scrollTop();

    if (scrollTop < 1987) {
        thisParam = 1;
        menuOut();
    }
    else if (scrollTop > 1987 && scrollTop < 4500) {
        thisParam = 2;
        menuOut();
    }
}

function goArea(flag) {
    switch (flag) {
        case 1:
            goPosition(0);
            break;
        case 2:
            goPosition(2007);
            break;
        case 3:
            openPopup('pop_gift');
            break;
    }
    thisParam = flag;
    menuOut;
}

function goPosition(to) {
    scrollReady = false;
    $("html, body").animate(
        { scrollTop: to },
        200,
        'easeOutExpo',
        function () { scrollReady = true; }
    );
}

$(function () {
    var scrollTop = $(window).scrollTop();

    $('#navi li a').focus(function () {
        this.blur();
    });

    $(window).bind('scroll', function () {
        scrollMov();
    });
});

/////////////////////////메인 스크롤 제어 끝/////////////////////////


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

//우편번호찾기 팝업 제어
