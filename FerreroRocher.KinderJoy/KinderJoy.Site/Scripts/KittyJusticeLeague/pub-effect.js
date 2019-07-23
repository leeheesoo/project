////---------------------------------------------------
//// effect-particle
////---------------------------------------------------
//(function($){
//    var particles = [];

//    for (var i = 2; i <= 3; i++) {
//        var $div = '<div class="particle particle-' + i + '"></div>';
//        particles.push($div);
//    }

//    // 팝업용
//    var effectBg = $('<div class="popup-bg"></div>');
//    effectBg.prependTo('.popup');
//    $('.popup-bg').html(particles);
//    // 페이지용
//    var pageBg = $('<div class="page-bg"></div>');
//    pageBg.insertAfter('.bacgkround-wrap');
//    $('.page-bg').html(particles);
//})(jQuery);


//---------------------------------------------------
// use greensocks
//---------------------------------------------------

var sceneBack = $('.item.back'),
    sceneHill = $('.item.snowhill'),
    sceneSky = $('.item.sky'),
    sceneTop = $('.item.top'),
    sceneToy = $('.item.toy'),
    sceneCenter = $('.item.center'),
    tl = new TimelineLite();

tl.add(getSceneAnamation());

// functions for building nested timelines
function getSceneAnamation() {
    var sceneTimeline = new TimelineLite();
    sceneTimeline
        .add('start' , 0)
        .from(sceneHill, 0.8, { top: 400 }, 'start') // 언덕
        .from(sceneBack, 0.8, { bottom: -700 }, "-=0.8") // 뒷배경
        .from(sceneSky, 3, { alpha: 0 }, 'start') // 하늘
        .from(sceneCenter,0.5, { top: 650, ease: Back.easeOut }, "-=2.7") //가운데
        .from(sceneTop, 0.8, { top: -400 }, "start") // 상단
        .from(sceneToy, 0.5, { top:600, ease: Back.easeOut }, 'start') // 인형
    ; return sceneTimeline;
}
