function make1stAnniversaryViewModel() {
    var self = this;
    self.entryId = ko.observable(-1);
    self.isLoading = ko.observable(true);

    self.openEntry = function (isClosed) {
        if (isClosed === 'True') {
            alert('이벤트가 종료되었습니다.');
            return;
        }
        $('#frmCreateOneYearAnniversaryEntry').clearForm();
        $('#frmUpdateOneYearAnniversaryEntry').clearForm();
        openPop('pop-textbox');
    }

    self.begin1stAnniversary = function () {
        self.isLoading(false);
    }
    self.complete1stAnniversary = function () {
        self.isLoading(true);
    }
    self.success1stAnniversary = function (data) {
        self.entryId(data.Id);
        closePop('pop-textbox');
        openPop('pop-Information');
    }

    self.updateOneYearAnniversaryEntry = function () {
        var frm = $('#frmUpdateOneYearAnniversaryEntry');
        var data = frm.serialize();
        if (frm.valid()) {
            $.ajax({
                url: '/api/1st-anniversary/put',
                method: 'PUT',
                data: data,
                beforeSend: function () {
                    self.begin1stAnniversary();
                },
                complete: function () {
                    self.complete1stAnniversary();
                },
                success: function () {
                    alert('완료되었습니다.');
                    closePop('pop-Information');
                    self.getData(1);
                },
                error: function (jqXHR, status, err) {
                    self.failure1stAnniversary(jqXHR);
                }
            });
        }
        return false;
    }

    self.failure1stAnniversary = function (err) {
        alert(err.responseJSON.Message);
        return;
    }

    self.checkMessageLength = function (data, event) {
        chkTextLength(event.currentTarget, 1000, event.currentTarget.placeholder);
    }

    self.TotalItemCount = ko.observable();
    self.PageNumber = ko.observable();
    self.Items = ko.observableArray();
    self.Count = ko.observable();
    self.PageCount = ko.observable();
    self.PageSize = ko.observable();
    self.HasPreviousPage = ko.observable(false);
    self.HasNextPage = ko.observable(false);
    self.IsFirstPage = ko.observable(true);
    self.IsLastPage = ko.observable(true);
    self.pageSlide = 1;
    self.pages = ko.computed(function () {
        var pageCount = self.PageCount();
        var pageFrom = Math.max(1, self.PageNumber() - self.pageSlide);
        var pageTo = Math.min(pageCount, self.PageNumber() + self.pageSlide);
        pageFrom = Math.max(1, Math.min(pageTo - 2 * self.pageSlide, pageFrom));
        pageTo = Math.min(pageCount, Math.max(pageFrom + 2 * self.pageSlide, pageTo));
        var result = [];
        for (var i = pageFrom; i <= pageTo; i++) {
            result.push(i);
        }
        return result;
    });

    self.list = ko.observableArray();
    self.getData = function (pageNumber) {
        if (pageNumber < 1 || pageNumber > self.PageCount()) {
            return;
        }
        $.getJSON('/api/1st-anniversary/get/list', {
            isShow: true,
            isList: true,
            Page: pageNumber,
            PageSize: 5
        }, function (res) {
            ko.mapping.fromJS(res, {}, self);
            //list height controll
            listResize();

        }).error(function (jqXHR, status, err) {
            self.failure1stAnniversary(jqXHR);
        });
    }
    self.getData(1);

    self.convertToDate = function (date) {
        return moment(date).format('YYYY-MM-DD');
    }

    self.getListIndex = function (idx) {
        var pageIdx = (self.PageNumber() - 1) * self.PageSize() + idx;
        return self.TotalItemCount() - pageIdx;
    }
}
