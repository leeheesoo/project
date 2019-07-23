// 팝업 여닫기
function openPop(flag) {
    var $scrollTop = $(document).scrollTop()+50;
    $('.pop-dim').show();
    $('#' + flag).show().css('top', '100px');
}
function closePop(flag) {
    $('.pop-dim').hide();
    $('#' + flag).hide();
}

function listResize() {
    m_size_img();
    window.parent.postMessage($('.wrap').height(), "*");
}
listResize();

$(window).resize(function () {
    listResize();
});

$(window).load(function () {
    listResize();
});



//// 유튜브제어 ================================================================
var tag = document.createElement('script');
tag.src = "https://www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

var youtube_v;

function onYouTubeIframeAPIReady() {
    youtube_v = new YT.Player('youtube', {
        height: '360',
        width: '980',
        videoId: '-o11S_tYxPk',
        playerVars: {
            'autoplay': 0,
            'controls': 1,
            'rel': 0, //추천영상표시안하기
            'showinfo': 0, //영상타이틀,정보표시여부
            'wmode': 'opaque'
        },
        events: {
            'onReady': function (event) {
                event.target.setPlaybackQuality('hd1080');
            }
        }
    });

} //onYouTubeIframeAPIReady




////모바일 사이즈일때 모바일이미지로 변경
function m_size_img() {
    var w = $('.wrap').width();
    var $event_top = $('.bag-kv > img');
    var $award = $('.event-award > img');
    var $file_text = $('.ticket-right > img');
    var $step_list = $('.step-list > img');
    var $footer = $('.note-wrap > img');
    var imgSrc = '/Content/images/bag-to-the-future/';

    if (w <= 640) {
        $event_top.attr('src', imgSrc + 'm_kv.jpg');
        $award.attr('src', imgSrc + 'm_award.jpg');
        $file_text.attr('src', imgSrc + 'm_wirte_note.png');
        $step_list.attr('src', imgSrc + 'm_event_step.png');

        $footer.attr('src', imgSrc + 'm_note.png');
    } else {
        $event_top.attr('src', imgSrc + 'kv.jpg');
        $award.attr('src', imgSrc + 'award.jpg');
        $file_text.attr('src', imgSrc + 'wirte_note.png');
        $step_list.attr('src', imgSrc + 'event_step.png');
        $footer.attr('src', imgSrc + 'note.png');
    }

}