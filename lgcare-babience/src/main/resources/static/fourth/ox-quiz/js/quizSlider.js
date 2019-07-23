//=== quiz slider
var quizSlider = (function(){
  var slider = null;
  var sliderName = '';

  var init = function(eleName){                
    sliderName = eleName;
    slider = $(sliderName).slick({
      infinite: true,
      slidesToShow: 1,
      arrows:false,
      draggable: false,    
      touchMove:false,
      swipe:false
      //fade: true    
    });        
  }

  var goNextQuiz = function(slideno){
    //console.log( sliderName);          
    $(sliderName).slick('slickNext');
  }

  return {
    init: init,
    goNextQuiz: goNextQuiz
  }
}());