var clipboard = null;
var selectArr = [];
var experienceRouteArr = [];
var advanceRouteArr=[];

var app = new Vue({
  el: '#surveyEvent',
  data: surveyEventData(),
  created: function () {
     openPopup('review-winner'); //당첨자발표
    surveyScrollTab();
    setClipboard();
    $('#nav__bottom li').eq(2).addClass('on');
  },
  methods: {
    openPostCode: function (popName, zipCode, address) { // 우편번호팝업
      var self = this;
      this.popName = popName;

      if(deviceKind === 'w'){
          closePopup(popName);
          openPopup('post');
        new daum.Postcode({
          oncomplete: function (data) {
            // 검색결과 항목을 클릭했을때 실행할 코드를 작성하는 부분.
  
            // 각 주소의 노출 규칙에 따라 주소를 조합한다.
            // 내려오는 변수가 값이 없는 경우엔 공백('')값을 가지므로, 이를 참고하여 분기 한다.
            var fullAddr = data.address; // 최종 주소 변수
            var extraAddr = ''; // 조합형 주소 변수
  
            // 기본 주소가 도로명 타입일때 조합한다.
            if (data.addressType === 'R') {
              //법정동명이 있을 경우 추가한다.
              if (data.bname !== '') {
                extraAddr += data.bname;
              }
              // 건물명이 있을 경우 추가한다.
              if (data.buildingName !== '') {
                extraAddr += (extraAddr !== '' ? ', ' + data.buildingName : data.buildingName);
              }
              // 조합형주소의 유무에 따라 양쪽에 괄호를 추가하여 최종 주소를 만든다.
              fullAddr += (extraAddr !== '' ? ' (' + extraAddr + ')' : '');
            }
  
            // 우편번호와 주소 정보를 해당 필드에 넣는다.
            $('#' + zipCode).val(data.zonecode);
            $('#' + address).val(fullAddr);
  
            // iframe을 넣은 element를 안보이게 한다.
            closePopup('post');
            // 개인정보 팝업을 다시 연다.
            openPopup(popName);
  
            if (popName == 'survey01-personal') {
              self.experienceData.zipCode = data.zonecode;
              self.experienceData.address = fullAddr
            } else if (popName == 'survey02-personal') {
              self.advanceApplicationSendData.zipCode = data.zonecode;
              self.advanceApplicationSendData.address = fullAddr;
            } else if (popName == 'review-personal') {
              self.reviewData.zipCode = data.zonecode;
              self.reviewData.address = fullAddr;
            }
  
            // 커서를 상세주소 필드로 이동한다.
            //$('#' + addressDetailId).focus();
  
          },
          width: '640px',
          height: '590px'
        }).embed(document.getElementById('searchPostCode'));
      } else {
        new daum.Postcode({
          oncomplete: function (data) {
            // 검색결과 항목을 클릭했을때 실행할 코드를 작성하는 부분.
  
            // 각 주소의 노출 규칙에 따라 주소를 조합한다.
            // 내려오는 변수가 값이 없는 경우엔 공백('')값을 가지므로, 이를 참고하여 분기 한다.
            var fullAddr = data.address; // 최종 주소 변수
            var extraAddr = ''; // 조합형 주소 변수
  
            // 기본 주소가 도로명 타입일때 조합한다.
            if (data.addressType === 'R') {
              //법정동명이 있을 경우 추가한다.
              if (data.bname !== '') {
                extraAddr += data.bname;
              }
              // 건물명이 있을 경우 추가한다.
              if (data.buildingName !== '') {
                extraAddr += (extraAddr !== '' ? ', ' + data.buildingName : data.buildingName);
              }
              // 조합형주소의 유무에 따라 양쪽에 괄호를 추가하여 최종 주소를 만든다.
              fullAddr += (extraAddr !== '' ? ' (' + extraAddr + ')' : '');
            }
  
            // 우편번호와 주소 정보를 해당 필드에 넣는다.
            $('#' + zipCode).val(data.zonecode);
            $('#' + address).val(fullAddr);
  

            if (popName == 'survey01-personal') {
              self.experienceData.zipCode = data.zonecode;
              self.experienceData.address = fullAddr
            } else if (popName == 'survey02-personal') {
   
              self.advanceApplicationSendData.zipCode = data.zonecode;
              self.advanceApplicationSendData.address = fullAddr;
            } else if (popName == 'review-personal') {
              self.reviewData.zipCode = data.zonecode;
              self.reviewData.address = fullAddr;
            }
  
            // 커서를 상세주소 필드로 이동한다.
            //$('#' + addressDetailId).focus();
  
          },
          //width: '640px',
          //height: '590px'
        }).open();
      }    
      return false;
    },
    advanceQuestionCheck: function () { //사전신청
      advanceRouteArr = [];
      var babyAge = this.advanceApplicationSendData.babyAge;
      var usedProduct = this.advanceApplicationSendData.usedProduct;

      var feedingType = this.advanceApplicationSendData.feedingType;
      var firstRoute = advanceRouteArr.length;
      var self = this;

      $('input[name=survey02-q05]:checked').each(function (firstRoute) {
        //console.log($(this).val())
        advanceRouteArr.push($(this).val());
      });

      //사전신청 설문 data check
      if (babyAge === '' || usedProduct === '' || feedingType === '' || advanceRouteArr.length === 0) {
        //console.log(advanceRouteArr.length)
        alert('설문을 모두 입력해 주세요');
        return false;
      } else if($('#survey02-q05-a05').is(':checked') && $('.cheack__only3').val() ===""){
        alert('설문을 모두 입력해 주세요');
      } else {
        this.advanceApplicationSendData.firstRoute = advanceRouteArr[0];
        this.advanceApplicationSendData.firstRoute2 = advanceRouteArr[1];
        closePopup('survey02', false);    
        openPopup('survey02-personal');
        self.QuestionList[3][4].questionAlt = ''
      }
    },
    experienceQuestionCheck: function () { //바로채험
      selectArr = [];
      experienceRouteArr = [];
      var self = this;

      $('input[name=survey01-q03]:checked').each(function (selectPoint) {
       // console.log('체크된 3번값'+ $(this).val())
        selectArr.push($(this).val());
      });

      $('input[name=survey01-q05]:checked').each(function (firstRoute) {
       // console.log('체크된 5번값'+ $(this).val())
        experienceRouteArr.push($(this).val());
      });

      

      var feedingType = this.experienceData.feedingType;
      var usedProduct = this.experienceData.usedProduct;
      var selectPoint = selectArr.length;
      var firstRoute = experienceRouteArr.length;

      //사전신청 설문 data check
      if (feedingType === '' || usedProduct === '' || selectPoint === 0 || firstRoute === 0) {
        alert('설문을 모두 입력해 주세요');
        return false;
      } else if($('#survey01-q03-a06').is(':checked') && $('.cheack__only').val() ===""  || $('#survey01-q05-a05').is(':checked') && $('.cheack__only2').val() ===""){
        alert('설문을 모두 입력해 주세요');
      } else {
        this.experienceData.selectPoint = selectArr[0];
        this.experienceData.selectPoint2 = selectArr[1];

        this.experienceData.firstRoute = experienceRouteArr[0];
        this.experienceData.firstRoute2 = experienceRouteArr[1];

        closePopup('survey01', false);
        openPopup('survey01-personal');
        self.QuestionList[3][4].questionAlt = '';
        self.QuestionList[1][5].inputString = '';
        
      }
    },
    advanceApplicationUserInfo: function (e) { //사전신청 data
      var self = this;

      $.ajax({
        url: '/api/kabrita-renewal/advance-application',
        type: 'POST',
        // dataType: 'json',
        data: this.advanceApplicationSendData,
        success: function (res) {
          closePopup('survey02-personal');

          $('#survey02Form').clearForm();
          $('#survey02FormPersonal').clearForm();
          self.$data.advanceApplicationSendData = surveyEventData().advanceApplicationSendData;
          //Object.assign(self.$data.advanceApplicationSendData, surveyEventData().advanceApplicationSendData);
          self.openShare('survey02');
        },
        error: function (res) {
          //console.log(res);
          alert(res.responseJSON.error);
        },
        beforeSend: function () {
          // console.log('before')
          self.loading = true;
        },
        complete: function () {
          self.loading = false;
        }
      })
    },
    experienceUserInfo: function (e) { // 바로체험신청data        	
      var self = this;

      $.ajax({
        url: '/api/kabrita-renewal/experience',
        type: 'POST',
        data: this.experienceData,

        success: function (res) {
          //console.log(this.experienceData);
          closePopup('survey01-personal');
          $('#survey01Form').clearForm();
          $('#survey01FormPersonal').clearForm();
  
          self.$data.experienceData = surveyEventData().experienceData;
          self.openShare('survey01');
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
    getQuestioinClass: function (idx) {
      return idx + 1
    },
    validURL: function () { //url값 체크
      var expUrl = /^http[s]?\:\/\//i;
      var textt = this.reviewData.reviewUrl; //입력값
      if (expUrl.test(textt)) {
        openPopup('review-personal');
      } else {
        alert('후기URL을 바르게 입력해주세요.\nEx) https://www.kabrita.kr');
      }
    },
    reviewUserInfo: function (e) { //후기이벤트
      var self = this;
      var $form = $(this);
      $.ajax({
        url: '/api/kabrita-renewal/review',
        type: 'POST',
        //dataType: 'application/json',
        data: this.reviewData,
        success: function (res) {
          closePopup('review-personal');
          alert('신청이 완료되었습니다.');
          $('#survey03Form').clearForm();
          $('#survey03FormPersonal').clearForm();
          // self.dataReset();
          self.$data.reviewData = surveyEventData().reviewData;
          // Object.assign(self.$data.reviewData, surveyEventData().reviewData);
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
    checkOnly: function (event) {
      tg = event.currentTarget;
      tgVal = $(tg).val();

      var checkedInputs = $('input[name="survey01-q03"]:checked');
      var $etcInput = $('.cheack__only')

      if (checkedInputs.length > 2) {
        alert('2개까지 체크 가능합니다.');
        tg.checked = !tg.checked;
        return false;
      }

      if ($('#survey01-q03-a06').is(':checked')) {
        $etcInput.removeAttr('readonly');
        $etcInput.focus();
      } else {
        $etcInput.attr('readonly', 'readonly');
        this.QuestionList[1][5].inputString = '';
      }
    },
    routeCheckOnly: function (event) {
      tg = event.currentTarget;
      tgVal = $(tg).val();
      var checkedInputs = $('input[name="survey01-q05"]:checked');
      var $etcInput = $('.cheack__only2')

      if (checkedInputs.length > 2) {
        alert('2개까지 체크 가능합니다.');
        tg.checked = !tg.checked;
        return false;
      }

      if ($('#survey01-q05-a05').is(':checked')) {
        $etcInput.removeAttr('readonly');
        $etcInput.focus();
      } else {
        $etcInput.attr('readonly', 'readonly');
        this.QuestionList[3][4].inputString = '';
      }
    },
    routeCheckOnly2: function (event) {
      tg = event.currentTarget;
      tgVal = $(tg).val();
      var checkedInputs = $('input[name="survey02-q05"]:checked');
      var $etcInput = $('.cheack__only3')

      if (checkedInputs.length > 2) {
        alert('2개까지 체크 가능합니다.');
        tg.checked = !tg.checked;
        return false;
      }

      if ($('#survey02-q05-a05').is(':checked')) {
        $etcInput.removeAttr('readonly');
        $etcInput.focus();
      } else {
        $etcInput.attr('readonly', 'readonly');
        this.QuestionList[3][4].inputString = '';
      }
    },
    popupControl: function (popBefore) { //자세히보기 버튼 click 이전팝업 저장
      var self = this;
      this.popBefore = popBefore;
      closePopup(popBefore);
    },
    kakaotalkShare: function (isSave) {
    	var self = this;
      var url = 'kabrita_try?utm_source=event01&utm_medium=kakaotalk&utm_campaign=kabrita-renewal';
      
      if (this.snsData.eventType == 'survey02') {
        url = 'kabrita_try?utm_source=event02&utm_medium=kakaotalk&utm_campaign=kabrita-renewal';
      } 
      
      if(isSave){
    	  // 이벤트 공유
    	  Kakao.Link.sendCustom({
    		  templateId: 13394,
          templateArgs: { 'url': url },
          success:function() {
            var shareData = {};
            shareData.sharingType = 'kakao';
            shareData.url = self.snsData.url;

            saveSnsShare(shareData, function () {
            })
            }
          });
      }else{
        // 단순공유
        Kakao.Link.sendCustom({
          templateId: 13395    		  
        });
      }
    },
    openShare: function (eventType) {
      this.snsData.eventType = eventType;

      if (eventType == 'survey02') {
        this.snsData.url = '/api/kabrita-renewal/advance-application/sharing';
      } else if (eventType == 'survey01') {
        this.snsData.url = '/api/kabrita-renewal/experience/sharing';
      }

      openPopup('share');
    },
    dataReset: function () {
      var def = getDefaultData();
      this.$data = def;
      //Object.assign(this.$data, def);
    }
  }
});

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

// clipboard
function setClipboard() {
  clipboard = new Clipboard('.codecopy');
  clipboard.on('success', function (e) {
      var snsData = {};
      snsData.sharingType = 'url';
      snsData.url = app.snsData.url;

      saveSnsShare(snsData, function () {
        alert('복사되었습니다');
      })
  });
  clipboard.on('error', function (e) {
    alert('복사실패')
  });
}

function surveyScrollTab() {

  var $paginationTabWrap = $('.survey-event__tab');
  var scrollTop = $(window).scrollTop();
  var fixedPos = $('.experience').offset().top;
  if(deviceKind === 'm'){
    if (scrollTop >= fixedPos - 80) {
      $paginationTabWrap.css({
        'position': 'fixed',
        'top':'80px'
      })
    } else {
      $paginationTabWrap.css({ 
        'position': 'absolute',
        'top':0
      })
    }
  }

}

function tabActive(){
  var scrollTop = $(window).scrollTop();
  var experienceTop = $('.experience').offset().top;
  var experienceH = $('.experience').height();
  var reviewTop = $('.review').offset().top;
  var $tab = $('.pagination-tab');
  var tabH = 145;

  if(scrollTop > experienceTop + tabH && scrollTop< experienceTop + experienceH - tabH){
    $tab.removeClass("active");
    $tab.eq(0).addClass("active");
  } else if(scrollTop >= reviewTop - tabH){
    $tab.removeClass("active");
    $tab.eq(1).addClass("active");
  }
}

function tabActivePc(){
  var scrollTop = $(window).scrollTop();

  var kvH = $('.kv').height();
  var experienceTop = $('.experience').offset().top;
  var experienceH = $('.experience').height();
  var reviewTop = $('.review').offset().top;
  var $tab = $('.pagination-tab');
  var tabMargin = 205;

  if(scrollTop<kvH - tabMargin){
    $tab.removeClass("active");
    $tab.eq(0).addClass("active");
  } 
  else if(scrollTop > experienceTop - tabMargin && scrollTop< experienceTop + experienceH - tabMargin){
    $tab.removeClass("active");
    $tab.eq(1).addClass("active");
  } 
  else if(scrollTop >= reviewTop - tabMargin){
    $tab.removeClass("active");
    $tab.eq(2).addClass("active");
  }
}



$(function () {
  positionPcFloating();
  tabActivePc();
  
  $('.mov-cover').click(function () {
    startMov();
  });

  $(window).scroll(function () {
    if(deviceKind === 'm'){
      surveyScrollTab();
      tabActive();
    } else {
      positionPcFloating();
      tabActivePc();
    }

  });

  $('.pagination-tab a').click(function(e) {
    e.preventDefault();
    
    if(deviceKind === 'm' && this.hash === '#review'){
      $('html, body').animate({scrollTop:$(this.hash).offset().top - 145});
    } else {
      $('html, body').animate({scrollTop:$(this.hash).offset().top});
    }
  });

});

function positionPcFloating(){
  var scrollTop = $(window).scrollTop() + 205;
  $('.survey-event__tab-wrap').stop().animate({top:scrollTop});
}
function startMov() {
  $('.mov-cover').hide();
  surveyYoutubePlayer.playVideo();
}
          
window.onload = function() {
  app.loading = false;
}