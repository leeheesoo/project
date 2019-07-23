var asperoApp = new Vue({
    el: '#aspero-instagram',
    data: {
        items: [],
        detail: {}
    },
    methods: {
        loadInstagramList: function () {
            var url = pentacleUrl + '/api/instagram/post/get/data?callback=callbackDataList';
            $.ajax({
                url: pentacleUrl + "/api/instagram/post/get/data?tagId=7&callback=asperoApp.callbackDataList",
                contentType: "application/json",
                dataType: "jsonp",
                type: 'GET',
                data: { PageSize: 10000, Page: 1 }
            });
        },
        callbackDataList: function (res) {
            this.items = res;
            if (this.items.length % 8 > 0) {
                for (var i = (8 - this.items.length % 8) ; i > 0; i--) {
                    this.items.push({ 'ThumbnailSrc': '/Content/images/aspero-launching/thum-logo.jpg' });
                }
            }
        },
        openDetailPop: function (idx) {
            this.detail = this.items[idx];
            if (this.detail.Id !== undefined) {
                openPop('popup-insta');
            }
        }
    }
});
asperoApp.loadInstagramList();

$(window).load(function () {
    $('.slider-item').css('display', 'block');
    $('.loader').css('display', 'none');
    //슬라이더
    $('.slider').slick({
        dots: false,
        infinite: false,
        speed: 300,
        rows: 2,
        slidesToShow: 4,
        slidesToScroll: 4,
        responsive: [
          {
              breakpoint: 935,
              settings: {
                  slidesToShow: 4,
                  slidesToScroll: 4,
              }
          },
          {
              breakpoint: 600,
              settings: {
                  slidesToShow: 2,
                  slidesToScroll: 2,
                  rows: 4
              }
          }
        ]
    });
    listResize();
})