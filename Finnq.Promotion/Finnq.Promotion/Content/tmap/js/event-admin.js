var adminTmapEventEntryModel = new Vue({
    el: '#admin-tmap',
    data: function () {
        return {
            data: [],
            dataFilters: {
                Page: 1,
                PageSize: 20,
                FromDate: null,
                ToDate: null,
                IsMobileDevice: null,
                Name: null,
                Phone: null,
                IpAddress: null
            }
        };
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
            return moment(date).format("YYYY-MM-DD HH:mm:ss");
        },
        isMobile: function (val) {
            return val ? 'mobile' : 'web';
        }
    },
    methods: {
        goToPage: function (pageNumber) {
            this.dataFilters.Page = (pageNumber > 0 ? pageNumber : 1);
            $.ajax({
                url: '/api/tmap-event',
                type: 'get',
                data: this.dataFilters,
                success: function (res) {
                    this.data = res;
                }.bind(this)
            });
        },
        excelDownload: function () {
            location.href = '/manager/tmap-event/download?' + $.param(this.dataFilters);
        }
    }
});
adminTmapEventEntryModel.goToPage(1);