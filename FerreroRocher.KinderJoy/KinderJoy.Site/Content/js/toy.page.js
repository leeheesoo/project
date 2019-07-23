var toyPage = {

	menuNum: 0,
	motionAreaLeft: null,
	scrollFlag: false,

	init: function () {
		this.sizeSet();
		this.onClickNavLink();

		var winH = $(window).height();
		if (winH <= 700) {
			$("#stickNav").animate({ top: 0 });
		} else {
			$("#stickNav").animate({ top: 100 });
		}
		$(".container").css("background", "#fbe122");
		//$(".container").css("background", "#55db82 url(../../Images/toy/main/bg_wrap.jpg) repeat 0 0");
		$(".content-image-inner").css({ "top": 0, "left": this.motionAreaLeft });
		//$(".content-image").css({ "height": "1120px" });
		$("html").css("overflow-x", "hidden");
		$("body,html").animate({ scrollTop: 0 });
        /*
         * 2017.10.19
         * 한승철
         */
		$('body').css('overflow', 'hidden');
	},
	sizeSet: function () {
		var $size = $(window).width();
		this.motionAreaLeft = -1 * (2560 - $size) / 2;
	},
	// 마우스 스크롤 할때
	handle: function (dNum) {
		var slideNum = 3;

		if (this.scrollFlag != true) {
			if (dNum < 0) { //아래로
				if (this.menuNum < slideNum) {
					this.menuNum = this.menuNum + 1;
					this.scrollFlag = true;

					this.play(this.menuNum);

					setTimeout(function () {
						toyPage.scrollFlag = false;
					}, 600);
				}
			} else { // 위로
				if (this.menuNum > 0) {
					this.menuNum = this.menuNum - 1;
					this.scrollFlag = true;

					this.play(this.menuNum);

					setTimeout(function () {
						toyPage.scrollFlag = false;
					}, 600);
				}
			}
		}
	},
	// handle에서 어느 위치로 가는지 값을 받아서 모션을 한다.
	play: function (param) {

		switch (param) {
		    case 0: this.motion('begin'); this.navActive('top'); break;
		    case 1: this.motion('top'); this.navActive(0); break;
		    case 2: this.motion('new'); this.navActive(1); break;
			case 3: this.motion('boy'); this.navActive(2); break;
		    // case 4: this.motion('girl'); this.navActive(2); break;
		}
	},
	motion: function (frameName) {
		var $target = $("#wrap"),
			aniTime = 1000,
			easing = "easeOutExpo";

        // 기준값에서 -100px 씩
		$(".new_toy").stop().animate({ right: 162 });
		$(".boy_toy").stop().animate({ left: 103 });
		$(".common_toy").stop().animate({ left: 524 });
		$(".girl_toy").stop().animate({ right: 392 });

		switch (frameName) {
			//상단화면
			case 'begin':
				$target.stop().animate({ top: 0, left: this.motionAreaLeft }, aniTime, easing);
				break;

		   //상단 이벤트
			case 'top':
			    $target.stop().animate({ top: -385, left: this.motionAreaLeft }, aniTime, easing);
			    break;

		    //새로운 장난감
		    case 'new':
		        $target.stop().animate({ top: -821, left: this.motionAreaLeft - 400 }, aniTime, easing);
		        $(".new_toy").stop().animate({ right: 312 }, aniTime);
		        break;

			//남아용 장난감
			case 'boy':
			    $target.stop().animate({ top: -1709, left: (this.motionAreaLeft + 400) }, aniTime, easing);
			    $(".boy_toy").stop().animate({ left: 432 }, aniTime);
				break;

			//남여 공용 장난감
			//case 'common':
			//    $target.stop().animate({ top: -2094, left: (this.motionAreaLeft + 300) }, aniTime, easing);
			//	$(".common_toy").stop().animate({ left: 624 }, aniTime);
			//	break;

				//여아용 장난감
			// case 'girl':
			// 	$target.stop().animate({ top: -2460, left: (this.motionAreaLeft - 130) }, aniTime, easing);
			// 	$(".girl_toy").stop().animate({ right: 492 }, aniTime);
			// 	break;

			//	//킨더조이 영상
			//case 'kinderMov':
			//	$target.stop().animate({ top: -1430, left: (this.motionAreaLeft - 400) }, aniTime, easing);
			//	break;

			//	//장난감 영상
			//case 'toyMov':
			//	$target.stop().animate({ top: -2040, left: (this.motionAreaLeft + 300) }, aniTime, easing);
			//	break;
		}
	},
	nav: function (e) {

	    var idx = $(this).parent().index();
		switch (idx) {
			case 0: toyPage.motion('top'); toyPage.menuNum = 3; break; //새로운
	    // case 0: toyPage.motion('new'); toyPage.menuNum = 3; break; //새로운
			case 1: toyPage.motion('new'); toyPage.menuNum = 4; break; //남아용
			case 2: toyPage.motion('boy'); toyPage.menuNum = 5; break; //여아용
		}
		toyPage.navActive(idx);
		e.preventDefault();
	},
	navActive: function (param) {

		var $navList = $("#stickNav ul li");
		// if (param == 'top') {
		// 	$navList.find("a").removeClass("on");
		// 	return;
		// }
		$navList.find("a").removeClass("on");
		$navList.eq(param).find("a").addClass("on");
	},
	onClickNavLink: function () {

		var $navList = $("#stickNav ul li");
		//네비 리스트 클릭
		$navList.on('click', 'a', this.nav);

		//네비 풍선 클릭
		$(".toy-sticky-go-top").on("click", function (e) {
			e.preventDefault();
			toyPage.motion('begin');
			toyPage.navActive('top');
			toyPage.menuNum = 0;
		});

		// 장난감 보기 팝업이 떴을때
		// 마우스 스크롤시
		$(document).mousewheel(function (event, delta) {
			var $toyDetailPopup = $('.popup_toys');

			// var popup_starwars = $('#boys').css('display');
			// var popup_couple = $('#girls').css('display');
			var scrollTop = $(window).scrollTop();

			if ($toyDetailPopup.hasClass('is-active')) {
				if (scrollTop > 550) {
					if (this.scrollFlag != true) {
						this.scrollFlag = true;
						$('html,body').animate({ scrollTop: 420 }, 500);
						setTimeout(function () {
							this.scrollFlag = false;
						}, 600);
					}
				}
			} else {
				toyPage.handle(delta);
				event.preventDefault();
				event.returnValue = false;
			}
		});

		//윈도우 리사이즈
		$(window).resize(function () {

			toyPage.sizeSet();

			$(".content-image-inner").css({ "left": toyPage.motionAreaLeft });

			var winH = $(window).height();

			if (winH <= 700) {
				$("#stickNav").animate({ top: 0 });
			} else {
				$("#stickNav").animate({ top: 100 });
			}

		});
	}

};//toyMotion

// =============================================================================
// 장난감 자세히 보기 팝업
// 다른 종류의 장난감이 추가 될경우 이미지는 boys / girls와 같은 방식으로 추가 후
// toyData.type 객체 안에 장난감 타입: 썸네일 이미지 갯수만 추가 할 것
// =============================================================================

var toyData = {
	$popup: $('.popup_toys'),
	$detail: $('.js-toy-detail'),
	itemUrl: '/Images/toy/bn/items/',
	thumbUrl: '/Images/toy/bn/thumb/',
	prefix: '/thumb-',
	imgType: '.png',
	sliderObj: [],
	type: {
		girls: 8,
		boys: 8
	}
};

// 장난감 디테일 팝업 썸네일 리스트 동적 생성
var setSliderItems = function(popupType) {
	var $list = toyData.$popup.find('.js-thumb');
	var $detail = toyData.$popup.find('.js-toy-detail');

	for (var i = 0; i < toyData.type[popupType]; i += 1){

		var item = '<li>'
				+ '<button type="button" data-detail-num="'+ (i + 1) +'">'
					+ '<img src="'+ toyData.thumbUrl + popupType + toyData.prefix + (i + 1) + toyData.imgType +'">'
				+'</button>'
			+ '</li>';


			// 홀수면 왼쪽 짝수면 오른쪽 슬라이드에 추가
			$list[(i % 2 === 0 ? 0 : 1)].innerHTML += item;
	} // for
};

// 장난감 디테일 팝업 띄우기
var showDetail = function(popupType) {

	var wrapHeight = $('#wrap').height();
	var positionTop = 0;
	var $detail = toyData.$popup.find('.js-toy-detail');
	var $dim = $('#png_bg');
	var currentPopupName = 'popup_toys-' + popupType;

	$dim.show().css('height', wrapHeight);

	$('html, body').animate({ scrollTop: positionTop });
	toyData.$popup.addClass(currentPopupName + ' is-active').animate({ top: positionTop });
	$detail.find('img').attr('src', toyData.itemUrl + popupType + toyData.prefix + '0' + toyData.imgType );
};

	// 장난감 팝업이 뜬 후 bxSlider를 호출해야 썸네일 리스트의 크기가 제대로
	// 표현 됨
var setBxSlider = function(popupType) {

	// bxslider 옵션
	// 왼쪽과 오른쪽 옵션이 같으므로 하나의 옵션을 사용
	var sliderOpt = {
		mode: 'vertical', // 가로 방향 수평 슬라이드
		speed: 500,        // 이동 속도를 설정
		pager: false,      // 현재 위치 페이징 표시 여부 설정
		moveSlides: 3,     // 슬라이드 이동시 개수
		slideWidth: 201,    // 슬라이드 너비
		minSlides: 3,      // 최소 노출 개수
		maxSlides: 3      // 최대 노출 개수
	};

	$.each(toyData.$popup.find('.js-thumb'), function(idx, aList) {
		toyData.sliderObj.push($(aList).bxSlider(sliderOpt));
	});

	toyData.$popup.find('.js-thumb').on('click', 'button', function() {
		var $button = $(this);
		var idx = $button.data('detail-num');

		toyData.$popup.find('.js-toy-detail').find('img').hide().attr('src', toyData.itemUrl + popupType + toyData.prefix + idx + toyData.imgType).show(200);
	});
};

// 닫기 버튼 눌렀을 경우 bxslider 삭제
var destroySlider = function() {
	$.each(toyData.sliderObj, function(idx, slider) {
		slider.destroySlider();
	});

	// 썸네일 슬라이더를 삭제하지 않으면 이전 보여진 장난감에 이어서 추가 됨
	toyData.$popup.find('.js-thumb').empty();

	// 이벤트를 삭제 해줘야 새로 생긴 썸네일 인터렉션이 제대로 먹힘
	toyData.$popup.find('.js-thumb').off('click', 'button');

	toyData.$popup.attr('class', 'popup_toys');
	$('#png_bg').hide();
};


// 장난감 자세히 보기 팝업
// @param: 장난감 종류 (boys, girls)
var openToyDetailArticle = function(popupId) {

	setSliderItems(popupId);
	showDetail(popupId);
	setBxSlider(popupId);
};


function openPop(param) {
	$("html").css("overflow-y", "auto");
	var wrapH = $('#wrap').height();
	var wTop = $(window).scrollTop();

	$('#png_bg').css('height', wrapH);
	$('#png_bg').show();
	$('#' + param).show();
	$('html, body').animate({ scrollTop: wTop });
}

function closePop() {
	$('html,body').animate({ scrollTop: 0 });
	//$("html").css("overflow-y", "hidden");

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

function resetViewHeight() {
    $('body').css('height', '');
}

$(document).ready(function () {
    toyPage.init();
});
