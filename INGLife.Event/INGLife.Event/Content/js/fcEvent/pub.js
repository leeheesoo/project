// ==================================================
// popup
// ==================================================
var dim = $('#dimmed');
function openPop(flag, dimmedBoolean) {
	var scrollTop = $(window).scrollTop();
	var $selector = $('#pop-' + flag);
	var dim = dimmedBoolean;

	if (dimmedBoolean != false) $("#dimmed").show();
	//$selector.css('top', scrollTop + 'px').show();
	$selector.css('top', 0).show();
	window.scrollTo(0, 0);
};

function closePop(flag, dimmedBoolean) {
	var dim = dimmedBoolean;
	if (dimmedBoolean != false) $("#dimmed").hide();
	$('#pop-' + flag).hide();

};


// ==================================================
// 휴대폰인증란 클릭X
// =================================================  
function close() {
	alert('휴대폰 인증을 진행해 주세요');
}

// ==================================================
// ios zoom 막기
// ==================================================
document.documentElement.addEventListener('touchstart', function (event) {
	if (event.touches.length > 1) {
		event.preventDefault();
	}
}, false);


$(function () {


	// ==================================================
	// 동의함 체크시 팝업open
	// =================================================   
	var $agree01 = $("#agree01");
	var $agree02 = $("#agree02");
	var $disagree01 = $("#disagree01");
	var $disagree02 = $("#disagree02");

	//동의함 체크시 팝업open
	//$agree01.bind('change , click', function () {
	//	closePop('personal');
	//	openPop('agree01');
	//});

	//$agree02.bind('change , click', function () {
	//	closePop('personal');
	//	openPop('agree02');
	//});

	//동의하지않음 체크시 alert
	$disagree01.bind('change, click', function () {
		if ($(this).is(":checked")) {
			alert('개인(신용)정보 수집·이용 동의 시에만 이벤트 참여가 가능합니다.');
		}
	});
	$disagree02.bind('change, click', function () {
		if ($(this).is(":checked")) {
			alert('개인(신용)정보 제공 동의 시에만 이벤트 참여가 가능합니다.');
		}
	});

	//동의팝업 닫기누를시 동의함 체크 해제
	$('.pop-agree01 .btn-close').bind('change, click', function () {
		$agree01.prop('checked', false);
	});

	$('.pop-agree02 .btn-close').bind('change, click', function () {
		$agree02.prop('checked', false);
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
		var selectAll = (boxLength === checkedLength);

		$chkAll.prop('checked', selectAll);
	});


	// 주소,이메일 제어
	$('input[type="checkbox"]').change(function () {
		var $email = $('.email-wrap');
		var $address = $('.address-wrap');
		$email.hide();
		$address.hide();

		var checkValues = $('input[type=checkbox]:checked').map(function (i, e) {
			// i는 몇 개가 선택 되었는지 체크하고 있음
			// e는 클릭하는 요소의 값을 가져오고 있음
			var chk = e.id;
			if (chk === 'email') {
				$email.show();
			} else if (chk === 'post') {
				$address.show();
			}
			return e.value;
		}).get();
	});


	// ==================================================
	// 이메일 select 제어
	// =================================================
	$('#emailSelect').change(function () {

		var $email02 = $("#email2");

		$("#emailSelect option:selected").each(function () {
			if ($(this).val() === '1') {
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

}); //function
