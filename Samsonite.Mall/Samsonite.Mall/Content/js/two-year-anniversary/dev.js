var asperoApp = new Vue({
    el: '#twoAnniversary',
    data: {
        prizeName: 'wlaossdefr'
    },
    methods: {
        createEvent: function () {
            console.log(twoYearCustNo);
            var URL = '/second-anniversary/create-entry?TwoYearCustNo=' + twoYearCustNo;
            $.ajax({
                url: URL,
                contentType: "application/json",
                dataType: "json",
                type: 'POST',
                beforeSend : function(){
                    $('.loading').show();
                },
                success: function (res) {
                    $('.loading').hide();                    
                    if (res.result) {
                        rotation(res.data.RouletteType);
                        this.prizeName = res.data.PrizeName;                        
                    }else{
                        alert(res.message);
                    }
                }.bind(this),
                error: function (err) {
                    console.log(err);
                    $('.loading').hide();
                    alert('다시 시도해주세요.');
                }
            });
        },       
    }
});



