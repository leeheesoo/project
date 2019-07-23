// 팝업 여닫기
function openPop(flag) {
    $('.pop-dim').show();
    var $scrollTop = $(window).scrollTop()+20;
    $('#' + flag).show().css('top', $scrollTop);
}
function closePop(flag) {
    $('.pop-dim').hide();
    $('#' + flag).hide();
}

function listResize() {
    list_height();
    list_height_m();
    pop_center();
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


//오행시list 높이값 설정
function list_height() {
    var list_box_h = $('.text-wrap li:first-child img').height();
    var $list_box = $('.text-wrap > ul > li');

    $list_box.css('height', list_box_h);
}
//오행시list 모바일사이즈일때 높이값 설정
function list_height_m() {
    var list_box_h = $('.text-wrap li:first-child img').height();
    var $list_box = $('.text-wrap > ul > li:not(.ex-img)');
    var w = $('.wrap').width();
    if (w <= 640) {
        $list_box.css('height', list_box_h + 160);
    }

}
//모바일 사이즈일때 모바일이미지로 변경
function m_size_img() {
    var w = $('.wrap').width();
    var $footer = $('.event-note > img');
    var $event_top = $('.event-top > img');
    var $text_ex = $('.ex-img > img');
    var $main_btn = $('.btn-register');
    var $popup = $('.popup');

    if (w <= 640) {
        $event_top.attr('src', '/Content/images/one-year-anniversary/m_bg-event02.jpg');       
        $footer.attr('src', '/Content/images/one-year-anniversary/m_event-note.png');
        $main_btn.css({ 'width': '60%', 'margin-left': '-30%', 'top': '36%' });
        $text_ex.attr('src', '/Content/images/one-year-anniversary/m_text-ex.jpg');
        $popup.css('marginTop', '300px');
    } else {
        $event_top.attr('src', '/Content/images/one-year-anniversary/bg-event02.jpg');
        $text_ex.attr('src', '/Content/images/one-year-anniversary/text-ex.jpg');
        $footer.attr('src', '/Content/images/one-year-anniversary/event-note.png');
        $main_btn.css({ 'width': '45%', 'margin-left': '-22.5%', 'top': '50%' });
        $popup.css('marginTop', '0');
    }

    //console.log(w)

}



//팝업 중앙 위치 설정
function pop_center() {
    var $wrap = $('.wrap');
    var $popup = $('.popup , .popup2');

    $popup.each(function () {
        $(this).css('margin-left',($(this).width()/2*-1)+'px')
    });
}


//$('.end-wrap').css('background', 'rgba(255,255,255,0.7)')