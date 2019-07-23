// =============================================================================
// 장난감 자세히 보기 팝업
// 다른 종류의 장난감이 추가 될경우 이미지는 boys / girls와 같은 방식으로 추가 후
// toyData.type 객체 안에 장난감 타입: 썸네일 이미지 갯수만 추가 할 것
// =============================================================================

var toyData = {
	$popup: null,
	$detail: null,
	$thumb: null,
	itemUrl: '/Images/toy/bn/items/',
	thumbUrl: '/Images/toy/bn/thumb/',
	prefix: '/thumb-',
	imgType: '.png',
	sliderObj: [],
	type: {
		girls: 8,
		boys: 8,
		common: 4
	}
};


// 장난감 디테일 팝업 썸네일 리스트 동적 생성
var setSliderItems = function (popupType) {
	var item = '';

	for (var i = 0; i < toyData.type[popupType]; i += 1) {

		toyData.$thumb.append('<li>' +
			'<button type="button" data-detail-num="' + (i + 1) + '">' +
			'<img src="' + toyData.thumbUrl + popupType + toyData.prefix + (i + 1) + toyData.imgType + '">' +
			'</button>' +
			'</li>');
	} // for
};

// 장난감 디테일 팝업 띄우기
var showDetail = function (popupType) {

	var wrapHeight = $('#wrap').height();
	var positionTop = 0;
	var $dim = $('#png_bg');
	var currentPopupName = 'popup_toys-' + popupType;

	$dim.show().css('height', wrapHeight);

	toyData.$popup.addClass(currentPopupName + ' is-active').animate({
		top: positionTop
	});

	$('html, body').animate({
		scrollTop: positionTop
	});

	toyData.$detail.find('img').attr('src', toyData.itemUrl + popupType + toyData.prefix + '0' + toyData.imgType);
};

// 장난감 팝업이 뜬 후 bxSlider를 호출해야 썸네일 리스트의 크기가 제대로
// 표현 됨
var setBxSlider = function (popupType) {

	// bxslider 옵션
	// 왼쪽과 오른쪽 옵션이 같으므로 하나의 옵션을 사용
	var sliderOpt = {
		mode: 'horizontal', // 가로 방향 수평 슬라이드
		speed: 500, // 이동 속도를 설정
		pager: false, // 현재 위치 페이징 표시 여부 설정
		moveSlides: 3, // 슬라이드 이동시 개수
		slideWidth: 140, // 슬라이드 너비
		minSlides: 3, // 최소 노출 개수
		maxSlides: 3, // 최대 노출 개수
		slideMargin: 0, // 슬라이드간의 간격15
		auto: false, // 자동 실행 여부
		// controls: false // 이전 다음 버튼 노출 여부
	};

	toyData.$thumb.bxSlider(sliderOpt);

	toyData.$thumb.on('click', 'button', function () {
		var $button = $(this);
		var idx = $button.data('detail-num');

		toyData.$detail.find('img').hide().attr('src', toyData.itemUrl + popupType + toyData.prefix + idx + toyData.imgType).show(200);
	});
};

// 닫기 버튼 눌렀을 경우 bxslider 삭제
var destroySlider = function () {
	$.each(toyData.sliderObj, function (idx, slider) {
		slider.destroySlider();
	});

	// 썸네일 슬라이더를 삭제하지 않으면 이전 보여진 장난감에 이어서 추가 됨
	toyData.$thumb.empty().off('click', 'button');

	toyData.$popup.attr('class', 'popup_toys');
	$('#png_bg').hide();
};


// 장난감 자세히 보기 팝업
// @param: 장난감 종류 (boys, girls)
var openToyDetailArticle = function (popupId) {
	toyData.$popup = $('.popup_toys');
	toyData.$detail = toyData.$popup.find('.js-toy-detail');
	toyData.$thumb = toyData.$popup.find('.js-thumb');

	setSliderItems(popupId);
	showDetail(popupId);
	setBxSlider(popupId);
};


//팝업 열기
function openPop(param) {
	var wrapH = $('.mobile_wrap').height();
	var wTop = $(window).scrollTop();
	var popupConfig = {
		top: wTop,
		bottom: 'auto'
	};

	//$('#png_bg').css('height', wrapH);
	$('#png_bg').show();
	$('#' + param).show();

	// 여아용 장난감 팝업 영역 벗어나지 않도록 처리
	// 2018.04.30 한승철 수정
	// if (wTop + 10 > 3980) {
	if (param === 'girls') {
		popupConfig = {
			top: 'auto',
			bottom: 0
		};
	}

	$('#' + param).css(popupConfig);

	$('html, body').animate({
		scrollTop: $('#' + param).offset().top
	}, 400);
	$('#png_bg').css("opacity", "0.6");
	if (param == 'popInapp') {
		$('#png_bg').css("opacity", "0.9");
	}

}
//팝업닫기
function closePop(param) {
	//$('html,body').animate({ scrollTop: 0 });
	//$("html").css("overflow-y", "hidden");
	$('#' + param).hide();
	$('#png_bg').hide();
	// var wrapH = $('.mobile_wrap').height();
	// $('.mobile_wrap').css("height", wrapH - 200);

		destroySlider();
		resetViewHeight();
}

/*
 * 2017.10.19
 * 한승철
 * 팝업 오픈 할때 화면 크기 조정
 */
function resizeVieHeight(id) {
	var popHeight = $('#' + id).height();
	// console.log(popHeight);
	$('body').css('height', popHeight);
} // resizeViewHeight


// quickMenu
// 2018.04.27
// 한승철
var quickMenu = function () {
	var $toyStick = $('.toy-stick');

	var clickQuickMenu = function () {
		var onClickLink = function (link) {

			var $link = $(link);
			var anchorTarget = $link.attr('href');
			var anchorLoc;

			anchorLoc = (anchorTarget === '#go-menu') ? 0 : $(anchorTarget).offset().top;

			$toyStick.find('a').removeClass('on');
			$link.addClass('on');

			$('html, body').animate({
				scrollTop: anchorLoc
			});
		};

		$toyStick.on('click', 'a', function (e) {
			e.preventDefault();
			onClickLink(this);
		});
	};

	var slideQuickMenu = function () {
		var $window = $(window);
		var $doc = $(document);
		var scrollStandHeight = 959;
		var slideTime = 300;
		var scrollLoc;

		var scrollLocation = function () {
			scrollLoc = ($doc.scrollTop() > scrollStandHeight) ? $doc.scrollTop() - 500 : 405;
			$toyStick.stop().animate({
				top: scrollLoc
			}, slideTime);
		};

		$window.on('scroll', scrollLocation);
	};

	clickQuickMenu();
	slideQuickMenu();
};

// 전광판 모션
var shineSign = function () {
	var $sign = $('.elec-sign');
	var isToggled = false;

	var toggleSignImg = function () {
		var posX = 0;

		if (isToggled) {
			posX = 'right';
			isToggled = false;
		} else {
			posX = 'left';
			isToggled = true;
		}
		$sign.css('background-position-x', posX);

		setTimeout(toggleSignImg, 100);
	};

	toggleSignImg();

};

$(function () {

	quickMenu();
	shineSign();

	//퀵메뉴 스크롤 활성화
	$(window).scroll(function () {

		var Top = $(this).scrollTop();
		var nav = $("#stickNav ul li");

		nav.find("a").removeClass("on");

		// console.log(Top);
		//여아용
		if (Top >= 835 && Top < 1671) {
			nav.eq(1).find("a").addClass("on");
		}
		//남여공용
		//else if (Top >= 2804) {
		//	nav.eq(2).find("a").addClass("on");
		//}
		//남아용
		else if (Top >= 1672) {
			nav.eq(2).find("a").addClass("on");
		}
		//새로운장난감
		/*else if (Top >= 1050) {
		    nav.eq(0).find("a").addClass("on");
		}*/
		//놀이영상보러가기
		//else if (Top >= 980) {
		//    nav.eq(0).find("a").addClass("on");
		//}
	});
});


