
loadEgg(); //계란 이미지 로드
eggCenter();//egg 가운데 정렬
popLocation(); //팝업 location
dropDown(); //자리로 드롭
balloonUpDn(); //열기구
moveCloud(); //구름

function loadEgg() { //로드 되자마자 이미지 숨기기
    $('.egg-front').addClass('hide');
}

/* egg center 정렬 */
function eggCenter() {
    var eggWidth = $('.js-motionEgg').width();

    $('.js-motionEgg').css({ 'margin-left': '-' + (eggWidth / 2) + 'px' });
}


//ie7 때문에 input button value 없애기
$('.pop-btn').attr('value', '');

//배경 패럴렉스
$('.parallax-layer').parallax({ mouseport: $('.parallax') });

//팝업 닫기
$('.js-pop-close').on('click', function () {
    $(this).parent().parent().hide();
});

//balloon 움직이기
function balloonUpDn() {
    $('.js-balloonUpDn').animate({
        top: '+=10'
    }, 2000).animate({ top: '-=10' }, 2000);

    setTimeout(function () {
        balloonUpDn();
    }, 4000);
}

//구름움직이기

function moveCloud() {
    $('.js-cloud1').animate({ left: '-10000px' }, 60000000, 'linear').animate({ left: '4420px' }, 0).animate({ left: '-10000px' }, 60000000, 'linear');
    $('.js-cloud2').animate({ left: '-5000px' }, 40000000, 'linear').animate({ left: '3087px' }, 0).animate({ left: '+=-5000px' }, 4000000, 'linear');
    $('.js-cloud3').animate({ left: '-2500px' }, 26000000, 'linear').animate({ left: '2398px' }, 0).animate({ left: '-20000px' }, 26000000, 'linear');
    $('.js-cloud4').animate({ left: '-3000px' }, 30000000, 'linear').animate({ left: '1120px' }, 0).animate({ left: '-2900px' }, 30000000, 'linear');
    $('.js-cloud5').animate({ left: '-500px' }, 35000000, 'linear').animate({ left: '3500px' }, 0).animate({ left: '-700px' }, 35000000, 'linear');
    $('.js-cloud6').animate({ left: '-9000px' }, 80000000, 'linear').animate({ left: '2580px' }, 0).animate({ left: '-6000px' }, 80000000, 'linear');

    setTimeout(function () {
        moveCloud();
    }, 300000);
}

//자이로 드롭
function dropDown() {
    $('.js-drop').delay(300).animate({
        top: '346px'
    }, 1800, 'swing').delay(500).animate({
        top: '90px'
    }, 12000);

    setTimeout(function () {
        dropDown();
    }, 14600);
}

//egg 클릭
$('.js-egg').on('click', function () {

    var $hand = $('<span class="hand"></span>');

    var $egg = $(this); // 이미지 감싸는 span
    var $img = $egg.find('img'); // 이미지

    switch ($egg.data('name')) {

        case 'flavor':
            $egg.parent().append($hand);
            moveHand($hand, $img, 0);
            break;

        case 'image':
            $egg.parent().append($hand);
            moveHand($hand, $img, 440);
            break;

        case 'together':
            $egg.parent().append($hand);
            moveHand($hand, $img, 880);
            break;
    }
});

//손움직이기
function moveHand(hand, img, leftV) {

    hand.css({
        top: '405px',
        left: '630px'
    }).animate({
        top: '404px',
        left: leftV + 'px'
    }, 800, function () {
        changeImg(img);
    });

}

//계란 모션 변수 초기화
var num = 0;

//이미지 변환
function changeImg(img) {

    var lefts = [625, 1244, 1833, 2434, 3040];
    var imgSrc = contentUrl + 'Images/items/egg-back-';
    var imgName = img.parent().data('name');

    setTimeout(function () {

        $(img).css('left', '-' + lefts[num] + 'px');

        if (num >= (lefts.length - 1)) {
            redirect(img.parent());
            return;
        }
        $('.hand').remove();

        num++;
        changeImg(img)
    }, 100);
}


//모션후 페이지 이동
function redirect(target) {

    var href = '';

    if (target.hasClass('egg-flavor')) {
        href = contentUrl + 'flavor';
    } else if (target.hasClass('egg-image')) {
        href = contentUrl + 'image';
    } else if (target.hasClass('egg-together')) {
        href = contentUrl + 'together';
    }

    setTimeout(function () { //화면 이동
        location.href = href;
    }, 200);

}

//팝업보이기
function popLocation() {
    var width = $('.popup-wrap').width();
    var height = $('.popup-wrap').height();

    $('.popup-wrap').css({
        marginLeft: '-' + width / 2 + 'px',
        marginTop: '-' + height / 2 + 'px'
    });
}

$(".bn-main button").click(function () {
    $(this).parent().hide();
});


function openPopMain() {
    $('.popup-main').show();
    $('html,body').animate({ scrollTop: 2400 });
};

function closePopMain() {
    $('.popup-main').hide();
    $('.popup-main iframe').attr('src', '');
}
