var gaViewModels = new Vue({
    el: '#gaElement',
    created() {
        this.filters.startDate = moment(new Date()).subtract(20, 'days').format("YYYY-MM-DD");
        this.filters.endDate = moment(new Date()).subtract(1, 'days').format("YYYY-MM-DD");
        this.getEventManagement();
        this.getGAList();
    },
    data: {
        options: [],
        filters: {
            pagePath: '/',
            startDate: '',
            endDate: ''
        },
        list: []
    },
    computed: {      
    },
    methods: {
        getEventManagement: function () {
            var self = this;
            $.get('/Api/EventManagement/Get', {
            }, function (res) {
                self.options = res;
            });
        },
        getGAList: function (res) {
            $.get('/Api/GoogleApisAnalyticsReporting/Get', this.filters, this.setGAList);
        },
        setGAList: function (res) {
            this.list = res;
            return false;
        },
        excelDownload: function () {
            location.href = '/manager/ga/excel?' + $.param(this.filters);
        }

    }
});
$('.datepicker').change(function () {
    gaViewModels.filters.startDate = $('#StartDate').val();
    gaViewModels.filters.endDate = $('#EndDate').val();
});

Vue.filter('formatDate', function (value) {
    if (value) {
        return moment(value).format('YYYY-MM-DD');
    }
});