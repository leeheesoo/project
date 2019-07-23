var checkPlay = false;


new Vue({
    el: '#skinEvent',
    data: {
        navList: [
            { class: "floating__nav-01", herf: "#event01", text: '01. 본품증정 이벤트 ' },
            { class: "floating__nav-02", herf: "#event02", text: '02. 검색 이벤트' },
            { class: "floating__nav-03", herf: "#event03", text: '03. 영상공유 이벤트' },
            { class: "floating__nav-04", herf: "#event04", text: '04. 피부리턴 스페셜 기프트세트' },
        ],
        loading: false,
        popName: '',
        giftSetting: '', //즉당선물
        lottery: lottery, //true false
        lotteryData: {
            prizeImageName: '', // image
            name: '',
            phone: '',
            zipcode: '',
            address: '',
            addressDetail: '',
            birth: '',
            email: '',
            agree: false
        },
        samplingData: {
            name: '',
            phone: '',
            zipcode: '',
            address: '',
            addressDetail: '',
            birth: '',
            email: '',
            agree: false
        },
        samplingSnsData: {
            snsType: ''
        }
    },
    created: function () {
        openPopup('winner');
        controlNav(); //flating nav 위치설정
        if (lottery) {
            this.lotteryEventResult();
        }
    },
    methods: {
        openPostCode: function (popName, zipCode, address) {
            var self = this;
            this.popName = popName;
            closePopup(popName);
            openPopup('post');
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
                    $('#' + zipCode).val(data.zonecode);
                    $('#' + address).val(fullAddr);

                    // iframe을 넣은 element를 안보이게 한다.
                    closePopup('post');
                    // 개인정보 팝업을 다시 연다.
                    openPopup(popName);

                    if (popName == 'win') {
                        self.lotteryData.zipcode = data.zonecode;
                        self.lotteryData.address = fullAddr
                    } else if (popName == 'sampling') {
                        self.samplingData.zipcode = data.zonecode;
                        self.samplingData.address = fullAddr;
                    }

                    // 커서를 상세주소 필드로 이동한다.
                    $('#' + addressDetailId).focus();
                    // ios에서 focus미작동으로 위치 이동
                    //if (navigator.userAgent.match(/iPhone|iPad|iPod/i)) {
                    //    goPosition('#pororo2018-address-position');
                    //}

                },
                /*
                // 우편번호 찾기 화면 크기가 조정되었을때 실행할 코드를 작성하는 부분. iframe을 넣은 element의 높이값을 조정한다.
                onresize: function (size) {
                    element_wrap.style.height = size.height + 'px';
                },
                */
                width: '720px',
                height: '590px'
            }).embed(document.getElementById('searchPostCode'));
            return false;
        },
        closePostPopup: function () {
            openPopup(this.popName);
            closePopup('post', false);
        },
        lotteryEventResult: function (e) { //즉당당첨여부
            var self = this;

            $.ajax({
                url: '/api/skin-regeneration/instant-lottery',
                type: 'POST',
                success: function (res) {
                    if (res.success) {
                        //팝업노출
                        if (res.winner) {
                            //당첨     
                            self.giftSetting = res.prizeImageName; //gift setting                      
                            self.lotteryData.prizeImageName = res.prizeImageName;
                            openPopup('win');
                        } else {
                            //미당첨
                            openPopup('fail');
                        }
                    } else {
                        alert(res.message);
                    }
                    $('#searchForm').clearForm();
                },
                beforeSend: function () {
                    self.loading = true;
                },
                complete: function () {
                    self.loading = false;
                }
            })
        },
        lotteryEventUserInfo: function (e) { // 즉당data        	
            var self = this;

            $.ajax({
                url: '/api/skin-regeneration/instant-lottery/update-winner',
                type: 'POST',
                data: self.lotteryData,

                success: function (res) {
                	alert(res.message);
                	
                	if (res.success) {
                		closePopup('win');
                        $('#searchForm').clearForm();

                        //self.lotteryData = {};
                        //surveyEvent.openShare('');
                	}
                },
                error: function (res) {
                    alert(res.responseJSON.error);
                },
                beforeSend: function () {
                    self.loading = true;
                },
                complete: function () {
                    self.loading = false;
                }
            })
        },
        samplingEventCheck : function(){
            if(checkPlay){
                openPopup('sampling');
            } else {
                alert('위의 영상을 60초 이상 감상해주세요.');
            }
        },
        samplingEventUserInfo: function (e) { // 샘플링이벤트        	
            var self = this;

            $.ajax({
                url: '/api/skin-regeneration/sampling/save-user',
                type: 'POST',
                data: self.samplingData,

                success: function (res) {
                	if (res.success) {
                		closePopup('sampling');
                        $('#samplingForm').clearForm();
                        self.samplingData = {};

                        openPopup('share');
                	} else {
                		alert(res.message);
                	}
                },
                error: function (res) {
                    console.log(res)
                    alert(res.responseJSON.error)
                },
                beforeSend: function () {
                    self.loading = true;
                },
                complete: function () {
                    self.loading = false;
                }
            })
        },
        samplingEventShare: function (snsType) { //샘플링 공유하기
            var self = this;
            self.samplingSnsData.snsType = snsType;
            
            if (snsType == 'facebook') {
            	var sharingURL = location.origin + location.pathname + '?utm_campaign=summer&utm_medium=sharing&utm_source=facebook';
            	window.open('https://www.facebook.com/dialog/share?app_id=441801156302191&href=' + encodeURIComponent(sharingURL), "fbPop", "menubar=false, scrollbars=no,width=600px,height=450px");
            } else if (snsType == 'kakaotalk') {
            	Kakao.Link.sendCustom({
            		templateId: 12006
        		});
            } else {
            	alert('어떤 SNS로 공유하실 건지 선택해주세요');
            	return;
            }

            $.ajax({
                url: '/api/skin-regeneration/sampling/sharing',
                type: 'POST',
                data: self.samplingSnsData,
                success: function (res) {
                    // console.log(res)
                    //closePopup('share')

                },
                error: function (res) {
                    // console.log(res)
                    //alert(res.responseJSON.error)
                },
                beforeSend: function () {
                    self.loading = true;
                },
                complete: function () {
                    self.loading = false;
                }
            })
        }

    } //methods
});





