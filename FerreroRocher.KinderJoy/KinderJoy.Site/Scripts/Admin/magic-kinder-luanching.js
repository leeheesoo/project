var adminModule = angular.module('app-admin-magic-kinder-launching', []);

adminModule.controller('ctrl-admin-magic-kinder-launching', function ($scope, $http) {
    var self = $scope;
    self.data = new Array();

    self.magicKinderFilter = {
        fromDate: null,
        toDate: null,
        name: null,
        mobile: null,
        channel: null,
        screenShotType: null,
        isShow:null,
        page: 1,
        pageSize: 20
    };

    self.goToPage = function (pageNumber) {
        self.magicKinderFilter.page = pageNumber;
        $http({
            method: 'GET',
            url: '/api/magickinder-launching/get-admin-list',
            params: self.magicKinderFilter
        }).then(function successCallback(response) {
            self.data = response.data;
        }, function errorCallback(response) {
            alert(response.data.message);
        });
    }

    self.changeShow = function (id) {
        $http({
            method: 'POST',
            url: '/api/magickinder-launching/change-isshow',
            data: { 'id' : id }
        }).then(function successCallback(response) {
            self.goToPage(self.magicKinderFilter.page);
        }, function errorCallback(response) {
            alert(response.data.message);
        });
    }
    self.excelDownload = function () {
        location.href = '/manager/2017-magickinderapp-launching/excel-download?' + $.param(self.magicKinderFilter);
    }
    self.goToPage(1);
}).filter('pages', function () {
    return function (pageNumber, pageCount) {
        pageCount = parseInt(pageCount);
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
});

adminModule.controller('ctrl-admin-magic-kinder-launching-stats', function ($scope, $http) {
    var self = $scope;
    self.data = new Array();

    self.statsFilter = {
        page: 1,
        pageSize: 20
    };

    self.goToPage = function (pageNumber) {
        self.statsFilter.page = pageNumber;

        $http({
            method: 'GET',
            url: '/api/magickinder-launching/get-admin-statistics',
            params: self.statsFilter
        }).then(function successCallback(response) {
            self.data = response.data;
        }, function errorCallback(response) {
            alert(response.data.message);
        });
    }
    self.goToPage(1);
}).filter('pages', function () {
    return function (pageNumber, pageCount) {
        pageCount = parseInt(pageCount);
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
});