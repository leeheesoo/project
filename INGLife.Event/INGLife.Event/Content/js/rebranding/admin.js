var marketingAgreeModel = new Vue({
    el: '#rebranding-marketing',
    data: {
        data: [],
        dataFilters: {
            Page: 1,
            PageSize: 20,
            Month:'october'
        }
    },
    filters: {
        getDate: function (date) {
            if (!date) {
                return null;
            }
            return moment(date).format("YYYY/MM/DD HH:mm:ss");
        },
        getGender: function (gender) {
            return gender == '0' ? '남' : '여';
        },
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
    methods: {
        goToPage: function (pageNumber) {
            this.dataFilters.Page = (pageNumber > 0 ? pageNumber : 1);

            $.ajax({
                url: '/Api/RebrandingApi/GetAdminRebrandingMarketingEntryList',
                type: 'get',
                data: this.dataFilters,
                success: function (res) {
                    this.data = res;
                }.bind(this)
            });
        },
        monthGoToPage: function (month) {
            this.dataFilters.Month = month;
            this.goToPage(1);
        },
        excelDownload: function () {
            location.href = '/manager/new-branding/marketing/excel?' + $.param(this.dataFilters);
        }
    }
});

var consultingAgreeModel = new Vue({
    el: '#rebranding-consulting',
    data: {
        data: [],
        dataFilters: {
            Page: 1,
            PageSize: 20,
            Month: 'october'
        }
    },
    filters: {
        getDate: function (date) {
            if (!date) {
                return null;
            }
            return moment(date).format("YYYY/MM/DD HH:mm:ss");
        },
        getGender: function (gender) {
            return gender == '0' ? '남' : '여';
        },
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
    methods: {
        goToPage: function (pageNumber) {
            this.dataFilters.Page = (pageNumber > 0 ? pageNumber : 1);

            $.ajax({
                url: '/Api/RebrandingApi/GetAdminRebrandingConsultingEntryList',
                type: 'get',
                data: this.dataFilters,
                success: function (res) {
                    this.data = res;
                }.bind(this)
            });
        },
        monthGoToPage: function (month) {
            this.dataFilters.Month = month;
            this.goToPage(1);
        },
        excelDownload: function () {
            location.href = '/manager/new-branding/consulting/excel?' + $.param(this.dataFilters);
        }
    }
});

