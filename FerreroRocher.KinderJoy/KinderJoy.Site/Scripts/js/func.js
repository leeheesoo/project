function checkAge() {
    var $year = $("#year");
    var $month = $("#month");
    var $day = $("#day");

    if ($.isNumeric($year.val()) == false ) {
        alert("생년월일을 입력 하세요");
        $year.focus();
        return false;
    }

    if ($.isNumeric($month.val()) == false || $month.val() < 1 || $month.val() > 12) {
        alert("생년월일을 입력 하세요");
        $month.focus();
        return false;
    }

    if ($.isNumeric($day.val()) == false || $day.val() < 1 || $day.val() > 31) {
        alert("생년월일을 입력 하세요");
        $day.focus();
        return false;
    }
    var now = new Date();
    var nowDay = now.getDate();
    var nowMonth = now.getMonth()+1;
    var nowYear = now.getFullYear();
    
    var age = (nowYear - $year.val() );
    if (($month.val() > nowMonth) || ($month.val() == nowMonth && $day.val() > nowDay)) {
        age = age - 1;
    }
    if (age <= 12) {
        alert("만 12세 이상만 접속 가능합니다");
        return false;
    }
    $.cookie("IsAuth", true, cookie_option);
    $('.popup').hide();
    return false;
}
var cookie_option = {
    path :  "/"
}