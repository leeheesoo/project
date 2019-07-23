﻿var tumblerEventModel = new Vue({
    el: '#tumbler-entry',
    data: {
        data: [],
        dataFilters: {
            Page: 1,
            PageSize: 20,
            EventType: 'tumbler'
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
        getGender: function (gender) {
            return gender == '0' ? '남' : '여';
        },
        getDate: function (date) {
            if (!date) {
                return null;
            }
            return moment(date).format("YYYY/MM/DD HH:mm:ss");
        }
    },
    methods: {
        goToPage: function (pageNumber) {
            this.dataFilters.Page = (pageNumber > 0 ? pageNumber : 1);

            $.ajax({
                url: '/Api/TumblerEventApi/GetAdminTumblerEventEntryList',
                type: 'get',
                data: this.dataFilters,
                success: function (res) {
                    this.data = res;
                }.bind(this)
            });
        },
        excelDownload: function () {
            location.href = '/manager/tumbler/excel?' + $.param(this.dataFilters);
        }
    }
});
