window.name = "kmcis_web";
var kmcOpenFlag = 0;
function openKMCISWindow() {

    var kmcWindowTarget = 'KMCISWindow' + kmcOpenFlag;
    kmcOpenFlag += 1;
    var KMCIS_window = window.open('', kmcWindowTarget, 'width=425, height=550, resizable=0, scrollbars=no, status=0, titlebar=0, toolbar=0, left=435, top=250');
    if (KMCIS_window == null) {
        alert(" ※ 윈도우 XP SP2 또는 인터넷 익스플로러 7 사용자일 경우에는 \n    화면 상단에 있는 팝업 차단 알림줄을 클릭하여 팝업을 허용해 주시기 바랍니다. \n\n※ MSN,야후,구글 팝업 차단 툴바가 설치된 경우 팝업허용을 해주시기 바랍니다.");
    }

    $('#reqMarketingKMCISForm')[0].action = 'https://www.kmcert.com/kmcis/web/kmcisReq.jsp';
    $('#reqMarketingKMCISForm')[0].target = kmcWindowTarget;
    $('#reqMarketingKMCISForm')[0].submit();
}

function setFinancialConcertMarketingKmcData(data) {
    $('#FinancialConcertMarketingAgreeCreateModel_Name').val(data.Name);
    $('#FinancialConcertMarketingAgreeCreateModel_Mobile').val(data.Mobile);
    $('#FinancialConcertMarketingAgreeCreateModel_BirthDay').val(data.BirthDay);
    if (data.Gender == "남") {
        $("#marketing-male").prop("checked", true);
    } else {
        $("#marketing-female").prop("checked", true);
    }
    $('#kmcArea').hide();
}

var financialConcertMarketingEventModel = new Vue({
    el: '#financial-concert-marketing-event',
    data: {
        turn: 3
    },
    methods: {
        beginEntry: function () {
            $('.loading').show();
        },
        completeEntry: function () {
            $('.loading').hide();
        },
        saveMarkeitngEntry: function (res) {
            $('.loading').hide();
            if (res.Result === true) {
                alert('참여가 완료되었습니다.');
                $('#frmFinancialConcertMarketingAgreeEntry').clearForm();
                $('#FinancialConcertMarketingAgreeCreateModel_ApplicationTurn').val(0);
                $('#kmcArea').show();
            } else {
                alert(res.Message);
            }
        },
        failureEntry: function (xhr, err) {
            $('.loading').hide();
            alert('다시 시도해주세요.');
        },
        openPostCode: function () {
            closePop('marketing');
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
                    $('#FinancialConcertMarketingAgreeCreateModel_ZipCode').val(data.zonecode);
                    $('#FinancialConcertMarketingAgreeCreateModel_Address').val(fullAddr);

                    // iframe을 넣은 element를 안보이게 한다.
                    closePop('post', false);
                    openPop('marketing');

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
        },
        closePostCode: function () {
            closePop('post', false);
            openPop('marketing');
        },
        checkApplicationTurn: function (ele) {
            var availableTurn = true;
            switch (parseInt(ele.currentTarget.value)) {
                case 1:
                    if (this.turn < 3) {
                        availableTurn = false;
                    }
                    break;
                case 2:
                    if (this.turn < 2) {
                        availableTurn = false;
                    }
                    break;
                case 3:
                    if (this.turn < 1) {
                        availableTurn = false;
                    }
                    break;
                default:
                    availableTurn = true;
                    break;
            }
            if (!availableTurn) {
                alert('해당 회차는 이미 종료되었습니다.');
                ele.currentTarget.value = 0;
            }
        }
    }
});

