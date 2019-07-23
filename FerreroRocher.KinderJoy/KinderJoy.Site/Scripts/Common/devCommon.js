//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Validation Check 코드
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Mobile Check
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
var isMobile = {
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
        return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS());
    }
};

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//우편번호검색
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function openDaumPostCode(eventId) {
    $('.pop-post').addClass('on');
    new daum.Postcode({
        oncomplete: function (data) {
            // 검색결과 항목을 클릭했을때 실행할 코드를 작성하는 부분.

            // 각 주소의 노출 규칙에 따라 주소를 조합한다.
            // 내려오는 변수가 값이 없는 경우엔 공백('')값을 가지므로, 이를 참고하여 분기 한다.
            var fullAddr = data.address; // 최종 주소 변수
            var extraAddr = ''; // 조합형 주소 변수

            // 기본 주소가 도로명 타입일때 조합한다.
            if (data.addressType === 'R') {
                //법정동명이 있을 경우 추가한다.
                if (data.bname !== '') {
                    extraAddr += data.bname;
                }
                // 건물명이 있을 경우 추가한다.
                if (data.buildingName !== '') {
                    extraAddr += (extraAddr !== '' ? ', ' + data.buildingName : data.buildingName);
                }
                // 조합형주소의 유무에 따라 양쪽에 괄호를 추가하여 최종 주소를 만든다.
                fullAddr += (extraAddr !== '' ? ' (' + extraAddr + ')' : '');
            }

            // 우편번호와 주소 정보를 해당 필드에 넣는다.
            $('#'+eventId+'_ZipCode').val(data.zonecode); //5자리 새우편번호 사용
            $('#' + eventId + '_Address1').val(fullAddr);

            // 커서를 상세주소 필드로 이동한다.
            $('#' + eventId + '_Address2').focus();

            // iframe을 넣은 element를 안보이게 한다.
            $('.pop-post').removeClass('on');
        },
        /*
        // 우편번호 찾기 화면 크기가 조정되었을때 실행할 코드를 작성하는 부분. iframe을 넣은 element의 높이값을 조정한다.
        onresize: function (size) {
            element_wrap.style.height = size.height + 'px';
        },
        */
        width: '600px',
        height: '640px'
    }).embed(document.getElementById('searchPostCode'));
    return false;
}



function lab(depth) {
    if (depth == null) {
        depth = 1;
    }
    var url = "https://vtag35.midas-i.com/vat-tag?cmp_no=3585&depth=" + depth    
    $.ajax({
        url: url,
        dataType: "jsonp",
        async: true,
        timeout: 500,
        success: function (data) {
            //console.log("succ" + depth)
        },
        error: function (e) {
            //console.log("fail")
        }
    });
    return false;
}