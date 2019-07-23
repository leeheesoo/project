/*
* google analytics helper 
* version: 0.01
* requires: JQuery 1.7(↑)
*/
(function (i, s, o, g, r, a, m) {
	i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
		(i[r].q = i[r].q || []).push(arguments)
	}, i[r].l = 1 * new Date(); a = s.createElement(o),
    m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
})(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');
(function ($) {
	$.ga = {};
	var settings = {
		analyticsId: 'UA-XXXXXXX-Y',
		obj: ".GA"
	};

	$.ga.init = function (options) {
		settings = $.extend(settings, options);
		$(document).on('click', settings.obj, function (e) {
			var $this = $(this);
			var category = $(this).attr('data-category');
			var action = $(this).attr('data-action') || $(this).attr('title') || $(this).attr('alt');
			var label = $(this).attr('data-label');
			var value = $(this).attr('data-value');
			var delay = $(this).attr('data-delay');

			$.ga.trackEvent(category, action, label, value);
			if (delay && !$this.attr('target')) {
				delay = parseInt(delay);
				e.preventDefault();
				var o = this;
				setTimeout(function () {
					document.location.href = o.href;
				}, delay);
			}
		});
		ga('create', settings.analyticsId, 'auto');
		ga('send', 'pageview');
	};

	$.ga.trackEvent = function (category, action, label, value) {
		ga('send', 'event', category, action, label, value);
	};

}(jQuery));