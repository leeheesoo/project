$(document).ready(function(){
  var config = {
    scrollT: -1,
    navOriginT : 300,
    navCtrlOffset : 300,
    navFixedOffsetX : -1,
    $nav : $('.nav'),
    $wrap: $('.wrap'),
    secOffsetT: []
  }
    
  var toggleNavActive = function(){
    config.scrollT = $(window).scrollTop();
    for(var i = 0; i < 3; i++) {
      if(config.scrollT >= config.secOffsetT[i] && config.scrollT < config.secOffsetT[i+1]){
        $('.anchor').removeClass('on');
        $('.anchor').eq(i).addClass('on');
      } else if (config.scrollT > config.secOffsetT[3]){
        $('.anchor').removeClass('on');
        $('.anchor').eq(3).addClass('on');
      }
    }
  }

  $(window).on('load', function(){
    $('section').each(function(idx, el){
      config.secOffsetT.push($(this).offset().top-165);
    });
    // console.log(config.secOffsetT);
  });

  $(window).on('load scroll', function(){
    // ctrlNav();
    toggleNavActive();
    // console.log($(window).scrollTop());
    // console.log($(window).innerWidth());
    // console.log($('.wrap').width());
    // console.log($('.wrap').offset().left);
  });


  $('.anchors').on('click', '.anchor' ,function(e){
    e.preventDefault();
    var target = $(this.hash);
    $('body, html').animate({
      scrollTop:target.offset().top - 160
    }, 500);
  });
  
});