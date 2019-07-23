
// ==================================================
// popup
// ==================================================
function openPop(flag, dimmedBoolean) {
	var scrollTop = $(window).scrollTop();
	var $selector = $('.pop-' + flag);
	var dim = dimmedBoolean; // 생략이 기본, false일 경우 dim을 사용하지 않는다
	if (dimmedBoolean != false) $("#dimmed").show();
	$selector.css('top', scrollTop).show();

};
function closePop(flag, dimmedBoolean) {
	var dim = dimmedBoolean; // 생략이 기본, false일 경우 dim을 사용하지 않는다
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
document.documentElement.addEventListener('touchstart', function(event) {
	if (event.touches.length > 1) {
		event.preventDefault();
	}
}, false);


$(function () {
	// ==================================================
	// 페이지 탭 제어
	// =================================================

	var $emoticon = $('.emoticon-select label');
			$emoticon.click(function () {

		var $tabBtn = $('.tab-btns a');
		var num = $(this).index() - 1;
		var $emoticonView = $('.emoticon-view > img');

		$emoticonView.hide();
		$emoticonView.eq(num).show();

	});

	// ==================================================
	// 동의함 체크시 팝업open
	// =================================================   
	var $agree01 = $("#agree01");
	var $agree02 = $("#agree02");
	var $disagree01 = $("#disagree01");
	var $disagree02 = $("#disagree02");

	//동의함 체크시 팝업open
	$agree01.bind('change , click', function () {
		closePop('personal');
		openPop('agree01');
	});

	$agree02.bind('change , click', function () {
		closePop('personal');
		openPop('agree02');
	});

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

	var boxes = $('.chkbox');
			boxes.change(function () {

				var boxLength = boxes.length;
				var checkedLength = $('input.chkbox:checked').length;
				var selectAll = (boxLength == checkedLength);

				$chkAll.prop('checked', selectAll);
			});

	// ==================================================
	// 이모티콘받기 활성화 제어
	// ==================================================
	//var $btnPlus = $('.btn-plus');
	//var $btnMarketing = $('.btn-marketing');

	//$btnPlus.click(function () {
	//	$btnMarketing.addClass('on');
	//});
	//$btnMarketing.click(function () {
	//	if ($(this).hasClass('on')) {
	//		openPop('personal');
	//	} else {
	//		alert('플러스친구를 추가를 해주세요');
	//	}
	//});


}); //function


