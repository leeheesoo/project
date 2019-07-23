$(document).ready(function() {
  // init slick
  $('.photo-slider').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: true,
    fade: true
  });

  // click e
  $('a[data-slide]').click(function(e) {
    e.preventDefault();
    var slideno = $(this).data('slide');
    //console.log(slideno)
    $('.photo-slider').slick('slickGoTo', slideno);
  });

  // before change
  $('.photo-slider').on('beforeChange', function(
    event,
    slick,
    currentSlide,
    nextSlide
  ) {
    // console.log(nextSlide);
    $('.slider-idx').text(nextSlide + 1); //mobile

    $('a[data-slide]').removeClass('slick-current');
    $('a[data-slide=' + nextSlide + ']').addClass('slick-current');
  });

  // add / hanb / 20181017 / 모바일에서는 화보 한장한장 다운로드 가능하도록 구현하기 위해
  $('.photo-slider').on('afterChange', function(e, slick, currentSlide) {
    setImgUrl(currentSlide + 1);
  });

  // set download image url
  setImgUrl(1);
});

function setImgUrl(imgNum) {
  var tgImgUrl = 'https://s3.ap-northeast-2.amazonaws.com/cetaphil-event/static/body/images/popup-m/photo0' + imgNum + '.jpg';
  $('#btn-down-gallery').attr('href', tgImgUrl);
}

//slick 재생성
function sliderSetting() {
  var $photoSlider = $('.slick-slider');
  $photoSlider.slick('slickGoTo', 0); // 순서 reset
  setTimeout(function() {
    $photoSlider.slick('refresh');
  }, 100);
}
