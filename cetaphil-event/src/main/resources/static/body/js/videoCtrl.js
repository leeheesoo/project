// page contents video ctroller
var cotentsMovCtrl;

function onYouTubeIframeAPIReady() {
  cotentsMovCtrl = cotentsMovCtrl('contentsMovPlayer');
}

//=== youtube api load
function loadVideoAPI() {
  // 2. This code loads the IFrame Player API code asynchronously.
  var tag = document.createElement('script');

  tag.src = 'https://www.youtube.com/iframe_api';
  var firstScriptTag = document.getElementsByTagName('script')[0];
  firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
}

//=== youtube player ctrl
function cotentsMovCtrl(playerId) {
  var player = player;
  init();

  function init() {
    player = new YT.Player(playerId, {
      height: '360',
      width: '640',
      playerVars: { rel: 0, playsinline: 1 }, // 동영상 재생 종료 후 동영상리스트가 보이지 않게 함.
      events: {
        onReady: function() {},
        onStateChange: function(e) {
          if (e.data === 0) {
            $('#' + playerId).trigger('videoEnd');
          }
        }
      }
    });
  }

  function playById(videoId) {
    player.loadVideoById(videoId);
  }
  function stopVideo() {
    player.stopVideo();
  }

  return {
    stopVideo: stopVideo,
    playById: playById
  };
}

// $(document).ready(function() {
//   var ageIntroMov = document.getElementById('ageIntro');
//   var $startDim = $('.start-wrap');

//   //영상종료 후 투명 start dimmed on
//   ageIntroMov.addEventListener('ended', function(e) {
//     $startDim.addClass('on');
//   });
// });

// });
