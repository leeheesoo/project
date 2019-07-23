var eventConWidth = $('.event-container').width();

$(function () {
    init();
});


$(window).resize(function () {
    eventConWidth = $('.event-container').width();
    init();
});

function init() {
    setIamgeUrl();
    pop_center();
    // iframe resize 위해 필요한 script
    setTimeout(function () {
        window.parent.postMessage($('.event-container').height(), "*");
    }, 500);
};



// ==================================================
// popup
// ==================================================
function openPop(flag, dimmedBoolean) {
    //var scrollTop = $(window).scrollTop();
    var $selector = $('.pop-' + flag);
    var dim = dimmedBoolean; // 생략이 기본, false일 경우 dim을 사용하지 않는다
    if (dimmedBoolean != false) $("#dimmed").show();
    $selector.css('top', '17%').show().focus();
};
function closePop(flag, dimmedBoolean) {
    var dim = dimmedBoolean; // 생략이 기본, false일 경우 dim을 사용하지 않는다
    if (dimmedBoolean != false) $("#dimmed").hide();
    $('#pop-' + flag).hide();
};

//팝업 중앙 위치 설정
function pop_center() {
    var $wrap = $('event-container');
    var $popup = $('.pop-win');

    $popup.each(function () {
        $(this).css('margin-left', ($(this).width() / 2 * -1) + 'px')
    });
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
};


// ==================================================
// roulette
// ==================================================
//var winCode = 5; //0~5 (시계 방향)

//var pArr = ["라인프렌즈 콜라보 캐리어", "20만원쿠폰", "키즈백팩", "스타벅스 기프티콘", "1만원쿠폰", "쌤소나이트 정품가방"];
var $buttonImg = $('.btn-start img');
    
    //$('.btn-start').click(function () {
    //    rotation();
    //});

    function reset() {
        $(".roulette").rotate({
            angle: 0,
            center: ["50%", "50%"]
        });
    };

    // 클릭제어
    var doubleSubmitFlag = false;

    function doubleSubmitCheck() {
        if (doubleSubmitFlag) {
            return doubleSubmitFlag;
        } else {
            doubleSubmitFlag = true;
            return false;
        }
    };

    // 룰렛돌리기
    function rotation(winCode) {
        if (doubleSubmitCheck()) return; // 클릭막기

        $buttonImg.attr('src', '/Content/images/two-year-anniversary/btn-start.png');

        var winnerSpot = 60 * winCode;
        //var stopAt = (winnerSpot + Math.floor((Math.random() * winCode)));
        var stopAt = winnerSpot;

        $(".roulette").rotate({
            angle: 0,
            animateTo: -(360 * 5 + stopAt),
            center: ["50%", "50%"],
            easing: $.easing.easeOutQuart,
            callback: function () {
                var n = $(this).getRotateAngle();
                endAnimate(n);
                $buttonImg.attr('src', '/Content/images/two-year-anniversary/btn-start.gif');
                doubleSubmitFlag = false;//클릭 해제
            },
            duration: 5000
        });
    };

    // 룰렛완료 후
    function endAnimate($n) {
        var n = $n;

        var real_angle = n % 360;
        var part = -(Math.floor(real_angle / 60));
        //var winNum = part + 1
        //var imgUrl = '/Content/images/two-year-anniversary/popup/win-';
        // console.log(winNum);
        //$('.win-gift > img').attr('src', imgUrl + winNum + '.jpg');
        //$('.win-note > img').attr('src', imgUrl + 'note-' + winNum + '.jpg');
        openPop('win');

        //reset();
    };