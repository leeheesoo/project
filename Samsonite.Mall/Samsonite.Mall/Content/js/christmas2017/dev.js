var christmas2017Model = new Vue({
    el: '#christmas-2017-entry',
    data: {
        tielonnCount: 0,
        marlonCount: 0,
        supremeLiteCount: 0,
        plumeBasicCount: 0,
        christmasBag: null,
        isDisabled: false
    },
    methods: {
        beginEntry: function () {
            this.isDisabled = true;
        },
        completeEntry: function () {
            this.isDisabled = false;
        },
        successEntry: function (res) {
            alert("응모되었습니다.");
            closePop('entry');
            this.isDisabled = false;
            $('#frmChristmas2017').clearForm();

            this.christmasBag = null;   // 가방선택해제
            $('#imgAgree img').attr('src', '/Content/images/christmas2017/agree_policy1_off.jpg');  // 개인정보 수집 동의 체크해제 이미지
            this.getChristmasStatsList();
        },
        failEntry: function (xhr, err, obj) {
            this.isDisabled = false;
            if (err !== 'abort') {
                alert(xhr.responseJSON.Message);
            }
        },
        openChristmasEntry: function () {
            if (isClose == 'True') {
                alert('이벤트가 종료되었습니다.');
                return;
            }
            if (this.christmasBag == null) {
                alert("가방을 선택해주세요.");
                return;
            }
            openPop('entry');
        },
        getChristmasStatsList: function () {
            $.ajax({
                url: '/api/2017-christmas/get-stats-list',
                type: 'get',
                success: function (res) {
                    this.tielonnCount = res.TielonnCount;
                    this.marlonCount = res.MarlonCount;
                    this.supremeLiteCount = res.SupremeLiteCount;
                    this.plumeBasicCount = res.PlumeBasicCount;

                    var tielonnPercantage = this.tielonnCount * 100 / res.TotalCount;
                    var marlonPercantage = this.marlonCount * 100 / res.TotalCount;
                    var supremeLitePercantage = this.supremeLiteCount * 100 / res.TotalCount;
                    var plumeBasicPercantage = this.plumeBasicCount * 100 / res.TotalCount;

                    setProductProgress(tielonnPercantage+30, marlonPercantage+30, supremeLitePercantage+30, plumeBasicPercantage+30);
                }.bind(this)
            });
        }
    }
});