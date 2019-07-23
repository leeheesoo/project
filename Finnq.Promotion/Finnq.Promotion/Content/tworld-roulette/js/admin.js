var tWorldRouletteEntryModel = new Vue({
    el: '#tworld-roulette-entry',
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
            PrizeType:null
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
    filters: {
        getDate: function (date) {
            if (date == null) {
                return null;
            }
            return moment(date).format("YYYY-MM-DD HH:mm:ss");
        }
    },
    methods: {
        goToPage: function (pageNumber) {
            this.dataFilters.Page = (pageNumber > 0 ? pageNumber : 1);
            $.ajax({
                url: '/api/tworld-roulette/admin-entry-list',
                type: 'get',
                data: this.dataFilters,
                success: function (res) {
                    this.data = res;
                }.bind(this)
            });
        },
        excelDownload: function () {
            location.href = '/manager/tworld-roulette/download?' + $.param(this.dataFilters);
        }
    }
});

var tWorldRoulettePrizeSettingModel = new Vue({
    el: '#tworld-roulette-prize-setting',
    data: {
        data:[],
        showLoading: false
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
        updateRoulettePrizeSetting:function(entry){
            $.ajax({
                url: '/api/tworld-roulette/update-prize',
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
                error:function (xhr, err, obj) {
                    if (err !== 'abort') {
                        alert(xhr.responseJSON.Message);
                        this.goToPage();
                    }
                }.bind(this)
            });
        },
        goToPage: function () {
            $.ajax({
                url: '/api/tworld-roulette/admin-prize-list',
                type: 'get',
                success: function (res) {
                    this.data = res;
                }.bind(this)
            });
        },
    }
});