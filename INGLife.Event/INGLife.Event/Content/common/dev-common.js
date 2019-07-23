var isMobile = navigator.userAgent.match(/iPhone|iPod|Android|Windows CE|BlackBerry|Symbian|Windows Phone|webOS|Opera Mini|Opera Mobi|POLARIS|IEMobile|lgtelecom|nokia|SonyEricsson/i) != null || navigator.userAgent.match(/LG|SAMSUNG|Samsung/) ? true : false;

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

var element_layer = $('#layer');
function closeDaumPostcode() {
    element_layer.hide();
}

$(function () {
    //구글애널리틱스
    $(document).on('click', '.ga-event', function (e) {
        var $this = $(this);
        var category = googleAnalyticCategory || $this.attr('data-category');
        var action = $this.attr('data-action');
        var delay = $this.attr('data-delay');

        ga('send', 'event', category, action);

        if (delay && !$this.attr('target')) {
            delay = parseInt(delay);
            e.preventDefault();
            var o = this;
            setTimeout(function () {
                document.location.href = o.href;
            }, delay);
        }
    });
});

function postcodePopOpen(zipcode, address, addressDetail, width, height) {
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
            $('#' + zipcode).val(data.zonecode); //5자리 새우편번호 사용
            $('#' + address).val(fullAddr);
            $('#' + addressDetail).focus();
            // iframe을 넣은 element를 안보이게 한다.
            // (autoClose:false 기능을 이용한다면, 아래 코드를 제거해야 화면에서 사라지지 않는다.)
            closeDaumPostcode();
        },
        width: width,
        height: height,
        maxSuggestItems: 5
    }).embed(element_layer[0]);

    // iframe을 넣은 element를 보이게 한다.
    element_layer.show();
}