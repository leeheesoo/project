/**
 * @author ares
 */

(function(window){

	var tag = document.createElement('script');
	tag.src = "https://www.youtube.com/iframe_api";
	var firstScriptTag = document.getElementsByTagName('script')[0];
	firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

	window.onYouTubeIframeAPIReady = function(){};

	var YoutubePlayer = function(option){
		var player;
		var interval;
		var self = this;
		var el = option.el || 'player';
		var events = {
			ready: option.events.ready,
			end: option.events.end,
			pause: option.events.pause,
			play: option.events.play,
			buffer: option.events.buffer,
			cue: option.events.cue
		}

		init();

		function init(){
			interval = setInterval(createYoutubePlayer, 100);
		}

		/*---------callback event------------*/

		function onPlayerReady(e){
			if(events.ready)
				events.ready.call(self, e);
		}
		function onStateChange(e){
			switch(e.data){
				case YT.PlayerState.ENDED :
					if(events.end)
						events.end.call(self, e);
					break;
				case YT.PlayerState.PLAYING :
					if(events.play)
						events.play.call(self, e);
					break;
				case YT.PlayerState.PAUSED :
					if(events.pause)
						events.pause.call(self, e);
					break;
				case YT.PlayerState.BUFFERING :
					if(events.buffer)
						events.buffer.call(self, e);
					break;
				case YT.PlayerState.CUED :
					if(events.cue)
						events.cue.call(self, e);
					break;
			}
		}

		function createYoutubePlayer() {

		    if (typeof (YT) == 'undefined' || typeof (YT.Player) == 'undefined') {
		        return;
		    } else {
		        if (!window.YT.Player) return;

		        clearInterval(interval);
		        player = new YT.Player(el, {
		            width: option.width,
		            height: option.height,
		            videoId: option.videoId,
		            playerVars: option.playerVars,
		            events: {
		                'onReady': onPlayerReady,
		                'onStateChange': onStateChange
		            }
		        });
		    }



		   
		}

		/*---------public method------------*/

		/*loadVideoById({'videoId': 'bHQqvYy5KYo',
               'startSeconds': 5,
               'endSeconds': 60,
               'suggestedQuality': 'large'});
		 */
		function loadVideoById(object){
			player.loadVideoById(object);
		}
		function cueVideoById(object){
			player.cueVideoById(object);
		}
		/*loadVideoById({'playlist': ['xXTwS__Nxuo', 'z8OE3XmfQ9c'],
               'index': 0,
               'startSeconds': 60,
               'suggestedQuality': 'large'});
        */
		function loadPlaylist(object){
			player.loadPlaylist(object);
		}
		function cuePlaylist(object){
			player.cuePlaylist(object);
		}
		function playVideo(){
			player.playVideo();
		}
		function pauseVideo(){
			player.pauseVideo();
		}
		function stopVideo(){
			player.stopVideo();
		}
		function seekTo(seconds, allowSeekAhead){
			player.seekTo(seconds, allowSeekAhead);
		}
		function nextVideo(){
			player.nextVideo();
		}
		function previousVideo(){
			player.previousVideo();
		}
		function playVideoAt(index){
			player.playVideoAt(index);
		}
		function mute(){
			player.mute();
		}
		function unMute(){
			player.unMute();
		}
		function isMuted(){
			return player.isMuted();
		}
		function setVolume(volume){
			player.setVolume(volume); // 0-100
		}
		function getVolume(){
			return player.getVolume();
		}
		function setSize(width, height){
			return player.setSize(width, height);
		}
		function setPlaybackRate(rate){
			player.setPlaybackRate(rate); //0.25, 0.5, 1, 1.5, 2
		}
		function getPlaybackRate(){
			return player.getPlaybackRate();
		}
		function setLoop(bool){
			player.setLoop(bool);
		}
		function setShuffle(bool){
			player.setShuffle(bool);
		}
		function getVideoLoadedFraction(){
			return player.getVideoLoadedFraction(); // 0-1
		}
		/*
		 * -1 –시작되지 않음
			0 – 종료
			1 – 재생 중
			2 – 일시중지
			3 – 버퍼링
			5 – 동영상 신호
		 */
		function getPlayerState(){
			return player.getPlayerState();
		}
		function getCurrentTime(){
			return player.getCurrentTime();
		}
		function setPlaybackQuality(suggestedQuality){
			player.setPlaybackQuality(suggestedQuality); //small, medium, large, hd720, hd1080, highres 또는 default
		}
		function getPlaybackQuality(){
			return player.getPlaybackQuality();
		}
		function getAvailableQualityLevels(){
			return player.getAvailableQualityLevels();
		}
		function getDuration(){
			return player.getDuration();
		}
		function getVideoUrl(){
			return player.getVideoUrl();
		}
		function getVideoEmbedCode(){
			return player.getVideoEmbedCode();
		}
		function getPlaylist(){
			return player.getPlaylist();
		}
		function getPlaylistIndex(){
			return player.getPlaylistIndex();
		}
		function getIframe(){
			return player.getIframe();
		}
		function destroy(){
			player.destroy();
		}

		return {
			loadVideoById: loadVideoById,
			cueVideoById: cueVideoById,
			loadPlaylist: loadPlaylist,
			cuePlaylist: cuePlaylist,
			playVideo: playVideo,
			pauseVideo: pauseVideo,
			stopVideo: stopVideo,
			seekTo: seekTo,
			nextVideo: nextVideo,
			previousVideo: previousVideo,
			playVideoAt: playVideoAt,
			mute: mute,
			unMute: unMute,
			isMuted: isMuted,
			setVolume: setVolume,
			getVolume: getVolume,
			setSize: setSize,
			setPlaybackRate: setPlaybackRate,
			getPlaybackRate: getPlaybackRate,
			setLoop: setLoop,
			setShuffle: setShuffle,
			getVideoLoadedFraction: getVideoLoadedFraction,
			getPlayerState: getPlayerState,
			getCurrentTime: getCurrentTime,
			setPlaybackQuality: setPlaybackQuality,
			getPlaybackQuality: getPlaybackQuality,
			getAvailableQualityLevels: getAvailableQualityLevels,
			getDuration: getDuration,
			getVideoUrl: getVideoUrl,
			getVideoEmbedCode: getVideoEmbedCode,
			getPlaylist: getPlaylist,
			getPlaylistIndex: getPlaylistIndex,
			getIframe: getIframe,
			destroy: destroy
		}
	};

	window.YoutubePlayer = YoutubePlayer;
})(window);