var twoYearAnniversaryPrizeSettingModel = new Vue({
    el: '#twoYearAnniversary',
    data: {
        data: [],
        prizeTotalCount: {
            StarBucksCount: 0,
            AtKidsBagPackCount: 0,
            OriginalBagCount: 0,
            Coupon_20Count: 0,
            LineFriendsCarrierCount:0
        }
    },
    filters: {
        getDate: function (date) {
            return moment(date).format("YYYY-MM-DD");
        },
    },
    methods: {
        beginEntry: function () {
        },
        completeEntry: function () {
        },
        successEntry: function (res) {
            this.goToPage();
        },
        failEntry: function (xhr, err, obj) {
            if (err !== 'abort') {
                alert(xhr.responseJSON.Message);
            }
        },
        updatePrizeSetting: function (entry) {
            $.ajax({
                url: '/api/2st-anniversary/update-admin-entry',
                type: 'post',
                data: {
                    date: entry.Date,
                    prizeType: entry.PrizeType,
                    totalCount: entry.TotalCount,
                    startTime: entry.StartTime,
                    rate: entry.Rate
                },
                success: function (res) {
                    alert('수정되었습니다.');
                    this.goToPage();
                }.bind(this),
                error: function (xhr, err, obj) {
                    if (err !== 'abort') {
                        alert(xhr.responseJSON.Message);
                        this.goToPage();
                    }
                }.bind(this)
            });
        },
        goToPage: function () {
            $.ajax({
                url: '/api/2st-anniversary/admin-prize-list',
                type: 'get',
                success: function (res) {
                    this.resetPrizeTotalCount();
                    for (var i in res) {
                        var prize = res[i];
                        if (prize.PrizeType == 1) {
                            this.prizeTotalCount.StarBucksCount += prize.WinnerCount;
                        } else if (prize.PrizeType == 2) {
                            this.prizeTotalCount.AtKidsBagPackCount += prize.WinnerCount;
                        } else if (prize.PrizeType == 3) {
                            this.prizeTotalCount.OriginalBagCount += prize.WinnerCount;
                        } else if (prize.PrizeType == 4) {
                            this.prizeTotalCount.Coupon_20Count += prize.WinnerCount;
                        } else if (prize.PrizeType == 5) {
                            this.prizeTotalCount.LineFriendsCarrierCount += prize.WinnerCount;
                        }
                    }
                    this.data = res;
                }.bind(this),
                error: function (err) {
                    console.log(err)
                },
            });
        },
        resetPrizeTotalCount: function () {
            this.prizeTotalCount.StarBucksCount=0;
            this.prizeTotalCount.AtKidsBagPackCount=0;
            this.prizeTotalCount.OriginalBagCount=0;
            this.prizeTotalCount.Coupon_20Count=0;
            this.prizeTotalCount.LineFriendsCarrierCount=0;
        }
    }
});



var twoYearAnniversaryEntrygModel = new Vue({
    el: '#twoAnniversaryEntry',
    data: {
        dataFilter: {
            Id: null,
            SamsoniteId:null,
            PrizeType: null,
            FromDate: null,
            ToDate: null,
            IpAddress: null,
            Page:1,
            PageSize:20
        },
        data: []
    },
    filters: {
        getDate: function (date) {
            return moment(date).format("YYYY-MM-DD HH:mm:ss");
        }
    },
    computed: {
        pages: function () {
            var pageCount = parseInt(this.data.PageCount);
            var pageNumber = parseInt(this.data.PageNumber);
            var pageSlide = 10;
            var pageFrom = Math.max(1, pageNumber - pageSlide);
            var pageTo = Math.min(pageCount, pageNumber + pageSlide);
            pageFrom = Math.max(1, Math.min(pageTo - 2 * pageSlide, pageFrom));
            pageTo = Math.min(pageCount, Math.max(pageFrom + 2 * pageSlide, pageTo));

            var result = [];
            for (var i = pageFrom; i <= pageTo; i++) {
                result.push(i);
            }
            return result;
        }
    },
    methods: {     
        goToPage: function (pageNumber) {
            this.dataFilter.Page = (pageNumber > 0 ? pageNumber : 1);
            $.ajax({
                url: '/api/2st-anniversary/admin-entry-list',
                type : 'get',
                data: this.dataFilter,
                success: function (res) {
                    this.data = res;
                }.bind(this),
                error: function (err) {
                    console.log(err)
                },
            });
        },
        goToExcel: function () {
            //console.log('1111');
            var data = $('form[name="test"]').serialize();
            //console.log(data)
            location.href = '/manager/2st-anniversary/download/excel?' + data;
        }
    }
});