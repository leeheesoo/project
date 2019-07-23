//레이어팝업 열기
function openPopup(param) {
    var wrapH = $('#wrap').height();
    $('#png_bg').css('height', wrapH);
    $('#png_bg').show();
    $('#' + param).show();
    $('html,body').animate({ scrollTop: 2840 });

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
    $('html,body').animate({ scrollTop: 150 });
};

//레이어팝업 닫기
function closePop(param) {
    $('#png_bg').hide();
    $('#' + param).hide();

    //개인정보입력창 닫기
    if (param == 'pop_entry') {
        $('#png_bg').hide();
        $('#pop_entry').hide();
        //$('.select_dim').show();

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
}

/* **************************************************
    메인 스크롤 제어
************************************************** */

//전역변수
var scrollReady = true;

//현재영역의 번호저장 전역변수
var thisParam = 0;

window.onload = function () {
    $('#navi li.navion img').each(function () {
        $(this).mouseover(function () {
            $(this).attr('src', "/Images/ChildrensDay/" + this.id + "on.png");
        });
        $(this).mouseout(function () {
            menuOut();
        });
    });
}

function menuOut() {
    $('#navi li.navion img').each(function () {
        this.src = this.src.replace('on.png', '.png');
    });
    $('#menu0' + thisParam).attr('src', "/Images/ChildrensDay/menu0" + thisParam + "on.png");
}

function scrollMov() {
    var scrollTop = $(window).scrollTop();

    if (scrollTop < 2300) {
        thisParam = 1;
        menuOut();
    }
    else if (scrollTop > 2400 && scrollTop < 4652) {
        thisParam = 2;
        menuOut();
    }
}

function goArea(flag) {
    switch (flag) {
        case 1:
            goPosition(0);
            break;
        case 2:
            goPosition(2460);
            break;
        case 3:
            openPopup2('pop_gift');
            break;
    }
    thisParam = flag;
    menuOut;
}

function goPosition(to) {
    scrollReady = false;
    $("html, body").animate(
        { scrollTop: to },
        200,
        'easeInOutExpo',
        function () { scrollReady = true; }
    );
}

$(function () {
    var scrollTop = $(window).scrollTop();

    $('#navi li a').focus(function () {
        this.blur();
    });

    $(window).bind('scroll', function () {
        scrollMov();
    });
});

/* **************************************************
    // 메인 스크롤 제어
************************************************** */




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

    $(".correct_img").empty();
    $(".all_img").empty();
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
        "/Images/ChildrensDay/tl_hidden_pic_boy1.png", "/Images/ChildrensDay/tl_hidden_pic_boy2.png", "/Images/ChildrensDay/tl_hidden_pic_boy3.png", "/Images/ChildrensDay/tl_hidden_pic_boy4.png", "/Images/ChildrensDay/tl_hidden_pic_boy5.png", "/Images/ChildrensDay/tl_hidden_pic_boy6.png", "/Images/ChildrensDay/tl_hidden_pic_boy7.png", "/Images/ChildrensDay/tl_hidden_pic_boy8.png");
    // 여아용
    var img_rnd_girl = new Array(
        "/Images/ChildrensDay/tl_hidden_pic_girl1.png", "/Images/ChildrensDay/tl_hidden_pic_girl2.png", "/Images/ChildrensDay/tl_hidden_pic_girl3.png", "/Images/ChildrensDay/tl_hidden_pic_girl4.png", "/Images/ChildrensDay/tl_hidden_pic_girl5.png", "/Images/ChildrensDay/tl_hidden_pic_girl6.png");

    // 변수 i에 랜덤 메소드를 돌려서 나온 값을 넣는다.
    i_boy = Math.floor(Math.random() * img_rnd_boy.length);// 남아용
    i_girl = Math.floor(Math.random() * img_rnd_girl.length);// 여아용

    // document에 기술해준다. imgrd의 array에서 i_girl번째 값을 불러오게 될 것이다.
    document.getElementById("tlRandomBoy").src = img_rnd_boy[i_boy];// 여아용
    document.getElementById("tlRandomGirl").src = img_rnd_girl[i_girl];// 여아용

    //게임 초기화
    init_game();
    $(".all_img").empty();
    $(".all_img").append("<img src='/Images/ChildrensDay/hidden_pic_" + param + ".png' id='diff_img_" + param + "' alt=''>");

    // alert(param);
    var $container = $("#event_wrap");

    /*
    $container.on('click', "#diff_img_" + param, function (event) {

        //alert('click ///////////////////////////////////// ' + $(this).attr('id') );
        var off_x = event.offsetX;
        var off_y = event.offsetY;

        if (param == 'boy') {
            if (i_boy == 0) {
                if (off_x > 508 && off_x < 755 && off_y > 664 && off_y < 823) {
                    //showCorrect(param);
                    viewModel.gender('남아용');
                    openPopup('pop_entry');
                } else {
                    showIncorrect(off_x, off_y);
                }
            } else if (i_boy == 1) {
                if (off_x > 274 && off_x < 466 && off_y > 653 && off_y < 793) {
                    //showCorrect(param);
                    viewModel.gender('남아용');
                    openPopup('pop_entry');
                } else {
                    showIncorrect(off_x, off_y);
                }
            } else if (i_boy == 2) {
                if (off_x > 42 && off_x < 246 && off_y > 642 && off_y < 764) {
                    //showCorrect(param);
                    viewModel.gender('남아용');
                    openPopup('pop_entry');
                } else {
                    showIncorrect(off_x, off_y);
                }
            } else if (i_boy == 3) {
                if (off_x > 202 && off_x < 355 && off_y > 493 && off_y < 613) {
                    //showCorrect(param);
                    viewModel.gender('남아용');
                    openPopup('pop_entry');
                } else {
                    showIncorrect(off_x, off_y);
                }
            } else if (i_boy == 4) {
                if (off_x > 469 && off_x < 627 && off_y > 514 && off_y < 638) {
                    //showCorrect(param);
                    viewModel.gender('남아용');
                    openPopup('pop_entry');
                } else {
                    showIncorrect(off_x, off_y);
                }

            } else if (i_boy == 5) {
                if (off_x > 718 && off_x < 866 && off_y > 520 && off_y < 641) {
                    //showCorrect(param);
                    viewModel.gender('남아용');
                    openPopup('pop_entry');
                } else {
                    showIncorrect(off_x, off_y);
                }
            } else if (i_boy == 6) {
                if (off_x > 391 && off_x < 527 && off_y > 404 && off_y < 493) {
                    //showCorrect(param);
                    viewModel.gender('남아용');
                    openPopup('pop_entry');
                } else {
                    showIncorrect(off_x, off_y);
                }
            } else if (i_boy == 7) {
                if (off_x > 104 && off_x < 222 && off_y > 401 && off_y < 486) {
                    //showCorrect(param);
                    viewModel.gender('남아용');
                    openPopup('pop_entry');
                } else {
                    showIncorrect(off_x, off_y);
                }
            }
            //if (off_x > 447 && off_x < 795 && off_y > 570 && off_y < 793) {
            //    showCorrect(param);
            //    viewModel.gender('남아용');
            //    openPopup('pop_entry');
            //} else {
            //    showIncorrect( off_x, off_y );               
            //}
        }


        if (param == 'girl') {  
            if (i_girl == 0) {
                console.log(i_girl);
                //alert(1);
                if (off_x > 424 && off_x < 579 && off_y > 363 && off_y < 676) {
                    //showCorrect(param);
                    viewModel.gender('여아용');
                    openPopup('pop_entry');
                } else {
                    showIncorrect(off_x, off_y);
                }
            } else if (i_girl == 1) {
                console.log(i_girl);
                // alert(2);
                if (off_x > 246 && off_x < 413 && off_y > 343 && off_y < 468) {
                    //showCorrect(param);
                    viewModel.gender('여아용');
                    openPopup('pop_entry');
                } else {
                    showIncorrect(off_x, off_y);
                }
            } else if (i_girl == 2) {
                console.log(i_girl);
                // alert(3);
                if (off_x > 755 && off_x < 893 && off_y > 406 && off_y < 669) {
                    //showCorrect(param);
                    viewModel.gender('여아용');
                    openPopup('pop_entry');
                } else {
                    showIncorrect(off_x, off_y);
                }
            } else if (i_girl == 3) {
                console.log(i_girl);
                //alert(4);
                if (off_x > 6 && off_x < 125 && off_y > 381 && off_y < 621) {
                    //showCorrect(param);
                    viewModel.gender('여아용');
                    openPopup('pop_entry');
                } else if (off_x > 125 && off_x < 256 && off_y > 381 && off_y < 417) {
                    viewModel.gender('여아용');
                    openPopup('pop_entry');
                } else {
                    showIncorrect(off_x, off_y);
                }
            } else if (i_girl == 4) {
                console.log(i_girl);
                //alert(5);
                if (off_x > 157 && off_x < 312 && off_y > 686 && off_y < 845) {
                    //showCorrect(param);
                    viewModel.gender('여아용');
                    openPopup('pop_entry');
                } else {
                    showIncorrect(off_x, off_y);
                }
               
            } else if (i_girl == 5) {
                console.log(i_girl);
                //alert(6);
                if (off_x > 404 && off_x < 767 && off_y > 706 && off_y < 806) {
                    //showCorrect(param);
                    viewModel.gender('여아용');
                    openPopup('pop_entry');
                } else {
                    showIncorrect(off_x, off_y);
                }
            }
            //if (off_x > 424 && off_x < 579 && off_y > 363 && off_y < 676) {
            //    showCorrect(param);
            //    viewModel.gender('여아용');
            //    openPopup('pop_entry');
            //} else {
            //    showIncorrect(off_x, off_y);
            //}
        }

    });    */
}

function showCorrect(param) {
    //alert('showCorrect>> ' + param);
    Hit_Correct++;
    $(".correct_img").css('z-index', '1');
    $(".correct_img").append("<div class='correct_img_pos'><img src='/Images/ChildrensDay/correct_img_" + param + ".png' alt=''></div>");
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
            if (off_x > 508 && off_x < 755 && off_y > 664 && off_y < 823) {
                //showCorrect(param);
                viewModel.gender('남아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_boy == 1) {
            if (off_x > 274 && off_x < 466 && off_y > 653 && off_y < 793) {
                //showCorrect(param);
                viewModel.gender('남아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_boy == 2) {
            if (off_x > 42 && off_x < 246 && off_y > 642 && off_y < 764) {
                //showCorrect(param);
                viewModel.gender('남아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_boy == 3) {
            if (off_x > 202 && off_x < 355 && off_y > 493 && off_y < 613) {
                //showCorrect(param);
                viewModel.gender('남아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_boy == 4) {
            if (off_x > 469 && off_x < 627 && off_y > 514 && off_y < 638) {
                //showCorrect(param);
                viewModel.gender('남아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }

        } else if (i_boy == 5) {
            if (off_x > 718 && off_x < 866 && off_y > 520 && off_y < 641) {
                //showCorrect(param);
                viewModel.gender('남아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_boy == 6) {
            if (off_x > 391 && off_x < 527 && off_y > 404 && off_y < 493) {
                //showCorrect(param);
                viewModel.gender('남아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_boy == 7) {
            if (off_x > 104 && off_x < 222 && off_y > 401 && off_y < 486) {
                //showCorrect(param);
                viewModel.gender('남아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        }
        //if (off_x > 447 && off_x < 795 && off_y > 570 && off_y < 793) {
        //    showCorrect(param);
        //    viewModel.gender('남아용');
        //    openPopup('pop_entry');
        //} else {
        //    showIncorrect( off_x, off_y );               
        //}
    }

    if (param == 'girl') {
        if (i_girl == 0) {
            console.log(i_girl);
            //alert(1);
            if (off_x > 424 && off_x < 579 && off_y > 363 && off_y < 676) {
                //showCorrect(param);
                viewModel.gender('여아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_girl == 1) {
            console.log(i_girl);
            // alert(2);
            if (off_x > 246 && off_x < 413 && off_y > 343 && off_y < 468) {
                //showCorrect(param);
                viewModel.gender('여아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_girl == 2) {
            console.log(i_girl);
            // alert(3);
            if (off_x > 755 && off_x < 893 && off_y > 406 && off_y < 669) {
                //showCorrect(param);
                viewModel.gender('여아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_girl == 3) {
            console.log(i_girl);
            //alert(4);
            if (off_x > 6 && off_x < 125 && off_y > 381 && off_y < 621) {
                //showCorrect(param);
                viewModel.gender('여아용');
                openPopup('pop_entry');
            } else if (off_x > 125 && off_x < 256 && off_y > 381 && off_y < 417) {
                viewModel.gender('여아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        } else if (i_girl == 4) {
            console.log(i_girl);
            //alert(5);
            if (off_x > 157 && off_x < 312 && off_y > 686 && off_y < 845) {
                //showCorrect(param);
                viewModel.gender('여아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }

        } else if (i_girl == 5) {
            console.log(i_girl);
            //alert(6);
            if (off_x > 404 && off_x < 767 && off_y > 706 && off_y < 806) {
                //showCorrect(param);
                viewModel.gender('여아용');
                openPopup('pop_entry');
            } else {
                showIncorrect(off_x, off_y);
            }
        }
        //if (off_x > 424 && off_x < 579 && off_y > 363 && off_y < 676) {
        //    showCorrect(param);
        //    viewModel.gender('여아용');
        //    openPopup('pop_entry');
        //} else {
        //    showIncorrect(off_x, off_y);
        //}
    }
});

});



