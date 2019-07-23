
// This function creates an <iframe> (and YouTube player)
//    after the API code downloads.

window.onload = function () {
    var tag = document.createElement('script');
    tag.src = "https://www.youtube.com/iframe_api";
    var firstScriptTag = document.getElementsByTagName('script')[0];
    firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
};

function onYouTubeIframeAPIReady() {
  var width = deviceKind === 'pc' ? 1000 : 600;
  var height=  deviceKind === 'pc' ? 562 : 337;
    cetaphilPlayer = new YT.Player('player', {
        height: height, 
        width: width,
        videoId: 'wmzMB1sq0fU',
        playerVars: {
            controls: 1,
            showinfo: 0,
            rel: 0, 
            playsinline: 1
        },
        events: {
            'onReady': onPlayerReady,
            'onStateChange': mainPlayerEvent
        }
    });
}

function onPlayerReady(event) {
    // console.log('ready');
    //event.target.playVideo();
    // event.target.setPlaybackQuality('large');
}

function mainPlayerEvent(event) {
    /* if (event.data == YT.PlayerState.ENDED) {
        //alert(event.target.getPlaybackQuality());
        // app.videoWatched = true;
        //event.target.loadVideoById('WwJEnONEuqk');
        //event.target.stopVideo();
        //setTimeout(function () {
        //    alert('공유하기 버튼을 클릭해주세요.');
        //}, 500);
        //console.dir(event);
    } */
}

/* function playVideo(player) {
  if(player){
    player.playVideo();
  } else {
    return;
  }
}

function stopVideo(player) {
  if(player){
    player.stopVideo();
  } else {
    return;
  }
} */
