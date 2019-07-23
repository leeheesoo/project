/**
* Youtube API 로드
*/
var tag = document.createElement('script');

tag.src = "https://www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);


var intervalId = null;

/**
* onYouTubeIframeAPIReady 함수는 필수로 구현해야 한다.
* 플레이어 API에 대한 JavaScript 다운로드 완료 시 API가 이 함수 호출한다.
* 페이지 로드 시 표시할 플레이어 개체를 만들어야 한다.
*/
var eventYoutubePlayer;

function onYouTubeIframeAPIReady() {
	shareEventYoutubePlayer = new YT.Player('eventMovShare', { 
        width: '100%',
        height: '100%',
        videoId: 'CLbHiF3fWUk', 
        playerVars: {
            'autoplay': 0,
            'rel': 0, 
            'showinfo': 0, 
            'wmode': 'opaque'
        }
    });
} //onYouTubeIframeAPIReady





