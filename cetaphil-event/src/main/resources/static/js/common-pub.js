//---------------------------------------------------
// openPop / closePop
//---------------------------------------------------
function openPopup(flag, dimmedBoolean) {
    var scrollTop = $(window).scrollTop() + 80;
    var $selector = $('#pop-' + flag);
    var dim = dimmedBoolean; 

    if (dimmedBoolean != false) $(".dimmed").show();
    $selector.css('top', scrollTop + 'px').show();
};

function closePopup(flag, dimmedBoolean) {
    var dim = dimmedBoolean; 
    if (dimmedBoolean != false) $(".dimmed").hide();
    $('#pop-' + flag).hide();
    
};


