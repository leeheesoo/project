//닫기
function closePop(name) {
    var $dim = $('.js-chDim');

    $(name).removeClass('on');

    if (name === '.pop-userInfo' || name === '.pop-prize' || name === '.popup-inapp') {
        $dim.removeClass('on');
    }
    
}

//열기
function openPop(name) {
    var $dim = $('.js-chDim');

    $(name).addClass('on');
    $dim.addClass('on');
}

// 상단으로 이동
function gotoTop() {
    $('html, body').animate({
        scrollTop: 0
    });
}

// 필드의 이미지 갯수 추출
function checkFieldFToyLen(field) {
    var $f = $(field);
    var $imgs = $f.find('img');

    return $imgs.size();

}


// 캔버스에 배경이미지 설정
var canvas = document.getElementById('canvas');
var ctx = canvas.getContext('2d');
var img = new Image();

img.onload = function () {    
    ctx.drawImage(img, 0, 0);
};
img.src = '/Images/Christmas2015/web/tree.png';

// 인형을 포합한 내용을 캔버스로 그리기
function drawImg(id) {
    var toy = document.getElementById(id);
    var currX = toy.offsetLeft - canvas.offsetLeft;
    var currY = toy.offsetTop - canvas.offsetTop;

    // console.log(toy.getAttribute('src'));

    ctx.drawImage(toy, currX, currY);

    // toy.style.display = 'none';

}


function drawLargeImg(treeCanvas, treeId, selectImgs) {
    /* 크리스마스 카드 문구 합성 START */
    var resultCanvas = document.getElementById('resultCanvas');
    var resultCtx = resultCanvas.getContext('2d');
    var resultImg = new Image();
    var resultContent = $('#txtCard').val();
    var src = '';
    var cardTxtType = $('input[name=cardTxt]:checked').attr('id');
    if (cardTxtType === 'cardTxt1') {
        resultImg.src = '/Images/Christmas2015/popup/card1.png';
        resultContent = '기본문구1';
    } else if (cardTxtType === 'cardTxt2') {
        resultImg.src = '/Images/Christmas2015/popup/card2.png';
        resultContent = '기본문구2';
    } else if (cardTxtType === 'cardTxt3') {
        resultImg.src = '/Images/Christmas2015/popup/card3.png';
        if (resultContent == '') {
            alert('띄어쓰기 포함하여 30자 이내로 직접 작성해주세요!');
            return;
        }
    }
    resultImg.onload = function () {
        resultCtx.drawImage(resultImg, 0, 0);
        resultCtx.drawImage(treeCanvas, 320, 0, 320, 326);

        if (cardTxtType === 'cardTxt3') {
            var x = 190;
            var y = 93;
            resultCtx.lineWidth = 1;
            resultCtx.fillStyle = "#fff";
            resultCtx.lineStyle = "#000";
            resultCtx.font = "bold 24pt sans-serif";
            resultCtx.textAlign = "center";
            resultCtx.textBaseline = 'middle';
            wrapText(resultCtx, resultContent, x, y, 300, 40);
        }
        makeImage(resultCanvas, treeId, selectImgs, resultContent);
    };
    /* 크리스마스 카드 문구 합성 END */
}

//canvas에 텍스트 그리기
function wrapText(context, text, x, y, maxWidth, lineHeight) {
    var words = text.split('');
    var line = '';

    var textWidth = context.measureText(text).width;
    if (textWidth > maxWidth) {
        y = y - ((lineHeight / 2) * (Math.ceil(textWidth / maxWidth) - 1));
    }

    for (var n = 0; n < words.length; n++) {
        var testLine = line + words[n] + '';
        var metrics = context.measureText(testLine);
        var testWidth = metrics.width;
        if (testWidth > maxWidth && n > 0) {
            context.fillText(line, x, y);
            line = words[n] + '';
            y += lineHeight;
        }
        else {
            line = testLine;
        }
    }
    context.fillText(line, x, y);
}

//이미지합성 후 DB에 저장
function makeImage(resultCanvas, treeId, selectImgs, resultContent) {
    var src = resultCanvas.toDataURL('image/png');

    /* 테스트로 만들어 놓은 이미지 */
    var $img = $('<img>');
    $img.css({
        position: 'fixed',
        top: 100,
        right: 0
    }).attr('src', src);
    $('.js-chDim').removeClass('on');

    //$('body').append($img);
    var synthesisImg = src.replace('data:image/png;base64,', '');

    $.ajax({
        type: 'post',
        url: '/api/2015-christmas/update-event2',
        async: false,
        cache: false,
        data: {
            MakeTreeId: treeId,
            SynthesisImage: synthesisImg,
            Content: resultContent,
            Toy1: selectImgs.split(',')[0],
            Toy2: selectImgs.split(',')[1],
            Toy3: selectImgs.split(',')[2],
            Toy4: (selectImgs.split(',').length >= 3 ? selectImgs.split(',')[3] : null),
            Toy5: (selectImgs.split(',').length >= 4 ? selectImgs.split(',')[4] : null),
            Toy6: (selectImgs.split(',').length >= 5 ? selectImgs.split(',')[5] : null),
            Toy7: (selectImgs.split(',').length >= 6 ? selectImgs.split(',')[6] : null),
        },
        success: function (data) {
            $('#hidMakeTreeId').val(data.id);
            $('#hidMakeTreeImage').val(data.synthesisImage);
            $('#hidMakeTreeMobile').val(data.mobile);
            closePop('.pop-card');
            openPop('.pop-sns');
        }, error: function (jqXHR, textStatus, errorThrown) {
            if (textStatus != 'abort') {
                alert('일시적인 장애가 발생했습니다. 잠시후 다시 시도해주세요. (' + textStatus + ')');
            }
            return;
        }
    });
}

// 트리 완성을 눌렀을때 인형 갯수가 3 ~ 7 개 사이면 통과  
function openChristmasTreeCard() {
    var smallToyLen = 3;
    if (checkFieldFToyLen('.tree-area') >= smallToyLen) {
        openPop('.pop-card');

    } else {
        alert('최소 3개에서 최대 7까지 장난감 선택이 가능합니다!');
    }
}
// 트리 완성을 눌렀을때 인형 갯수가 3 ~ 7 개 사이면 통과

function drawTree(treeId, selectImgs) {
    var $tree = $('.tree-area');
    var $imgs = $tree.find('img');
    $imgs.each(function () {
        drawImg($(this).attr('id'));
    });
    drawLargeImg(canvas, treeId, selectImgs);
}

function makeSlider() {
    var toySlider = $('.toys-list').bxSlider({
        slideWidth: 92,
        maxSlides: 5,
        slideMargin: 0,
        pager: false,
        controls: false
    });

    // 트리만들기 슬라이더 다음
    $('.arrow-next').on('click', function () {
        toySlider.goToNextSlide();
    });
    // 트리만들기 슬라이더 이전
    $('.arrow-prev').on('click', function () {
        toySlider.goToPrevSlide();
    });
}


// 슬라이드 장난감 클릭시 트리 이미지에 동적 생성
function createToy() {

    var $toyItem = $('.toys').find('img');
    var src = '/Images/Christmas2015/toy/';
    var maxToyLen = 6;

    $toyItem.each(function () {
        $(this).on('click', function () {
            var alt = $(this).attr('alt');
            var $newToy = $('<img>');
            var $field = $('.tree-area__inner');

            // 클릭한 이미지가 blank 이미지 이거나 갯수가 7개가 넘으면 새로운 이미지를 생성하지 않는다.
            if (checkFieldFToyLen($field) > maxToyLen) {
                alert('최소 3개에서 최대 7까지 장난감 선택이 가능합니다!');
                return;
            }

            // 클릭한 이미지는 숨김
            $(this).css('visibility', 'hidden');

            // 클릭한 이미지를 트리 이미지 위에 새로 생성한다
            $newToy.attr('src', src + alt + '.png')
                    .attr('class', 'toyItem')
                     .attr('id', alt);

            $field.append($newToy);

            $('.toyItem').draggable({ containment: $field });

        });
    });
}

createToy();


// 페이지 팝업
$('.c-tab').on('click', 'a', function (e) {
    e.preventDefault();
    var hash = this.hash;
    var $content = $('.js-content');
    
    $(this).addClass('on').parent().siblings().find('a').removeClass('on');

    $content.removeClass('on');
    $(hash).addClass('on');

    // display 가 none 였다가 block 가 되었을때
    // 높이값을 잡지 못해 이미지가 보이지 않아서
    // 이벤트2 클릭시 bxslider 생성하도록 함
    // 이벤트2를 먼저 실행하도록 되어서 주석처리함
    /*
    if (hash === '#event2') {
       // makeSlider();    
    }
    */
    //alert(hash);
});

// 당첨자 팝업 tab
$('.pop-winner__item').on('click', 'a', function (e) {
    e.preventDefault();

    $(this).parent().addClass('on').siblings().removeClass('on');
});

// 팝업 닫기
$('.js-chClose').on('click', function () {
    var popName = $(this).data('pop');
    var prefix = '.pop-';
    var $treeArea = $('.pop-tree');

    closePop(prefix + popName);
   // alert(popName);
    if (popName !== 'post') {
        closePop('.dim');
    }

    // 개인정보 입력 팝업에서 닫기 버튼 을 눌렀을때
    // 트리만들기 팝업 띄우기
    if (popName === 'userInfo') {

        $treeArea.addClass('on');
    } else if (popName === 'sns') {

        location.href = location.pathname;
    }
});

// 경품안내
$('.js-gift').on('click', function (e) {
    e.preventDefault();
    openPop('.pop-winner');
    gotoTop();
});

//// 트리만들기 시작 버튼
//// 사용자 정보 팝업 띄움(연락처, 이름 팝업)
//$('.pop-tree__btn').on('click', function (e) {
//    e.preventDefault();
//    if (isInapp) {
//        openPop('.popup-inapp');
//    } else {
//        var $popup = $('.pop-tree');
//        var $userInfo = $('.pop-userInfo');
//        closePop('.pop-tree');
//        openPop('.pop-userInfo');
//    }    
//});

// 플레이스 홀더
$('.js-phone').placeholder({
    text: '-없이 입력해주세요'
});

$('.pop-card__item').eq(2).on('click', function () {
    $(this).find('input[type="radio"]').attr('checked', true);
    $(this).find('.js-txt').focus();
});

$('.js-txt').css('background', 'url(/Images/Christmas2015/popup/txt-card3.png) no-repeat 0 0');
$('.js-txt').on({
    click: function () {
        $(this).css('background', '');
        $('#cardTxt3').attr('checked', true);
    },
    focus: function () {
        $(this).css('background', '');
        $('#cardTxt3').attr('checked', true);
    },
    blur: function () {
        if ($(this).val() === '') {
            $(this).css('background', 'url(/Images/Christmas2015/popup/txt-card3.png) no-repeat 0 0');
        }
    }
});

$('.label-box3').on('click', function () {
    $('.js-txt').focus();
});