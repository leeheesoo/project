var device = getDeviceName();

$(document).ready(function(){
    if(device === 'w'){
        $('.slick').slick({
            infinite: true,
            arrows  : false,
            
            // width         : 200,
            slidesToShow  : 1,
            slidesToScroll: 1,
        });
        $('.btn--prev').on('click', function(){
            $('.slick').slick('slickPrev');
            // console.log('clk')
        });
        $('.btn--next').on('click', function(){
            $('.slick').slick('slickNext');
        });
    } else {
        $('.slick').slick({
            dots    : true,
            infinite: true,
            arrows  : false,
            easing  : 'easeInOutCubic',
            // speed   : 00,
            // width         : 200,
            slidesToShow  : 1,
            slidesToScroll: 1
        });
    }
    // 2018.07.27 / heeok /     
    //openPop('winner', true); 세타필 케리비안베이 이벤트 참여자 개인정보 파기 - 당첨자 팝업 내림
});

/* 팝업 제어 */
function openPop(flag, dimmedBoolean) {
    var scrollTop = $(window).scrollTop();
    var $selector = $('.popup--' + flag);
    var dim       = dimmedBoolean;          // 생략이 기본, false일 경우 dim을 사용하지 않는다
    if (dimmedBoolean != false) {
        $('.dimmed').show();
        // if(device === 'w'){
            $selector.css('top', scrollTop + 'px').show().focus();
        /* } else {
            $selector.css('top', scrollTop - 100 + 'px').show().focus();
        } */
    } // dimmed 제어
    // console.log('open')
};

function closePop(flag, dimmedBoolean) {
    var dim = dimmedBoolean;  // 생략이 기본, false일 경우 dim을 사용하지 않는다
    if (dimmedBoolean != false) {
        $('.dimmed').hide();
        $('.popup--' + flag).hide();
    } //dimmed 제어
    
};

/* 네비게이션 */
/* var scrollTop = $(window).scrollTop();
var floatNavH = $('.float_nav:visible').height();
var kvH       = $('.key_visual:visible').height();
 */
/* $(function(){
    setTimeout(function(){
        scrollTop = $(window).scrollTop();
        floatNavH = $('.float_nav:visible').height();
        kvH       = $('.key_visual:visible').height();

        console.log('dd');
        floatNav();

        $('.float_nav a').click(function (e) {
              e.preventDefault();
              theID = $(this).attr('href');

              $('html,body').animate({
                  scrollTop: $($.attr(this, 'href')).offset().top - floatNavH
              }, 300);
          });

    },3000);

}); */

/* $(window).resize(function(){
    floatNavH = $('.float_nav:visible').height();
    kvH       = $('.key_visual:visible').height();

    floatNav();
}) 

$(window).scroll(function(){
    scrollTop = $(window).scrollTop();
    floatNav();
})
*/

var scrollTop;
var $fNav;
var fNavPos = 380;
var $lis;
var posEvt1, posEvt2, posEvt3;
var posArr = [];
// var floatNavH =  $('.float_nav:visible').height();
// var kvH = $('.key_visual:visible').height();

$(window).on('load',function(){
    getEventRange();
    $fNav = $('.floating-nav');
    $lis  = $('.floating-nav__item');
    // setInitialOffset();
    controlNav();

    // console.log(fNav.offset().top);
    $lis.on('click', function(){
        var Idx = $(this).index();
        // console.log(Idx);
        // var tmp = $(this).data('id');
        // console.log(tmp);
        goToW(Idx);
    });
});

$(window).on('scroll', function(){
    // console.log(fNavPos);
    // getEventRange();
    // setInitialOffset();
        controlNav();
    
});

// $(window).on('resize', function(){
//     getEventRange(); // 이벤트 영역 세팅
//     setInitialOffset(); // 네비 위치 초기값 설정
//     controlNav();
//     // setInitialOffset();
//     // var Wid = window.innerWidth;
//     // console.log(Wid);
// })

function controlNav() { // 네비 컨트롤
    // var docH      = $('.restoraderm-event-wrap').height();
    scrollTop = $(window).scrollTop();
    
    if(device === 'w' && $fNav){
        // if(scrollTop < docH){
            if(scrollTop > fNavPos){
            // $fNav.css({top:scrollTop+20}); // 상단에 붙이기
            $fNav.animate({top:scrollTop+20},10); // 상단에 붙이기
            // $fNav.css({position:'fixed',
            //             top: 10+'px'
            //         });

            /* $.each($lis, function(i, val){  // 클래스 초기화
                $(val).removeClass('active');
            }); */
            /* if(scrollTop > posEvent1 && scrollTop < posEvent2){
                $lis.eq(0).addClass('active');
            } else if(scrollTop > posEvent2 && scrollTop < posEvent3){
                $lis.eq(1).addClass('active');
            } else if(scrollTop > posEvent3) {
                $lis.eq(2).addClass('active');
            } *
            / //else {
            //     $.each($lis, function(i, val){
            //         $(val).removeClass('active');
            //     });
            // }

            // 이벤트 최상단 영역일 때 투명하게 만들기
            /* if(scrollTop === posEvent1 || scrollTop === posEvent2 || scrollTop === posEvent3){  
                $fNav.animate({ opacity: 0.5 });
            } else {
                $fNav.css('opacity', '1');
            }  */
            
            } else {
            $fNav.css({position:'absolute',
                        top: fNavPos
                    });
            }
        // }
    }
}

/* function setInitialOffset(){ // 네비 위치 초기값 설정 (웹, 모바일)
    var chkW = window.innerWidth;
    
    if(chkW > 640){
        fNavPos = 380;
        console.log(fNavPos);
    } else {
        fNavPos = $('.tb-wrap').height() * 0.05;
        console.log(fNavPos);    
    }
    fNavPos = fNav.offset().top;
    console.log(fNavPos);
} */

function getEventRange(){ // 이벤트 영역 세팅
    posEvt1 = $('.video').offset().top;
    posEvt2 = $('.main-event').offset().top;
    posEvt3 = $('.reservation-event').offset().top;
    if(device==='m'){
        posEvt3 = $('.reservation-event').offset().top-50;
    }
    // console.log(posEvent1);
    // console.log(posEvent2);
    // console.log(posEvent3);
    posArr = [posEvt1,posEvt2,posEvt3];
    // console.log(typeof(posArr[0]));
}

function goToW(Idx){ // 선택 이벤트 영역으로 스크롤
    // console.log(tg);
    // var Idx = 'posEvt'+ Idx;
    // console.log(Idx);
    var tg = posArr[Idx];
    // console.log(tg);
    $('html, body').animate({scrollTop:tg})
    /* switch(tg){
        case 'ev1': 
        // console.log('test');
        $('html body').animate({scrollTop:posEvent1});    
        break;
        case 'ev2': 
        $('html body').animate({scrollTop:posEvent2});
        break;           
        case 'ev3': 
        $('html body').animate({scrollTop:posEvent3});
        break;           
    } */
    
}




/* 유튜브 영상 */
 // 2. This code loads the IFrame Player API code asynchronously.
    var tag     = document.createElement('script');
        tag.src = "https://www.youtube.com/iframe_api";
        
    var firstScriptTag = document.getElementsByTagName('script')[0];
        firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

        // 3. This function creates an <iframe> (and YouTube player)
        //    after the API code downloads.
    var player;
    function onYouTubeIframeAPIReady() {
        player = new YT.Player('player', {
        width     : (device === 'w' ? '1061' : '658'),   // 1061
        height    : (device === 'w' ? '596' : '370'),    // 560, 347
        videoId   : 'KjAu7HX_Vew',
        playerVars: {
            'playsinline'   : 1,
            'showinfo'      : 0,
            'modestbranding': 0,
            'rel'           : 0
            },
        // events        : {
        //     'onReady'      : onPlayerReady,
        //     'onStateChange': onPlayerStateChange
        //     }
        });
    }

    // function onPlayerReady(e){
    //     e.target.setVolume(50);
    //     $('.video-cover').show();
    // }

    // function onPlayerStateChange(e){
    //     if(e.data == YT.PlayerState.ENDED){
    //         $('.video-cover').show();
    //     }
    // }

