var fcSharingEventModel = new Vue({
    el: '#fc-sharing-new-event',
    methods: {
        beginEntry:function(){
            $('#loading').show();
        },
        completeEntry:function(){
            $('#loading').hide();
        },
        saveEntry: function (res) {            
            $('#loading').hide();
            if (res.Result == true) {
                var fc = $('#FinancialConsultantSharingNewCreateModel_FcCode').val();
                var name = $('#FinancialConsultantSharingNewCreateModel_ProposerName').val();
                $('#fcSharingNewModelEntry').clearForm();
                $('#FinancialConsultantSharingNewCreateModel_FcCode').val(fc);
                $('#FinancialConsultantSharingNewCreateModel_ProposerName').val(name);
                $('#kmcArea').show();
                closePop('personal');
            }
            alert(res.Message);
        },
        failureEntry: function (xhr, err) {
            $('#loading').hide();
            alert('다시 시도해주세요.');
        },                
        openPostCode: function () {
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
                    $('#FinancialConsultantSharingNewCreateModel_ZipCode').val(data.zonecode);
                    $('#FinancialConsultantSharingNewCreateModel_Address').val(fullAddr);

                    // iframe을 넣은 element를 안보이게 한다.
                    closePop('post', false);
                    openPop('personal');

                    // 커서를 상세주소 필드로 이동한다.
                    var postPos = $('.post-wrap').offset().top;
                    $('body,html').animate({ 'scroll-top': postPos });
                    $('#FinancialConsultantSharingNewCreateModel_AddressDetail').focus();

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
            openPop('personal');
        },
    }
});

function setKmcData(data) {
    $('#FinancialConsultantSharingNewCreateModel_Name').val(data.Name);
    $('#FinancialConsultantSharingNewCreateModel_Mobile').val(data.Mobile);
    $('#FinancialConsultantSharingNewCreateModel_BirthDay').val(data.Birth);
    if (data.Gender === "0") {
        $("#male").prop("checked", true);
    } else {
        $("#female").prop("checked", true);
    }

    $('#kmcArea').hide();
}