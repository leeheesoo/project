//---------------------------------------------------
// [device check.js]
// util
// 웹, 모바일 구분, 모바일일 경우 os 구분
//---------------------------------------------------
// usage
// var device = getDeviceName();
// var mobileKind = getMobileKind();
//---------------------------------------------------

function getDeviceName() {
    var device = navigator.userAgent.match(/Android|Mobile|iP(hone|od|ad)|BlackBerry|IEMobile|Kindle|NetFront|Silk-Accelerated|(hpw|web)OS|Fennec|Minimo|Opera M(obi|ini)|Blazer|Dolfin|Dolphin|Skyfire|Zune/);
    if (!device) {
        return 'w';
    } else {
        return 'm';        
    }
}

// ios, android check
function getMobileKind() {
    var userAgent = navigator.userAgent || navigator.vendor || window.opera;
    if (/iPad|iPhone|iPod/.test(userAgent) && !window.MSStream) {
        return 'ios';
    } else {
        return 'android';
    }
}

var isMobile = {
    Android: function () {
        return navigator.userAgent.match(/Android/i) ? true: false;
    },
    BlackBerry: function () {
        return navigator.userAgent.match(/BlackBerry/i) ? true: false;
    },
    iOS: function () {
        return navigator.userAgent.match(/iPhone|iPad|iPod/i) ? true: false;
    },
    Windows: function () {
        return navigator.userAgent.match(/IEMobile/i) ? true: false;
    },
    any: function () {
        return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS());
    }
};