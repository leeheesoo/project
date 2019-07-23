var scrollTop = window.parent.postMessage($(window).scrollTop(), "*"); 
var mobDevice;
var eventConWidth = $('.event-container').width();

$(function () {
    popCtrl();
    eventUrlCopy();
    init();
});


$(window).resize(function () {
    eventConWidth = $('.event-container').width();
    init();
})

function init() {
    setIamgeUrl();

    // iframe resize 위해 필요한 script
    setTimeout(function () {
        window.parent.postMessage($('.event-container').height(), "*");
    }, 500);
}

//*==========================
// popup control
//*--------------------------
function popCtrl() {
    // popup : 개인정보 입력 placeholder
    setPlaceHolder('.entry-phone', 'center center');

    $('#Agree').change(function () {
        var $this = $(this);
        var $iconImg = $this.parent().find('#imgAgree img');
        if ($this.is(':checked')) {
            $iconImg.attr('src', '/Content/images/christmas2017/agree_policy1_on.jpg');
        } else {
            $iconImg.attr('src', '/Content/images/christmas2017/agree_policy1_off.jpg');
        }
    });
}

function setPlaceHolder(stEle, stPos) {
    var $ele = $(stEle);
    $ele.placeholder({
        type: 'background',
        background: '/Content/images/christmas2017/ph_img.png'
    });
    $ele.css('background-position', stPos);
}


//*==========================
// 레이어팝업 여닫기
//*--------------------------
function openPop(flag, num) {
     $('#pop-' + flag).fadeIn().focus();
     $('#dimmed').fadeIn();
};

function closePop(flag) {
    $('#pop-' + flag).fadeOut();
    $('#dimmed').fadeOut();
};


//*==========================
// URL 복사
//*--------------------------
function eventUrlCopy() {
    $('#btnCopy').attr('data-clipboard-text', 'https://www.samsonitemall.co.kr/17_Christmas?utm_source=Promotion&utm_campaign=17_Christmas&utm_medium=Share');
    var clipboard = new Clipboard('#btnCopy');
    clipboard.on('success', function (e) {
        alert('쌤소나이트 크리스마스 이벤트 URL이 복사 되었습니다.\n친구들에게 공유해 주세요!');
    });
    clipboard.on('error', function (e) {
        console.log(e);
    });
}


//*==========================
// Progress bar control
//*--------------------------
function setProductProgress(prod1Total, prod2Total, prod3Total, prod4Total) {
    var progressArray = [$('#dipsProgressBar1'), $('#dipsProgressBar2'), $('#dipsProgressBar3'), $('#dipsProgressBar4')];
    var setPercent = [prod1Total, prod2Total, prod3Total, prod4Total];
    for (var i = 0; i < progressArray.length; i++) {
        progressArray[i].css('width', setPercent[i] + '%');
    }
}


//*==========================
// W/M 이미지 경로 변경
//*--------------------------
function setIamgeUrl() {
    var dirW = '/w/';
    var dirM = '/m/';
    if (eventConWidth <= 692) {
        $('.event-container img').each(function () {
            this.src = this.src.replace(dirW, dirM);
        });
    } else {
        $('.event-container img').each(function () {
            this.src = this.src.replace(dirM, dirW);
        });
    }
}