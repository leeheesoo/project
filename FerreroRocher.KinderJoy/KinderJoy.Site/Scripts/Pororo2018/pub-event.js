
$(document).ready(function () {
    var $videoCover = $('.play-cover');
    $videoCover.click(function () {
        alert('이벤트가 종료되었습니다.\n전체 영상 보러 가기 버튼을 눌러주세요.')
    });
});
$(window).load(function () {
    var $eventTitle = $('.kv-inner h3');
    var $kvRino = $('.rino');
    $eventTitle.addClass('scale-in-hor-center');
    $kvRino.addClass('bounce');

});



//---------------------------------------------------
// wow
//---------------------------------------------------
wow = new WOW(
{
    boxClass: 'wow',      // default
    animateClass: 'animated', // default
    offset: 250,          // default
    mobile: true,       // default
    live: true        // default
}
)
wow.init();






