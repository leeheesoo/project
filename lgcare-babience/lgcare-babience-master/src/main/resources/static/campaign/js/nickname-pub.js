var device;
var commitmentSlide;
var readyParticipate = true ;

$(function () {
    $('.btn__move-nickname').on('click', function () {
        $('html, body').animate({
            scrollTop: $('#writeNickname').offset().top
        }, 500, 'linear');
    })

    // slide 생성 완료 체크하여 loading dimm 숨김처리
    // $('.nickname__galary-slide').on('init', function (event, slick) {
    //     nicknameEventModel.isLoading = false;
    // });
    
    $('#pop-nickname-personal').find('form').validate({
		rules: {
            name: "required",
            phone: {
                required: true,
                minlength: 10,
                maxlength: 11
            },
            zipcode: "required",
            address: "required",
            addressDetail: "required",
            agree1: "required",
            agree2: "required",
        },
        messages: {
            name: "이름을 입력해주세요",
            phone: {
                required: "휴대폰번호를 입력해주세요",
                minlength: "휴대폰번호를 정확히 입력해주세요",
                maxlength: "휴대폰번호를 정확히 입력해주세요"
            },
			zipcode: "주소를 입력해주세요",
			address: "주소를 입력해주세요",
			addressDetail: "주소를 입력해주세요",
			agree1: "개인정보 활용동의에 동의해주세요",
			agree2: "경품 배송 및 이벤트 관련 위탁동의에 동의해주세요",
		},
		submitHandler: function(e) {
			nicknameEventModel.setuserModel();
		}
	});
});

$(window).on('load', function () {

    commitmentSlide = $('.nickname__galary-slide').slick({
        infinite: true,
        rows: 2,
        slidesPerRow: 3,
        responsive: [
           {
               breakpoint: 650,
               settings: {
                   rows: 3,
                   slidesPerRow: 2,
               }
           }
        ]
    });
})

var nicknameEventModel = new Vue({
    el: '#nicknameEvent',
    data: {
        device: '',
        popName: '',
        userModel: {
            nickname: '',
            reason: '',
            name: '',
            phone: '',
            zipcode: '',
            address: '',
            addressDetail: '',
            agree1: false,
            agree2: false,
            agreeMarketing: false            
        },
        checkUser: {
            name: '',
            phone: '',
        },
        isLoading: false,
        items: [],
        isEvent: false,
        urlConponResit: ''
    },
    created: function () {
        this.checkDevice();
        this.createNicknameSlide();
        this.setCouponUrl();
        setClipboard();
        openPopup('winner');
    },
    updated: function () {
        this.$nextTick(function () {
            if (commitmentSlide) {
                $('.nickname__galary-slide').slick('refresh');
            }
        });
    },
    methods: {
        checkDevice: function() {
            if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
                this.device = 'm';
            } else {
                this.device = 'w';
            }
        },
        setMessage: function () {
            $.ajax({
                type: 'POST',
                url: '/api/kabrita/nickname',
                data: this.userModel,
                success: function (res) {
                    // if (res.success) {
                    //     //console.log(readyParticipate);
                    //     if (!readyParticipate) {
                    //         alert('영상을 끝까지 시청한 후에 참여 가능합니다.');
                    //         $('html, body').animate({
                    //             scrollTop: $('#eventMov').offset().top - 50
                    //         }, 500, 'linear');

                    //     } else {
                    //         openPopup('nickname-personal');
                    //     }
                    // }
                    openPopup('nickname-personal');
                }.bind(this),
                error: function (e) {
                    //console.log(e);
                    alert(e.responseJSON.error);
                }
            })
        },
        setuserModel: function () {
        	$.ajax({
                type: 'POST',
                url: '/api/kabrita/nickname/user',
                data: this.userModel,
                success: function (res) {
                    if (res.success) {
                        closePopup('nickname-personal');
                        openPopup('coupon-share');
                        this.createNicknameSlide();
                        this.isEvent = true;

                        // form clear
                        this.userModel = {};
                        $('#pop-nickname-personal form')[0].reset();
                    } else {
                        alert(res.message);
                    }
                }.bind(this),
                error: function (e) {
                    alert(e.responseJSON.error);
                },
                beforeSend: function () {
                	this.isLoading = true;
                },
                complete: function () {
                	this.isLoading = false;
                }
            })
        },
        setCouponUrl: function () {
            if (this.device == 'w') {
                this.urlConponResit = 'https://www.babience.com:553/member/login.jsp';
            } else {
                this.urlConponResit = 'https://www.babience.com:553/m/member/login.jsp';
            }
        },
        createNicknameSlide: function () {
            var self = this;
            $.ajax({
                type: 'GET',
                url: '/api/kabrita/nickname/list',
                data: {
                    Page: 0,
                    PageSize: 60
                },
                success: function (res) {
                    self.items = res;
                }.bind(self),
                error: function (e) {
                    //console.log(e);
                    alert(e.responseJSON.error);
                }
            })
        },
        checkUserRecord: function (popName) {
            var self = this;
            this.popName = popName;
            $.ajax({
                type: 'GET',
                url: '/api/kabrita/nickname/user',
                data: this.checkUser,
                success: function (res) {
                    if (res.success) {
                        closePopup('participate-record');
                        openPopup('coupon-guide');
                    } else {
                        alert(res.message);
                    }
                }.bind(self),
                error: function (e) {
                    //console.log(e);
                    alert(e.responseJSON.error);
                }
            })
        },
        kakaotalkShare: function (templateId) {
            var data = {};

            // 애칭 : 11156 , 단순공유 : 11170
            data.templateId = templateId;

            kakaoTalkShareTemplate(data, function () {
                if (data.templateId == 11156) {
                    var shareData = {};
                    shareData.type = 'kakao';
                    shareData.url = '/api/kabrita/nickname/sharing';

                    saveSnsShare(shareData, function () {

                    })
                }
            }, function () {
            }, function () {
            });
        },
        openSnsShare: function () {
            this.isEvent = false;
            openPopup('event-share');
        },
        openPostCode: function (popName, zipCode, address) { // 우편번호팝업
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

                    //console.log(data.zonecode + '////' + fullAddr);
                    self.userModel.zipcode = data.zonecode;
                    self.userModel.address = fullAddr;
                    // 커서를 상세주소 필드로 이동한다.
                    //$('#' + addressDetailId).focus();

                },
                width: '640px',
                height: '590px'
            }).embed(document.getElementById('searchPostCode'));
            return false;
        },
        startMov: function () {
            $('.cover__mov').hide();
            eventYoutubePlayer.playVideo();
        }

    }
})

function saveSnsShare(data, successCallback) {
    $.ajax({
        url: data.url,
        type: 'POST',
        data: {
            type: data.type
        },
        success: function (res) {
            if (typeof successCallback == 'function') {
                successCallback();
            }
        },
        error: function (res) {
            alert(res.responseJSON.error);
        }
    });
}

//clipboard
function setClipboard() {
    clipboard = new Clipboard('.codecopy');
    clipboard.on('success', function (e) {
        //console.log(nicknameEventModel.isEvent);
        if (nicknameEventModel.isEvent) {
            var snsData = {};
            snsData.type = 'url';
            snsData.url = '/api/kabrita/nickname/sharing';

            saveSnsShare(snsData, function () {
                alert('복사되었습니다');
            })
        } else {
            alert('복사되었습니다');
        }
    });
    clipboard.on('error', function (e) {
        alert('복사실패')
    });
}
