﻿var adminModule = angular.module('app-admin-findingdory', []);

adminModule.controller('ctrl-admin-findingdory-user', function ($scope, $http) {
    var self = $scope;
    self.data = new Array();

    self.userFilter = {
        fromDate: null,
        toDate: null,
        name: null,
        mobile: null,
        channel: null,
        page: 1,
        pageSize: 20
    };

    self.goToPage = function (pageNumber) {
        self.userFilter.page = pageNumber;

        $http({
            method: 'GET',
            url: '/api/2017-findingdory/get-users',
            params: self.userFilter
        }).then(function successCallback(response) {
            self.data = response.data;
        }, function errorCallback(response) {
            alert(response.data.message);
        });
    }

    self.excelDownload = function () {
        location.href = '/manager/2017-findingdory/user-excel-download?' + $.param(self.userFilter);
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



adminModule.controller('ctrl-admin-findingdory-sns', function ($scope, $http) {
    var self = $scope;
    self.data = new Array();

    self.snsFilter = {
        fromDate: null,
        toDate: null,
        name: null,
        mobile: null,
        channel: null,
        snsType:null,
        page: 1,
        pageSize: 20
    };

    self.goToPage = function (pageNumber) {
        self.snsFilter.page = pageNumber;

        $http({
            method: 'GET',
            url: '/api/2017-findingdory/get-sns',
            params: self.snsFilter
        }).then(function successCallback(response) {
            self.data = response.data;
        }, function errorCallback(response) {
            alert(response.data.message);
        });
    }

    self.excelDownload = function () {
        location.href = '/manager/2017-findingdory/sns-excel-download?' + $.param(self.snsFilter);
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



adminModule.controller('ctrl-admin-findingdory-stats', function ($scope, $http) {
    var self = $scope;
    self.data = new Array();

    self.snsStatsFilter = {
        page: 1,
        pageSize: 20
    };

    self.goToPage = function (pageNumber) {
        self.snsStatsFilter.page = pageNumber;

        $http({
            method: 'GET',
            url: '/api/2017-findingdory/get-stats',
            params: self.snsStatsFilter
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