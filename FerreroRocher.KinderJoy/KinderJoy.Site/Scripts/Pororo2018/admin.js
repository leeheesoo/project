var pororo2018Model = new Vue({
    el: '#pororo-2018-entry',
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
                url: '/api/2018-pororo/admin-pororo-entry',
                type: 'get',
                data: this.dataFilters,
                success: function (res) {
                    this.data = res;
                }.bind(this)
            });
        },
        excelDownload: function () {
            location.href = '/manager/2018-pororo/download?' + $.param(this.dataFilters);
        }
    }
});

var pororo2018PrizeSettingModel = new Vue({
    el: '#pororo2018-prize-setting',
    data: {
        data: [],
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
                alert(xhr.responseJSON.message);
            }
        },
        updateRoulettePrizeSetting: function (entry) {
            $.ajax({
                url: '/api/2018-pororo/update-prize',
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
                url: '/api/2018-pororo/admin-prize-list',
                type: 'get',
                success: function (res) {
                    this.data = res;
                }.bind(this)
            });
        },
    }
});