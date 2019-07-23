//---------------------------------------------------
//  타이머
//---------------------------------------------------
var num = 0;
var gabTime;
var navBottomShow = false;
var $mainNav = window.document.getElementsByClassName("nav__bottom");
var element = document.getElementById("nav__bottom");

function timmer() {

  num++;
  //console.log(num)
  if (num === 4) { //4초 설정
    clearInterval(gabTime); //num이 4 되면 타이머를 해제 
    app.navBottomShow = false; //hide

  }

  return num;
}




$(function () {

  $(document).bind('touchstart',function(e){
    var $mainNav = $('.nav__bottom');
    
    app.navBottomShow = true; //show
    clearInterval(gabTime);
    num = 0;
    gabTime = setInterval("timmer()", 1000);
  })


  //터치방식
  //var lastY;
  // $(document).bind('touchmove', function (e) {
  //   var $mainNav = $('.nav__bottom');
  //   var currentY = e.originalEvent.touches[0].clientY;

  //   if (currentY > lastY) { // up
  //     $mainNav.removeClass('active');
  //     if($mainNav.parent().attr("id") === 'kabrita-nav-bottom-quiz'){
  //       $mainNav.addClass('active');
  //     }
  //   } else if (currentY < lastY) { //down   
  //     $mainNav.addClass('active');
  //   }
  //   lastY = currentY;
  // });

  //스크롤방식
  // var didScroll; 
  // var lastScrollTop = 0; 
  // var delta = 5; 
  // var $mainNav = $('.nav__bottom');
  // var navbarHeight = $mainNav.outerHeight(); 
  
  // $(window).scroll(function (event) { 
  //   didScroll = true; 
  // }); 

  // setInterval(function () { 
  //   if (didScroll) { 
  //     hasScrolled(); didScroll = false; 
  //   } }, 250); 

  // function hasScrolled() {
  //   var st = $(this).scrollTop();

  //   if (Math.abs(lastScrollTop - st) <= delta) return;

  //   if (st > lastScrollTop && st > navbarHeight) {
  //     // Scroll Down 
  //     console.log('down')
  //     $mainNav.addClass('active');
  //   } else {
  //     // Scroll Up 
  //     if (st + $(window).height() < $(document).height()) {
  //       console.log('up')
  //       $mainNav.removeClass('active');
  //     }
  //   } lastScrollTop = st;
  // }


})