var rouletteEventEntryModel = new Vue({
    el: '#rouletteEventEntry',
    data: {
        isLoading: false,
        rouletteEventEntryId: 0,
        instantPrize: "waqpwpelre",
        isFailed: "pop_tit_rolette03"
    },
    methods: {
        beginEntry: function () {
            this.isLoading = true;
        },
        completeEntry: function () {
            this.isLoading = false;
        },
        successEntry: function (res) {
            this.isLoading = false;
            this.rouletteEventEntryId = res;
            $('#frmRouletteEventEntry').clearForm();
            closePop('pop-info');
            this.openRoulette();
        },
        failEntry: function (xhr, err, obj) {
            this.isLoading = false;
            if (err !== 'abort') {
                alert(xhr.responseJSON.Message);
            }
        },
        openRoulette : function(){
            openPop('pop-roulette01');
            $('.btn-start').prop('disabled', false);
        },
        updateRouletteEventEntry: function () {
            $('.btn-start').prop('disabled', true);
            $.ajax({
                url: '/api/roulette/update',
                type: 'post',
                data: {
                    id: this.rouletteEventEntryId
                },
                success: function (res) {
                    this.isFailed = "pop_tit_rolette03";
                    if (res.PrizeType != 0) {
                        this.isFailed = "pop_tit_rolette02";
                    }
                    this.instantPrize = res.PrizeName;
                    calculatePrize(res.PrizeType);
                }.bind(this),
                error: function (xhr, err, obj) {
                    if (err !== 'abort') {
                        alert(xhr.responseJSON.Message);
                    }
                }.bind(this)
            });
        },
        kakaotalkShare: function () {
            var data = {};
            data.btnText = '이벤트 참여하기';
            data.link = 'https://' + document.domain + '/roulette?utm_source=share&utm_campaign=roulette&utm_medium=kakaotalk';
            data.picture = 'https://' + document.domain + '/Content/roulette/images/sns/kakaotalk.jpg';
            data.description = '[핀크] 앱 다운로드 URL\r\nhttps://finnq.onelink.me/YERF?pid=Tmap\r\n\r\n지금 핀크 앱 다운 받고 가입하면\r\n메시지 추천인과 함께 더블 혜택 증정!\r\n\r\n금액이 얼마인지 궁금하시죠?\r\n친구에게 연락해서 한번 물어보세요!';
            kakaotalkShare(data, function () {
            }, function () {
                alert('모바일에서 참여가 가능합니다 ^^');
            });
        }
    }
});