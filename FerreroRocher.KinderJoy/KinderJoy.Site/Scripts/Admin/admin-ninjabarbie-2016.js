//참여자리스트
(function () {
    var app = angular.module('user', []);

    app.filter('pages', function () {
        return function (input, total) {
            total = parseInt(total);

            for (var i = 1; i < total; i++) {
                input.push(i);
            }
            return input;
        }
    })
    app.controller('List', function ($scope, $http) {
        //목록
        $scope.data = [];
        $scope.params = {
            id: null,
            fromDate: null,
            toDate: null,
            channel: null,
            ipAddress: null,
            name: null,
            mobile: null,
            address: null,
            zipCode: null,
            age: null,
            surprizeType: null,
            page: 1,
            pageSize: 15
        }

        $scope.goToPage = function (pageNumber) {
            $scope.params.page = pageNumber;
            $http({
                method: 'POST',
                url: '/api/2016-ninjabarbie/manager/get-users',
                data: $scope.params
            }).then(function successCallback(response) {
                $scope.data = response.data;
            }, function errorCallback(response) {
                alert(response);
            });
        }
        $scope.goToPage(1);
        $scope.excelDownload = function () {
            location.href = '/manager/2016-ninjabarbie/user-excel-download?' + $.param($scope.params);
        }
    });
})();

//공유리스트
(function () {
    var app = angular.module('sharing', []);

    app.filter('pages', function () {
        return function (input, total) {
            total = parseInt(total);

            for (var i = 1; i < total; i++) {
                input.push(i);
            }
            return input;
        }
    })
    app.controller('List', function ($scope, $http) {
        //목록
        $scope.data = [];
        $scope.params = {
            userId: null,
            name: null,
            mobile: null,
            channel: null,
            surprizeType: null,
            fromDate: null,
            toDate: null,
            snsType: null,
            post: null,
            page: 1,
            pageSize: 15
        }

        $scope.goToPage = function (pageNumber) {
            $scope.params.page = pageNumber;
            $http({
                method: 'POST',
                url: '/api/2016-ninjabarbie/manager/get-sharings',
                data: $scope.params
            }).then(function successCallback(response) {
                $scope.data = response.data;
            }, function errorCallback(response) {
                alert(response);
            });
        }
        $scope.goToPage(1);
        $scope.excelDownload = function () {
            location.href = '/manager/2016-ninjabarbie/sharing-excel-download?' + $.param($scope.params);
        }
    });
})();

//통계
(function () {
    var app = angular.module('statistics', []);
    app.filter('pages', function () {
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
    })
    app.controller('List', function ($scope, $http) {
        $scope.data = [];
        $scope.params = {
            page: 1,
            pageSize: 20
        }
        $scope.goToPage = function (pageNumber) {
            $scope.params.page = pageNumber;
            $http({
                method: 'POST',
                url: '/api/2016-ninjabarbie/manager/get-statistics',
                data: $scope.params
            }).then(function successCallback(response) {
                console.log(response.data);
                $scope.data = response.data;
            }, function errorCallback(response) {
                alert(response);
            });
        }
        $scope.goToPage(1);   
    });
})();