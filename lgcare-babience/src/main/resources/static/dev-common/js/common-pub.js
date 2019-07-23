//---------------------------------------------------
// openPop / closePop
//---------------------------------------------------
function openPopup(flag, dimmedBoolean) {
  var scrollTop = $(window).scrollTop() + 50;
  var $selector = $('#pop-' + flag);
  var dim = dimmedBoolean;

  if (dimmedBoolean != false) $(".dimmed").show();
  var hasQuiz = $selector.hasClass('quiz');
  if(deviceKind ==='w' && hasQuiz){
    $selector.css('top', '5px').show();
  } else {
    $selector.css('top', scrollTop + 'px').show();
  }
};

function closePopup(flag, dimmedBoolean) {
  var dim = dimmedBoolean;
  var $selector = $('#pop-' + flag);
  if (dimmedBoolean != false) $(".dimmed").hide();
  $selector.hide();

  //체험신청 당첨자발표 팝업 닫은 후 후기이벤트로 이동 
  // if(flag === 'prizewinner'){
  //   $('html, body').animate({scrollTop:$('#review').offset().top}, 500);
  // }
};

//---------------------------------------------------
// copy
//---------------------------------------------------
function copy() {
  var clipboard = new Clipboard('#copy__url');
  $('#copy__url').attr('data-clipboard-text', document.location.href);
  clipboard.on('success', function (e) {
    alert('복사되었습니다');
    // 복사완료 후 Clipboard 지우기
    Clipboard = null;
  });

}

//---------------------------------------------------
// top 버튼
//---------------------------------------------------

function topBtn() {
  $('html,body').stop().animate({
    scrollTop: 0
  }, 500);
};






//---------------------------------------------------
//  스크롤 효과 중복 제어
//---------------------------------------------------

var keys = {37: 1, 38: 1, 39: 1, 40: 1};

function preventDefault(e) {
  e = e || window.event;
  if (e.preventDefault)
    e.preventDefault();
  e.returnValue = false;
}

function preventDefaultForScrollKeys(e) {
  if (keys[e.keyCode]) {
    preventDefault(e);
    return false;
  }
}

function disableScroll() {
  if (window.addEventListener) // older FF
    window.addEventListener('DOMMouseScroll', preventDefault, false);
  window.onwheel = preventDefault; // modern standard
  window.onmousewheel = document.onmousewheel = preventDefault; // older browsers, IE
  window.ontouchmove = preventDefault; // mobile
  document.onkeydown = preventDefaultForScrollKeys;
}

function enableScroll() {
  if (window.removeEventListener)
    window.removeEventListener('DOMMouseScroll', preventDefault, false);
  window.onmousewheel = document.onmousewheel = null;
  window.onwheel = null;
  window.ontouchmove = null;
  document.onkeydown = null;
}



$(window).scroll(function (event) {
  var scrollTop = $(document).scrollTop();
  var $btnTop = $('.btn__top');

  if (scrollTop >= 1054) {
    $btnTop.fadeIn();
  } else {
    $btnTop.fadeOut();
  }
});

