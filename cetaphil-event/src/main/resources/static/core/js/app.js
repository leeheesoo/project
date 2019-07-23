/* 개인정보 유효성 검사 */
var validationName = function(name) {
  var regx = /[^a-zA-Z가-힣]/;
  return !regx.test(name);
};

var validationPhone = function(phone) {
  //var regx = /^\d{10,11}$/;
  var regx = /^(010|011|016|017|019)[0-9]{7,8}$/;
  return regx.test(phone);
};

var validationInsta = function(id) {
  //var regx = /^[a-z]+[a-z0-9]{5,20}$/g;
  var regx = /^[a-z0-9\._-]{4,20}/;
  return regx.test(id);
};

var validationId = function(id) {
  //var regx = /^[a-z]+[a-z0-9]{5,20}$/g;
  //var regx = /^[a-zA-Z0-9\._-]{1,20}/;
  var regx = /^[ㄱ-ㅎ|ㅏ-ㅣ|가-힣]/g;
  return regx.test(id);
};

var app = new Vue({
  el: '#app--cetaphil-core',
  data: {
    imgUrl: 'https://cetaphil-event.s3.amazonaws.com/static/core',
    eventInfo: {
      naverSearch: { desc: '즉당-네이버검색이벤트', needUserInfo: true, win: false },
      fandomApply: { desc: '팬덤지원이벤트', needUserInfo: true },
      shareUrl: { desc: '이벤트 url 공유', needUserInfo: true },
      gotoSite: { desc: '단순 사이트 랜딩', url: '' },
      instaList: { desc: '인스타 리스트 슬라이더' }
    },

    currentEvent: '',
    currentPop: '', // 현재 팝업 저장
    prevPop: '',
    snsType: '',
    loading: '',
    currentScrT: '', // 팝업 띄울 때 스크롤 위치 저장
    currentTab: 'share', // 당첨자 팝업 선택
    prizeName: '',
    imgSrc: 'https://via.placeholder.com/170x170',

    eventUserData: {
      isUse: '', // Y or N
      isChildren: '', // Y or N
      childrenAge: false,
      childrenAge2: false,
      childrenAge3: false,
      childrenAge4: false,
      comment: '',
      name: '',
      mobile: '',
      zipcode: '',
      address: '',
      addressDetail: '',
      snsAddress: '',
      snsAddress2: '',
      snsAddress3: '',
      snsAddress4: '',
      agree: false
    },   
    winnerShare: [],
    // fandomFirst: [], 
    bestFandomList: [],
    fandomWinnersList: [],
    userInfoDefault: {} // 개인정보 폼 초기값 복사
  },
  watch: {
    'eventUserData.isChildren' : function(val){
      if(val === 'N'){
        this.eventUserData.childrenAge = false;
        this.eventUserData.childrenAge2 = false;
        this.eventUserData.childrenAge3 = false;
        this.eventUserData.childrenAge4 = false;
      }
    }
  },
  created: function () {
    this.userInfoDefault = JSON.parse(JSON.stringify(this.eventUserData));

    if(lottery){
      this.lotteryEventResult();
    }

    this.winnerShare = winnerShare;
    /* this.fandomFirst = fandomFirst;
    this.fandomSecond = fandomSecond; */
    this.bestFandomList = bestFandomList;
    this.fandomWinnersList = fandomWinnersList;

  },
  mounted: function () {
    // this.initSlick();
    this.addHashUI();    
  },

  computed: {
    prizeType: function(){
      return this.type[this.prizeName];
    }
  },
  methods: {
    addHashUI: function(){
      var tag = document.createElement('script');
    tag.src = "https://s3.ap-northeast-2.amazonaws.com/hashsnap-static-files/cetaphil/js/embeder.js";
    var firstScriptTag = document.getElementsByTagName('script')[0];
    firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

    },
    /*  */
    openPopup: function(flag) {
      var $selector = $('#pop-' + flag);
      $(".dimmed").show();
      $('html, body').animate({ scrollTop: this.currentScrT }, 300);
      $selector.css('top', this.currentScrT + 'px').show();
    },
  
    /* 팝업창을 완전히 닫을 때 */
    hidePopup: function(flag){
      $(".dimmed").hide();
      $('#pop-' + flag).hide();
      this.currentScrT = '';
      this.currentPop = '';
    },

    /* 팝업 제어 */
    openPop: function(Name){
      if(Name === 'prev'){
        this.closePop(this.currentPop);
        this.currentPop = this.prevPop;
        this.prevPop = '';
      } else {        
        this.prevPop = this.currentPop || '';
        this.currentPop = Name;
      }
      this.openPopup(this.currentPop);
    },

    /* 팝업창을 변경할 때 */
    closePop: function(Name){
      $(".dimmed").hide();
      $('#pop-' + Name).hide();
    },

    /* 슬라이더 초기화 */
    initSlick: function(){
      $('.img-slider').slick({
        draggable: false,
        dots: true
      });
    },
    daumPostComplete: function(zonecode, addr) {
      this.eventUserData.zipcode = zonecode;
      this.eventUserData.address = addr;
    },

    /* 자녀 연령층 체크 */
    checkChildrenAge: function(){
      return this.eventUserData.childrenAge || this.eventUserData.childrenAge2 || this.eventUserData.childrenAge3 || this.eventUserData.childrenAge4
    },

    /* sns 주소 검사 */
    checkSns : function(){
      return this.eventUserData.snsAddress || this.eventUserData.snsAddress2 || this.eventUserData.snsAddress3 || this.eventUserData.snsAddress4
    },
    
    /* 개인정보 유효성 검사 */
    validateUserInfo: function(){
      // var isPassInsta = validationInsta(this.eventUserData.snsAddress);
      
      var hasSnsId = this.checkSns();
      var hasValues = this.eventUserData.comment;
      
      if(this.eventUserData.isChildren === 'Y'){
        if(!this.checkChildrenAge()){
          alert('자녀의 나이를 체크해주세요.');
          return;
        }
      }

      if(this.eventUserData.isUse === '' || this.eventUserData.isChildren === ''){
        alert('입력폼을 모두 작성해주세요.');
        return;
      }

      if(!hasValues){
        alert('한줄다짐을 작성해주세요');        
        return;
      }
      if(hasValues.length >20) {
    	  alert('한줄 다짐을 20자 이하로 작성해주세요.');
    	  return;
      }
      if (!hasSnsId) {
        alert('한 개 이상 SNS 주소를 입력해주세요.')
        return;
      } else {
        if(this.eventUserData.snsAddress){
          if(validationId(this.eventUserData.snsAddress)){
            alert('SNS 주소는 한글을 제외하고 입력해주세요.');   
            return;
          }
        }
        if(this.eventUserData.snsAddress2){
          if(validationId(this.eventUserData.snsAddress2)){
            alert('SNS 주소는 한글을 제외하고 입력해주세요.')            
            return;
          }
        }
        if(this.eventUserData.snsAddress3){
          if(validationId(this.eventUserData.snsAddress3)){
            alert('SNS 주소는 한글을 제외하고 입력해주세요.')            
            return;
          }
        }
        if(this.eventUserData.snsAddress4){
          if(validationId(this.eventUserData.snsAddress4)){
            alert('SNS 주소는 한글을 제외하고 입력해주세요.')            
            return;
          }
        }
      }

      this.closePop('fandom-support'); 
      this.openPop('fandom-personal');
    },
    
    /* 개인정보 eventUserData 초기화 */
    initUserInfo: function () {
      this.eventUserData = JSON.parse(JSON.stringify(this.userInfoDefault));
    },

    /* 즉당 제품받기 클릭시 */
    getPrize: function(){
      this.closePop('search-win'); 
      this.initUserInfo();
      this.openPop('search-personal');
    },

    /* 다른 이벤트 보기버튼 */
    goToAnotherEvent: function(){
      this.closePop('search-fail');
      $('[href="#event02"]').trigger('click');
    },

    /* 공유버튼 클릭시  */
    shareUrl: function () {
      this.currentScrT = $(window).scrollTop();
      this.initUserInfo();
      this.openPop('share-personal');
    },

    /* 팬덤지원서 버튼 클릭시 */
    applyFandom: function () {
      this.currentScrT = $(window).scrollTop();
      this.initUserInfo();
      this.openPop('fandom-support');
    },

    /* 팬덤지원 자세히보기 버튼 클릭시 */
    checkFandomPick: function () {
      this.currentScrT = $(window).scrollTop();
      this.openPop('fandom-pick');
    },

    /* 네이버 검색하러가기 버튼 */
    onNaverlanding: function(){
      alert('해당 이벤트는 1월 9일 부터 참여 가능합니다. 다른 이벤트에 참여해주세요!');
    },

    /* 당첨자 팝업 버튼 */
    onWinnerPop: function(){
      this.currentScrT = $(window).scrollTop();
      this.openPop('winner');
    },

    // 당첨확인
    checkWinner: function () {
      // 당첨여부 팝업 -> 당첨시 확인 -> 개인정보입력 

      this.currentScrT = $(window).scrollTop();
      // this.currentEvent = 'naverSearch';
      openPopup('search-personal');
    },

    // 인스타 이미지 리스트 불러오기
    getInstaList: function () {

    },

    /* 팬덤지원서 저장 */
    sendFandomUserInfo: function () {
      var self = this;
      // console.log(self.eventUserData);
      $.ajax({
        url: '/api/core/fandom',
        type: 'POST',
        data: self.eventUserData,

        success: function (res) {
          // console.log(res);
          if (res.success) {
            alert("세타필 팬덤 지원이 완료되었습니다.");
            self.closePop('fandom-personal');
          } else {
            alert(res.message);
          }
        },
        error: function (res) {
          // console.log(res);
          alert(res.responseJSON.error);
        },
        beforeSend: function () {
          self.loading = true;
        },
        complete: function () {
          self.loading = false;
        }

      })
    },

    /* 공유이벤트 개인정보 저장 */
    sendSnsShareUserInfo: function () {
      var self = this;
      var snsShareInfo = {
        name: this.eventUserData.name,
        mobile: this.eventUserData.mobile,
        agree : this.eventUserData.agree
      };

      $.ajax({
        url: '/api/core/sns',
        type: 'POST',
        data: snsShareInfo,

        success: function (res) {
          if (res.success) {
            self.closePop('share-personal');
            self.openPop('share');
          } else {
            alert(res.message);
          }
        },
        error: function (res) {
          // console.log(res)
          alert(res.responseJSON.error);
        },
        beforeSend: function () {
          // console.log(snsShareInfo);
          self.loading = true;
        },
        complete: function () {
          self.loading = false;
        }

      })
    },

    /* 공유 데이터 저장 */
    snsShare: function (snsType) {
      var self = this;
      var snsData = { snsType: snsType };

      if (snsType == 'facebook') {
        var sharingURL = location.origin + location.pathname + '?utm_campaign=core&utm_medium=sharing&utm_source=facebook';
        window.open('https://www.facebook.com/dialog/share?app_id=441801156302191&href=' + encodeURIComponent(sharingURL), "fbPop", "menubar=false, scrollbars=no,width=600px,height=450px");
      } else if (snsType == 'kakaostory') {
        var sharingURL = location.origin + location.pathname + '?utm_campaign=core&utm_medium=sharing&utm_source=kakaostory';
        window.open('https://story.kakao.com/share?url=' + encodeURIComponent(sharingURL), "ksPop", "menubar=false, scrollbars=no,width=600px,height=450px");
      } else if (snsType == 'kakaotalk') {
        Kakao.Link.sendCustom({
          templateId: 13948
        });
      } else {
        alert('어떤 SNS로 공유하실 건지 선택해주세요');
        return;
      }

      $.ajax({
        url: '/api/core/sns/sharing',
        type: 'POST',
        data: snsData,

        success: function (res) {
          if (res.success) {
            // alert('공유 완료');
          } else {
            alert(res.message);
          }
        },
        error: function (res) {
          // console.log(res)
          alert(res.responseJSON.error);
        },
        beforeSend: function () {
          // console.log(snsType);
          self.loading = true;
        },
        complete: function () {
          self.loading = false;
        }

      })

    },

    /* 네이버 검색 즉당 */
    lotteryEventResult: function (e) { //즉당당첨여부
      var self = this;
      this.currentScrT = $(window).scrollTop();

      $.ajax({
        url: '/api/core/instant-lottery',
        type: 'POST',
        success: function (res) {
          if (res.success) {
            //팝업노출
            if (res.winner) {
              //당첨     
              self.prizeName = res.prizeImageName;
              self.openPop('search-win');
            } else {
              //미당첨
              self.prizeName = res.prizeImageName;
              self.openPop('search-fail');
            }
          } else {
            alert(res.message);
          }
          
        },

        error: function (res) {
          // console.log(res)
          alert(res.responseJSON.error);
        },

        beforeSend: function () {
          // self.loading = true;
        },
        complete: function () {
          // self.loading = false;
        }
      })
    },

    /* 즉당 당첨자 개인정보 */
    lotteryEventUserInfo: function (e) { // 즉당data        	
      var self = this;
      var lotteryData = {
        name: self.eventUserData.name,
        phone: self.eventUserData.mobile,
        zipcode: self.eventUserData.zipcode,
        address: self.eventUserData.address,
        addressDetail: self.eventUserData.addressDetail,
        agree: self.eventUserData.agree,
        prizeImageName: self.prizeName,
        // prizeType: self.prizeType
      }
      // console.log(lotteryData);
      $.ajax({
        url: '/api/core/instant-lottery/update-winner',
        type: 'POST',
        data: lotteryData,

        success: function (res) {
          alert(res.message);
          self.closePop('search-personal');
        },
        error: function (res) {
          alert(res.responseJSON.error);
        },
        beforeSend: function () {
          // self.loading = true;
        },
        complete: function () {
          // self.loading = false;
        }
      })
    }

  }
})