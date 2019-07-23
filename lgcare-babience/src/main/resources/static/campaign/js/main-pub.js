$(function () {
    var $gnbLi=$('.kabrita__gnb li');
    $gnbLi.mouseover(function () {
        $(this).stop().animate({ 'top': '-25px' },500);
    }).mouseleave(function () {
        $(this).stop().animate({ 'top': 0 },300);
    });
})

