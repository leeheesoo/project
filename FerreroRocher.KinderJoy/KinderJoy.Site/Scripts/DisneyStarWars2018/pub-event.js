//---------------------------------------------------
// [pub-event.js]
// 이벤트용 function
//---------------------------------------------------

//if (device === 'm') {

//} else {

//}

$(document).ready(function () {
    openPop('winner');
    visualChangeControl();
    $('.video-cover').on('click', function () {
        $(this).hide();
        eventPlayer.playVideo();        
    });

    $('.btn-agree').on('click', function () {
        $('.text-box').slideToggle();        
    });
});

//---------------------------------------------------
// function
//---------------------------------------------------
// 팝업에서 영상 시청 끝날 경우(임시로 짠 것이니 수정하거나 별도로 제작하여 사용하면 됩니다.
function eventResult() {
    closePop('mov-view');

    // 성공, 꽝을 구분하는 개발 필요
    //setTimeout(function () {
    openPop('entry');
    //}, 300);

}

//---------------------------------------------------
// 상단 비쥬얼 제어
//---------------------------------------------------
function visualChangeControl() {
    var $container = $('.toyview-container');
    var $all = $('.toyview');
    var $kj = $('.toyview.kj');
    var $star = $('.toyview.star');
    var $disney = $('.toyview.disney');
    var $btnPrev = $('.btn-prev');
    var $btnNext = $('.btn-next');
    var zindex = 1;
    var scene = 0;
    var sceneInfo = [
        {
            scenName: 'kj',
            item: $kj,
            bgColor: '#61bcd2',
            deco: false,
            prev:'disney',
            next: 'star'
        },
        {
            scenName: 'star',
            item: $star,
            bgColor: '#000',
            deco: false,
            prev:'kj',
            next: 'disney'
        },
        {
            scenName: 'disney',
            item: $disney,
            bgColor: '#f5a2ec',
            deco: false,
            prev:'star',
            next: 'kj'
        }
    ];

    changeScene(scene);
    // 장면 처리
    function changeScene(scene) {
        // 전체 장면에 대한 처리
        $all.css('z-index', -1);
        $all.hide();
        //현재 장면에 대한 처리
        var thisScene = sceneInfo[scene];
        $(thisScene.item).css('z-index', 0);
        $(thisScene.item).show();
        $('.toyview-container').css('background', thisScene.bgColor);
        thisScene.deco ? $('.heading .deco').fadeIn() : $('.heading .deco').fadeOut();
        $btnPrev.find('div').removeClass().addClass(thisScene.prev);
        $btnNext.find('div').removeClass().addClass(thisScene.next);
        if (thisScene.scenName == 'kt') {
            $('.event-intro .heading').addClass('kt');
        } else {
            $('.event-intro .heading').removeClass('kt');
        }
        tl.restart();
    }
    // 좌우버튼 처리
    $btnPrev.click(function () {
        scene == 0 ? scene = sceneInfo.length - 1 : scene -= 1;
        changeScene(scene);
    });
    $btnNext.click(function () {
        scene == sceneInfo.length - 1 ? scene = 0 : scene += 1;
        changeScene(scene);
    });
}


