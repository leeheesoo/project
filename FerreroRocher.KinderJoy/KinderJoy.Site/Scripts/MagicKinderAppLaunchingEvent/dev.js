
function magicKinderLaunchingModel() {
    var self = this;

    self.reviewComment = ko.observable();
    self.reviewImage = ko.observable();

    $(function () {
        $('#frm').data('validator').settings.submitHandler = function (e) {

            $('#frm').ajaxSubmit({
                cache: false,
                dataType: 'json',
                beforeSend: self.beginEntry,
                complete: self.completeEntry,
                success: self.successEntry,
                error: self.failureEntry
            });
        }
        self.beginEntry = function () {
            $('.loading').show();
        }
        self.completeEntry = function () {
            $('.loading').hide();
        }
        self.successEntry = function (data) {
            if (data.result) {
                alert('완료되었습니다.');
                $('#frm').clearForm();
                closePop('entry');
            } else {
                alert(data.message);
            }
        }
        self.failureEntry = function (data, textStatus, jqXHR) {
            if (textStatus != 'abort') {
                console.log(data.error);
                alert('일시적인 장애가 발생했습니다. 잠시후 다시 시도해주세요. ');
            }
            $('.loading').hide();
        }

        //우편번호 찾기
        self.openPostCode = function () {
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
                    $('#ZipCode').val(data.zonecode); //5자리 새우편번호 사용
                    $('#Address').val(fullAddr);

                    // 커서를 상세주소 필드로 이동한다.
                    $('#AddressDetail').focus();

                    // iframe을 넣은 element를 안보이게 한다.
                    closePop('post');
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
    });

    self.reviewDetail = function (image, comment) {
        //$('#pop-review').css('background', 'url(' + image + ') center center');
        self.reviewImage(image);
        self.reviewComment(comment);
        openReview();
    }

    self.reviewList = ko.observableArray();

    // bxSLider 리스트 불러오기
    self.getImageList = function () {
        var lineSize = 3;
        if (isMobile.any()) {
            lineSize = 2;
        }

        var pageSize = lineSize * lineSize;
        $.ajax({
            url: '/api/magickinder-launching/get-list',
            type: 'get',
            async: false,
            cache: false,
            success: function (data) {
              
                var item = new Array();
                var list = new Array();
                var i = 0;
                for (var d in data) {
                    item.push(data[d]);
 
                    if (d % lineSize == lineSize-1) {
                        list.push(item);
                        item = new Array();
                    }

                    if (d % pageSize == pageSize-1) {
                        self.reviewList.push(list);
                        list = new Array();
                        i++;
                    }
                }

                if (data.length != i * pageSize) {
                    var remainCount = data.length + (pageSize + i * pageSize - data.length);
                    item = new Array();
                    list = new Array();
                    for (var j = i * pageSize ; j < remainCount; j++) {

                        if (j < data.length) {
                            item.push(data[j]);
                        } else {
                            item.push(null);
                        }

                        if (j % lineSize == lineSize - 1) {
                            list.push(item);
                            item = new Array();
                        }

                        if (j % pageSize == pageSize - 1) {
                            self.reviewList.push(list);
                            list = new Array();
                        }
                    }
                } 
            },
            error: function (res) {
                alert(res.responseJSON.message);
            }
        });
    }
    self.getImageList();
}
