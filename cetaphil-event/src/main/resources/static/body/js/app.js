/*
도메인 주소 전달드립니다.
1.    메인페이지 주소 : http://cetaphil.pentacle.co.kr/body
2.    서리나의 동안바디 주소 : http://cetaphil.pentacle.co.kr/body#contents
3.    동안바디 계산기 주소 : http://cetaphil.pentacle.co.kr/body#calculator
4.    동안바디 측정기 주소 : http://cetaphil.pentacle.co.kr/body#age
5.    동안바디 스페셜 키트 주소 : http://cetaphil.pentacle.co.kr/body#gift
6.    동안바디 챌린지 주소 : http://cetaphil.pentacle.co.kr/body#challenge

이벤트마다 붙어있는 공유 이벤트의 랜딩은 각각 해당 이벤트 도메인으로 부탁드립니다.
*/
Vue.filter('commaNum', function(value) {
  return (value + '').replace(/(\d)(?=(\d{3})+$)/g, '$1,');
});

new Vue({
  el: '#app',
  data: {
    utils: null,
    urls: {
      insta: 'https://www.instagram.com/p/Bpqm9weAfc8/',
      facebook:
        'https://www.facebook.com/Cetaphil.Korea/videos/263245004376536/?__xts__[0]=68.ARDrBbxpfZiV7rloqARpTtR19RxM_mzV9ijq6utIp9llBvELzKk9kvInXLHzYiPuOz2a17y2AiTnr_c1zKjRTpo5BKdWWOUys-vils6hVDgZ_smSjfdkKPbYJH4QMF37T3uYhPQjM33gbQiaFBkN25_Xs3XA84hIehIVPNCC5siDNtqKMEETabwZN1p4Z2sE1OE9gmySKju1xeseOd3YP_pdzpDQJpnDMUNw6CA&__tn__=-R',
      productBuy: 'http://item.gmarket.co.kr/Item?goodscode=1512154871',
      productBuyM:
        'http://mitem.gmarket.co.kr/Item?goodsCode=1512154871&jaehuid='
    },
    urlProductBuy: '',

    app: { fitHeight: false, fit1080: false },
    pagesInfo: {}, // page 정보 obj
    pages: [], // page 정보 리스트
    page: {
      current: ''
    },

    nav: [],

    popsInfo: {},
    pops: ['', ''],
    pop: {
      current: ''
    },

    // 페이지별 정보들( 주로 전송용 )
    eventsInfo: {},
    eventsInfoInit: {},

    userInfo: {
      name: '',
      phone: '',
      birth: '',
      agree: false,
      link: ''
    },
    userInfoInit: {},

    snsType: [
      { key: 'kakaotalk', label: '카카오톡', class: 'btn__kakao' },
      { key: 'kakaostory', label: '카카오스토리', class: 'btn__kakaostory' },
      { key: 'facebook', label: '페이스북', class: 'btn__facebook' }
    ],
    onPageTransition: false,
    mobile: {
      onNav: false
    },
    topBtnShow: false,
    ageVideoEnd: false,
    uploadReady: true,
    stores: {
      list: [{ name: 'costco' }, { name: 'oliveyoung' }],
      currentIdx: 0
    },
    winnersInfo: null
  },
  created: function() {
    var self = this;
    this.utils = appUtils(this);
    var settedData = this.utils.settingData(appData);
    this.pagesInfo = settedData.pagesInfo;
    this.pages = settedData.pages;
    this.popsInfo = settedData.popsInfo;
    this.eventsInfo = settedData.eventsInfo;

    //--- nav
    this.nav = settedData.nav;

    //--- content spage
    this.contents.kindsInfo.video.current = this.contents.kindsInfo.video.list[0];

    //--- init setting
    this.page.current = 'main';
    this.eventsInfoInit = JSON.parse(JSON.stringify(this.eventsInfo));
    this.userInfoInit = JSON.parse(JSON.stringify(this.userInfo));

    //--- winners list
    this.winnersInfo = {};
    for (var key in appData.winnersInfo) {
      // this.winnersInfo.key = [];
      this.winnersInfo[key] = this.utils.getListFromArr(
        appData.winnersInfo[key]
      );
    }

    // load youtube API
    loadVideoAPI();
    // contents page video end event
    $(document).on('videoEnd', function() {
      self.contents.kindsInfo.video.onNav = true;
    });

    //--- load page, addEvent hash
    this.loadPage();
    window.onpopstate = function(event) {
      self.loadPage();
    };

    //--- mobile top button control
    if (deviceKind === 'm') {
      $(window).scroll(function() {
        self.topBtnControl();
      });
    }

    this.urlProductBuy =
      deviceKind === 'pc' ? this.urls.productBuy : this.urls.productBuyM;
  },
  computed: {
    contents: function() {
      return this.pagesInfo.contents.contentsInfo;
    },
    calculator: function() {
      return this.pagesInfo.calculator.contentsInfo;
    },
    age: function() {
      return this.pagesInfo.age.contentsInfo;
    },
    ageE: function() {
      return this.eventsInfo.age;
    }
  },

  methods: {
    goBuy: function() {
      window.open(this.urlProductBuy, '_blank');
    },

    notice: function(st) {
      if (st) {
        alert('짐볼을 포함한 스페셜 키트 구매 페이지는 추후 오픈 예정입니다.');
        return;
      }
      alert('해당 이벤트는 10월 19일 오픈 됩니다');
    },
    loadPage: function() {
      var tgPath = location.pathname.replace('/body/', '');
      if (tgPath === '/body') {
        tgPath = 'main';
      }
      $(window).scrollTop(0);
      this.setCurrentPage(tgPath);
    },
    showPage: function(pageName) {
      // console.log('fn:showPage> ', pageName);

      if (this.page.current === pageName) {
        if (deviceKind === 'm') {
          // this.mobile.onNav = false;
          this.closeNav();
        }
        return;
      }

      if (pageName === 'main') {
        window.history.pushState('', '', '/body');
      } else {
        window.history.pushState('', '', '/body/' + pageName);
      }

      if (deviceKind === 'pc') {
        this.startTransition(pageName);
      } else {
        // this.mobile.onNav = false;
        this.closeNav();
        this.setCurrentPage(pageName);
      }
    },
    // page 관련 셋팅
    setCurrentPage: function(pageName) {
      this.onPageTransition = false;
      this.page.current = pageName;
      this.initInfos(); // 정보 초기화

      // if (this.page.current === 'contents') {
      //   this.showPop('contents-winners');
      // }
      if (this.page.current != 'gift' || this.page.current != 'main') {
        this.showPop(this.page.current + '-winners');
      }

      if (this.page.current === 'gift') {
        this.app.fit1080 = true;
      } else {
        this.app.fit1080 = false;
      }

      // console.log('setCurrentPage> ', pageName);
      //=== nav view
      this.nav.map(function(ele) {
        if (pageName === 'main' || ele.key != pageName) {
          ele.hover = false;
        } else {
          ele.hover = true;
        }
      });
    },
    setNavOn: function() {
      this.nav.map(function(ele) {
        ele.hover = true;
        // if (pageName === 'main' || ele.key != pageName) {
        //   ele.hover = false;
        // } else {
        //   ele.hover = true;
        // }
      });
    },
    // page 전환
    startTransition: function(nextPageName) {
      if (this.page.current === nextPageName) return;
      if (this.onPageTransition) return;

      this.onPageTransition = true;
      // 필요한 요소
      // offset,
      // bgColor
      var self = this;
      var tgScaleNum = Math.ceil($(window).width() / 11);
      var duTime = 1 * (tgScaleNum / 180).toFixed(1);
      var $transition = $('.transition');
      var $transitionWrap = $('.transition-wrap');

      var tgData = this.getTransitionData(nextPageName);

      $transitionWrap.css({ display: 'block' });
      TweenMax.to('.transition', 0, {
        scaleX: 1,
        scaleY: 1,
        opacity: 1,
        top: tgData.offset.top,
        left: tgData.offset.left,
        background: tgData.bg
      });
      // scaleup
      TweenMax.to('.transition', duTime, {
        scaleX: tgScaleNum,
        scaleY: tgScaleNum,
        ease: Cubic.easeOut
      });
      // fadeout
      TweenMax.to('.transition', duTime, {
        delay: duTime - 0.5,
        opacity: 0,
        onComplete: function() {
          $transitionWrap.css({ display: 'none' });
        }
      });
      setTimeout(function() {
        self.setCurrentPage(nextPageName);
      }, (duTime - 0.5) * 1000);
    },
    getTransitionData: function(nextPageName) {
      var offset =
        nextPageName === 'main'
          ? $('.logo').offset()
          : $('.nav-item-' + nextPageName + ' .nav-item-icon').offset();
      var bg = '#fff';
      if (nextPageName != 'main') {
        var obj = this.pages.find(function(obj) {
          return obj.key === nextPageName;
        });
        bg = obj.bgColor;
      }
      return {
        offset: offset,
        bg: bg
      };
    },

    //=== pop
    showPop: function(popName) {
      $(window).scrollTop(0);

      if (deviceKind === 'm') {
        this.utils.setHeightM(true, popName);
      } else {
        this.utils.setHeight(true, popName);
      }

      var self = this;

      switch (popName) {
        case 'user-info':
          if (!this.onPop('info-clause')) this.initInfos();

          if (this.page.current === 'contents') {
            this.setPop(popName, 'overlay');
          } else {
            this.setPop(popName);
          }
          break;

        case 'store':
          this.setPop(popName, 'overlay');
          break;

        case 'info-clause':
          if (this.page.current === 'contents') {
            this.setPop(popName, 'overlay');
          } else {
            this.setPop(popName);
          }
          break;

        case 'contents-gallery':
          sliderSetting();
          this.setPop(popName);
          break;

        case 'age-checklist-intro':
          this.startAgeVideo();
          this.setPop(popName);
          break;

        default:
          this.setPop(popName);
          break;
      }
    },
    closePop: function(popName) {
      // console.log('closePop> ', popName);
      if (deviceKind === 'm') {
        this.utils.setHeightM(false, popName);
      } else {
        this.utils.setHeight(false, popName);
      }

      if (popName === 'age-checklist-intro') {
        this.endAgeVideo();
        this.pops = ['', ''];
        return;
      }

      if (!popName) {
        this.pops = ['', ''];
      } else {
        switch (popName) {
          case 'contents-video':
            cotentsMovCtrl.stopVideo();
            this.showPop('contents-video-select');
            break;

          case 'info-clause':
            this.showPop('user-info');
            break;

          case 'user-info':
            if (this.page.current === 'contents') {
              this.showPop('contents-event');
            } else {
              this.closePop();
            }
            //
            // this.showPop('user-info');
            break;

          default:
            // 열린팝업이 여러개 열린 경우
            this.pops = this.pops.map(function(ele) {
              return ele === popName ? '' : ele;
            });
        }
      }
    },
    setPop: function(popName, stOverlay) {
      if (stOverlay === 'overlay') {
        this.pops = this.pops.map(function(ele, idx) {
          if (idx === 1) {
            return (ele = popName);
          } else {
            return ele;
          }
        });
      } else {
        this.pops = ['', ''];
        this.pops[0] = popName;
      }
    },
    showPopM: function(popName) {
      if (this.onPageTransition) return;
      this.onPageTransition = true;
      var self = this;
      if (popName.indexOf('gallery') != -1) {
        this.contents.selected = 'gallery';
      } else {
        this.contents.selected = 'video';
      }
      setTimeout(function() {
        self.showPop(popName);
        self.onPageTransition = false;
      }, 700);
    },

    // 팝업이 켜져 있는지 찾아낸다.
    onPop: function(popName) {
      var exist = this.pops.find(function(ele) {
        return ele === popName;
      });
      return exist;
    },

    //=== mov
    showVideoContents: function(videoIdx) {
      var self = this;
      var videoData = this.contents.kindsInfo.video;
      videoData.current = videoData.list[videoIdx];

      var tgObj = videoData.current;
      videoData.onNav = false;
      cotentsMovCtrl.playById(tgObj.videoId);

      this.showPop('contents-video');
    },
    //=== 사용자정보 보내기
    sendUserInfo: function() {
      var self = this;
      var tgUrl = '';
      // 계산기, 측정기에서는 사용자 정보 validate 만 한다.
      if (this.page.current === 'calculator' || this.page.current === 'age') {
        tgUrl = '/api/body/valid-user';
      } else {
        tgUrl = '/api/body/' + this.page.current + '-save';
      }
      this.utils.sendData(
        this.userInfo,
        tgUrl,
        function(res) {
          // console.log(self.page.current, res);
          switch (self.page.current) {
            case 'contents':
              self.showPop('contents-share-sns');
              break;
            case 'calculator':
              self.showPop('calculator');
              break;
            case 'age':
              self.showPop('age-checklist');
              break;
            case 'challenge':
              self.showPop('share-sns');
              break;
            default:
              // console.log(self.page.current, res);
              break;
          }
        },
        function(res) {
          alert(res.responseJSON.error);
        }
      );
    },

    //=== sns 공유시 호출( 키트 제외한 모든 페이지에서 사용함 )
    shareSns: function(snsType) {
      // console.log('fn::shareSns>> ', snsType, this.page.current);
      switch (snsType) {
        case 'kakaotalk':
          kakaotalk(this.page.current);
          break;
        case 'kakaostory':
          kakaostory(this.page.current);
          break;
        case 'facebook':
          facebook(this.page.current);
          break;
      }

      var self = this;
      var data = { eventType: this.page.current, snsType: snsType };
      this.utils.sendData(
        data,
        '/api/body/sharing-save',
        function(res) {
          self.eventsInfo[self.page.current].joined = true;
          // console.log(self.eventsInfo[self.page.current].joined);
        },
        function(error) {
          //alert(error.responseJSON.error);
        }
      );
    },

    initInfos: function() {
      this.userInfo = JSON.parse(JSON.stringify(this.userInfoInit));
      this.eventsInfo = JSON.parse(JSON.stringify(this.eventsInfoInit));

      var self = this;
      this.uploadReady = false;
      this.$nextTick(function() {
        self.uploadReady = true;
      });
    },

    // 계산기
    sendCalcInfo: function() {
      var calcData = this.eventsInfo.calculator;
      var scoreInfo = this.eventsInfo.calculator.scoreInfo;

      if (!this.checkCalcVal(scoreInfo)) {
        alert('제품 사용 수가 1개 이상이어야 결과 값이 계산됩니다.');
        return;
      }
      this.calcCalcInfo();

      // 정보를 모아서 보낸다.
      var data = $.extend(
        true,
        {},
        scoreInfo,
        {
          bodyPayment: calcData.bodyTotal,
          facialPayment: calcData.facialTotal
        },
        this.userInfo
      );

      // console.log('calcData> ', data);
      // this.showPop('calculator-result');

      var self = this;
      this.utils.sendData(
        data,
        '/api/body/calculator-save',
        function(res) {
          self.showPop('calculator-result');
          // self.initInfos();
        },
        function(error) {
          // console.log('err> ', error);
        }
      );
    },

    calcCalcInfo: function() {
      var calcData = this.eventsInfo.calculator;
      var scoreInfo = this.eventsInfo.calculator.scoreInfo;
      var priceInfo = this.eventsInfo.calculator.priceInfo;

      //=== calc
      var bodyTotal = 0;
      var facialTotal = 0;
      var priceTotal = 0;

      // console.log('> calc price >');
      for (var key in scoreInfo) {
        if (scoreInfo[key] === 0 || scoreInfo[key] === '0') scoreInfo[key] = 0;

        scoreInfo[key] = scoreInfo[key] * 1; // 숫자화
        priceTotal = scoreInfo[key] * priceInfo[key];

        // 검증을 위해
        // console.log(
        //   key,
        //   '> ',
        //   scoreInfo[key],
        //   '*',
        //   priceInfo[key],
        //   ' = ',
        //   scoreInfo[key] * priceInfo[key]
        // );

        if (key.indexOf('facial') != -1) {
          // facial 이면
          facialTotal += priceTotal;
        } else {
          bodyTotal += priceTotal;
        }
      }
      calcData.highPrice = '';
      calcData.highPrice = facialTotal - bodyTotal > 0 ? 'facial' : 'body';
      calcData.multiple = Math.floor(facialTotal / bodyTotal); // 얼굴에 쓴돈이 더 많을때

      // console.log('face>> 몇배?  ', facialTotal, bodyTotal, calcData.multiple);

      // update data: 왜인지 모르지만 유독 이속성만 갱신이 자동으로 않되네...
      calcData.bodyTotal = bodyTotal;
      calcData.facialTotal = facialTotal;
      // console.log(this.eventsInfo.calculator.bodyTotal);
      // Vue.set(calcData, 'bodyTotal', bodyTotal);
      // Vue.set(calcData, 'facialTotal', facialTotal);

      // console.log(
      //   'body>',
      //   calcData.bodyTotal,
      //   'facial> ',
      //   calcData.facialTotal,
      //   'hiprice> ',
      //   calcData.highPrice
      // );
      // console.log(calcData.bodyTotal);
    },

    checkCalcVal: function(scoreInfo) {
      // 0 은 사용자 입력없이도 자동입력되도록 처리
      var info = JSON.parse(JSON.stringify(scoreInfo));
      var list = [];

      for (var key in info) {
        if (info[key] === '0' || info[key] === '') {
          // info[key] = 0;
        } else {
          info[key] = info[key] * 1;
          if (key.indexOf('facial') != -1) list[0] = info[key];
          if (key.indexOf('body') != -1) list[1] = info[key];
        }
      }

      var selectedList = list.filter(function(ele, idx) {
        if (ele) return ele;
      });

      var exist = selectedList.length >= 2 ? true : false;
      return exist;
    },

    topMove: function() {
      $('html, body').animate({ scrollTop: 0 }, 200);
      return false;
    },

    topBtnControl: function() {
      var scrollTop = $(document).scrollTop();

      if (scrollTop > 300) {
        this.topBtnShow = true;
      } else {
        this.topBtnShow = false;
      }
    },
    //=== age
    startAgeVideo: function() {
      var self = this;
      this.ageVideoEnd = false;
      var ageVideo = document.getElementById('ageVideo');
      // console.log(this.ageVideo);
      ageVideo.addEventListener('ended', function() {
        // console.log('end>>> ');
        self.ageVideoEnd = true;
      });
      ageVideo.currentTime = 0;
      ageVideo.play();
    },
    endAgeVideo: function() {
      var ageVideo = document.getElementById('ageVideo');
      ageVideo.pause();
      ageVideo.removeEventListener('ended', function() {});
    },
    showHandView: function() {
      var self = this;
      this.ageE.onLoading = true;
      setTimeout(function() {
        self.ageE.onLoading = false;
        self.showPop('age-result');
      }, 3000);
    },
    loadImg: function(e) {
      var self = this;
      this.showHandView();

      var files = e.target.files || e.dataTransfer.files;
      var file = files[0];

      loadImage.parseMetaData(file, function(data) {
        var orientation = 0;
        if (data.exif) {
          orientation = data.exif.get('Orientation');
        }
        var loadingImage = loadImage(
          file,
          function(canvas) {
            //here's the base64 data result
            var base64data = canvas.toDataURL('image/jpeg');
            //here's example to show it as on imae preview
            var img_src = base64data.replace(/^data\:image\/\w+\;base64\,/, '');
            // $('#result-preview').attr('src', base64data);
            self.ageE.userImg = base64data;
            // alert('hey canvas~~~~');
          },
          {
            //should be set to canvas : true to activate auto fix orientation
            canvas: true,
            orientation: orientation,
            maxWidth: 560,
            maxHeight: 560
          }
        );
      });
    },
    sendAgeInfo: function() {
      this.calcAge();
      var data = $.extend(true, {}, this.userInfo, this.ageE.checkedInfo, {
        bodyAge: this.ageE.bodyAge
      });

      var self = this;
      this.utils.sendData(
        data,
        '/api/body/age-save',
        function(res) {
          self.showPop('age-skin-test');
        },
        function(res) {
          alert(res.responseJSON.error);
        }
      );
    },
    calcAge: function() {
      this.ageE.userAge = this.utils.getAge(this.userInfo.birth);
      var info = this.utils.getAgeUserChecked(
        this.ageE.checklist,
        this.ageE.userAge,
        this.ageE.scoreAgesInfo
      );
      this.ageE.bodyAge = info.total;
      this.ageE.bodyAgeLevel = info.level;
      this.ageE.checkedInfo = Object.assign({}, info.checked);
      // console.log('calcAge> ', this.age.bodyAge);
    },

    //=== mobile
    openNav: function() {
      this.mobile.onNav = true;
      this.utils.setHeightM(true, 'nav');
    },
    closeNav: function() {
      this.mobile.onNav = false;
      this.utils.setHeightM(false);
    }
  }
});
