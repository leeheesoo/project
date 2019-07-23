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

function chkTextLength(txt, maxLength, msg) {
    var idx = 0;
    var currentIdx = 0;
    for (i = 0; i < txt.value.length; i++) {
        var checkOneChar = txt.value.charAt(i);   //한글자 추출
        if (escape(checkOneChar).length > 4) {
            idx += 2;   //한글이면 2를 더한다
        } else {
            idx++;     //한글아니면 1을 다한다
        }

        if (idx <= maxLength) {
            currentIdx = i + 1;
        }
    }
    if (idx > maxLength) {
        alert(msg);
        txt.value = txt.value.substr(0, currentIdx);
    }
    return;
}
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

var isInapp = (navigator.userAgent.toUpperCase().match(/KAKAOTALK|KAKAOSTORY|KAKAO|TWITTER|FB|NAVER|CriOS/i) ? true : false);