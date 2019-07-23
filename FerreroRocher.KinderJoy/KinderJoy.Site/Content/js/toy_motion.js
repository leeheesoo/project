

 //자이로 드롭
    function dropDown() {
        $('.gyro_img').delay(300).animate({
            top: '254px'
        }, 1000, 'swing').delay(500).animate({
            top: '65px'
        }, 8000);

        setTimeout(function () {
            dropDown();
        }, 3000);
    }

// 전광판 빛내기
var sign = (function() {
  var $sign = $('.electronic-sign');
  var isToggled = false;

  var toggleSignImg = function() {
		var posX = 0;

		if (isToggled) {
			posX = '100%';
			isToggled = false;
		} else {
			posX = 0;
			isToggled = true;
		}
		$sign.css('background-position-x', posX);

		setTimeout(toggleSignImg, 100);
	};

	return {
		shine: toggleSignImg
	};

}());


$(function(){
    dropDown();
    car_movie();

		sign.shine();
	var tennis_ball_down = function (){
		$(".tennis_ball").animate({
			top: "300px"
		},750);
	};
	var tennis_ball_up = function (){
		$(".tennis_ball").animate({
			top: "226px"
		},750,'swing');
	};
	var hand_move_down = function (){
		$(".tennis_hand_area").rotate({
			duration:700,
			angle:0,
			animateTo:10,
			callback: hand_move_down,
			easing: function (x,t,b,c,d){        // t: current time, b: begInnIng value, c: change In value, d: duration
			  return c*(t/d)+b;
			}
		});
	};
	var hand_move_up = function (){
		$(".tennis_hand_area").rotate({
			duration:700,
			angle:0,
			animateTo:0,
			callback: hand_move_up,
			easing: function (x,t,b,c,d){        // t: current time, b: begInnIng value, c: change In value, d: duration
			  return c*(t/d)+b;
			}
		});
	};

//	tennis_hand_move()
	setInterval(function(){
		hand_move_down()
		setTimeout(function () {
			tennis_ball_down();
		}, 300);
		setTimeout(function () {
			hand_move_up();
		}, 500);
		setTimeout(function () {
			tennis_ball_up();
		}, 1000);
	}, 1800);


//	쿵푸캐릭호랑이
	function kungfu_panda_movie1(){
		$('.kungfu_panda').animate({
			left: "190px"
		}, 300);

		setTimeout(function () {
		    /* $('.kungfu_panda').effect("shake", { distance: 2 }, 300);*/

		    $(".kungfu_panda").rotate({
		        duration: 100,
		        angle: 0,
		        animateTo: 10,
		        easing: function (x, t, b, c, d) {        // t: current time, b: begInnIng value, c: change In value, d: duration
		            return c * (t / d) + b;
		        }
		    });

		}, 400);
	}

	function kungfu_panda_movie2(){
		$('.kungfu_panda').animate({
			left: "320px"
		}, 200);
		$(".kungfu_panda").rotate({
		    duration: 500,
		    angle: 0,
		    animateTo: 0,
		    easing: function (x, t, b, c, d) {        // t: current time, b: begInnIng value, c: change In value, d: duration
		        return c * (t / d) + b;
		    }
		});
	}

	function kungfu_tiger_movie1(){
	    $('.kungfu_tiger img').attr('src', "/images/toy/motion_img/kungfu_tiger1.png");
		$(".kungfu_tiger").rotate({
			duration:50,
			angle:0,
			animateTo:-15,
			easing: function (x,t,b,c,d){        // t: current time, b: begInnIng value, c: change In value, d: duration
			  return c*(t/d)+b;
			}
		});
	}

	function kungfu_tiger_movie2(){
	    $('.kungfu_tiger img').attr('src', "/images/toy/motion_img/kungfu_tiger0.png");
		$(".kungfu_tiger").rotate(0);
	}
	setInterval(function(){
		kungfu_panda_movie1();
		setTimeout(function () {
			kungfu_tiger_movie1();
		}, 200);
		setTimeout(function () {
			kungfu_panda_movie2()
			kungfu_tiger_movie2();
		}, 700);
	}, 3000);
//	모노레일
	function mono_train_movie(){
		$('.mono_train').animate({
			left: "870px"
		},20000);
	}
	function mono_train_init(){
		$('.mono_train').css({
			left: "0px"
		},1000);
	}
	mono_train_movie();
	setInterval(function(){
		mono_train_init();
		mono_train_movie();
	}, 21000);

//자동차풍선
	$(".car_balloon_img").rotate({
		angle:5
	});
	var car_ballon_left = function (){
		$(".car_balloon_img").rotate({
			duration:2000,
			angle:5,
			animateTo:-5,
			easing: function (x,t,b,c,d){        // t: current time, b: begInnIng value, c: change In value, d: duration
			  return c*(t/d)+b;
			}
		});
	};
	var car_ballon_right = function (){
		$(".car_balloon_img").rotate({
			duration:2000,
			angle:-5,
			animateTo:5,
			easing: function (x,t,b,c,d){        // t: current time, b: begInnIng value, c: change In value, d: duration
			  return c*(t/d)+b;
			}
		});
	};
	setInterval(function(){
		car_ballon_left();
		setTimeout(function () {
			car_ballon_right();
		}, 2000);
	}, 4000);

	$(".car_balloon_green").rotate({
		angle:-2
	});

	var car_balloon_green_left = function (){
		$(".car_balloon_green").rotate({
			duration:3000,
			angle:-2,
			animateTo:2,
			easing: function (x,t,b,c,d){        // t: current time, b: begInnIng value, c: change In value, d: duration
			  return c*(t/d)+b;
			}
		});
	}
	var car_balloon_green_right = function (){
		$(".car_balloon_green").rotate({
			duration:3000,
			angle:2,
			animateTo:-2,
			easing: function (x,t,b,c,d){        // t: current time, b: begInnIng value, c: change In value, d: duration
			  return c*(t/d)+b;
			}
		});
	}
	setInterval(function(){
		car_balloon_green_left();
		setTimeout(function () {
			car_balloon_green_right();
		}, 3000);
	}, 6000);

	$(".car_balloon_blue").rotate({
		angle:3
	});

	var car_balloon_blue_left = function (){
		$(".car_balloon_blue").rotate({
			duration:3000,
			angle:3,
			animateTo:-3,
			easing: function (x,t,b,c,d){        // t: current time, b: begInnIng value, c: change In value, d: duration
			  return c*(t/d)+b;
			}
		});
	}
	var car_balloon_blue_right = function (){
		$(".car_balloon_blue").rotate({
			duration:3000,
			angle:-3,
			animateTo:3,
			easing: function (x,t,b,c,d){        // t: current time, b: begInnIng value, c: change In value, d: duration
			  return c*(t/d)+b;
			}
		});
	}
	setInterval(function(){
		car_balloon_blue_left();
		setTimeout(function () {
			car_balloon_blue_right();
		}, 3000);
	}, 6000);

	$(".girl_balloon_img").rotate({
		angle:5
	});

	var kitty_ballon_left = function (){
		$(".girl_balloon_img").rotate({
			duration:2000,
			angle:5,
			animateTo:-5,
			easing: function (x,t,b,c,d){        // t: current time, b: begInnIng value, c: change In value, d: duration
			  return c*(t/d)+b;
			}
		});
	}
	var kitty_ballon_right = function (){
		$(".girl_balloon_img").rotate({
			duration:2000,
			angle:-5,
			animateTo:5,
			easing: function (x,t,b,c,d){        // t: current time, b: begInnIng value, c: change In value, d: duration
			  return c*(t/d)+b;
			}
		});
	}
	setInterval(function(){
		kitty_ballon_left();
		setTimeout(function () {
			kitty_ballon_right();
		}, 2000);
	}, 4000);



//	상단애드벌룬()
	var top_adballoon_down = function () {
	    $(".top_adballoon").animate({
	        top: "300px"
	    }, 3000);
	}
	var top_adballoon_up = function () {
	    $(".top_adballoon").animate({
	        top: "226px"
	    }, 3000, 'swing');
	}
	setInterval(function () {
        top_adballoon_down();
	   setTimeout(function () {
	        top_adballoon_up();
	   }, 3000);
	}, 6000);
});

// $(function () {
//     /* 카테고리 타이틀 */

//     var toggleSignImg = function() {
//     	var posX = 0;

//     	if (isToggled) {
//     		posX = '100%';
//     		isToggled = false;
//     	} else {
//     		posX = 0;
// 	  		isToggled = true;

//     	}
//     	$sign.css('background-position-x', posX);

//     	setTimeout(toggleSignImg, 100);
//     };

//     // toggleSignImg();
// });



//남아용장남감 자동차모션
function car_left() {
	$('.car_img').stop().animate({
		left: "30px"
	}, 3000);
}
function car_right() {
	$('.car_img').stop().animate({
		left: "280px"
	}, 3000);
}
function carwheel1_move() {
	$(".carwheel1_area").rotate({
		duration: 2500,
		angle: 0,
		animateTo: -20,
		easing: function (x, t, b, c, d) {        // t: current time, b: begInnIng value, c: change In value, d: duration
			return c * (t / d) + b;
		}
	});
}
function carwheel2_move() {
	$(".carwheel2_area").rotate({
		duration: 2500,
		angle: 0,
		animateTo: -20,
		easing: function (x, t, b, c, d) {        // t: current time, b: begInnIng value, c: change In value, d: duration
			return c * (t / d) + b;
		}
	});
}
function carwheel1_remove() {
	$(".carwheel1_area").rotate({
		duration: 2500,
		angle: -20,
		animateTo: 0,
		easing: function (x, t, b, c, d) {        // t: current time, b: begInnIng value, c: change In value, d: duration
			return c * (t / d) + b;
		}
	});
}
function carwheel2_remove() {
	$(".carwheel2_area").rotate({
		duration: 2500,
		angle: -20,
		animateTo: 0,
		easing: function (x, t, b, c, d) {        // t: current time, b: begInnIng value, c: change In value, d: duration
			return c * (t / d) + b;
		}
	});
}

function car_movie() {
    setTimeout(function () {
		car_left();
		carwheel1_move();
		carwheel2_move();
	}, 1000);

	setInterval(function () {
		car_right();
		carwheel1_remove();
		carwheel2_remove();
		setTimeout(function () {
			car_left();
			carwheel1_move();
			carwheel2_move();
		}, 3000);
	}, 6000);
}
