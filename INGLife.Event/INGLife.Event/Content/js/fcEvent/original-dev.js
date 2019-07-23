var fcSharingEventModel = new Vue({
	el: '#fc-sharing-event',
	data:{
		shareBtnText:'' // eventEnd
	},
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
            	this.shareBtnText ='eventEnd';
                alert('이벤트 응모가 완료되었습니다.');
                $('#fcSharingOrginModelEntry').clearForm();
                $('#fccode').removeAttr('readonly');
                $('#kmcArea').show();
                closePop('personal');
                openPop('share');
            } else {
                alert(res.Message);
            }
        },
        failureEntry: function (xhr, err) {
            $('#loading').hide();            
            alert('다시 시도해주세요.');
        },
        kakaoUrlShare: function () {
            var self = this;
            var res = self.kakaoAuthcation();
            if (res != null) {
                if (res.Result == true) {
                    self.kakaoShareAjax(res);
                } else {
                    alert(res.Message);
                    $('#loading').hide();
                }                
            }            
        },
        kakaoAuthcation: function () {
            var response = null;
            $.ajax({
                url: '/fc-sharing/kakao-share',
                type: 'post',
                async: false,
                beforeSend: function () {
                    $('#loading').show();
                },
                success: function (res) {
                	response = res;
                },
                error: function () {
                    $('#loading').hide();
                    response = res;
                }
            });

            return response
        },
        kakaoShareAjax: function (res) {    
            var converter = res.Data;            
            Kakao.Link.sendCustom({
                templateId: 12851,
                installTalk: true,
                templateArgs: { 'url': converter },
                success: function () {
                    $('#loading').hide();
                },
                fail: function () {
                    $('#loading').hide();
                }
            });
        },
        checkFcCode: function () {
            var data = {
                fcCodeModel: {
                    FcCode: $('#fccode').val()
                }
            }            
            $.ajax({
                url: '/fc-sharing/checkFcCode',
                type: 'post',
                data: data.fcCodeModel,
                async: false,
                beforeSend: function () {
                    $('#loading').show();
                },
                success: function (res) {                    
                    var result = res.Result;                    
                    if (result) {
                        $('#fccode').prop('readonly', 'readonly');
                    }
                    alert(res.Message);
                    $('#loading').hide();
                },
                error: function () {
                    $('#loading').hide();
                }
            });
        },
        checkPhone: function () {
            var self = this;
            var data = {
                phoneModel: {
                    phone: $('#checkPhone').val()
                }
            }
            $.ajax({
                url: '/fc-sharing/customer',
                type: 'post',
                data: data.phoneModel,
                async: false,
                beforeSend: function () {
                    $('#loading').show();
                },
                success: function (res) {
                    var result = res.Result;
                    if (result) {
                        self.kakaoShareAjax(res);
                    } else {
                        alert(res.Message);
                        $('#checkPhone').val('')
                        closePop('confirm');
                    }
                    $('#loading').hide();
                },
                error: function () {
                    $('#loading').hide();
                }
            });
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
                    $('#FinancialConsultantSharingOriginCreateModel_ZipCode').val(data.zonecode);
                    $('#FinancialConsultantSharingOriginCreateModel_Address').val(fullAddr);

                    // iframe을 넣은 element를 안보이게 한다.
                    closePop('post', false);
                    openPop('personal');

                    // 커서를 상세주소 필드로 이동한다.
                    var postPos = $('.post-wrap').offset().top;
                    $('body,html').animate({ 'scroll-top': postPos });
                    $('#FinancialConsultantSharingOriginCreateModel_AddressDetail').focus();

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
    $('#FinancialConsultantSharingOriginCreateModel_Name').val(data.Name);
    $('#FinancialConsultantSharingOriginCreateModel_Mobile').val(data.Mobile);
    $('#FinancialConsultantSharingOriginCreateModel_BirthDay').val(data.Birth);
    if (data.Gender === "0") {
        $("#male").prop("checked", true);
    } else {
        $("#female").prop("checked", true);
    }

    $('#kmcArea').hide();
}