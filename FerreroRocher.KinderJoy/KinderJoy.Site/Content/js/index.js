// =============================================
//자이로 드롭
// =============================================
(function() {
	var gyrodrop = (function() {
		var $el = $('.gyrodrop');
		var hook = 'is-active';
		var time = 3000;

		var loop = function() {
			$el.toggleClass(hook);
			setTimeout(loop, time);
		};
		return {
			init: loop
		};
	}());

	gyrodrop.init();
}());



//ie7 때문에 input button value 없애기
// 2017.10.27
// 한승철
// 주석처리
// $('.pop-btn').attr('value', '');

// =============================================
//배경 패럴렉스
// =============================================
(function() {
  var parallaxMain = (function() {
    var $layer = $('.parallax-layer');

    var init = function() {
      $layer.parallax({
        mouseport: $('.parallax')
      });
    };
    return {
      init: init
    };
  }());

  parallaxMain.init();
}());

// =============================================
// 계란 모션, 페이지 전환
// =============================================
(function() {

  var onEggMotion = (function() {

    var conf = {
      $wrap: $('.js-eggs'),
      $hand: null,
      defaultTop: 405,
      defaultLeft: 630,
      moveTop: 404,
      duration: 800
    };

    //  화면에 손 이미지가 없으면 손 이미지를 넣고
    //  conf.$hand에 손 이미지 객체를 넣는다.
    var insertHand = function() {
      if (conf.$wrap.children('span').hasClass('hand')) {
        return;
      }
      conf.$wrap.append('<span class="hand"></span>');
      conf.$hand = $(conf.$wrap.find('.hand'));
    };

    // 클릭한 이미지손잡이 위치 (left) 반환
    var checkClickImg = (function() {
      var result = {};

      return function(name) {
        // console.log(name);
        if (name === 'flavor') {
          result.left = 0;
        } else if (name === 'image') {
          result.left = 440;
        } else if (name === 'together') {
          result.left = 880;
        }

        result.name = name;
        return result;
      };
    }());

    // 페이지 전환
    var redirectPage = function(name) {
      setTimeout(function() {
        location.href = '' + name;
      }, 200);
    };

    // 껍질 벗기는 모션
    // motionHandImg에서 페이지 이름을 받아서
    // 껍질 벗기는 모션 후
    // redirectPage()
    // 호출하여 페이지 이동을 한다.
    var takeOffSkin = (function() {
      var num = 0;
      var clearId = null;
      var lefts = [625, 1244, 1833, 2434, 3040];
      var imgSrc = 'Images/items/egg-back-';

      var loop = function(name) {
        clearId = setTimeout(function() {
          takeOffSkin(name);
        }, 100);
      };

      return function(name) {
        var $el = $('.egg-' + name);

        conf.$hand.remove();

        $el.find('img').css('left', '-' + lefts[num] + 'px');

        if (num >= (lefts.length - 1)) {

          if (clearId) {
            clearTimeout(clearId);
            clearId = null;
          }
          num = 0;
          redirectPage(name);
          return;
        }
        num++;

        loop(name);
      };
    }());

    var motionHandImg = function(obj) {
      conf.$hand.css({
          top: conf.defaultTop,
          left: conf.defaultLeft
        })
        .animate({
          top: conf.moveTop,
          left: obj.left
        }, conf.duration, function() {
          takeOffSkin(obj.name);
        });
    };

    var init = function(e) {
      insertHand();
      motionHandImg(checkClickImg($(e.target).parent().data('name')));
    };

    return {
      init: init
    };
  }());

  $('.js-eggs').on('click', '.js-egg', onEggMotion.init);

}());


// =============================================
//팝업보이기
//2017.10.24
//한승철
// =============================================
(function() {
  var popup = {
    setPopLoc: (function() {
      var $wrap = $('.popup-wrap');

      return function() {
        $wrap.css({
          marginleft: '-' + ($wrap.width() / 2),
          marginTop: '-' + ($wrap.height() / 2)
        });
      };
    }()),
    hide: function(target) {
      $(target).hide();
    }
  };

  $('.js-pop-close').on('click', function() {
    popup.hide($(this).parent().parent());
  });

  $('.bn-main button').on('click', function() {
    popup.hide($(this).parent());
  });

  // popup.setPopLoc();
}());

// 2017.10.24
// 한승철
// 주석처리
/*function openPopMain() {
  $('.popup-main').show();
  $('html,body').animate({ scrollTop: 2400 });
};

function closePopMain() {
  $('.popup-main').hide();
  $('.popup-main iframe').attr('src', '');
}
*/