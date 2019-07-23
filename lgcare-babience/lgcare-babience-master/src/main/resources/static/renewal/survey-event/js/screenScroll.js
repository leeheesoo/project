//=== screenScroll 
var scrennScroll = (function () {
  var optObj = null;
  var eleNavName = '';

  var init = function (obj) {
    optObj = obj;

    // scrollfy 생성
    $.scrollify({
      section: ".page",
      easing: "easeOutExpo",
      interstitialSection:'nav .survey-event__tab',
      standardScrollElements: "#review", // one scroll 제외
      scrollSpeed: 1000,
      scrollbars:false,
      offset: 0,
      scrollbars: true,
      setHeights: false, //기본 section높이값 설정 여부
      updateHash: false, // 해시테그 업데이트
      before: function (i, panels) {

        //console.log(i)
        //var ref = panels[i].attr("data-section-name");
        $(optObj.eleNavName).parent().removeClass("active");
        $(optObj.eleNavName).parent().eq(i-1).addClass("active");
        if(i === 3){
          $(optObj.eleNavName).parent().eq(1).addClass("active");
        }
        disableScroll();
        stop();
      },
      after: function(){
        enableScroll();   
        
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


  return {
    init: init,
    makeNav: makeNav,
  }

}());