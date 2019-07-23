var device = getDeviceName();

// floating nav 스크롤
function controlNav() { 
    var $Nav = $('.pc-nav');
    var NavPos = 380;
    var scrollTop = $(window).scrollTop();
    
    scrollTop > NavPos ? $Nav.animate({top:scrollTop+100},10) : $Nav.css({position:'absolute',top: NavPos});
};



$(function(){
    // floating nav 영역이동
    $('.floating__nav li a').click(function(){
        var target = $(this.hash);       
        if(device === 'm'){
            var navH = $('.mo-nav').height(); // 모바일 nav height
            $('body,html').animate({scrollTop:target.offset().top - navH}, 500);
        } else {
            $('body,html').animate({scrollTop:target.offset().top}, 500);
        }
        return false;
    });
}); //jquery

$(window).scroll(function(){
    controlNav();
}); //scroll