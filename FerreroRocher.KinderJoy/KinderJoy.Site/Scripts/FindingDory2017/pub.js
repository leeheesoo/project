
// 팝업 여닫기
function openPop(flag) {
    // 당첨자 발표 처리
    //if (flag == 'pop_dang'){
    //    alert('당첨자 발표 전입니다.');
    //    return;
    //}
    //$(window).scrollTop(0,1);
    $('.popup_dimmed').show();
    var $scrollTop = $(window).scrollTop() + 82;
    $('#' + flag).show().css('top', $scrollTop);
}
function closePop(flag) {
    $('.popup_dimmed').hide();
    $('#' + flag).hide();
}

// ==================================================
// init
// ==================================================

// 모바일 체크 (true면 모바일)
var mobileCheck;
// 게임 관련 초기설정
var stage;
var stage2Check;
var gameNavigationFlag;

function init() {
    mobileCheck = $('.mobile_wrap').length;
    stage = 0;
    stage2Check = 0;
    gameNavigationFlag = 1;

    $('.toy_group').on('focus', '.grow', function (e) {
        // alert('>> ' +  e.type );
        //$(this).trigger('mouseout');
       // $(this).blur();
    });
}
function initEvent() {
    noZoom();
    setPlaceHolder();
    openPop('pop_dang');
}
$(document).ready(function () {
    init();
    initEvent();
});


// ==================================================
// 이벤트 영역
// ==================================================

// 스테이지 제어
function gameStageControl() {

    // 스테이지 이동
    $('.stage' + stage++).hide();
    $('.stage' + stage).show();
    
    // 내비게이션 이동
    if (mobileCheck) {
        $('.play_navi .indi').animate({ top: 25 * gameNavigationFlag, left: stage / 6 * 100 + '%' }, 500);
        $('.play_navi').css('background-position-y', stage * 140 * -1);
        gameNavigationFlag *= -1;
    }else{
        $('.play_navi .indi').animate({ left: stage / 6 * 100 + '%' }, 500);
        $('.play_navi').css('background-position-y', stage * 100 * -1);
    }

    // 공통 되는 부분
    function common(param) {
        $('.char_selector').animate({ bottom: -120 }, 100, function () {
            $('.char_selector>div').removeClass('on');
        }).animate({ bottom: 0 }, 500, function () {
            
        });
        $('.char_selector .txt').css('background-position-y', (param - 2) * 30 * -1);
    }

    // stage별 세팅
    if (stage == 1) {

    } else if (stage == 2) {
        $('.char_selector').show();
        common(stage);
        $('.nemo, .malin').one("click", function (e) {
            stage2Check++;
            $(this).addClass('on');
            if (stage2Check == 2) {
                //$('.nemo, .malin').removeClass('on');
                $('.stage' + stage).find('.twinkle').removeClass('twinkle');
                setTimeout(gameStageControl, 1000);// gameStageControl();
            } else {
                setTimeout(alert('한 마리 더 찾아주세요! ^^'), 500);
            }
        });
    } else if (stage == 3) {
        common(stage);
        $('.hank').one("click", function () {
            $(this).addClass('on');
            $('.stage' + stage).find('.twinkle').removeClass('twinkle');
            setTimeout(gameStageControl, 1000);
        });
    } else if (stage == 4){
        common(stage);
        $('.otter').one("click", function () {
            $(this).addClass('on');
            $('.stage' + stage).find('.twinkle').removeClass('twinkle');
            setTimeout(gameStageControl, 1000);
        });
    } else if (stage == 5){
        common(stage);
        $('.destiny').one("click", function () {
            $(this).addClass('on');
            $('.stage' + stage).find('.twinkle').removeClass('twinkle');
            setTimeout(gameStageControl, 1000);
        });
    } else if (stage == 6) {
        $('.char_selector').animate({ bottom: -120 }, 100, function () {
            $(this).hide();
        });
    }

}

// 모바일에서 더블탭(터치시작)시 줌 현상을 막는다.
function noZoom() {
    $('.no-zoom').bind('touchstart', function (e) {
        e.preventDefault();
        $(this).click();
    });
}

//placeholder
function setPlaceHolder() {
    $('.ph_phone').placeholder({
        type: 'background',
        background: '/Images/FindingDory2017/popup/ph_phone.jpg'
    });
    $('.ph_phone').css('background-position', '10px 10px');

    $('.ph_address').placeholder({
        type: 'background',
        background: '/Images/FindingDory2017/popup/ph_address.jpg'
    });
    $('.ph_address').css('background-position', '10px 15px');

    $('.ph_age').placeholder({
        type: 'background',
        background: '/Images/FindingDory2017/popup/ph_age.jpg'
    });
    $('.ph_age').css('background-position', '13px 15px');
}


