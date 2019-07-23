$(function () {
  $('.mov-cover').click(function () {
    startMov();
  });

  $('#nav__bottom li').eq(0).addClass('on');

  //main은 터치 후 계속 nav show
  $(document).bind('touchstart',function(e){
    var $mainNav = $('.nav__bottom');
    app.navBottomShow = true;
  });


})


function startMov() {
  $('.mov-cover').hide();
  mainYoutubePlayer.playVideo();
}



// var device;

var app = new Vue({
  el: '#main',
  data: {
    loading: false,
    navBottomShow:false
  },
  created: function () {
    this.checkDevice();

  },
  mounted: function(){          
    // this.$nextTick(function () {
    //  // quizSlider.init('.quizSlider');      
    //   scrennScroll.init({
    //    //eleNavName: '.pagination a'        
    //   });
    // });
  },
  methods: {
    checkDevice: function() {
      if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
          this.device = 'm';
      } else {
          this.device = 'w';
      }
    }

  }
});
