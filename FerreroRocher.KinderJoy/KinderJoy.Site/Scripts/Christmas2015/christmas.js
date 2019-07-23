//닫기
function closePop(name) {
    var $dim = $('.js-chDim');

    $(name).removeClass('on');

    if (name === '.pop-userInfo' || name === '.pop-prize') {
        $dim.removeClass('on');
    }

}

//열기
function openPop(name) {
    var $dim = $('.js-chDim');

    $(name).addClass('on');
    $dim.addClass('on');
}

// 필드의 이미지 갯수 추출
function checkFieldFToyLen(field) {
    var $f = $(field);
    var $imgs = $f.find('img');

    return $imgs.size();

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


// 따라다니는 네비게이션
var $stick = $('.stick');
var stickLeft = $stick.offset().left;

function flowNav() {
    var winW = $(window).width();

    var standX = 1900;
    var currX;

    if (winW >= standX) {
        currX = stickLeft + ((winW - standX) / 2);
    } else if (winW > 1150 && winW < standX) {

        currX = stickLeft - ((standX - winW) / 2);
    } else if (winW <= 1150) {
        currX = 1076;
    }

    $stick.css({
        left: currX + 'px'
    });
}

// 해당 섹션으로 이동
function gotoSection(val) {
    $('html, body').animate({
        scrollTop: val
    });
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

    ctx.drawImage(toy, currX, currY);
/*
    var $img = $('<img>');
    $img.css({
        position: 'fixed',
        top: 100,
        left: 0
    }).attr('src', canvas.toDataURL('image/png'));

    $('body').append($img);
    */

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
        makeImage(resultCanvas, treeId, selectImgs,resultContent);
    };
    /* 크리스마스 카드 문구 합성 END */
}

//canvas에 텍스트 그리기
function wrapText(context, text, x, y, maxWidth, lineHeight) {
    var words = text.split('');
    var line = '';

    var textWidth = context.measureText(text).width;
    if (textWidth > maxWidth) {        
        y = y - ((lineHeight/2) * (Math.ceil(textWidth / maxWidth) - 1));
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
    if (checkFieldFToyLen('.tree-area__inner') >= smallToyLen) {
        openPop('.pop-card');

    } else {
        alert('최소 3개에서 최대 7까지 장난감 선택이 가능합니다!');
    }
}
function drawTree(treeId, selectImgs) {
    var $tree = $('.tree-area__inner');
    var $imgs = $tree.find('img');
    $imgs.each(function () {
        drawImg($(this).attr('id'));
    });
    drawLargeImg(canvas, treeId, selectImgs);
}
$(function () {
    // 로딩된후 화면 사이즈에 맞게 stick 위치 설정
    flowNav();
    createToy();
});

$(window).scroll(function () {
    flowNav();

    // 스크롤시 오른쪽 따라다니는 메뉴
    // 현재 위치 표시하도록 
    var windowPos = $(window).scrollTop();
    var aArray = ['#visual', '#event1', '#event2'];
    var current;
    var $stick = $('.stick');
    var top = 370;
    var middle = 1640;


    if (windowPos == 0 || windowPos < top) {
        current = aArray[0];
    } else if (windowPos >= top && windowPos < middle) {
        current = aArray[1];
    } else if (windowPos >= middle) {
        current = aArray[2];
    }

    $stick.find('a').removeClass('on');
    $('a[href="' + current + '"]').addClass('on');
    // console.log(windowPos);

});

$(window).resize(function () {
    flowNav();
});

