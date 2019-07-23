/*!

 * jQuery shy4blue Placeholder Plugin 

 * Copyright (c) 2011 Hyeok Namkung

 * Version: 0.02

 * Requires: jQuery 

 */

;(function($) {



	$.fn.placeholder = function(options) {

		var opts = $.extend({

			type: 'text',

			text: 'Input here.',

			color: '#aaa',

			background: {} 

		}, options || {});

		return this.each(function() {

			

			this.s4b_focused = false;

			switch (opts.type) {

			case 'text':			

				if ($(this).val() == '') {

					$(this).val(opts.text);

					this.oldColor = $(this).css('color');

					$(this).css('color', opts.color);

				}



				$(this).focus(function() {

					if (this.s4b_focused == false) {

						$(this).val('');

						$(this).css('color', this.oldColor);

						this.s4b_focused = true;

					}

				});

				$(this).blur(function() {

					if ($(this).val() == '') {

						$(this).val(opts.text);

						this.oldColor = $(this).css('color');

						$(this).css('color', opts.color);

						this.s4b_focused = false;

					}

				});

				break;

			case 'background':

				if ($(this).val() == '') {

					$(this).css('background-image', 'url(' + opts.background + ')').css('background-repeat', 'no-repeat');

				}



				$(this).focus(function() {

					if (this.s4b_focused == false) {

						$(this).css('background-image', 'none');

						this.focused = true;

					}

				});

				$(this).blur(function() {

					if ($(this).val() == '') {

						$(this).css('background-image', 'url(' + opts.background + ')').css('background-repeat', 'no-repeat');

						this.focused = false;

					}

				});

				break;

			}

		});

	}



})(jQuery);

