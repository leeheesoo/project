var eventManagementViewModel = new Vue({
    el: '#eventManagementElement',
    created() {
        this.getAffiliates();
        this.getEventManagement();
    },
    data: {
        options: [],
        affiliates: '',
        list: []
    },
    computed () {

    },
    methods: {
        getAffiliates: function () {
            var self = this;
            $.get('/Api/Affiliates/Get', {}, function (res) {
                self.options = res;
            });
        },
        getEventManagement: function () {
            var self = this;
            $.get('/Api/EventManagement/Get', {}, function (res) {
                self.list = res;
            });
        },
        successSaveEventManagement: function (res) {
            alert('등록되었습니다.');
            this.getEventManagement();
        },
        failureSaveEventManagement: function (xhr, err) {
            alert(xhr.responseJSON.message);
        }
    }
})