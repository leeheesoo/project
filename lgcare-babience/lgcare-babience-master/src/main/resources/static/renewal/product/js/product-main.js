var app = new Vue({
  el: '#product',
  data: {
    navBottomShow: false, //bottom nav
    loading: true,
    imgUrlPrefix:
      '/renewal/images/product/',
    sections: [
      {
        key: 'visual',
        title: '네델란드 산양의 특별함',
        desc:
          '베비언스에서 직수입한 글로벌 프리미엄 산양조제식 카브리타를 만나보세요.'
      },
      {
        key: 'philosophy',
        title: '아기를 사랑하는 마음을 담아 산양을 사랑으로 키웁니다.',
        desc:
          '베비언스에서 직수입한 글로벌 프리미엄 산양조제식 카브리타를 만나보세요.'
      },
      {
        key: 'nutrition',
        title: '국내는 물론 EU 기준을 만족하는 영양설계',
        desc:
          '베비언스에서 직수입한 글로벌 프리미엄 산양조제식 카브리타를 만나보세요.'
      },
      {
        key: 'technology',
        title: '80년 역사의 축척된 기술 노하우',
        desc:
          '80년의 기술 노하우로, 미국/캐나다/유럽 등 전세계 28개국에서 사랑 받는 Kabrita'
      },
      {
        key: 'management-system',
        content: { type: 'slide', listNum: 2 },
        title: '체계적인 산양관리 "퀄리고트" 시스템',
        desc:
          '네덜란드 농림부의 품질 보장을 받은 "퀄리고트" 인증  목장의 프리미엄 산양 원유'
      },
      {
        key: 'kabrita-goat',
        content: { type: 'slide', listNum: 3 },
        contentPc: { type: 'slide', listNum: 3 },
        title: 'Kabrita 전용목장에서 사랑으로 기르는 산양',
        desc:
          '온 가족이 함께 사랑으로 관리해 어린 자녀의 주식이 되기도 하는 Kabrita'
      },
      {
        key: 'kabrita-origin',
        title: '카브리타를 경험하세요!',
        desc:
          '80년의 노하우와 사랑으로 탄생한 네덜란드 퀄리고트 산양원유를 지금 만나보세요!'
      }
    ]
  },
  created: function() {
    this.imgUrlPrefix += deviceKind;
    $('#nav__bottom li').eq(1).addClass('on');
  },
  mounted: function() {
    var self = this;
    this.$nextTick(function() {
      self.setSlide();
      scrennScroll.init({});
    });
  },
  methods: {
    setSlide: function() {
      $('.section-slider').slick({
        centerMode: true,
        centerPadding: '17%',
        arrows: true,
        focusOnSelect: true,
        speed: 300,
        //autoplay: true,
        autoplaySpeed: 3000,
        touchThreshold:10000,
       // slidesToShow: 3,
      });
    }
  }
});

window.onload = function() {
  app.loading = false;  
}
$(function() {
  
});


function startMov() {
  $('.mov-cover').hide();
  productYoutubePlayer.playVideo();
}