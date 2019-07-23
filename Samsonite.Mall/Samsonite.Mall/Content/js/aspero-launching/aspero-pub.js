// 팝업 여닫기
function openPop(flag) {
    $('.pop-dim').show();
    $('#' + flag).show().css('top', '40%');
}
function closePop(flag) {
    $('.pop-dim').hide();
    $('#' + flag).hide();
}
//팝업 중앙 위치 설정
function pop_center() {
    var $wrap = $('.wrap');
    var $popup = $('.popup-insta');

    $popup.each(function () {
        $(this).css('margin-left', ($(this).width() / 2 * -1) + 'px')
    });
}

function listResize() {
    pop_center();
    m_size_img();
    window.parent.postMessage($('.wrap').height(), "*");
}
listResize();

$(window).resize(function () {
    listResize();
});



var $videoSound = $('.video-sound');
var $videoMute = $('.video-mute');

// 유튜브제어 ================================================================
var tag = document.createElement('script');
tag.src = "https://www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

var playerSound;

function onYouTubeIframeAPIReady() {
    //본영상
    playerSound = new YT.Player('player-sound', {
        height: '360',
        width: '980',
        videoId: 'WZqm_j03QK4',
        playerVars: {
            'autoplay': 0,
            'controls': 1,
            'rel': 0, //추천영상표시안하기
            'showinfo': 0, //영상타이틀,정보표시여부
            'wmode': 'opaque'
        },
        events: {
            'onStateChange': onPlayerStateChange,
            'onReady': function (event) {
                event.target.setPlaybackQuality('hd1080');
            }
        }
    });

} //onYouTubeIframeAPIReady


//종료시점 확인 후 실행
function onPlayerStateChange(event) {

    //동영상보기 완료
    if (event.data == YT.PlayerState.ENDED) {
        $('.coupon-ex-text').hide();
        $('#copy').slideDown(); //쿠폰보여주기
        playerSound.stopVideo();
        $videoSound.hide();
        $videoMute.show();
        $('.codecopy').click(function () {
            alert('쿠폰코드 복사가 완료되었습니다. 마이페이지에서 쿠폰 등록하고, 공식몰 혜택가로 구매하세요!');
        });
    }
}


//영상 커버 눌렀을때 재생
function startVideo() {
    $("#cover").on('click', function () {
        $videoMute.hide();
        $videoSound.show();
        playerSound.playVideo();
    })
}

$(function () {
    startVideo();
    //쿠폰코드복사
    window.onload = function () {
        $(document).ready(function () {
            var clipboard = new Clipboard('.codecopy');
        });
    };


})





//모바일 사이즈일때 모바일이미지로 변경
function m_size_img() {
    var w = $('.wrap').width();
    var $event_top = $('.aspro-kv > img');
    var $vd_cover = $('.vd-cover > img');
    var $coupon_note = $('.coupon-wrap > span > img');
    var $coupon_method = $('.coupon-method > img');
    var $event02 = $('.event02-wrap > img');
    var $event02_pr1 = $('.pr1');
    var $event02_pr2 = $('.pr2');
    var $event03 = $('.event03-wrap > img');
    var $footer = $('.event-note > img');
    var imgSrc = '/Content/images/aspero-launching/';

    if (w <= 640) {
        $event_top.attr('src', imgSrc +'m_kv.jpg');
        $vd_cover.attr('src', imgSrc + 'm_vd_cover.jpg');
        $coupon_note.attr('src', imgSrc + 'm_cupon_note.png');
        $coupon_method.attr('src', imgSrc + 'm_event_rull.png');
        $event02.attr('src', imgSrc + 'm_event02.jpg');
        $event02_pr1.attr('src', imgSrc + 'm_aspero.jpg');
        $event02_pr2.attr('src', imgSrc + 'm_liteshock.jpg');
        $event03.attr('src', imgSrc + 'm_event03_text.png');
        $footer.attr('src', imgSrc + 'm_event_note.jpg');
    } else {
        $event_top.attr('src', imgSrc + 'kv.jpg');
        $vd_cover.attr('src', imgSrc + 'vd_cover.jpg');
        $coupon_note.attr('src', imgSrc + 'cupon_note.png');
        $coupon_method.attr('src', imgSrc + 'event_rull.png');
        $event02.attr('src', imgSrc + 'event02.jpg');
        $event02_pr1.attr('src', imgSrc + 'aspero.jpg');
        $event02_pr2.attr('src', imgSrc + 'liteshock.jpg');
        $event03.attr('src', imgSrc + 'event03_text.png');
        $footer.attr('src', imgSrc + 'event_note.jpg');
    }

}