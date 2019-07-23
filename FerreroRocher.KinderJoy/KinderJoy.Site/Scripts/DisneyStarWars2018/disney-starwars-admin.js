var disneyStarWars2018Model = new Vue({
    el: '#disney-starwars-2018-entry',
    data: {
        data: [],
        dataFilters: {
            Page: 1,
            PageSize: 20,
            FromDate: null,
            ToDate: null,
            Channel: null,
            Name: null,
            Mobile: null,
            UpdateFromDate: null,
            UpdateToDate: null,
            PrizeType: null,
            Address: null
        }
    },
    computed: {
        pages: function () {
            var pageCount = parseInt(this.data.pageCount);
            var pageNumber = parseInt(this.data.pageNumber);
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
    filters: {
        getDate: function (date) {
            if (!date) {
                return null;
            }
            return moment(date).format("YYYY-MM-DD HH:mm:ss");
        },
    },
    methods: {
        goToPage: function (pageNumber) {
            this.dataFilters.Page = (pageNumber > 0 ? pageNumber : 1);
            $.ajax({
                url: '/api/disney-starwars/admin-DisneyStarWars-entry',
                type: 'get',
                data: this.dataFilters,
                success: function (res) {
                    this.data = res;
                }.bind(this)
            });
        },
        excelDownload: function () {
            location.href = '/manager/disney-starwars/download?' + $.param(this.dataFilters);
        }
    }
});

var disneyStarWras2018PrizeSettingModel = new Vue({
    el: '#disney-starwars-2018-prize-setting',
    data: {
        data: [],
        showLoading: false,
        prizeTotalCount: {
            KinderJoyGifty: 0,
            DisneyQuenMirror: 0,
            StarWarsCupSet: 0            
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
                alert(xhr.responseJSON.message);
            }
        },
        updateRoulettePrizeSetting: function (entry) {
            $.ajax({
                url: '/api/disney-starwars/update-prize',
                type: 'post',
                data: {
                    date: entry.date,
                    prizeType: entry.prizeType,
                    totalCount: entry.totalCount,
                    startTime: entry.startTime,
                    rate: entry.rate
                },
                success: function (res) {
                    alert('수정되었습니다.');
                    this.goToPage();
                }.bind(this),
                error: function (xhr, err, obj) {
                    if (err !== 'abort') {
                        alert(xhr.responseJSON.message);
                        this.goToPage();
                    }
                }.bind(this)
            });
        },        
        goToPage: function () {
            $.ajax({
                url: '/api/disney-starwars/admin-prize-list',
                type: 'get',
                success: function (res) {
                    this.resetPrizeTotalCount();                    
                    for (var i in res) {
                        var prize = res[i];                        
                        if (prize.prizeType == 1) {
                            this.prizeTotalCount.KinderJoyGifty += prize.winnerCount;
                        } else if (prize.prizeType == 2) {
                            this.prizeTotalCount.DisneyQuenMirror += prize.winnerCount;
                        } else if (prize.prizeType == 3) {
                            this.prizeTotalCount.StarWarsCupSet += prize.winnerCount;
                        } 
                    }                    
                    this.data = res;
                }.bind(this)
            });
        },
        resetPrizeTotalCount: function () {
            this.prizeTotalCount.KinderJoyGifty = 0;
            this.prizeTotalCount.DisneyQuenMirror = 0;
            this.prizeTotalCount.StarWarsCupSet = 0;            
        }
    }
});