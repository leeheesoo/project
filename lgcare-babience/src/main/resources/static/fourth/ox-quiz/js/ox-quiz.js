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
      { answer:'x', userAnswer: '', quizId: 0, boolean:null },
      { answer:'x', userAnswer: '', quizId: 1, boolean:null },
      { answer:'o', userAnswer: '', quizId: 2, boolean:null },
      { answer:'o', userAnswer: '', quizId: 3, boolean:null },  
      { answer:'o', userAnswer: '', quizId: 4, boolean:null },  
      { answer:'x', userAnswer: '', quizId: 5, boolean:null },  
      { answer:'o', userAnswer: '', quizId: 6, boolean:null },  
      { answer:'x', userAnswer: '', quizId: 7, boolean:null },  
      { answer:'o', userAnswer: '', quizId: 8, boolean:null },
      { answer:'x', userAnswer: '', quizId: 9, boolean:null },  
      { answer:'o', userAnswer: '', quizId: 10, boolean:null },  
    ],
    quizShuffled: [],// shuffle된 quizData
    quizUserData: { // 개인정보
      name: '',
      mobile: '',
      answerCount:'',
      agree: false,
      agree2: false,
      agree3: false
    },
    isSave :false,
    quizCouponImgName:'',
    urlConponResit :'',
    shareBtnText: '' //쿠폰발급 및 안내 팝업 - URL복사, 공유하기 버튼 GA text명 설정
  },  
  created: function () {
    this.imgUrl += '/fourth/images/ox-quiz/'+deviceKind+'/';
    // this.checkDevice();
    this.doShuffle();   
    //this.setCouponUrl();
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
      this.quizShuffled = utils.getShuffledTgNum(this.quizData, 3);
    },
    // setCouponUrl: function () {
    //   if (deviceKind === 'w') { 
    //     //this.urlConponResit = 'http://dev.babience.com/member/login.jsp'; //test url
    //     this.urlConponResit = 'https://www.babience.com:553/member/login.jsp';
    //   } else {
    //     //this.urlConponResit = 'http://dev.babience.com/m/member/login.jsp'; //test url
    //     this.urlConponResit = 'https://www.babience.com:553/m/member/login.jsp';
    //   }
    // },
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
    quizCouponImg:function(){
      var answerCount = this.quizUserData.answerCount;
      var imgLocation = '/fourth/images/ox-quiz/popup/';

      if(answerCount<=1){
        this.quizCouponImgName = imgLocation+'coupon5.jpg';
        this.shareBtnText = '5%';
        if (deviceKind === 'w') { 
          this.urlConponResit = 'https://www.babience.com:553/member/login.jsp?ct=DheidhjA34';
        } else {
          this.urlConponResit = 'https://www.babience.com:553/m/member/login.jsp?ct=DheidhjA34';
        }
      } else if (answerCount === 2){
        this.quizCouponImgName = imgLocation+'coupon10.jpg';
        this.shareBtnText = '10%';
        if (deviceKind === 'w') { 
          this.urlConponResit = 'https://www.babience.com:553/member/login.jsp?ct=tLq310dfkc';
        } else {
          this.urlConponResit = 'https://www.babience.com:553/m/member/login.jsp?ct=tLq310dfkc';
        }
      } else {
        this.quizCouponImgName = imgLocation+'coupon15.jpg';
        this.shareBtnText = '15%';
        if (deviceKind === 'w') { 
          this.urlConponResit = 'https://www.babience.com:553/member/login.jsp?ct=tlqDh398df';
        } else {
          this.urlConponResit = 'https://www.babience.com:553/m/member/login.jsp?ct=tlqDh398df';
        }
      }
    },
    quizFinish:function(){
      var $quizEnd = $('.quiz-finish');
      $('.quiz-number').hide();
      $('.quiz-text').hide();
      $quizEnd.fadeIn();

      var trueLength = 0;
      this.quizShuffled.forEach(function(e) {   
        var quizBoolean = e.boolean;    
        if(quizBoolean === true){
          trueLength +=1;
        }     
      });
     // console.log(trueLength);
      this.quizUserData.answerCount = trueLength;
      this.quizCouponImg();
    },
    quizStart:function(){
      $('.kv').hide();
      $('.quiz-wrap').fadeIn();
      $('.quizSlider')[0].slick.refresh();  
      $('#babience-landing-btn').attr('src','/renewal/images/ox-quiz/w/event-bi-quiz.png');
      $('html,body').css('background','#a2b3d8');
    },
    quizReplay:function(){
      $('.quizSlider').slick('slickGoTo', 0); 
      $('.quiz-finish').hide();
      $('.quiz-number').fadeIn();
      //this.quizUserData={}; //user초기화
      this.quizStart();

      //quiz data 초기화
      $.each(this.quizData, function(){
        this.userAnswer = '';
        this.boolean = null;
      });
      this.doShuffle();
      closePopup('coupon-share');

      this.$nextTick(function(){
        $('.ox-btns').show();
      });

      
    },


  }
});




//clipboard
function setClipboard() {
  clipboard = new Clipboard('.codecopy');
  clipboard.on('success', function (e) {

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