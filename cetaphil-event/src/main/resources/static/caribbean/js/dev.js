var cetaphilEventModel = new Vue({
    el  : '#cetaphil-event',
    data: {
        userInfo: {
            name         : "",
            mobile       : "",
            zipcode      : "",
            address      : "",
            addressDetail: "",
            birth        : "",
            email        : "",
            content      : "",
            snsType      : "",
            agree        : false
        },
        dataArr   : [],
        listLength: 0,
        moment    : moment
    },
    created: function () {
    	this.getEventList()
    },
    
    filters: {
        filter: function(content){
            return content.substr(0,41);
        }
    },
        
    methods: {
        beginEntry: function () {
            $('.loading').show();
            $('#entryPopCloseBtn').hide();
            $('#entrySaveBtn').hide();
        },
        completeEntry: function () {
            $('.loading').hide();
        },
        successEntry: function (res) {
            $('.loading').hide();
            alert('이벤트 응모가 완료되었습니다.');
            closePop('input-form');
            $('#caribbeanForm')[0].reset();
            $('#entryPopCloseBtn').show();
            $('#entrySaveBtn').show();
        },
        failEntry: function (xhr, err) {
            if (err !== 'abort') {
                alert(xhr.responseJSON.message);
            }
        },
        saveUserInfo: function () {
            var self = this;
            $.ajax({
                url    : '/api/caribbean',
                type   : 'post',
                data   : self.userInfo,
                async  : false,
                success: function (res) {
                	if(res.success == false){
                		alert(res.message);
                		return false;
                	}
                    closePop('form');
                    openPop('sns-share');
                    self.userInfo = {};
                    self.getEventList();
                    $('.slick')[0].slick.refresh();
                    
                }.bind(this),
                error:function(e){
		            alert(e.responseJSON.errors[0].defaultMessage);  
		        }
            });
        },
        getEventList: function() { // 기대평 리스트 get 함수
        	var self = this;
            $.ajax({
                url    : '/api/caribbean',
                type   : 'get',
                async  : false,
                success: function (res) {
                    self.dataArr = res;
                    for(var i =0; i< self.dataArr.length; i++){
                    	self.dataArr[i].name = self.dataArr[i].name.replace(self.dataArr[i].name.charAt(1),'*');
                    }
                    self.dataArr = self.dataArr.reverse();
                    if(device === 'w'){
                        self.listLength = Math.ceil(self.dataArr.length / 6);
                    } else {
                        self.listLength = Math.ceil(self.dataArr.length / 4);
                    }
                    
                }.bind(this)
            });
        },
        openInfoPop: function () { // 글쓰기 버튼 누를 때 호출
        	if(this.userInfo.content.length > 55){
        		alert('기대평을 55자 이내로 입력 해주세요');
        		return false;
        	}
        	var content = $("#content").val();
        	if(content == null || !content) {
        		alert("기대평을 작성해주세요.");
        	} else {
                openPop('form');
        		this.userInfo.content = content;
        	}
        },
        updateShareCount: function (snsType) { // 공유 버튼 누를 때 호출
            var self = this;
            
            self.userInfo.snsType = snsType;
            $.ajax({
                url    : '/api/caribbean/sharing',
                type   : 'post',
                data   : self.userInfo,
                async  : false,
                success: function (res) {
                	if(res.success == false){
                		alert(res.message);
                		return false;
                	}
                	 if(snsType === 'facebook') {
                     	self.shareFacebook();
                     } else {
                     	self.shareKakao();
                     }
                }.bind(this),
                error:function(e){
                    alert(e.responseJSON.error);
                }
            });
        },
        shareFacebook: function () {
    		var url = "https://cetaphil.pentacle.co.kr"  //this.curServer;
    		window.open('https://www.facebook.com/sharer/sharer.php?u=' + encodeURIComponent(url), "fbPop", "menubar=false, scrollbars=no,width=600px,height=450px");
    	},
    	shareKakao: function () {
    		Kakao.Link.sendCustom({
    	        templateId: 11184
    	      });
    	},
    	closePopButton: function() {
    		closePop('form');
    		$('#caribbeanForm')[0].reset();
    	},
        execDaumPostcode: function() {
            var self = this;
            self.userInfo.address = "";
            
            daum.postcode.load(function(){
            	new daum.Postcode({
                    oncomplete: function(data) {

                        // 검색결과 항목을 클릭했을때 실행할 코드를 작성하는 부분.
                        closePop('search-zip');
                        openPop('form');
                        $('#addressDetail').focus();
                        // 각 주소의 노출 규칙에 따라 주소를 조합한다.
                        // 내려오는 변수가 값이 없는 경우엔 공백('')값을 가지므로, 이를 참고하여 분기 한다.
                        var fullAddr  = data.address;  // 최종 주소 변수
                        var extraAddr = '';            // 조합형 주소 변수
        
                        // 기본 주소가 도로명 타입일때 조합한다.
                        if(data.addressType === 'R'){
                            //법정동명이 있을 경우 추가한다.
                            if(data.bname !== ''){
                                extraAddr += data.bname;
                            }
                            // 건물명이 있을 경우 추가한다.
                            if(data.buildingName !== ''){
                                extraAddr += (extraAddr !== '' ? ', ' + data.buildingName : data.buildingName);
                            }
                            // 조합형주소의 유무에 따라 양쪽에 괄호를 추가하여 최종 주소를 만든다.
                            fullAddr += (extraAddr !== '' ? ' ('+ extraAddr +')' : '');
                        }
        
                        // 우편번호와 주소 정보를 해당 필드에 넣는다.
                        document.getElementById('zipcode').value                = data.zonecode;           //5자리 새우편번호 사용
                        document.getElementById('address').value                = fullAddr;
                        //document.getElementById('sample2_addressEnglish').value = data.addressEnglish;
                        
                        self.userInfo.zipcode = data.zonecode;
                        self.userInfo.address = fullAddr;

                        
                        // iframe을 넣은 element를 안보이게 한다.
                        // (autoClose:false 기능을 이용한다면, 아래 코드를 제거해야 화면에서 사라지지 않는다.)
                        element_layer.style.display = 'none';
                    },
                    width          : '100%',
                    height         : '100%',
                    maxSuggestItems: 5
                }).embed(element_layer);
            });
            
            
    
            // iframe을 넣은 element를 보이게 한다.
            element_layer.style.display = 'block';
    
            // iframe을 넣은 element의 위치를 화면의 가운데로 이동시킨다.
            initLayerPosition();
        }

    }
    
});


    /* 다음 우편번호 찾기 api */
    // 우편번호 찾기 화면을 넣을 element
    var element_layer = document.getElementById('layer');

    function closeDaumPostcode() {
        // iframe을 넣은 element를 안보이게 한다.
        element_layer.style.display = 'none';
    }   

    // 브라우저의 크기 변경에 따라 레이어를 가운데로 이동시키고자 하실때에는
    // resize이벤트나, orientationchange이벤트를 이용하여 값이 변경될때마다 아래 함수를 실행 시켜 주시거나,
    // 직접 element_layer의 top,left값을 수정해 주시면 됩니다.
    function initLayerPosition(){
        var width       = 720  //우편번호서비스가 들어갈 element의 width
        var height      = 500  //우편번호서비스가 들어갈 element의 height
        var borderWidth = 0;   //샘플에서 사용하는 border의 두께

        // 위에서 선언한 값들을 실제 element에 넣는다.
        element_layer.style.width  = width + 'px';
        element_layer.style.height = height + 'px';
        element_layer.style.border = borderWidth + 'px solid';
        // 실행되는 순간의 화면 너비와 높이 값을 가져와서 중앙에 뜰 수 있도록 위치를 계산한다.
        element_layer.style.left = (((window.innerWidth || document.documentElement.clientWidth) - width)/2 - borderWidth) + 'px';
        element_layer.style.top  = (((window.innerHeight || document.documentElement.clientHeight) - height)/2 - borderWidth) + 'px';
    }

    