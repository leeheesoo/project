//=== screenScroll 
var scrennScroll = (function () {
  var optObj = null;
  var eleNavName = '';

  var init = function (obj) {
    optObj = obj;

    // scrollfy 생성
    $.scrollify({
      section: ".onescroll",
      easing: "easeOutExpo",
      //interstitialSection: "nav",// one scroll 제외
      scrollSpeed: 700,
      offset: 0,
      scrollbars: true,
      setHeights: false, //기본 section높이값 설정 여부
      updateHash: false, // 해시테그 업데이트
      before: function (i, panels) {      
        disableScroll();      
      },
      after:function(i){
        enableScroll();      
        if(i === 1){
          $('#productMov').show(); // section 넘김 버벅임 때문에 따로show
        }
      }
    });
    makeNav();
  }


  var makeNav = function () {
    $(optObj.eleNavName).on("click", function () {
      $.scrollify.move($(this).attr("href"));
      return false;
    });
  }

  $(function() {
    $('.onescroll').on('touchmove', function(e){ 
      e.preventDefault();
    });
  });

  return {
    init: init,
    makeNav: makeNav,
  }

}());