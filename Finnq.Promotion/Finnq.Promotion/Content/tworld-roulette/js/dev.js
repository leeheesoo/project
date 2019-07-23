var tWorldRouletteEventEntryModel = new Vue({
    el: '#tworld-roulette-event-entry',
    data: {
        isLoading: false,
        rouletteEventEntryId: 0,
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
            this.isLoading = false;
            this.rouletteEventEntryId = res;
            $('#frmTRouletteEventEntry').clearForm();
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
                url: '/api/tworld-roulette/prize',
                type: 'post',
                data: {
                    id: this.rouletteEventEntryId
                },
                success: function (res) {
                    this.instantPrize = res.PrizeName;
                    calculatePrize(res.PrizeType);
                }.bind(this),
                error: function (xhr, err, obj) {
                    if (err !== 'abort') {
                        alert(xhr.responseJSON.Message);
                    }
                }.bind(this)
            });
        }
    }
});