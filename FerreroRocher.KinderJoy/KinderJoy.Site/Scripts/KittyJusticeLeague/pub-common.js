//---------------------------------------------------
// [pub-common.js]
// 자주쓰는 function 관리
//---------------------------------------------------
// [function list]
// deviceCheck -- device, deviceOS체크
// goPosition(to) -- to: 목표selector나 고정위치
// openPop, closePop
// placeholder
//---------------------------------------------------

var device = getDeviceName();
var deviceOs = getMobileKind();
var scrollTop = $(window).scrollTop();
var popState = false;

var mainPlayMovie = true; // true || false
var src = 'KittyJusticeLeague'; // event name or folder name
var placeholderList = false; // array || false
var directInput = false; // true || false

function initEventC() {
    if (placeholderList) placeholder(placeholderList);
    if (directInput) directInput;
}

$(document).ready(function () {
    initEventC();
    //loading();
});

//---------------------------------------------------
// 위치로 이동
// to : number, string(targetArea)
//---------------------------------------------------
function goPosition(to) {
    scrollReady = false;
    var tg = to;
    if (tg.indexOf('#') != -1) {
        var tg = $(tg).offset().top;
    }
    $("html, body").stop().animate(
        { scrollTop: tg },
        500,
        function () { scrollReady = true; }
    );
}

//---------------------------------------------------
// 레이어팝업 여닫기 기능
// openPop / closePop

// 공통 기능 외에 단발성 내용은 Etc function에 추가
// openPopEtc / closePopEtc
//---------------------------------------------------
function openPop(flag, dimmedBoolean) {
    popState = !popState;
    var scrollTop = $(window).scrollTop();
    var $selector = $('#pop-' + flag);
    var dim = dimmedBoolean; // 생략이 기본, false일 경우 dim을 사용하지 않는다
    if (dimmedBoolean != false) $("#dimmed").show();
    $selector.css('top', scrollTop + 'px').show().focus();
    // 영상팝업일 경우
    if (flag == 'mov-view') {
        if (!popAutoPlayMode) popP.playVideo();
        $('.popup .btn-right').hide();
    }
};
function closePop(flag, dimmedBoolean) {
    popState = !popState;
    var dim = dimmedBoolean; // 생략이 기본, false일 경우 dim을 사용하지 않는다
    if (dimmedBoolean != false) $("#dimmed").hide();
    $('#pop-' + flag).hide();
    // 영상팝업일 경우
    if (flag == 'mov-view') {
        popP.stopVideo();
    }
};

//function openPopEtc(flag) {
    // 영상 제어 관련
    //if (mainPlayMovie) {
    //    mainPlayMovie = !mainPlayMovie;
    //    stopVideo(mainPlayer);
    //}
    //var intervalNum = 0;
    //if (flag == 'mov-view') {
        //playVideo(popPlayer);
    //}
//}
//function closePopEtc(flag) {
    //if (flag == 'mov-view') {
    //    stopVideo(popPlayer);
    //    $('.mov-cover').hide();
    //}
//}

//---------------------------------------------------
// inapp
//---------------------------------------------------
//var showInapp = function() {
//    var $inapp = $('.inapp');
//    var winHeight = $(window).height();
//    var scrollTop = $(window).scrollTop();
//    var inappHeight = $inapp.height();
//    openPop('inapp');
//    $inapp.css({
//        top: Math.max(0, (winHeight - inappHeight) / 2) + scrollTop
//    });
//};

//---------------------------------------------------
// placeholder
//---------------------------------------------------
var placeholder = function (array, position) {
    $.each(array, function(i,e){
        var $selector = '.input-'+array[i];
        $($selector).placeholder({
            type: 'background',
            background: '/Images/'+src+'/pop/form/ph-'+array[i]+'.png'
        });
        $($selector).css('background-position', position);
    });
}

//---------------------------------------------------
// directInput : 직접입력칸 (radio+input)
// - 여러 항목중 직접 입력칸이 존재하고 그 input을 클릭할 경우 radio 활성화 처리
//---------------------------------------------------
var directInput = function () {
    var $directMsgRadio = $('.input-direct-message input[type=radio');
    var $directMsgInput = $('.input-direct-message input[type=text]');

    $directMsgInput.click(function () {
        $directMsgRadio.trigger("click");
    });
}

//---------------------------------------------------
// 해당영역에 들어오면 section-tab fixed처리
// scroll timer : https://stackoverflow.com/questions/7392058/more-efficient-way-to-handle-window-scroll-functions-in-jquery
//---------------------------------------------------
//function tabNaviAction() {
//    $(window).scroll(function () {
//        // 두번째 섹션부터 탭 고정
//        if ($(window).scrollTop() >= $('#eventArea').offset().top) {
//            $('.section-tab').addClass('fixed');
//            // 세번째 섹션 판단하여 탭 배경 변경
//            if ($(window).scrollTop() >= $('#productIntro').offset().top) {
//                $('.section-tab').addClass('tab-2');
//            } else {
//                $('.section-tab').removeClass('tab-2');
//            }
//        } else {
//            $('.section-tab').removeClass('fixed');
//        }
//    });
//}

//---------------------------------------------------
// loading
//---------------------------------------------------
function loading() {
    var duration = 6; // duration in seconds
    var fadeAmount = 0.5 // fade duration amount relative to the time the image is visible

    var images = $("#loading .img-loader");
    var numImages = images.size();
    var durationMs = duration * 1000;
    var imageTime = durationMs / numImages; // time the image is visible 
    var fadeTime = imageTime * fadeAmount; // time for cross fading
    var visibleTime = imageTime - (imageTime * fadeAmount * 2);// time the image is visible with opacity == 1
    var animDelay = visibleTime * (numImages - 1) + fadeTime * (numImages - 2); // animation delay/offset for a single image 

    images.each(function (index, element) {
        if (index != 0) {
            $(element).css("opacity", "0");
            setTimeout(function () {
                doAnimationLoop(element, fadeTime, visibleTime, fadeTime, animDelay);
            }, visibleTime * index + fadeTime * (index - 1));
        } else {
            setTimeout(function () {
                $(element).animate({ opacity: 0 }, fadeTime, function () {
                    setTimeout(function () {
                        doAnimationLoop(element, fadeTime, visibleTime, fadeTime, animDelay);
                    }, animDelay)
                });
            }, visibleTime);
        }
    });

    // creates a animation loop
    function doAnimationLoop(element, fadeInTime, visibleTime, fadeOutTime, pauseTime) {
        fadeInOut(element, fadeInTime, visibleTime, fadeOutTime, function () {
            setTimeout(function () {
                doAnimationLoop(element, fadeInTime, visibleTime, fadeOutTime, pauseTime);
            }, pauseTime);
        });
    }

    // shorthand for in- and out-fading
    function fadeInOut(element, fadeIn, visible, fadeOut, onComplete) {
        return $(element).animate({ opacity: 1 }, fadeIn).delay(visible).animate({ opacity: 0 }, fadeOut, onComplete);
    }
}

