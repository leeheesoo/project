var $dim = $('.dim');
var number1;
var number2;
var numResult;
var $firstResult = $(".first-result-num");
var $numberInput = $('.number-input');
var $firstResultWrap = $('.first-result');


// 팝업 여닫기
function openPop(flag) {
    $dim.show();
    var $scrollTop = $(window).scrollTop() + 20;
    $('#' + flag).show().css('top', $scrollTop);
}
function closePop(flag) {
    $dim.hide();
    $('#' + flag).hide();
}

function nextBlank(N, Obj, nextID) {
    if (document.getElementById(Obj).value.length == N) {
        document.getElementById(nextID).focus();
    }
}

$(function () {

    //content의 input에 keyup이 발생하면 다음칸
    $("input[type=tel]").keyup(function (e) {
        if ($(this).val().length >= 1) {
            $(this).next().focus();
        }
    });


    //input 입력할 때 실행
    $numberInput.on("textchange", function (ele) {

        // 숫자만인지 체크하는 정규식
        regNumber = /^[0-9]*$/;
        if (!regNumber.test(ele.currentTarget.value)) {
            alert('숫자만 입력해주세요.');
            ele.currentTarget.value = '';
            return;
        }

        number1 = $(".num1").val() + $(".num2").val() + $(".num3").val() + $(".num4").val(); //첫번째 자리
        number2 = $(".num5").val() + $(".num6").val() + $(".num7").val() + $(".num8").val(); // 두번째 자리
        numResult = (isNaN(parseInt(number1)) ? 0 : parseInt(number1)) + (isNaN(parseInt(number2)) ? 0 : parseInt(number2)); //결과깂

        $firstResult.val(function () {
            $firstResult.text(numResult);
        });


        // 번호 다 채워졌을때 자릿수 체크
        var count = 0;

        $('input[type=tel]').each(function () {
            if ($(this).val() !== '') {
                count++;
            }
        });

        if (count === 8) {
            if (numResult < 9999) {
                $firstResultWrap.css('width', '740px');
            } else {
                $firstResultWrap.css('width', '900px');
            }
        }

        //input 값있을때 style 변경
        if ($(this).val() !== '') {
            $(this).css({ 'border': 'none', 'background-color': '#ffdf4f' });
        } else {
            $(this).css({ 'background-color': '#eee', 'border': '2px dashed #b1afb0' });
        }


    });


    //  placeholder
    $numberInput.placeholder({
        type: 'background',
        background: '../Content/tmap/images/icon_zero.png'
    });
    $numberInput.css('background-position', 'center');


});