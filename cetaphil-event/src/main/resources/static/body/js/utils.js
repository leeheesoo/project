//+++++++++++++++++++++++++++++++++++++++++++++++++++++
// for share sns
//+++++++++++++++++++++++++++++++++++++++++++++++++++++
function kakaotalk(event) {
  switch (event) {
    case 'contents':
      Kakao.Link.sendCustom({
        templateId: 12478
      });
      break;
    case 'calculator':
      Kakao.Link.sendCustom({
        templateId: 12196
      });
      break;
    case 'age':
      Kakao.Link.sendCustom({
        templateId: 12204
      });
      break;
    case 'challenge':
      Kakao.Link.sendCustom({
        templateId: 12205
      });
      break;
    default:
      break;
  }
}

function kakaostory(event) {
  var sharingURL =
    location.origin +
    location.pathname +
    '?utm_campaign=body&utm_medium=' +
    event +
    'Sharing&utm_source=kakaostory';
  window.open(
    'https://story.kakao.com/share?url=' + encodeURIComponent(sharingURL),
    'ksPop',
    'menubar=false, scrollbars=no,width=550px,height=815px'
  );
}

function facebook(event) {
  var sharingURL =
    location.origin +
    location.pathname +
    '?utm_campaign=body&utm_medium=' +
    event +
    'Sharing&utm_source=facebook';
  window.open(
    'https://www.facebook.com/dialog/share?app_id=441801156302191&href=' +
      encodeURIComponent(sharingURL),
    'fbPop',
    'menubar=false, scrollbars=no,width=600px,height=450px'
  );
}

// app 에서 전반적으로 사용하는 util
function appUtils(tgApp) {
  var thisApp = tgApp;

  function settingData(data) {
    var tgData = JSON.parse(JSON.stringify(data));

    // nav
    tgData.nav = initNavList(tgData.pagesInfo);
    // pages info obj to array
    tgData.pages = Object.values(tgData.pagesInfo);
    // contents page: contents info
    var contents = tgData.pagesInfo.contents.contentsInfo;
    contents.kinds = Object.values(contents.kindsInfo);

    // 이벤트 참여관련 저장되는 정보 객체
    tgData.eventsInfo = initEventsInfo(tgData.pagesInfo);

    // calculator event 정보
    initCalcData(tgData);

    // init age events Info
    tgData.eventsInfo.age = JSON.parse(
      JSON.stringify(tgData.pagesInfo.age.contentsInfo)
    );
    tgData.eventsInfo.age.joined = false;
    return tgData;
  }

  function initNavList(obj) {
    var list = [];
    for (var key in obj) {
      if (key != 'main') {
        list.push({
          key: key,
          hover: false,
          emphasis: key === 'gift' || key === 'challenge' ? true : false,
          label: obj[key].label
        });
      }
    }
    return list;
  }
  function initContentsInfo(obj) {
    obj.kinds = Object.values(obj.kindsInfo);
    return obj;
  }
  function initEventsInfo(obj) {
    var result = {};
    for (var key in obj) {
      result[key] = { joined: false };
    }
    return result;
  }

  // calculator page: 초기 변수들 셋팅( 반복되는 것이 많아 함수로 처리함 )
  function initCalcData(data) {
    var contentsInfo = data.pagesInfo.calculator.contentsInfo;

    var eventsInfo = data.eventsInfo.calculator;
    eventsInfo.bodyTotal = 0;
    eventsInfo.facialTotal = 0;
    eventsInfo.highPrice = '';
    eventsInfo.multiple = 0;

    // kinds array(facial, body)
    contentsInfo.kinds = Object.values(contentsInfo.kindsInfo);

    // UI 정보들
    contentsInfo.kinds[0].info = getCalcInfos('facial', contentsInfo.baseList);
    contentsInfo.kinds[1].info = getCalcInfos('body', contentsInfo.baseList);

    // info들을 합친 리스트
    var tgInfos = _.concat(
      contentsInfo.kinds[0].info,
      contentsInfo.kinds[1].info
    );

    eventsInfo.priceInfo = {};
    eventsInfo.scoreInfo = {};

    tgInfos.map(function(ele) {
      eventsInfo.priceInfo[ele.key] = ele.price;
      eventsInfo.scoreInfo[ele.key] = '';
    });

    // 서버에 보낼 facial, body 변수들을 만들어 놓는다.
    // // // console.log('>', eventsInfo.priceInfo); // 가격정보
    // // // console.log('>', eventsInfo.scoreInfo); // 점수정보

    // // // console.log(eventsInfo.scoreInfo);
    // bodyAssence: 0
    // bodyEtc: 0
    // bodyNutritionWhitening: 0
    // bodySkinLotion: 0
    // facialAssence: 0
    // facialEtc: 0
    // facialNutritionWhitening: 0
    // facialSkinLotion: 0
  }

  // 계산기 데이타 셋팅
  function getCalcInfos(prefix, baseList) {
    var list = [];
    list = baseList.map(function(ele) {
      var obj = {};
      for (var key in ele) {
        switch (key) {
          case 'key':
            obj[key] = prefix + ele[key];
            break;

          case 'for':
            obj[key] = prefix + '-' + ele[key];
            break;

          case 'price':
            obj[key] = ele[key][prefix];
            break;

          default:
            obj[key] = ele[key];
            break;
        }
      }
      return obj;
    });
    return list;
  }

  //--- app의 height 값 셋팅: mobile
  function setHeightM(bool, popName, pop) {
    // // // console.log('setH> ', bool, popName);

    if (bool) {
      var tgH = '';
      if (thisApp.popsInfo[popName].mode === 'full') {
        tgH = '100vh';
      } else if (thisApp.popsInfo[popName].mode === '100%') {
        tgH = '100%';
      } else {
        tgH = thisApp.popsInfo[popName].h + 'px';
      }

      // var tgH =
      //   thisApp.popsInfo[popName].mode === 'full'
      //     ? '100%'
      //     : thisApp.popsInfo[popName].h + 'px';

      $('.app').css({
        height: tgH,
        overflow: 'hidden'
      });
    } else {
      $('.app').css({
        height: 'auto'
      });
    }
  }

  //--- app의 height 값 셋팅: pc
  function setHeight(bool, popName, obj) {
    // // // console.log('setHeight> ', bool, popName);
    // setHeight
    if (bool) {
      if (
        popName === 'contents-gallery' ||
        popName === 'contents-video' ||
        popName === 'contents-video-select' ||
        popName === 'age-checklist-intro'
      ) {
        thisApp.app.fitHeight = true;
      } else if (popName === 'challenge-detail') {
        $('.app').height(1400);
      } else {
        thisApp.app.fitHeight = false;
      }

      // setHeight cancel
    } else {
      if (popName === 'challenge-detail') {
        $('.app').height('');
      } else {
        thisApp.app.fitHeight = false;
      }
    }
  }

  function getAge(birth) {}

  //--- call ajax
  function sendData(tgData, tgUrl, callSuccess, callError) {
    // // console.log('fn::sendData>> ', tgUrl, tgData);
    $.ajax({
      type: 'POST',
      url: tgUrl,
      data: tgData,
      cache: false,
      success: function(res) {
        callSuccess(res);
      },
      error: function(err) {
        callError(err);
      }
    });
  }

  //=== for age
  function getAge(stBirth) {
    var stYear = stBirth.substr(0, 2);
    var stMonth = stBirth.substr(2, 2);
    var stDate = stBirth.substr(4, 2);
    var tgYear = '';
    if (stYear * 1 >= 0 && stYear * 1 <= 18) {
      tgYear = '20' + stYear;
    } else {
      tgYear = '19' + stYear;
    }

    var dateString = tgYear + '/' + stMonth + '/' + stDate;
    var today = new Date();
    var birthDate = new Date(dateString);
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
      age--;
    }
    if (!age) age = 0;
    return age;
  }
  function getAgeUserChecked(checkedList, userAge, scoreAgesInfo) {
    var result = {
      total: 0,
      checked: {},
      level: 1
    };

    var checkedScore = 0;
    checkedList.map(function(obj, idx) {
      var checked = obj.checked == 'true';
      // age
      if (checked) {
        checkedScore += obj.o * 1;
      } else if (!checked) {
        checkedScore += obj.x * 1;
      }
      // checklist
      var keyName = 'checklist' + (idx + 1);
      result.checked[keyName] = obj.checked;
    });

    // 노안 후기
    if (checkedScore < 11) {
      result.level = 3;
      // 노안 초기
    } else if (checkedScore < 21) {
      result.level = 2;
      //
    } else {
      result.level = 1;
    }

    var addAge = scoreAgesInfo[result.total];
    result.total = userAge + addAge;

    // debugCalcAge({
    //   userAge: userAge,
    //   checkedScore: checkedScore,
    //   level: result.level,
    //   addAge: addAge
    // });
    // console.log(':::bodyA> ', result.total);
    return result;
  }

  function debugCalcAge(obj) {
    console.log(
      '::: userA> ',
      obj.userAge,
      '/ checkedScore> ',
      obj.checkedScore,
      ' /level> ',
      obj.level,
      ' /addAge> ',
      obj.addAge
    );
  }

  // add/ hanheeok / 2018.12.03 / string list to array list
  function getListFromArr(arr) {
    var result = arr.map(function(ele, idx) {
      return ele.split(',');
    });
    return result;
  }

  return {
    settingData: settingData,

    setHeightM: setHeightM,
    setHeight: setHeight,
    sendData: sendData,

    getAge: getAge,
    getAgeUserChecked: getAgeUserChecked,
    getListFromArr: getListFromArr
  };
}
