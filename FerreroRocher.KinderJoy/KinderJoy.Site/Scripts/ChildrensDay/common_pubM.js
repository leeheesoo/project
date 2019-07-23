//레이어팝업 열기
function openPopup(param) {
    var wrapH = $('#wrap').height();
    $('#png_bg').css('height', wrapH);
    $('#png_bg').show();
    $('#' + param).show();
    $('html,body').animate({ scrollTop: 1607 });

    if (param == 'pop_post') {
        $('#pop_entry').hide();
        $('#' + param).show();
    }
};

//레이어팝업 열기 (상단)
function openPopup2(param) {
    var wrapH = $('#wrap').height();
    $('#png_bg').css('height', wrapH);
    $('#png_bg').show();
    $('#' + param).show();
    $('html,body').animate({ scrollTop: 0 });
};

// 인앱 팝업 열기
function openInapp() {
    var wrapH = $('#wrap').height();
    $('#png_bg2').css('height', wrapH);
    $('#png_bg2').show();
    $('#pop_inapp').show();
    $('html, body').animate({ scrollTop: 0 });
}


//레이어팝업 닫기
function closePop(param) {
    $('#png_bg').hide();
    $('#' + param).hide();


    //개인정보입력창 닫기
    if (param == 'pop_entry') {
        $('#png_bg').hide();
        $('#pop_entry').hide();
        $('.select_dim').show();

        if (viewModel.isGender() == '남아용') {
            find_wrongPic('boy');
        } else {
            find_wrongPic('girl');
        }
    }

    //우편번호팝업 닫기
    if (param == 'pop_post') {
        $('#pop_entry').show();
        $('#' + param).hide();
        $('#png_bg').show();
    }

    if (param == 'pop_sns') {
        goBack(param);
    }

    if (param == 'pop_inapp') {
        $('#png_bg2').hide();
    }
}


// 모바일 메인 탭 제어
function mobTab(tab) {
    $('#mobTab01').attr('src', "/Images/ChildrensDay/m/mob_tab01.png");
    $('#mobTab02').attr('src', "/Images/ChildrensDay/m/mob_tab02.png");
    $('#mobTab03').attr('src', "/Images/ChildrensDay/m/mob_tab03.png");

    var src = $('#mobTab0' + tab).attr('src');
    $('#mobTab0' + tab).attr('src', src.replace('.png', 'on.png'));

    if (tab == 1) {
        $('.event_con').show();
        $('.new_toy_con').hide();
        $('.package_con').hide();
    } else if (tab == 2) {
        $('.event_con').hide();
        $('.new_toy_con').show();
        $('.package_con').hide();
    } else if (tab == 3) {
        $('.event_con').hide();
        $('.new_toy_con').hide();
        $('.package_con').show();
    }
}


/* **************************************************
    메인 숨은그림찾기
************************************************** */

var Hit_Correct = 0;	//정답클릭수
var Hit_inCorrect = 0;	//오답클릭수

// 변수 i에 랜덤 메소드를 돌려서 나온 값을 넣는다.
var i_girl;// 여아용
var i_boy;// 남아용

var param;


//게임초기화
function init_game() {
    // 데이터 초기화
    $(document).data("WrongPictureGameId", "");

    $(".all_img").empty();
    $(".correct_img").empty();
    $(".correct_img").css('z-index', '-1');
    Hit_Correct = 0;
    Hit_inCorrect = 0;
}

function find_wrongPic(param) {
    this.param = param;
    $('.select_dim').hide();

    //openPopup('pop_entry');

    $('.hidden_pic_' + param).show();

    // 랜덤하게 보여줄 사진 개체 저장
    // 남아용
    var img_rnd_boy = new Array(
        "/Images/ChildrensDay/m/tl_hidden_pic_boy1.png", "/Images/ChildrensDay/m/tl_hidden_pic_boy2.png", "/Images/ChildrensDay/m/tl_hidden_pic_boy3.png", "/Images/ChildrensDay/m/tl_hidden_pic_boy4.png", "/Images/ChildrensDay/m/tl_hidden_pic_boy5.png", "/Images/ChildrensDay/m/tl_hidden_pic_boy6.png", "/Images/ChildrensDay/m/tl_hidden_pic_boy7.png", "/Images/ChildrensDay/m/tl_hidden_pic_boy8.png");
    // 여아용
    var img_rnd_girl = new Array(
        "/Images/ChildrensDay/m/tl_hidden_pic_girl1.png", "/Images/ChildrensDay/m/tl_hidden_pic_girl2.png", "/Images/ChildrensDay/m/tl_hidden_pic_girl3.png", "/Images/ChildrensDay/m/tl_hidden_pic_girl4.png", "/Images/ChildrensDay/m/tl_hidden_pic_girl5.png", "/Images/ChildrensDay/m/tl_hidden_pic_girl6.png");

    // 변수 i에 랜덤 메소드를 돌려서 나온 값을 넣는다.
    i_boy = Math.floor(Math.random() * img_rnd_boy.length);// 남아용
    i_girl = Math.floor(Math.random() * img_rnd_girl.length);// 여아용

    // document에 기술해준다. imgrd의 array에서 i_girl번째 값을 불러오게 될 것이다.
    document.getElementById("tlRandomBoy").src = img_rnd_boy[i_boy];// 여아용
    document.getElementById("tlRandomGirl").src = img_rnd_girl[i_girl];// 여아용

    init_game();
    $(".all_img").empty();
    $(".all_img").append("<img src='/Images/ChildrensDay/m/hidden_pic_" + param + ".png' id='diff_img_" + param + "' alt='배경이미지' style='position:absolute; top:0; left:0;'>");

    // alert(param);
    //var $container = $(".event_wrap");
    //$container.on('click', "#diff_img_" + param, function (event) {
    //    //alert('click ///////////////////////////////////// ' + $(this).attr('id') );
    //    var off_x = event.offsetX;
    //    var off_y = event.offsetY;
    //    if (param == 'boy') {            
    //        if (off_x > 280 && off_x < 496 && off_y > 357 && off_y < 495) {
    //            showCorrect(param);
    //            viewModel.gender('남아용');
    //            openPopup('pop_entry');
    //        } else {
    //            showIncorrect( off_x, off_y );               
    //        }
    //    }
    //    if (param == 'girl') {
    //        if (off_x > 262 && off_x < 382 && off_y > 226 && off_y < 420) {
    //            showCorrect(param);
    //            viewModel.gender('여아용');
    //            openPopup('pop_entry');
    //        } else {
    //            showIncorrect(off_x, off_y);
    //        }
    //    }
    //});    
}

function showCorrect(param) {
    //alert('showCorrect>> ' + param);
    Hit_Correct++;
    $(".correct_img").css('z-index', '1');
    $(".correct_img").append("<div class='correct_img_pos'><img src='/Images/ChildrensDay/m/correct_img_" + param + ".png' alt=''></div>");
    $(".incorrect_img").hide();
}


function showIncorrect(offsetX, offsetY) {
    //alert('showIncorrect>> ');
    Hit_inCorrect++;
    $(".incorrect_img").show();
    $(".incorrect_img_pos").css({
        top: offsetY - 30,
        left: offsetX - 28
    });
    setTimeout(function () { $(".incorrect_img").hide(); }, 500);

    //정답 찾았을경우 오답표시X
    if (Hit_Correct == 1) {
        $(".incorrect_img").hide();
    }

}

//남아용,여아용 선택 dim으로 돌아가기
function goBack(param) {
    //console.log('back.....');
    $('.hidden_pic_boy').hide();
    $('.hidden_pic_girl').hide();
    $('.select_dim').show();
    init_game();
}

/* **************************************************
    //메인 숨은그림찾기
************************************************** */


$(function () {
//연락처 placeholder
$('.ph_phone').placeholder({
    type: 'background',
    background: '/Images/ChildrensDay/pop/ph_phone.png'
});
$('.ph_phone').css('background-position', '15px 15px');



$(".all_img").on('click', function (event) {
    //alert('click ///////////////////////////////////// ' + $(this).attr('id') );
    var off_x = event.offsetX;
    var off_y = event.offsetY;

    if (param == 'boy') {
        if (i_boy == 0) {
            if (off_x > 319 && off_x < 471 && off_y > 416 && off_y < 514) {
                //showCorrect(param);
                viewModel.gender('남아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_boy == 1) {
            if (off_x > 174 && off_x < 290 && off_y > 409 && off_y < 494) {
                //showCorrect(param);
                viewModel.gender('남아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_boy == 2) {
            if (off_x > 28 && off_x < 155 && off_y > 403 && off_y < 476) {
                //showCorrect(param);
                viewModel.gender('남아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_boy == 3) {
            if (off_x > 128 && off_x < 220 && off_y > 309 && off_y < 383) {
                //showCorrect(param);
                viewModel.gender('남아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_boy == 4) {
            if (off_x > 295 && off_x < 391 && off_y > 322 && off_y < 397) {
                //showCorrect(param);
                viewModel.gender('남아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }

        } else if (i_boy == 5) {
            if (off_x > 453 && off_x < 540 && off_y > 326  && off_y < 397) {
                //showCorrect(param);
                viewModel.gender('남아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_boy == 6) {
            if (off_x > 245 && off_x < 330 && off_y > 255 && off_y < 306) {
                //showCorrect(param);
                viewModel.gender('남아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_boy == 7) {
            if (off_x > 68 && off_x < 138 && off_y > 251 && off_y < 303) {
                //showCorrect(param);
                viewModel.gender('남아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        }
    }


    if (param == 'girl') {
        if (i_girl == 0) {
            console.log(i_girl);
            //alert(1);
            if (off_x > 262 && off_x < 375 && off_y > 232 && off_y < 422) {
                //showCorrect(param);
                viewModel.gender('여아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_girl == 1) {
            console.log(i_girl);
            // alert(2);
            if (off_x > 155 && off_x < 258 && off_y > 216 && off_y < 291) {
                //showCorrect(param);
                viewModel.gender('여아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_girl == 2) {
            console.log(i_girl);
            // alert(3);
            if (off_x > 471 && off_x < 560 && off_y > 255 && off_y < 417) {
                //showCorrect(param);
                viewModel.gender('여아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_girl == 3) {
            console.log(i_girl); 
            //alert(4);
            if (off_x > 4 && off_x < 76 && off_y > 237 && off_y < 387) {
                //showCorrect(param);
                viewModel.gender('여아용');
                openPopup('pop_entry');
            } else if (off_x > 76 && off_x < 138 && off_y > 240 && off_y < 262) {
                viewModel.gender('여아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_girl == 4) {
            console.log(i_girl);
            //alert(5);
            if (off_x > 100 && off_x < 196 && off_y > 430 && off_y < 525) {
                //showCorrect(param);
                viewModel.gender('여아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }

        } else if (i_girl == 5) {
            console.log(i_girl);
            //alert(6);
            if (off_x > 255 && off_x < 481 && off_y > 445 && off_y < 505) {
                //showCorrect(param);
                viewModel.gender('여아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        }
    }
});

});


