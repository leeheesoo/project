
//---------------------------------------------------
// openPop / closePop
//---------------------------------------------------

var dim = $('#dimmed');
function openPop(flag, dimmedBoolean) {
    var scrollTop = $(window).scrollTop();
    var $selector = $('#pop-' + flag);
    var dim = dimmedBoolean;

    if (dimmedBoolean != false) $("#dimmed").show();
	//$selector.css('top', scrollTop + 'px').show();
    $selector.css('top', 0).show();
    window.scrollTo(0, 0);
};

function closePop(flag, dimmedBoolean) {
    var dim = dimmedBoolean;
    if (dimmedBoolean != false) $("#dimmed").hide();
    $('#pop-' + flag).hide();

};


// ==================================================
// 휴대폰인증란 클릭X
// =================================================  
function close() {
    alert('휴대폰 인증을 진행해 주세요');
}


$(function () {

    // ==================================================
    // 동의함 체크시 팝업open
    // ================================================= 
    var $MarketingDisagree01 = $("#MarketingDisagree01");
    var $MarketingDisagree02 = $("#MarketingDisagree02");
    var $consultDisagree01 = $("#consultDisagree01");
    var $consultDisagree02 = $("#consultDisagree02");

    var msg01 = '개인(신용)정보 수집·이용 동의 시에만 이벤트 참여가 가능합니다.'
    var msg02 = '개인(신용)정보 제공 동의 시에만 이벤트 참여가 가능합니다.'

    $MarketingDisagree01.bind('change, click', function () {
        if ($(this).is(":checked")) {
            alert(msg01);
        }
    });
    $MarketingDisagree02.bind('change, click', function () {
        if ($(this).is(":checked")) {
            alert(msg02);
        }
    });
    $consultDisagree01.bind('change, click', function () {
        if ($(this).is(":checked")) {
            alert(msg01);
        }
    });
    $consultDisagree02.bind('change, click', function () {
        if ($(this).is(":checked")) {
            alert(msg02);
        }
    });

    // ==================================================
    // 이메일 select 제어
    // =================================================
    $('#emailSelect').change(function () {

        var $email02 = $("#email2");

        $("#emailSelect option:selected").each(function () {
            if ($(this).val() == '1') {
                //직접입력일 경우
                $email02.val(''); //값 초기화
                $email02.attr("readonly", false); //활성화
                $email02.focus()
            } else {
                //직접입력이 아닐경우
                $email02.val($(this).text()); //선택값 입력
                $email02.attr("readonly", true); //비활성화
            }
        });
    });



    // ==================================================
    // 연락방식 선택 제어
    // ==================================================

    // 전체선택
    var $chkAll = $('input[type=checkbox]#all');
        $chkAll.change(function () {
        var $this = $(this);
        var checked = $this.prop('checked'); // checked (true, false)
        // console.log(checked);
        $('input.chkbox').prop('checked', checked);
    });

	// 다른항목 모두선택시 전체선택 체크
    var $inputContactMethods = $('.input__contact-method');
    $inputContactMethods.change(function () {

        var boxLength = $inputContactMethods.length; // 연락방식 개수
        var checkedLength = $('.input__contact-method:checked').length; // 연락방식 선택 개수
        var selectAll = (boxLength == checkedLength);

        $chkAll.prop('checked', selectAll);
    });


    // 주소,이메일 제어
    $('input[type="checkbox"]').change(function () {
        var $email = $('.email-wrap');
        var $address = $('.address-wrap');
            $email.hide();
            $address.hide();

        var checkValues = $('input[type=checkbox]:checked').map(function (i, e) {
            // i는 몇 개가 선택 되었는지 체크하고 있음
            // e는 클릭하는 요소의 값을 가져오고 있음
            var chk = e.id;
            if (chk === 'email') {
                $email.show();
            } else if (chk === 'post'){
                $address.show();
            }  
            return e.value;
        }).get();
    });



}); //function