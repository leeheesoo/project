// 모바일 체크 (true면 모바일)
var mobileCheck = $('.mobile_wrap').length;

// 팝업 여닫기 (경품안내/당첨자발표 팝업 제어)
function openPop(flag, selector) {
    $('.popup').css({
        top: $(window).scrollTop()+90 // X 버튼 위치때문에 팝업컨텐츠는 약간 아래로 띄움
    });
    $('.popup_dimmed').height($(document).height()).show();
    $('#' + flag).show();
    if (selector != ''){
        controlBenefitTab(selector);    
    }
}
function closePop(flag) {
    $('.popup_dimmed').hide();
    $('#' + flag).hide();
}

// 경품/당첨자팝업에서 탭 제어
function controlBenefitTab(selector) {
    if (selector == 'gift') {
        $('.pop_tab a').eq(0).addClass('active');
        $('.pop_tab a').eq(1).removeClass('active');
        $('.gift_group').show();
        $('.dang_group').hide();
    } else if (selector == 'dang') {
        //alert('당첨자 발표 내역이 없습니다.');
        //return;
        $('.pop_tab a').eq(0).removeClass('active');
        $('.pop_tab a').eq(1).addClass('active');
        $('.gift_group').hide();
        $('.dang_group').show();
    }
}


// ==================================================
// 이벤트 게임 영역 함수 관련 
// ==================================================

var selectedCharacter = ''; // 선택된 케릭터 변수
var playCheckBB = 0; // 게임이 재실행인지 체크를 위한 함수(for bxslider)
var playCheckNJ = 0; 
var bbSlider, njSlider; // bxslider

// 스텝1: 케릭터 선택
function selectChar(flag) {
    selectedCharacter = flag;
    $('.select_char').hide();
    $('.personal_info').show();
}

// 스텝2: 스텝3에서 진행할 스테이지 세팅
function playGame() {
    $('.personal_info').hide();
    $('.game_result_share').hide();
    $('.game_stage').show();
    $('.game_stage>div').hide();
    $('.stage_' + selectedCharacter).show();
    $('.stage_cover').show();

    if (mobileCheck){ // 모바일이면
        var bxOption = {
            pager: false,
            moveSlides: 2,
            slideWidth: 105,
            minSlides: 2,
            maxSlides: 2,
            slideMargin: 0,
            infiniteLoop: false,
            auto: false
        };   
    }else{
        var bxOption = {
            pager: false,
            moveSlides: 4,
            slideWidth: 130,
            minSlides: 4,
            maxSlides: 4,
            slideMargin: 0,
            infiniteLoop: false,
            auto: false
        };
    }


    if (selectedCharacter == 'bb') {
        if (playCheckBB == 0) {
            playCheckBB = 1;
            bbSlider = $('.stage_bb .toy_bxslider1, .stage_bb .toy_bxslider2 , .stage_bb .toy_bxslider3').bxSlider(bxOption);
        } else {
            bbSlider.reLoadSlider(bxOption);
        }

    } else if (selectedCharacter == 'nj') {
        if (playCheckNJ == 0) {
            playCheckNJ = 1;
            njSlider = $('.stage_nj .toy_bxslider1, .stage_nj .toy_bxslider2 , .stage_nj .toy_bxslider3').bxSlider(bxOption);
        } else {
            njSlider.reLoadSlider(bxOption);
        }
    }

}

// 스텝3: 인형 옷 바꾸기
//function changeSuite(stCharacter, stSuitNum) {
    //var suitList = stSuitNum.split(',');    
    //var imgUrl = '/Images/NinjaBarbie2016/w/step3/resize/' + stCharacter;
    //var $imgCont = $('.mix_result .back');

    //// top
    //if (suitList[0] != 0) {
    //    $('.mix_result .back .toy_top img').attr('src', imgUrl + '_top_' + suitList[0] + '.png');
    //}
    //// bottom
    //if (suitList[1] != 0) {
    //    $('.mix_result .back .toy_bottom img').attr('src', imgUrl + '_bottom_' + suitList[1] + '.png');
    //}
    //// acc
    //if (suitList[2] != 0) {
    //    $('.mix_result .back .toy_acc img').attr('src', imgUrl + '_acc_' + suitList[2] + '.png');
    //}
//}

// 결과
function goResult() {
    $('.game_stage').hide();
    $('.game_result_share').show();
}

// 게임 리셋
//function suitReset() {
    //if (selectedCharacter == 'bb') {
    //    changeSuite(selectedCharacter, '2,4,1');
    //    $('.stage_bb .mix_default_set ul li input').eq(0).prop('checked', true);
    //} else {
    //    changeSuite(selectedCharacter, '2,7,4');
    //    $('.stage_nj .mix_default_set ul li input').eq(0).prop('checked', true);
    //}
//}

// 스텝4: 처음 화면으로
function goIntro() {
    $('.game_result_share').hide();
    $('.select_char').show();
}

$(function () {

    // 당첨자 발표
    openPop('pop_benefit', 'dang');

    if (!mobileCheck) { // 웹이면
        // 크리스마스팩 팝업버튼 효과
        $('.btn_xmas').animate({ top: -10 }, 600).animate({ top: -20 }, 600);
        setInterval(function () {
            $('.btn_xmas').animate({ top: -10 }, 600).animate({ top: -20 }, 600);
        }, 1200);
    } else {
        var intervalCount = 0;
        // 크리스마스팩 팝업버튼 효과
        $('.btn_xmas').animate({ top: 8 }, 500).animate({ top: 4 }, 500);
        var mobileInterval = setInterval(function () {
            $('.btn_xmas').animate({ top: 8 }, 500).animate({ top: 4 }, 500);
            intervalCount++;
            if (intervalCount >= 20) {
                clearInterval(mobileInterval);
            }
        }, 1000);


    }

    //// 바비세트
    //var setBBList = [
    //    '2,4,1',
    //    '1,5,1',
    //    '6,3,3',
    //    '5,2,0',
    //    '8,6,2',
    //    '4,1,4',
    //    '3,8,0',
    //    '7,7,0'
    //];
    //// 닌자세트
    //var setNJList = [
    //    '2,7,4',
    //    '1,1,2',
    //    '3,3,6',
    //    '4,2,7',
    //    '6,4,3',
    //    '5,6,5',
    //    '7,5,1'
    //    //'7,7,0'
    //];
    //// 기본세트 선택시 작동
    //var $setList = $('.mix_default_set ul li input');
    //$setList.on('click', function () {    
    //    var idList = $(this).attr('id').split('-');
    //    var stCharacter = idList[0];
    //    var idx = idList[1].substr(3, 1) - 1;
    //    if (stCharacter == 'bb') {
    //        changeSuite(stCharacter, setBBList[idx]);
    //    } else {
    //        changeSuite(stCharacter, setNJList[idx]);
    //    }
    //    $('.stage_cover').fadeOut(); // 세트를 먼저 선택해야 슬라이더 선택 가능
    //});
   

    // 웹, 모바일일때 다르게 넣어줄 부분..
    if (mobileCheck) {
        var phSrc =  'm';
        var phCss = '15px 16px';
    } else {
        var phSrc = 'w';
        var phCss = '15px 12px';
    }

    $('.ph_phone').placeholder({
        type: 'background',
        background: '/Images/NinjaBarbie2016/' + phSrc + '/step2/ph_phone.jpg'
    });
    $('.ph_phone').css('background-position', phCss);

    $('.ph_age').placeholder({
        type: 'background',
        background: '/Images/NinjaBarbie2016/' + phSrc + '/step2/ph_age.jpg'
    });
    $('.ph_age').css('background-position', phCss);
    
});