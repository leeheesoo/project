// ==================================================
// popup
// ==================================================
function openPop(flag, dimmedBoolean) {
    var scrollTop = $(window).scrollTop();
    var $selector = $('.pop-' + flag);
    var dim       = dimmedBoolean;          // 생략이 기본, false일 경우 dim을 사용하지 않는다
    if (dimmedBoolean != false) {
        $('.dimmed').show();
        $selector.css('top', scrollTop + 'px').show().focus();
    } // dimmed 제어
};

function closePop(flag, dimmedBoolean) {
    var dim = dimmedBoolean;  // 생략이 기본, false일 경우 dim을 사용하지 않는다
    if (dimmedBoolean != false) {
        $('.dimmed').hide();
        $('.pop-' + flag).hide();
    } //dimmed 제어
    
};
$(function () {
    //    
    $('.slick--desc').slick({
        infinite: true,
        arrows: false,
        width: 420,
    });

    $('.event-howto__nav .btn--prev').on('click', function () {
        $('.slick--desc').slick('slickPrev');
    });

    $('.event-howto__nav .btn--next').on('click', function () {
        $('.slick--desc').slick('slickNext');
    });

    $('.slick--photolist').on('init', function (slick) {
        //--- 이미지 비율 지키며 사이즈 조정: true / 그냥 사이즈만 조정: false
        setTimeout(function () {
            keepRatio(false);
        }, 100)
        

    }).slick({
        infinite: true,
        arrows  : false,
        rows: 3,
        width: 260,
        slidesToShow: 1,
        slidesToScroll: 1,
        slidesPerRow: 2
    });
    
    function keepRatio(boolKeepRatio) {
        var imgs = $('.slick--photolist .single-slider__item img');

        if (boolKeepRatio) {
            //console.log(boolKeepRatio);
            imgs.each(function (idx, ele) {
                if ($(ele).width() - $(ele).height() < 0) {
                    $(ele).css({ 'width': 260 + 'px', 'height': 'auto', 'top': '50%', 'transform': 'translate( 0, -50%)' });

                } else {
                    $(ele).css({ 'width': 'auto', 'height': 260 + 'px', 'left': '50%', 'transform': 'translate( -50%, 0)' });
                }
            })

        } else {
            //console.log(boolKeepRatio);
            imgs.each(function (idx, ele) {
                $(ele).css({ 'width': '100%', 'height': '100%' });
                
            })

        }
    }

    $('.event-photo-list__nav .btn--prev').on('click', function () {
        $('.slick--photolist').slick('slickPrev');
    });

    $('.event-photo-list__nav .btn--next').on('click', function () {
        $('.slick--photolist').slick('slickNext');
    });
})
