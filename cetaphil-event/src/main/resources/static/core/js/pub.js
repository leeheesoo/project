$(document).ready(function(){
  var config = {
    scrollT: -1,
    navOriginT : 600,
    navCtrlOffset : 200,
    navFixedOffsetX : -1,
    $nav : $('.nav'),
    $wrap: $('.wrap'),
    secOffsetT: []
  }
  
  var ctrlNav = function(){
    config.scrollT = $(window).scrollTop();
    
    if(config.scrollT > config.navOriginT - 200){
      config.$nav.css({
        'position' : 'fixed',
        'top' : config.navCtrlOffset + 'px', 
        'left': calcFixedLeft() + 'px'
      })
    } else {
      config.$nav.css({
        'position' : 'absolute',
        'top' : config.navOriginT + 'px',
        'left' : '100%'
      })
    }
  }

  var calcFixedLeft = function(){
    var offset;
    if ($(window).innerWidth() < 1920){
      offset = $('.wrap').width();
    } else {
      offset = $(window).innerWidth() - $('.wrap').offset().left;
    }
    // console.log(offset);
    return offset
  }
  
  var toggleNavActive = function(){
    config.scrollT = $(window).scrollTop();
    for(var i = 0; i < 3; i++) {
      if(config.scrollT+30 >= config.secOffsetT[i] && config.scrollT+30 < config.secOffsetT[i+1]){
        $('.anchor').removeClass('on');
        $('.anchor').eq(i).addClass('on');
      } else if (config.scrollT > config.secOffsetT[3]){
        $('.anchor').removeClass('on');
        $('.anchor').eq(3).addClass('on');
      }
    }
  }

  $(window).on('load resize', function(){
    $('section').each(function(idx, el){
      config.secOffsetT.push($(this).offset().top);
    });
    if($(window).height() > $('section').last().height()){
      config.secOffsetT[config.secOffsetT.length-1] -= $('section').last().height() * 0.2;
      // console.log(config.secOffsetT[config.secOffsetT.length-1]);
    }
    // console.log(config.secOffsetT);
  });

  $(window).on('load scroll resize', function(){
    ctrlNav();
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
      scrollTop:target.offset().top - 30
    }, 500);
  });
  
});