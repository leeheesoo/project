var isInapp = (navigator.userAgent.toUpperCase().match(/KAKAOTALK|KAKAOSTORY|KAKAO|TWITTER|FB|NAVER|CriOS|GSA/i) ? true : false);

function startEvent(isClosed) {
    if (isClosed == 'Y') {
        alert('이벤트가 종료되었습니다.\n감사합니다^^');
        return;
    }
    if (isInapp) {
        pop.open('inapp'); pop.dimOn();
        return;
    }
    openExt('privateInfo');
    closeExt('extIntro');
}
function successEntry(entry) {
    $('#hidName').val(entry.name);
    $('#hidMobile').val(entry.mobile);
    $('#hidAge').val(entry.age);
    openExt('extQuestion');
    closeExt('privateInfo');
    $("#Loading").hide();
}

function failureEntry(err) {
    alert(err.responseJSON.message);
    $("#Loading").hide();
}

function beginEntry() {
    if ($('#hidGender').val() == '') {
        alert('아이의 성별을 골라주세요');
        return false;
    }

    if ($('#hidQuiz').val() == '') {
        alert('우리 아이에게 필요한 서프라이즈를 선택해주세요');
        return false;
    }
    $("#Loading").show();
}

function successEntry2(data) {
    $('#HidEntryId').val(data);
    $('#gift-result').attr('src', '/Images/MainStream/img_result_type' + $('#hidQuiz').val()  + '.png');

    openExt('extResult');
    closeExt('extQuestion');
    $("#Loading").hide();
}

function searchOtherSurprises() {
    $('#frmQuestion').clearForm();
    $('#hidGender').val('');
    $('#hidQuiz').val('');
    openExt('extQuestion');
    closeExt('extResult');
    $("#Loading").hide();
}
// 페이스북 공유
function shareFacebook() {
    $("#Loading").show();

    var data = {};
    data.name = '[킨더조이] 우리아이 서프라이즈 찾기 EVENT';
    data.picture = 'https://www.kinderjoy.co.kr/Images/MainStream/sns/mainstream_facebook.png';
    data.link = 'https://goo.gl/VFh6k1';
    data.top = 1300;
    data.description = '이벤트 참여하고 킨더조이 받자!';
    facebookShare(data, function (userId, userName, isDefault, userImage, scrapURL) {
        if (isMobile.any()) {
            $('meta[name=viewport]').attr('content', 'width=640, user-scalable=yes');
        }
        if (!scrapURL) {
            $("#Loading").hide();
            return;
        }
        var sns = { snsType: 'facebook', snsId: userId, snsName: userName, nickName: null, scrapURL: scrapURL };
        saveSnsShare(sns);
    }, function () {
        alert("페이스북 인증 후 사용 가능합니다.");
        $("#Loading").hide();
        if (isMobile.any()) {
            $('meta[name=viewport]').attr('content', 'width=640, user-scalable=yes');
        }
    });
}

// 카카오스토리 공유
function shareKakaoStory() {
    var data = {};
    data.link = 'https://www.kinderjoy.co.kr/event/main-stream?utm_source=KakaoStory&utm_medium=KakaoStory_Share&utm_campaign=Main_Stream&_=1';
    data.description = '[킨더조이] 새로운 서프라이즈로 행복한 시간을 만들어보세요!\r\n#킨더조이 #서프라이즈\r\n우리아이 서프라이즈 찾기 EVENT! 1,000분께는 킨더조이를 드립니다!\r\n지금 참여하기 >> https://goo.gl/8CyMoE';

    kakaostoryShareVideo(data, function (userId, nickName, scrapURL) {
        var sns = { snsType: 'kakaostory', snsId: userId, snsName: nickName, nickName: nickName, scrapURL: scrapURL };
        saveSnsShare(sns);

    }, function (e) {
        alert(e.responseJSON.message);
        $("#Loading").hide();
    });
}

// 카카오톡 공유
function shareKakaoTalk() {
    $("#Loading").show();
    var data = {};
    data.btnText = '킨더조이 받기';
    data.picture = 'https://www.kinderjoy.co.kr/Images/MainStream/sns/mainstream_kakaotalk.png';
    data.link = 'https://www.kinderjoy.co.kr/event/main-stream?utm_source=KaKao&utm_medium=KaKao_Share&utm_campaign=Main_Stream';
    data.description = '[킨더조이] 킨더조이의 새로운 서프라이즈로 행복한 시간을 만들어보세요!! #킨더조이 #서프라이즈\r\n\r\n우리아이 서프라이즈 찾기 EVENT! 1,000분께는 킨더조이를 드립니다!\r\nhttps://goo.gl/IIeKhH';

    kakaotalkShare(data, function () {
        var sns = { snsType: 'kakaotalk', userId: null, userName: null, nickName: null, scrapURL: null };
        saveSnsShare(sns);
    }, function () {
        alert('모바일에서 참여가 가능합니다 ^^');
        $("#Loading").hide();
    });
}

// sns 공유 저장
function saveSnsShare(result) {
    $("#Loading").show();
    $.ajax({
        type: 'post',
        cache: false,
        async: false,
        dataType: 'json',
        url: '/api/main-stream/create-sns',
        data: {
            MainStreamSurpriseId: $('#HidEntryId').val(),
            SNSType: result.snsType,
            SNSId: result.snsId,
            SNSName: result.snsName,
            SNSNickName: result.nickName,
            ScrapURL: result.scrapURL
        },
        success: function () {
            $("#Loading").hide();
            alert('완료되었습니다!\n다른채널도 이용해주세요.');
            if (isMobile.any()) {
                $('meta[name=viewport]').attr('content', 'width=640, user-scalable=yes');
            }
        },
        error: function (data, textStatus, jqXHR) {
            $("#Loading").hide();
            if (textStatus != 'abort') {
                alert(data.responseJSON.message);
            }
            if (isMobile.any()) {
                $('meta[name=viewport]').attr('content', 'width=640, user-scalable=yes');
            }
        }
    });
}

$(function () {

    $('.radio_Gentder').click(function (e) {
        $('#hidGender').val($(this).attr('data-bind'));
    });

    $('.radio_Quiz').click(function (e) {
        $('#hidQuiz').val($(this).attr('data-bind'));
    });

});