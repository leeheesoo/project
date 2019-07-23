var animateCharacter;
var animateCursor;

// 대관람차
var angle = 0;
var angleMinus = 0;

var left;

$(function () {
    animateCharacter = $(".charater-motion").animateSprite({
        fps: 10,
        loop: false,
        complete: function () {
            setTimeout(function () {
                animateCharacter.animateSprite('restart');
            }, 2000);
        }
    });

    animateCursor = $(".animate-cursor").animateSprite({
        fps: 25,
        loop: false,
        complete: function () {
            setTimeout(function () {
                animateCursor.animateSprite('restart');
            }, 500);
        }
    });

    // 대관람차 setInterval
    setInterval(function () {
        angle += 1;
        angleMinus -= 1;
        $(".ferris-wheel-line, .capsule-box").rotate(angle);
        $(".ferris-capsule").rotate(angleMinus);
    }, 30);

    animateClouds();

})

function animateClouds() {
    $(".visual-cloud li").each(function () {
        var $cloud = $(this);
        var cloudLeft = $cloud.position().left;
        var cloudSpeed = $cloud.attr("data-speed");

        if (cloudLeft > 500) {
            setInterval(function () {
                cloudLeft--;
                if (cloudLeft <= -500) { // cloudLeft 값이 -500보다 작을때 cloudLeft값 reset
                    cloudLeft = 1500;
                }
                $cloud.css('left', cloudLeft);
            }, cloudSpeed);
        } else {
            setInterval(function () {
                cloudLeft++;
                if (cloudLeft > 1500) { // cloudLeft 값이 1500보다 클때 cloudLeft값 reset
                    cloudLeft = -500;
                }
                $cloud.css('left', cloudLeft);
            }, cloudSpeed);
        }
    })
};


// visual영역 모션 함수 정의
//function floatingIslandLeft() {
//    $('.island-left').animate({ 'top': '270px' }, 1500, 'easeInOutSine').animate({ 'top': '280px' }, 1500)
//};

//function floatingIslandRight() {
//    $('.island-right').animate({ 'top': '300px' }, 2000, 'easeInOutSine').animate({ 'top': '308px' }, 2000)
//};

//function floatingIslandCenter() {
//    $('.island-center').animate({ 'top': '295px' }, 1200, 'easeInOutSine').animate({ 'top': '305px' }, 1200)
//};

//function floatingIslandCenter() {
//    $('.island-center').animate({ 'top': '295px' }, 1200, 'easeInOutSine').animate({ 'top': '305px' }, 1200)
//};

//function floatingBgIsland1() {
//    $('.bg_island1').animate({ 'top': '660px' }, 1000, 'easeInOutSine').animate({ 'top': '670px' }, 1000)
//};


