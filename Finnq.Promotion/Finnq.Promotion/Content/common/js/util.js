$.validator.setDefaults({
    onkeyup: false,
    onclick: false,
    onfocusout: false,
    showErrors: function (errorMap, errorList) {
        if (this.numberOfInvalids() && errorList != '') { // 에러가 있을 때만..
            alert(errorList[0].message);
            $(errorList[0].element).focus();
        }
    }
});
var checkMobile = {
    Android: function () {
        return navigator.userAgent.match(/Android/i) ? true : false;
    },
    BlackBerry: function () {
        return navigator.userAgent.match(/BlackBerry/i) ? true : false;
    },
    iOS: function () {
        return navigator.userAgent.match(/iPhone|iPad|iPod/i) ? true : false;
    },
    Windows: function () {
        return navigator.userAgent.match(/IEMobile/i) ? true : false;
    },
    any: function () {
        return (checkMobile.Android() || checkMobile.BlackBerry() || checkMobile.iOS());
    }
};
var isInapp = (navigator.userAgent.toUpperCase().match(/DAUM|KAKAOTALK|KAKAOSTORY|KAKAO|TWITTER|FB|NAVER|CriOS/i) ? true : false);