
$(function () {
    if ($.cookie("IsAuth") == "true") {
        $('.popup').hide();
    }
});