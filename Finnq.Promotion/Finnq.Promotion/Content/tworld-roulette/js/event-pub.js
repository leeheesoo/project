﻿var $finger = $('.finger-img');
var $dim = $('.dim');
var winCode; //0~6 당첨
winCode = 0;


/*
==============================================
 popup
==============================================
*/
function openPop(flag) {
    $dim.show();
    var $scrollTop = $(window).scrollTop() + 20;
    $('#' + flag).show().css('top', $scrollTop);
}
function closePop(flag) {
    $dim.hide();
    $('#' + flag).hide();
}

/*
==============================================
 input check
==============================================
*/

function nextBlank(N, Obj, nextID) {
    if (document.getElementById(Obj).value.length == N) {
        document.getElementById(nextID).focus();
    }
}


/*
==============================================
 roulette
==============================================
*/
var theWheel = new Winwheel({
    'numSegments'       : 7,     // 분할
    'outerRadius'       : 394,       
    'drawMode'          : 'image',   // 룰렛 이미지모드
    'segments'     :           
    [
       {'text' : '100원'},
       {'text' : '5천원'},
       {'text' : '1만원'},
       {'text' : '5만원'},
       {'text' : '10만원'},
       {'text' : '50만원'},
       {'text' : '100만원'}
    ],
    'animation' :                   
    {
        'type'     : 'spinToStop',
        'duration' : 3,     
        'spins'    : 5,     
        'callbackFinished' : 'alertPrize()'
    }
});

// 룰렛reset
function resetWheel() {
    theWheel.stopAnimation(false);  
    theWheel.rotationAngle = 0;     
    theWheel.draw();               


    wheelSpinning = false;          // Reset to false to power buttons and spin can be clicked again.
    $finger.addClass('floating');
}


// winner
function calculatePrize(winCode)
{
    var winnerSpot = (51 * winCode) + 9;//51

    var stopAt = (winnerSpot + Math.floor((Math.random() * 30)))
    theWheel.animation.stopAngle = stopAt;

    theWheel.startAnimation();

    $finger.removeClass('floating');
}


var loadedImg = new Image();

// 룰렛그리기
loadedImg.onload = function()
{
    theWheel.wheelImage = loadedImg;   
    theWheel.draw();                   
}

loadedImg.src = "/Content/t-roulette/images/popup/rolette_v2.png";


var wheelPower    = 0;
var wheelSpinning = false;

// 룰렛종료 후 실행
function alertPrize()
{
    var imgSrc = '/Content/roulette/images/popup/roulette_'
    var winningSegment = theWheel.getIndicatedSegment();
    
    setTimeout("closePop('pop-roulette01')", 1000);
    setTimeout("openPop('pop-roulette02')", 1000);
}

$(function () {

    $('.game-start').click(function () {
        resetWheel();
    });


});






