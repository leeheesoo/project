var kittyJusticeLeagueModel = new Vue({
    el: '#kitty-justiceleague',
    data: {
        isLoading: false,
        isShowAddress: false,
        instantEntry: {
            id: 0,
            prizeType: 0
        },
        instantPrize: "waqpwpelre"
    },
    methods: {
        beginEntry: function () {
            this.isLoading = true;
        },
        completeEntry: function () {
            this.isLoading = false;
        },
        successEntry: function (res) {
            if (res.result) {
                closePop('entry');
                $('#frmInstantLotteryEntry').clearForm();
                alert('킨더조이 이벤트에 참여해주셔서 감사합니다.');
            } else {
                alert(res.message);
            }
         
        },
        failEntry: function (xhr, err, obj) {
            this.isLoading = false;
            if (err !== 'abort') {
                alert(xhr.responseJSON.message);
            }
        },
        createInstantLottery: function () {
            closePop('mov-view');
            $.ajax({
                url: '/event/2017-HK_JL/instant-lottery',
                type: 'post',
                success: function (res) {
                    if (res.result) {
                        if (res.data.PrizeType == 0) {
                            openPop('fail');
                        } else {
                            this.instantEntry.id = res.data.Id;
                            this.instantEntry.prizeType = res.data.PrizeType;
                            this.instantPrize = res.data.PrizeName;
                            openPop('entry');
                        }
                    } else {
                        alert(res.message);
                    }
                    this.isLoading = false;
                }.bind(this),
                error: function (xhr, err, obj) {
                    this.isLoading = false;
                    if (err !== 'abort') {
                        alert(xhr.responseJSON.message);
                    }
                }.bind(this),
                complete: function () {
                    this.isLoading = false;
                }.bind(this),
                beforeSend: function () {
                    this.isLoading = true;
                }.bind(this)
            });
        },
        openPostCode: function () {
            closePop('entry');
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
                    $('#ZipCode').val(data.zonecode);
                    $('#Address').val(fullAddr);

                    // iframe을 넣은 element를 안보이게 한다.
                    closePop('post');
                    // 개인정보 팝업을 다시 연다.
                    openPop('entry');

                    // 커서를 상세주소 필드로 이동한다.
                    $('#AddressDetail').focus();

                    // ios에서 focus미작동으로 위치 이동
                    if (navigator.userAgent.match(/iPhone|iPad|iPod/i)) {
                        goPosition('#hk-jl-address-position');
                    }
                },
                /*
                // 우편번호 찾기 화면 크기가 조정되었을때 실행할 코드를 작성하는 부분. iframe을 넣은 element의 높이값을 조정한다.
                onresize: function (size) {
                    element_wrap.style.height = size.height + 'px';
                },
                */
                width: '640px',
                height: '590px'
            }).embed(document.getElementById('searchPostCode'));
            return false;
        }
    }
});