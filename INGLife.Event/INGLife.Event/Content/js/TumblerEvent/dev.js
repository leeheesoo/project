var secretBoxEventModel = new Vue({
    el: '#tumblerEvent',
    data: {
        closeEventOneCheck: false,
        closeEventTwoCheck: false
    },
    methods: {
        beginEntry: function () {
            $('.dimm__loading').show();
        },
        completeEntry: function () {
            $('.dimm__loading').hide();
        },
        saveEntry: function (res) {
            $('.dimm__loading').hide();
            if (res.Result === true) {
                alert(res.Message);
                closePop('personal');
                $('#frmorange4050EventModelEntry').clearForm();
                $('.dimm__block').attr('onclick', 'alert("휴대폰 인증을 진행해 주세요");');

            } else {
                //if (!res.IsRequiredAllCheck) {
                //    openPopup('Alert');
                //} else {
                alert(res.Message);
                //}
            }
        },
        failureEntry: function (xhr, err) {
            $('.dimm__loading').hide();
            alert('다시 시도해주세요.');
        },
        isCloseCheck: function () {
            if (eventClose == 'True') {
                alert("해당이벤트는 선착순 마감되었습니다.");
                this.closeCheck = false;
                return false;
            }
            this.closeCheck = true;
        }, openPostCode: function () {
            closePop('personal');
            openPop('post');

            new daum.Postcode({
                oncomplete: function (data) {
                    // 검색결과 항목을 클릭했을때 실행할 코드를 작성하는 부분.

                    // 각 주소의 노출 규칙에 따라 주소를 조합한다.
                    // 내려오는 변수가 값이 없는 경우엔 공백('')값을 가지므로, 이를 참고하여 분기 한다.
                    var fullAddr = data.address; // 최종 주소 변수
                    var extraAddr = ''; // 조합형 주소 변수

                    // 기본 주소가 도로명 타입일때 조합한다.
                    if (data.addressType === 'R') {
                        //법정동명이 있을 경우 추가한다.
                        if (data.bname !== '') {
                            extraAddr += data.bname;
                        }
                        // 건물명이 있을 경우 추가한다.
                        if (data.buildingName !== '') {
                            extraAddr += (extraAddr !== '' ? ', ' + data.buildingName : data.buildingName);
                        }
                        // 조합형주소의 유무에 따라 양쪽에 괄호를 추가하여 최종 주소를 만든다.
                        fullAddr += (extraAddr !== '' ? ' (' + extraAddr + ')' : '');
                    }

                    // 우편번호와 주소 정보를 해당 필드에 넣는다.
                    $('#TumblerCreateModel_ZipCode').val(data.zonecode);
                    $('#TumblerCreateModel_Address').val(fullAddr);

                    // iframe을 넣은 element를 안보이게 한다.
                    closePop('post', false);
                    openPop('personal');

                    // 커서를 상세주소 필드로 이동한다.
                    var postPos = $('.post-wrap').offset().top;
                    $('body,html').animate({ 'scroll-top': postPos });
                    $('#FinancialConcertMarketingAgreeCreateModel_AddressDetail').focus();

                },
                /*
                // 우편번호 찾기 화면 크기가 조정되었을때 실행할 코드를 작성하는 부분. iframe을 넣은 element의 높이값을 조정한다.
                onresize: function (size) {
                    element_wrap.style.height = size.height + 'px';
                },
                */
                width: '100%',
                height: '700px',
                maxSuggestItems: 5
            }).embed(document.getElementById('searchPostCode'));
            return false;
        }, closePostCode: function () {
            closePop('post', false);
            openPop('personal');
        }, openEventPop: function (eventType) {

            if (eventType == 'tumbler' && eventOneClose == 'True') {
                alert("해당이벤트는 선착순 마감되었습니다.");
                this.closeEventOneCheck = false;
                return false;
            }

            if (eventType == 'secret' && eventTwoClose == 'True') {
                alert("해당이벤트는 선착순 마감되었습니다.");
                this.closeEventTwoCheck = false;
                return false;
            }

            $('#frmCreateSecretBoxEntry')[0].reset();
            $('#eventType').val(eventType);
            if (eventType == 'tumbler') {
                $('#kmc-btn').attr('data-action', '텀블러받기 휴대폰 인증');
                $('#submit-btn').attr('data-action', '텀블러받기 입력 완료');
                $('#interest-check').hide();
                $('.address-wrap').hide();
                $('.email-wrap').hide();
                openPop('personal'); return false;
            } else {
                $('#kmc-btn').attr('data-action', '시크릿박스 휴대폰 인증');
                $('#submit-btn').attr('data-action', '시크릿박스 입력 완료');
                $('#interest-check').show();
                $('.address-wrap').hide();
                $('.email-wrap').hide();
                openPop('personal'); return false;
            }
        }
    }
});

function setTumblerKmcData(data) {
    if (data == null) {
        closePop('personal');
    } else {
        $('#TumblerCreateModel_Name').val(data.Name);
        $('#TumblerCreateModel_Mobile').val(data.Mobile);
        $('#TumblerCreateModel_BirthDay').val(data.BirthDay);
        if (data.Gender == "1") {
            $("#female").prop("checked", true);
        } else {
            $("#male").prop("checked", true);
        }
        $('.dimm__block').attr('onclick', '');
    }
}