var game;

$(function () {   
    init();
    //--- for test
    // game.setStep(3);
}())

function init() {
    var $phone = $('.info-phone input');
    // placeholder
    $phone.placeholder({
        type: 'background',
        background: '/Images/MarvelFrozen/common/placeholder-phone.png'
    });
    $phone.css('background-position', '10px 10px');
    
    addEvent();
    game = cardGame({
        container: $('#cardGame'),
        setNum: 6,         
        carSetDataList: [
            { 'name': 'd', 'setNum': '4' },
            { 'name': 'f', 'setNum': '4' },
            { 'name': 'm', 'setNum': '8' }
        ]       
    });    
}

function addEvent() {
    // 당첨 확인
    $('.btn-check-win').click(function (e) {
        alert('당첨확인 기간이 아닙니다. ');
        return false;
    });    
}

// btn events 
// 구매하러 가기
function goBuySite() {
    window.open("http://item2.gmarket.co.kr/Item/DetailView/Item.aspx?goodscode=894651595", '_blank');
    return false;
}

//================================================================
// popup util
//================================================================
function openPopup(stId) {
    $('.popup-dimmed').show();
    var $popup = $('#' + stId);
    $popup.show();
    var tgTop = $(document).scrollTop() + 60;
    $popup.css({
        'top': tgTop
    });    
}

function closePopup(stId) {
    $('.popup-dimmed').hide();
    $('#' + stId).hide();

    $(document).off('scroll');    
}





