var participationSlider;

$(function () {
    participationSlider = $('#participationSlider').bxSlider({
        auto: true,
        autoControls: true,
        pager: false,
        pause: 2500,
    });

    // popup : 개인정보 입력 placeholder
    $('#Mobile').placeholder({
        type: 'background',
        background: '/Images/MagicKinderAppLaunchingEvent/pop/ph_mobile.png'
    });
    $('#Mobile').css('background-position', '10px 15px');

    // popup: pop-app-detail 탭변경
    changeTab();
})

// 레이어팝업 여닫기
function openPop(flag) {
    var $scrollTop = $(window).scrollTop();
    $('#pop-' + flag).css('top', $scrollTop + 30 + 'px' ).show();

    if (flag == 'post') {
        $('#pop-entry').hide();
    } else {
        $('#png-bg').show();
    }
};
function closePop(flag) {
    $('#pop-' + flag).hide();

    if (flag == 'post') {
        $('#pop-entry').show();
    } else {
        $('#png-bg').hide();
    }
}

// 리뷰팝업 여닫기
function openReview() {
    $("#pop-review").fadeIn();
}
function closeReview() {
    $("#pop-review").fadeOut();
}

function changeTab() {
    var $tabAreaList = $(".tab-area ul li");
    var $contentsAreaList = $(".contents-area ul li");
    var $this;

    $tabAreaList.on("click", function () {
        $this = $(this);
        var index = $(this).index();

        // tab init
        initTab();
        // tab active
        activeTab($this, index);
    })

    function initTab() {
        $tabAreaList.removeClass("active");
        $contentsAreaList.removeClass("active");
    }

    function activeTab($this, index) {
        $this.addClass("active");
        $contentsAreaList.eq(index).addClass("active");
    }
}