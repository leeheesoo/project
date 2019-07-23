// place holder
$.fn.placeholder = function(option){

	var input = $(this);

	$.fn.placeholder.option = {
		color: '#8e8b8b',
		fontSize: '18px',
		fontFamily: 'dotum, sans-serif',
        fontWeight: 'normal'
	};

	option = $.extend($.fn.placeholder.option, option);

	return this.each(function(){
		var text = option.text;

		this.value = text;

		input.css({
			color: option.color
		});

		input.on('click, focus',function(){
			if(this.value === text) {
				this.value = '';
			}
		});

		input.on('focusout, blur',function(){
			if(this.value === '' ){
				this.value = text;
			}
		});
	});
};
