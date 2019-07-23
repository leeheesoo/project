function createAdminMainStreamViewModel() {
    var self = this;

    self.totalItemCount = ko.observable(0);
    self.pageNumber = ko.observable();
    self.items = ko.observableArray();
    self.count = ko.observable();
    self.pageCount = ko.observable();
    self.pageSize = ko.observable();
    self.hasPreviousPage = ko.observable();
    self.hasNextPage = ko.observable();
    self.isFirstPage = ko.observable();
    self.isLastPage = ko.observable();
    self.firstItemOnPage = ko.observable();
    self.lastItemOnPage = ko.observable();

    self.pageSlide = 10;
    self.pages = ko.computed(function () {
        var pageCount = self.pageCount();
        var pageFrom = Math.max(1, self.pageNumber() - self.pageSlide);
        var pageTo = Math.min(pageCount, self.pageNumber() + self.pageSlide);
        pageFrom = Math.max(1, Math.min(pageTo - 2 * self.pageSlide, pageFrom));
        pageTo = Math.min(pageCount, Math.max(pageFrom + 2 * self.pageSlide, pageTo));

        var result = [];
        for (var i = pageFrom; i <= pageTo; i++) {
            result.push(i);
        }
        return result;
    });

    self.filterFromDate = ko.observable();
    self.filterToDate = ko.observable();
    self.filterDevice = ko.observable();
    self.filterIpAddress = ko.observable();
    self.filterName = ko.observable();
    self.filterAge = ko.observable();
    self.filterGender = ko.observable();
    self.filterSurprise = ko.observable();
    self.filterMobile = ko.observable();

    self.goToPage = function (pageNumber) {
        if (pageNumber != undefined) {
            $.getJSON('/api/main-stream/list/entry', {
                fromDate: self.filterFromDate(),
                toDate: self.filterToDate(),
                device: self.filterDevice(),
                ipAddress: self.filterIpAddress(),
                name: self.filterName(),
                age: self.filterAge(),
                gender: self.filterGender(),
                mainStreamSurpriseType: self.filterSurprise(),
                mobile: self.filterMobile(),
                page: pageNumber,
                pageSize: 20
            }, function (data) {
                ko.mapping.fromJS(data, {}, self);
            }).error(function (data, textStatus, jqXHR) {
                if (textStatus != 'abort') {
                    alert(data.responseJSON.message);
                }
                return;
            });
        }
    }
    self.goToPage(1);
}

function createAdminMainStreamSnsViewModel() {
    var self = this;

    self.totalItemCount = ko.observable(0);
    self.pageNumber = ko.observable();
    self.items = ko.observableArray();
    self.count = ko.observable();
    self.pageCount = ko.observable();
    self.pageSize = ko.observable();
    self.hasPreviousPage = ko.observable();
    self.hasNextPage = ko.observable();
    self.isFirstPage = ko.observable();
    self.isLastPage = ko.observable();
    self.firstItemOnPage = ko.observable();
    self.lastItemOnPage = ko.observable();

    self.pageSlide = 10;
    self.pages = ko.computed(function () {
        var pageCount = self.pageCount();
        var pageFrom = Math.max(1, self.pageNumber() - self.pageSlide);
        var pageTo = Math.min(pageCount, self.pageNumber() + self.pageSlide);
        pageFrom = Math.max(1, Math.min(pageTo - 2 * self.pageSlide, pageFrom));
        pageTo = Math.min(pageCount, Math.max(pageFrom + 2 * self.pageSlide, pageTo));

        var result = [];
        for (var i = pageFrom; i <= pageTo; i++) {
            result.push(i);
        }
        return result;
    });

    self.filterFromDate = ko.observable();
    self.filterToDate = ko.observable();
    self.filterDevice = ko.observable();
    self.filterIpAddress = ko.observable();
    self.filterName = ko.observable();
    self.filterAge = ko.observable();
    self.filterSurprise = ko.observable();
    self.filterMobile = ko.observable();
    self.filterSnsType = ko.observable();
    self.filterSnsId = ko.observable();
    self.filterSnsName = ko.observable();
    self.filterSnsNickName = ko.observable();
    self.filterScrapUrl = ko.observable();
    self.goToPage = function (pageNumber) {
        if (pageNumber != undefined) {
            $.getJSON('/api/main-stream/list/sns', {
                fromDate: self.filterFromDate(),
                toDate: self.filterToDate(),
                device: self.filterDevice(),
                ipAddress: self.filterIpAddress(),
                name: self.filterName(),
                age: self.filterAge(),
                mainStreamSurpriseType: self.filterSurprise(),
                mobile: self.filterMobile(),
                snsType: self.filterSnsType(),
                snsId: self.filterSnsId(),
                snsName: self.filterSnsName(),
                snsNickName: self.filterSnsNickName(),
                scrapUrl: self.filterScrapUrl(),
                page: pageNumber,
                pageSize: 20
            }, function (data) {
                ko.mapping.fromJS(data, {}, self);
            }).error(function (data, textStatus, jqXHR) {
                if (textStatus != 'abort') {
                    alert(data.responseJSON.message);
                }
                return;
            });
        }
    }
    self.goToPage(1);
}

function createAdminMainStreamSnsStatsViewModel() {
    var self = this;

    self.totalItemCount = ko.observable(0);
    self.pageNumber = ko.observable();
    self.items = ko.observableArray();
    self.count = ko.observable();
    self.pageCount = ko.observable();
    self.pageSize = ko.observable();
    self.hasPreviousPage = ko.observable();
    self.hasNextPage = ko.observable();
    self.isFirstPage = ko.observable();
    self.isLastPage = ko.observable();
    self.firstItemOnPage = ko.observable();
    self.lastItemOnPage = ko.observable();

    self.pageSlide = 10;
    self.pages = ko.computed(function () {
        var pageCount = self.pageCount();
        var pageFrom = Math.max(1, self.pageNumber() - self.pageSlide);
        var pageTo = Math.min(pageCount, self.pageNumber() + self.pageSlide);
        pageFrom = Math.max(1, Math.min(pageTo - 2 * self.pageSlide, pageFrom));
        pageTo = Math.min(pageCount, Math.max(pageFrom + 2 * self.pageSlide, pageTo));

        var result = [];
        for (var i = pageFrom; i <= pageTo; i++) {
            result.push(i);
        }
        return result;
    });


    self.goToPage = function (pageNumber) {
        if (pageNumber != undefined) {
            $.getJSON('/api/main-stream/list/sns-stats', {
                page: pageNumber,
                pageSize: 20
            }, function (data) {
                ko.mapping.fromJS(data, {}, self);
            }).error(function (data, textStatus, jqXHR) {
                if (textStatus != 'abort') {
                    alert(data.responseJSON.message);
                }
                return;
            });
        }
    }
    self.goToPage(1);
}

function convertToDate(date) {
    return moment(date).format('YYYY-MM-DD HH:mm:ss');
}