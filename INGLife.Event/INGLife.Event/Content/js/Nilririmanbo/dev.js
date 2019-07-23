var nilririmamboEventModel = new Vue({
    el: '#nilririmanbo-event',
    data: {
        dataFilters: {
            limit: 100,
            page: 1,
            tags: '닐리리만보',
            types: 'image'
        },
        imageData: []
    },
    created: function () {
        this.getInstagramList();       

    },
    methods: {
        beginEntry: function () {
            $('.loading').show();
            $('#entryPopCloseBtn').hide();
            $('#entrySaveBtn').hide();
        },
        completeEntry: function () {
            $('.loading').hide();
        },
        successEntry: function (res) {
            $('.loading').hide();
            alert('이벤트 응모가 완료되었습니다.');
            closePop('input-form');
            $('#nilririmamboForm')[0].reset();
            $('#entryPopCloseBtn').show();
            $('#entrySaveBtn').show();
        },
        failEntry: function (xhr, err) {
            if (err !== 'abort') {
                alert(xhr.responseJSON.message);
            }
        },
        openEntryInsertPop: function () {
            if (eventClose == 'True') {
                alert('이벤트가 종료되었습니다.');
                return;
            }
            openPop('input-form');
        },
        openInstaPopup: function (item) {
            if (item.isEmpty) return;
            console.log(item);
            openPop('insta-post');
            $("#insta_image").attr('src', item.url);
            $("#insta_user").text(item.username);
            $("#insta_time").text(item.ts.substring(0,10));
            $("#insta_text").text(item.caption);
            $('.insta_move').prop('href', item.link);
        },
        getInstagramList: function () {
            var self = this;

            $.ajax({
                url: 'https://kr-api.awesomewallhq.com/posts/megazone3',
                type: 'get',
                data: self.dataFilters,
                async: false,
                success: function (res) {
                    var leftNum = Math.abs( 6 - (res.items.length % 6) );
                    var tempList = res.items;
                    for (var i = 0; i < leftNum; i++) {
                        tempList.push({ url: '/Content/Images/Nilririmanbo/slider-init-img.png', isEmpty: true });
                    }
                    self.imageData = [];
                    self.imageData = tempList;
                }.bind(this)
            });
        },
    }
});