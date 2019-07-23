var bagtoTheFutureModel = new Vue({
    el: '#bagtotheFutureEntry',
    data: function () {
        return {
            data: [],
            dataFilters: {
                Page: 1,
                PageSize: 20,
                FromDate: null,
                ToDate: null,
                Channel: null,
                IpAddress: null,
                Name: null,
                Mobile: null,
                IdeaName: null,
                IdeaDescription:null
            }
        };
    },
    filters: {
        getDate: function (date) {
            return moment(date).format("YYYY-MM-DD HH:mm:ss");
        }
    },
    methods: {
        goToPage: function (pageNumber) {
            this.dataFilters.Page = (pageNumber > 0 ? pageNumber : 1);
            $.ajax({
                url: '/api/bag-to-the-future/entry/get/list',
                type: 'get',
                data: this.dataFilters,
                success: function (res) {
                    this.data = res;
                }.bind(this)
            });
        },
        excelDownload : function(){
            location.href = '/manager/bag-to-the-future/excel-download?' + $.param(this.dataFilters);
        }
    },
});
//Vue.directive('datepicker', {
//    bind: function () {
//        var vm = this.vm;
//        var key = this.expression;
//        // how to find the value of product.deliveryDateVp

//        $(this.el).daterangepicker(
//        {
//            locale: locals,
//            'linkedCalendars': false,
//            'opens': 'left',
//            'buttonClasses': 'Button Button--full',
//            'applyClass': 'Button--primary',
//            'cancelClass': 'Button--secondary',
//            'singleDatePicker': true
//        });
//    }
//});


var bagtoTheFutureSnsUserModel = new Vue({
    el: '#bagtotheFutureSnsUser',
    data: function () {
        return {
            data: [],
            dataFilters: {
                Page: 1,
                PageSize: 20,
                FromDate: null,
                ToDate: null,
                Channel: null,
                IpAddress: null,
                Name: null,
                Mobile: null
            }
        };
    },
    filters: {
        getDate: function (date) {
            return moment(date).format("YYYY-MM-DD HH:mm:ss");
        }
    },
    methods: {
        goToPage: function (pageNumber) {
            this.dataFilters.Page = (pageNumber > 0 ? pageNumber : 1);
            $.ajax({
                url: '/api/bag-to-the-future/sns-user/get/list',
                type: 'get',
                data: this.dataFilters,
                success: function (res) {
                    this.data = res;
                }.bind(this)
            });
        },
        excelDownload : function(){
            location.href = '/manager/bag-to-the-future/excel-download/sns-user?' + $.param(this.dataFilters);
        }
    }
});

var bagtoTheFutureSnsShareModel = new Vue({
    el: '#bagtotheFutureSnsShare',
    data: function () {
        return {
            data: [],
            dataFilters: {
                Page: 1,
                PageSize: 20,
                FromDate: null,
                ToDate: null,
                Channel: null,
                IpAddress: null,
                Name: null,
                Mobile: null,
                SnsType:null
            }
        };
    },
    filters: {
        getDate: function (date) {
            return moment(date).format("YYYY-MM-DD HH:mm:ss");
        }
    },
    methods: {
        goToPage: function (pageNumber) {
            this.dataFilters.Page = (pageNumber > 0 ? pageNumber : 1);
            $.ajax({
                url: '/api/bag-to-the-future/sns/get/list',
                type: 'get',
                data: this.dataFilters,
                success: function (res) {
                    this.data = res;
                }.bind(this)
            });
        },
        excelDownload: function () {
            location.href = '/manager/bag-to-the-future/excel-download/sns?' + $.param(this.dataFilters);
        }
    }
});

