//=== screenScroll 
var scrennScroll = (function(){
  var optObj = null;
  var eleNavName = '';

  var init = function(obj){
    optObj = obj;

    // scrollfy 생성
    $.scrollify({
      section: ".section",
      easing: "easeOutExpo",
      interstitialSection: ".header,.footer", // one scroll 제외
      scrollSpeed: 500,
      offset: 0,
      scrollbars: true,
      setHeights: false, //기본 section높이값 설정 여부
      updateHash: false, // 해시테그 업데이트
      before: function (i, panels) {
        console.log(i)
        var ref = panels[i].attr("data-section-name");
      }
    });
    makeNav();          
  }       


  var makeNav = function(){        
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
