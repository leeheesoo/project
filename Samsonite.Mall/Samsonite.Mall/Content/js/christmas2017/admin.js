var christmas2017Model = new Vue({
    el: '#christmas-2017',
    data: function () {
        return {
            data: [],
            dataFilters: {
                Page: 1,
                PageSize: 20,
                Channel: null,
                Name: null,
                Mobile: null,
                SamsoniteMallId: null,
                ChristmasBagType: null
            }
        };
    },
    filters: {
        getDate: function (date) {
            return moment(date).format("YYYY-MM-DD HH:mm:ss");
        }
    },
    methods: {
        beginEntry: function () {
            console.log('begin');
        },
        completeEntry: function () {
            console.log('complete');
        },
        successEntry: function (res) {
            console.log('success');
            alert("추가되었습니다.");
            this.goToPage(1);
        },
        failEntry: function (xhr, err, obj) {
            console.log('fail');
            if (err !== 'abort') {
                alert(xhr.responseJSON.Message);
            }
        },
        goToPage: function (pageNumber) {
            this.dataFilters.Page = (pageNumber > 0 ? pageNumber : 1);
            $.ajax({
                url: '/api/2017-christmas/get-admin-list',
                type: 'get',
                data: this.dataFilters,
                success: function (res) {
                    this.data = res;
                }.bind(this)
            });
        },
        //excelDownload: function () {
        //    location.href = '/manager/bag-to-the-future/excel-download?' + $.param(this.dataFilters);
        //}
    },
});