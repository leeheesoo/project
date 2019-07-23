var youtubePlayer = new YoutubePlayer({
			el:'player',
			width:560,
			height:315,
			videoId:'gKCBtpYnpYk',
			playerVars:{
				 'autoplay': 1,
				 'controls': 0,
				 'rel' : 0,
				 'showinfo': 0,
				 'wmode':'opaque'
			},
			events:{
				ready: function(e){
					movieResize($(window).width());
					youtubePlayer.setVolume(0);
					youtubePlayer.setPlaybackQuality('hd720');
				},
				end: function(e){
					youtubePlayer.loadVideoById('gKCBtpYnpYk');
				},
				play: function(e){

				},
				pause: function(e){
				    YoutubePlayer.pauseVideo();
				},
				butter: function(e){

				},
				cue: function(e){

				}
			}
		});

		var oriW = 560;
		var oriH = 315;
		var defaultHeight = 700;
		var $movie = $('#player');

		$(window).resize(function(){
			movieResize($(this).width());
		});

		function movieResize(sw){
			if(!youtubePlayer.getIframe()) return;
			var scale = sw/oriW;
			var height = oriH*scale;
			var width;
			if(height < defaultHeight){
				width = oriW*defaultHeight/oriH;
				height = defaultHeight;
				$movie.css({
					top:0,
					left: (width-sw)*-.5
				});
			}else{
				width = sw;
				$movie.css({
					top: (height-defaultHeight)*-.5,
					left:0
				});
			}
			youtubePlayer.getIframe().width = parseInt(width);
			youtubePlayer.getIframe().height = parseInt(height);
		}
