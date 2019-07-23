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
var samplingEventYoutubePlayer;
var shareEventYoutubePlayer;

function onYouTubeIframeAPIReady() {
    //샘플링이벤트
	samplingEventYoutubePlayer = new YT.Player('eventMovKit', { 
        width: '100%',
        height: '100%',
        videoId: 'nTgUomxt2pQ', //youtube palyer id 설정
        playerVars: {
            'autoplay': 0,
            'rel': 0, //추천영상표시안하기
            'showinfo': 0, //영상타이틀,정보표시여부
            'wmode': 'opaque',
            'controls': 0 
        },
        events: {
            'onStateChange': onPlayerStateChange // 플레이어의 상태가 변경될 때마다 실행
        }

    });
    //영상공유이벤트
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

var intervalId = null;
var currentTime = '';
var $playTime = $('.playtime');

function onPlayerStateChange(event) {
    //동영상보기 완료
    if (event.data ===  1 ) { // 재생됐을때   
     intervalId = setInterval( function(){
        currentTime = Math.floor(event.target.getCurrentTime()); // play time       
        $playTime.text(60-currentTime);
       // console.log('몇 초? // ' + currentTime )

        if(currentTime>=60){
            checkPlay = true;
            $playTime.fadeOut();           
            clearInterval(intervalId);
            intervalId = null;
        } 
     }, 1000)
    } else if ( event.data === 0 || event.data === 2 ){ //일시중지,종료
        //일시중지
        clearInterval(intervalId);
        intervalId = null;
    }
}

 
