/**
* Youtube API 로드
*/
var tag = document.createElement('script');
tag.src = "https://www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
 
/**
* onYouTubeIframeAPIReady 함수는 필수로 구현해야 한다.
* 플레이어 API에 대한 JavaScript 다운로드 완료 시 API가 이 함수 호출한다.
* 페이지 로드 시 표시할 플레이어 개체를 만들어야 한다.
*/
var surveyYoutubePlayer;


function onYouTubeIframeAPIReady() {
	surveyYoutubePlayer = new YT.Player('surveyMov', { 
        width: '100%',
        height: '100%',
        videoId: 'XIUDzkXJoxU', //youtube palyer id 설정
        playerVars: {
            'autoplay': 0,
            'rel': 0, //추천영상표시안하기
            'showinfo': 0, //영상타이틀,정보표시여부
            'wmode': 'opaque'
        },
        events: {
            'onReady': onPlayerReady, // 플레이어 로드가 완료되고 API 호출을 받을 준비가 될 때마다 실행
            'onStateChange': onPlayerStateChange // 플레이어의 상태가 변경될 때마다 실행
        }
    });
} //onYouTubeIframeAPIReady
 
function onPlayerReady(event) {
    // 플레이어 자동실행 (주의: 모바일에서는 자동실행되지 않음)
	//event.target.playVideo();
}
 
var playerState;
function onPlayerStateChange(event) {
    playerState = event.data == YT.PlayerState.ENDED ? checkEnded() :
                  event.data == YT.PlayerState.PLAYING ? '재생 중' :
                  event.data == YT.PlayerState.PAUSED ? '일시중지 됨' :
                  event.data == YT.PlayerState.BUFFERING ? '버퍼링 중' :
                  event.data == YT.PlayerState.CUED ? '재생준비 완료됨' :
                  event.data == -1 ? '시작되지 않음' : '예외';
}

function checkEnded(){
   // readyParticipate = true;
    $('.mov-cover').show();
}