
var device;
var imgUrl;

var app = new Vue({
  el: '#quizEvent',
  data: {
    loading: true,
    navBottomShow:true,
    device: '',
    imgUrl:'',
    startQuiz:true,
    quizData: [  
      { answer:'o', userAnswer: '', quizId: 0 },
      { answer:'o', userAnswer: '', quizId: 1 },
      { answer:'o', userAnswer: '', quizId: 2 }
    ],
    quizDataV3: [  
      { answer:'o', userAnswer: '', quizId: 0 },
      { answer:'o', userAnswer: '', quizId: 1 },
      { answer:'o', userAnswer: '', quizId: 2 },
      { answer:'x', userAnswer: '', quizId: 3 },  
      { answer:'x', userAnswer: '', quizId: 4 },  
      { answer:'o', userAnswer: '', quizId: 5 },  
      { answer:'x', userAnswer: '', quizId: 6 },  
      { answer:'o', userAnswer: '', quizId: 7 },  
      { answer:'x', userAnswer: '', quizId: 8 } 
    ],
    quizShuffled: [],// shuffle된 quizData
    quizUserData: { // 개인정보
      name: '',
      mobile: '',
      agree: false,
      agree2: false,
      agree3: false
    },
    isSave :false
  },  
  created: function () {
    this.imgUrl += '/renewal/images/ox-quiz/'+deviceKind+'/';
    // this.checkDevice();
    this.doShuffle();   
    this.setCouponUrl();
    setClipboard();
    $('#nav__bottom li').eq(3).addClass('on');
    $('.nav__bottom').addClass('active');
  },
  mounted: function(){            
    this.$nextTick(function () {
      quizSlider.init('.quizSlider');   
    });
  },
  methods: {      
    goNextQuiz: function(){      
      quizSlider.goNextQuiz();      
    },
    doShuffle: function(){
      this.quizShuffled = [];

      if(openVersion === 2){ //이전버전 문제3개
        this.quizShuffled = utils.getShuffledTgNum(this.quizData, 3);
      } else { //문제9개 3차오픈
        this.quizShuffled = utils.getShuffledTgNum(this.quizDataV3, 3);
      }
    },
    setCouponUrl: function () {
      if (deviceKind === 'w') { 
        //this.urlConponResit = 'http://dev.babience.com/member/login.jsp'; //test url
        this.urlConponResit = 'https://www.babience.com:553/member/login.jsp';
      } else {
        //this.urlConponResit = 'http://dev.babience.com/m/member/login.jsp'; //test url
        this.urlConponResit = 'https://www.babience.com:553/m/member/login.jsp';
      }
    },
    quizUserInfo: function (e) { 
      var self = this;
      var $form = $(this);
      $.ajax({
          url: '/api/kabrita-renewal/quiz',
          type: 'POST',
          data: this.quizUserData,
          success: function (res) {
        	  self.isSave = true;
              closePopup('quiz-personal');
              openPopup('coupon-share');
              $('#quiz-form').clearForm();
              // self.dataReset();
          },
          error: function (res) {
              alert(res.responseJSON.error);
          },
          beforeSend: function () {
              self.loading = true;
          },
          complete: function () {
              self.loading = false;
          }
      });
    },
    sharePop : function(){
    	this.isSave = false;
    	openPopup('event-share');
    },
    kakaotalkShare: function () {
    	var self = this;
    	var url = 'kabrita_quiz?utm_source=quiz-share&utm_medium=kakaotalk&utm_campaign=kabrita-renewal';
      
    	if(self.isSave){
    		url = 'kabrita_quiz?utm_source=quiz-event&utm_medium=kakaotalk&utm_campaign=kabrita-renewal';
    	}
      
    	Kakao.Link.sendCustom({
    		templateId: 13398,
    		templateArgs: { 'url': url },
    		success:function() {
    			var shareData = {};
    			shareData.sharingType = 'kakao';
    			shareData.url = '/api/kabrita-renewal/quiz/sharing';
    			if(self.isSave){
    				saveSnsShare(shareData, function () {
    				})  
    			}
    		}
    	});
    },
    movSection:function(){
      var quizTop = $('.quiz-wrap').offset().top;
      $('html,body').stop().animate({scrollTop:quizTop},500)
    },
    quizFinish:function(){
      var $quizEnd = $('.quiz-finish');
      $('.quiz-number').hide();
      $('.quiz-text').hide();
      $quizEnd.fadeIn();
    },
    quizStart:function(){
      $('.kv').hide();
      $('.quiz-wrap').fadeIn();
      $('.quizSlider')[0].slick.refresh();  
      $('#babience-landing-btn').attr('src','/renewal/images/ox-quiz/w/event-bi-quiz.png');
      $('html,body').css('background','#a2b3d8');
    }

  }
});




//clipboard
function setClipboard() {
  clipboard = new Clipboard('.codecopy');
  clipboard.on('success', function (e) {
    //console.log(nicknameEventModel.isEvent);

    var snsData = {};
    snsData.sharingType = 'url';
    snsData.url = '/api/kabrita-renewal/quiz/sharing';
    
    if(app.isSave){
    	saveSnsShare(snsData, function () {
            alert('복사되었습니다');
        })
    }else{
    	alert('복사되었습니다.');
    }
  });
  clipboard.on('error', function (e) {
      alert('복사실패')
  });
}

function saveSnsShare(data, successCallback) {
  $.ajax({
      url: data.url,
      type: 'POST',
      data: {
    	  sharingType: data.sharingType
      },
      success: function (res) {
          if (typeof successCallback == 'function') {
              successCallback();
          }
      },
      error: function (res) {
          alert(res.responseJSON.error);
      }
  });
}

window.onload = function() {
  app.loading = false;
}

//slick 재생성
function sliderSetting() {
  var $quizSlider = $('.slick-slider');
  $quizSlider.slick('slickGoTo', 0); // 순서 reset
  setTimeout(function() {
    $quizSlider.slick('refresh');
  }, 100);
}
