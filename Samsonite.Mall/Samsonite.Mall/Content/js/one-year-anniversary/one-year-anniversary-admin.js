function makeAdmin1stAnniversaryViewModel(module, ctrl) {    
    module.controller(ctrl, function ($scope, $http) {
        var self = $scope;
        self.totalItemCount = 0;
        self.totalItems = new Array();
        self.items = new Array();
        self.currentPage = 1;
        self.itemsPerPage = 20;
        self.maxSize = 5;

        self.filterBy1stAnniversary = {
            fromDate: null,
            toDate: null,
            channel: null,
            ipAddress: null,
            name: null,
            mobile: null,
            samsoniteId: null,
            acrosticPoemFirst: null,
            acrosticPoemSecond: null,
            acrosticPoemThird: null,
            acrosticPoemFourth: null,
            acrosticPoemFifth: null,
            congratulationMessage: null
        };

        self.get1stAnniversaryList = function () {
            self.filterBy1stAnniversary.page = self.currentPage;
            self.filterBy1stAnniversary.pageSize = self.itemsPerPage;

            $http.get('/api/1st-anniversary/get/list', {
                params: self.filterBy1stAnniversary
            }).then(function (res) {
                self.totalItemCount = res.data.TotalItemCount;
                self.items = res.data.Items;
            }, function (xhr, err) {
                alert(xhr.data.Message);
            });
        }
        self.get1stAnniversaryList();
        
        self.update1stAnniversary = function (data) {
            if (confirm('수정하시겠습니까?')) {
                $http.put('/api/1st-anniversary/put/admin', data)
                    .then(function () {
                        alert('수정되었습니다.');
                        self.get1stAnniversaryList();
                    }, function (xhr, err) {
                        alert(xhr.data.Message);
                    });
            }
        }
        
        self.checkMessageLength = function (element) {
            chkTextLength(element.currentTarget, 100, element.currentTarget.placeholder);
        }
       
    }).filter('convertToDate', function () {
        return function (date) {
            if (date != null) {
                return moment(date).format("YYYY-MM-DD HH:mm:ss");
            }
        }
    })
}