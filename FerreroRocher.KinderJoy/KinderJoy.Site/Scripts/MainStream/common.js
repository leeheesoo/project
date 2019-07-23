var pop = {

    $dimObj: $(".dimm"),
    duration: 600,
    easing: null,

    open: function ($ele) {
        var scTop = $(window).scrollTop();
        $("#" + $ele).show();
        $("#" + $ele).css("top", scTop + 150);
    },
    close: function ($ele) {
        $("#" + $ele).hide();
    },
    dimOn: function () {
        this.$dimObj.show();
    },
    dimOff: function () {
        this.$dimObj.hide();
    }
};

function openExt(target) {
	$("#"+ target).show();
}

function closeExt(target) {
	$("#"+ target).hide();
}

jQuery(document).ready(function(){
	$(".pop_tab_memu").on("click", "> a" ,function (e) {
		$(this).addClass("is_on");
		$(this).siblings().removeClass("is_on");
		$(".pop_tab_con").hide();
		$(this.hash).show();
		e.preventDefault();
	});
	$(".tab_menu_winner").trigger("click");
});
