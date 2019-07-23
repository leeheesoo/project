var device;
var vrImgH, bgTopH;

var stepItemList;
var totalGateNum;

var currentIdx = 0;
var prevIdx;
var $currentStepItem, $prevStepItem;
var $kinderSelectSlide, $kinderSelectItem;

var $rino, $txtBox;

$(function () {
    checkDevice();
    init();

    var vrImg = new Image();
    vrImg.onload = function () {
        resetH();
    };
    vrImg.src = '/Images/gate/vr_contents.png';
});

$(window).resize(function () {
    resetH();
});


/* ================================== */
/* init
/* ================================== */
function init() {
    // set variables
    stepItemList = $('.gate-step');
    totalGateNum = stepItemList.length; // 4

    $kinderSelectSlide = $('.kinder-select-slide');
    $kinderSelectItem = $('.kinder-item-selected');

    // init step setting
    setStep(0);

    // go next step
    $('.txt-box').click(function () {
        //console.log(currentIdx);
        currentIdx++;
        setStep(currentIdx);
    })
    // go to step3
    $('.btn-retry').on('click', function () {
        retrySelectItem();
    })

    // slide setting
    if (device === 'w') {
        $kinderSelectSlide.slick({
            infinite: true,
            rows: 4,
            slidesPerRow: 4
        });
    } else {
        $kinderSelectSlide.slick({
            infinite: true,
            rows: 2,
            slidesPerRow: 4
        });
    }
}

/* ================================== */
/* 단계별 제어
/* ================================== */
function setStep(tgIdx) {
    currentIdx = tgIdx; // list index number start to 0
    if (currentIdx > totalGateNum) currentIdx === totalGateNum;
    prevIdx = currentIdx - 1;

    $currentStepItem = $(stepItemList[currentIdx]);
    $prevStepItem = $(stepItemList[prevIdx]);

    $currentStepItem.fadeIn();
    $prevStepItem.fadeOut();

    startStep();
}

function startStep() {
    $rino = $('.rino:visible');
    $txtBox =$('.txt-box:visible');

    if (currentIdx === 0 || currentIdx === 1) {
        $rino.addClass('bounceInUp');
        $txtBox.addClass('fadeIn');
        if (currentIdx === 0) {
            setTimeout(function () {
                $rino.removeClass('bounceInUp').addClass('bounce infinite');
            }, 2500);
        }
    }
    else if (currentIdx === 2 || currentIdx === 3) {
        $rino.addClass('bounceInLeft');
        $txtBox.addClass('fadeIn');
        if (currentIdx === 2) {
            $kinderSelectSlide.slick('refresh');
        }
    }
}

/* ================================== */
/*  킨더조이 알 클릭시 이미지 경로 제어
/* ================================== */
function selectItem(param) {
    var $prdTxt = $('.gate-step4 .txt-egg');
    var preUrl = '/Images/gate/';


    /* 말풍선이 2가지 일 경우 사용 */
    //if (param % 2 == 0) {
    //    $itemImg.attr('src', preUrl + 'txt_egg_girl.png')
    //} else {
    //    $itemImg.attr('src', preUrl + 'txt_egg_boy.png')
    //}

    /* 제품 별로 나와야 할 말풍선이 다를 경우 사용 */
    // $itemImg.attr('src', preUrl + 'txt_item' + param + '.png')
    $prdTxt.attr('class', 'txt-egg txt-egg' + param);
    // console.log(param);
    setStep(3);

    $kinderSelectItem.addClass('show').find('img').attr('src', preUrl + 'toy' + param + '.png');
}


/* ================================== */
/* 다시뽑기 : 3단계로 이동
/* ================================== */
function retrySelectItem() {
    $kinderSelectItem.removeClass('show');
    $(stepItemList[3]).hide();
    setStep(2);
}


/* ================================== */
/*  Device check
/* ================================== */
function checkDevice() {
    if (navigator.userAgent.match(/Mobile|iP(hone|od)|BlackBerry|IEMobile|Kindle|NetFront|Silk-Accelerated|(hpw|web)OS|Fennec|Minimo|Opera M(obi|ini)|Blazer|Dolfin|Dolphin|Skyfire|Zune/)) {
        //스마트폰일 때 실행 될 스크립트
        device = 'm';
    } else {
        device = 'w';
    }
}


/* ================================== */
/*  높이값 제어
/* ================================== */
function resetH() {
    if (device === 'w') {
        vrImgH = $currentStepItem.find('.vr-contents').height();
        bgTopH = $('.bg-top').height();

        /*  리노 이미지 높이값에 따라
        gate-step height값 셋팅하여 페이지 높이 셋팅 */
        $('.gate-wrap').css('height', bgTopH + vrImgH);
    }
}