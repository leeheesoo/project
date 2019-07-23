$(function () {
    // 동의하지 않음 체크시 alert
    $("#disagreePolicy01").bind('change, click', function () {
        if ($("#disagreePolicy01").is(":checked")) {
            alert('개인(신용)정보 수집·이용에 동의하지 않으시면 이벤트 참여가 불가합니다.');
        }
    });
    $("#disagreePolicy02").bind('change, click', function () {
        if ($("#disagreePolicy02").is(":checked")) {
            alert('개인(신용)정보 제공에 동의하지 않으시면 이벤트 참여가 불가합니다.');
        }
    });

    // 연락방식 선택 제어
    var $selectContactAll = $('#selectContactAll');
    $selectContactAll.change(function () {
        var $this = $(this);
        var checked = $this.prop('checked'); // checked (true, false)
        $('.input__contact-method').prop('checked', checked);
    });

    var $inputContactMethods = $('.input__contact-method');
    $inputContactMethods.change(function () {
        var boxLength = $inputContactMethods.length;
        var checkedLength = $('.input__contact-method:checked').length;
        var selectAll = (boxLength == checkedLength);

        $selectContactAll.prop('checked', selectAll);
    });
});

//---------------------------------------------------
// openPop / closePop
//---------------------------------------------------
function openPopup(flag, dimmedBoolean) {
    var scrollTop = $(window).scrollTop() + 50;
    var $selector = $('#popup' + flag);
    var dim = dimmedBoolean;

    if (dimmedBoolean != false) $("#popupDimm").show();
    $selector.css('top', scrollTop + 'px').show();
};

function closePopup(flag, dimmedBoolean) {
    var dim = dimmedBoolean;
    if (dimmedBoolean != false) $("#popupDimm").hide();
    $('#popup' + flag).hide();

};