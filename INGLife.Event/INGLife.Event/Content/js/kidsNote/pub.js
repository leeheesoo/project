
// ==================================================
// popup
// ==================================================
function openPop(flag, dimmedBoolean) {
    var scrollTop = $(window).scrollTop();
    var $selector = $('.pop-' + flag);
    var dim = dimmedBoolean; // 생략이 기본, false일 경우 dim을 사용하지 않는다
    if (dimmedBoolean != false) $("#dimmed").show();
    $selector.css('top', scrollTop + 'px').show().focus();
    // dimmed 제어
    if (flag == 'agree01' || flag == 'agree02' || flag == 'post') {
        $('#dimmed').css('z-index', '99');
    }
};
function closePop(flag, dimmedBoolean) {
    var dim = dimmedBoolean; // 생략이 기본, false일 경우 dim을 사용하지 않는다
    if (dimmedBoolean != false) $("#dimmed").hide();
    $('#pop-' + flag).hide();
    //dimmed 제어
    if (flag == 'agree01' || flag == 'agree02' || flag == 'post') {
        $('#dimmed').css('z-index', 0);
    }
};


// ==================================================
// 휴대폰인증란 클릭X
// =================================================  
function close() {
    alert('휴대폰 인증을 진행해 주세요');
}



$(function () {
    // ==================================================
    // 페이지 탭 제어
    // =================================================
    $('.tab-btns a').click(function () {

        var $tabBtn = $('.tab-btns a');
        var num = $(this).index();
        var $tabView = $('.tab-view div');

        $tabBtn.removeClass('on');
        $tabView.hide();
        $(this).addClass('on');
        $tabView.eq(num).show();

    });

    // ==================================================
    // 동의함 체크시 팝업open
    // =================================================   
    $("#disagree01").bind('change, click', function () {
        if ($("#disagree01").is(":checked")) {
            alert('개인(신용)정보 수집·이용·제공에 동의하지 않으시면 이벤트 참여가 불가합니다.');
        }
    });
    //$("#agree02").change(function () {
    //    if ($("#agree02").is(":checked")) {
    //        openPop('agree02');
    //    }
    //});

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

    //전체선택
    var $chkAll = $('input[type=checkbox]#all');
        $chkAll.change(function () {
        var $this = $(this);
        var checked = $this.prop('checked'); // checked (true, false)
        // console.log(checked);
        $('input.chkbox').prop('checked', checked);
    });

    var boxes = $('input.chkbox');
        boxes.change(function () {

        var boxLength = boxes.length;
        var checkedLength = $('input.chkbox:checked').length;
        var selectAll = (boxLength == checkedLength);

        $chkAll.prop('checked', selectAll);
    });


    // 주소,이메일 제어
    $('input[type="checkbox"]').change(function () {
        var $email = $('.email-wrap');
            $email.hide();

        var checkValues = $('input[type=checkbox]:checked').map(function (i, e) {
            // i는 몇 개가 선택 되었는지 체크하고 있음
            // e는 클릭하는 요소의 값을 가져오고 있음
            var chk = e.id;
            if (chk === 'email') {
                $email.show();
            }  
            return e.value;
        }).get();
    });

    // ==================================================
    // 이벤트 완료시 전화체크 확인
    // =================================================
    //$('.complete').click(function () {
    //    var phoneChk = $('#phone').is(":checked");
    //    var smsChk = $('#sms').is(":checked");
    //    var phonSmsChk = $('#sms').is(":checked") && $('#phone').is(":checked");

    //    1차
    //    if (phoneChk == false) {
    //        alert('전화 수신 동의를 하셔야\n미아방지키트 전달이 가능합니다'); //1차
    //    }

    //    2차
    //    if (phonSmsChk == false) {
    //        console.log('둘다체크안뎀')
    //        alert('전화 ㆍ문자 수신 동의를 하셔야\n이벤트 참여가 가능합니다.'); //2차          
    //    }
        
    //});


    // ==================================================
    // placeholder
    // =================================================
    //$('.name-line input').placeholder({
    //    type: 'background',
    //    background: '/Content/Images/RezhineComics/form/bg-name.png'
    //});

    //$('.phone input').placeholder({
    //    type: 'background',
    //    background: '/Content/Images/RezhineComics/form/bg-phone.png'
    //});

    //$('.birth input').placeholder({
    //  type: 'background',
    //background: '/Content/Images/RezhineComics/form/bg-birth.png'
    //});
}); //function