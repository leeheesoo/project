var appData = {
  pagesInfo: {
    main: { key: 'main', desc: '메인' },
    contents: {
      key: 'contents',
      desc: '공유이벤트',
      label: '서리나의 동안바디',
      bgColor: '#e2d5c9',
      // page contents
      contentsInfo: {
        kindsInfo: {
          gallery: {
            key: 'gallery',
            desc: '갤러리',
            onNav: false,
            next: 'contents-gallery'
          },
          video: {
            key: 'video',
            desc: '비디오',
            onNav: false,
            next: 'contents-video-select',
            current: '',
            list: [
              // 티징
              // {
              //   key: 'dance',
              //   desc: '비디오_동안바디댄스 이동',
              //   descBtn: '비디오_댄스 다운로드',
              //   videoId: 'aOwUq4HuY0c'
              // },
              {
                key: 'dance2',
                desc: '비디오_동안바디댄스2 이동',
                descBtn: '비디오_댄스2 다운로드',
                videoId: 'yuUWsM6bksc'
              },
              {
                key: 'body',
                desc: '비디오_동안바디법칙 이동',
                descBtn: '비디오_법칙 다운로드',
                videoId: 'Y6N06zksH-8'
              }
            ]
          }
        },
        kinds: [],
        selected: 'gallery'
      }
    },
    calculator: {
      key: 'calculator',
      desc: '계산기',
      label: '얼굴 VS 바디 계산기',
      bgColor: '#cde4e3',
      // 페이지별 contents info
      contentsInfo: {
        kindsInfo: {
          facial: {
            key: 'facial',
            desc: '페이셜 용품'
          },
          body: {
            key: 'body',
            desc: '바디 용품'
          }
        },
        kinds: [],
        baseList: [
          {
            key: 'SkinLotion',
            for: 'skin',
            img: 'text-skin-lotion.png',
            alt: '페이셜 용품',
            price: { facial: 30000, body: 10000 }
          },
          {
            key: 'Assence',
            for: 'essence',
            img: 'text-essence.png',
            alt: '페이셜 용품',
            price: { facial: 40000, body: 15000 }
          },
          {
            key: 'NutritionWhitening',
            for: 'nutrition',
            img: 'text-nutrition.png',
            alt: '영양/미백',
            price: { facial: 30000, body: 10000 }
          },
          {
            key: 'Etc',
            for: 'etc',
            img: 'text-etc.png',
            alt: '기타',
            price: { facial: 20000, body: 10000 }
          }
        ]
      }
    },
    age: {
      key: 'age',
      desc: '측정기',
      label: '나의 바디 나이는?',
      bgColor: '#d5e0e4',
      contentsInfo: {
        checklist: [
          { o: 5, x: 0, checked: null },
          { o: 0, x: 5, checked: null },
          { o: 0, x: 5, checked: null },
          { o: 5, x: 0, checked: null },
          { o: 5, x: 0, checked: null }
        ],
        scoreAgesInfo: { 0: 8, 5: 5, 10: 4, 15: 3, 20: 1, 25: 0 },
        checkedInfo: {},
        onLoading: false,
        userAge: 0,
        bodyAge: 0,
        userImg: '',
        bodyAgeLevel: 1
      }
    },
    gift: {
      key: 'gift',
      desc: '스페셜키트',
      label: '동안바디 스페셜 키트',
      bgColor: '#cdcee4'
    },
    challenge: {
      key: 'challenge',
      desc: '챌린지',
      label: '동안바디 챌린지',
      bgColor: '#dadbde'
    }
  },
  popsInfo: {
    nav: { mode: '100%', desc: '모바일 네비게이션' },
    'item-wash': { h: 1710, desc: '메인 상품 워시 팝' },
    'item-lotion': { h: 2030, desc: '메인 상품 로션 팝' },

    'contents-event': {
      h: 2580,
      desc: '동안바디 이벤트',
      imgName: 'share-event'
    }, // share-event
    'contents-gallery': { h: 1280, desc: '동안바디 화보' },
    'contents-video-select': {
      h: 1400,
      mode: 'full',
      desc: '동안바디 영상 선택'
    },
    'contents-video': { h: 1400, mode: 'full', desc: '동안바디 비디오' },
    'contents-share-sns': { h: 1400, desc: '동안바디 sns 공유 팝업' },
    'contents-winners': { h: 1400, desc: '티저 영상 공유이벤트 담첨자 발표' }, // 2018.11.02 / hanb / 추가

    'calculator-detail': { h: 1830, desc: '계산기 이벤트 안내 팝' },
    calculator: { h: 1350, desc: '계산기 이벤트의 계산기 팝' },
    'calculator-result': { h: 1512, desc: '계산기 이벤트 결과값' },
    'calculator-winners': { h: 1400, desc: '계산기 이벤트  당첨자 리스트' }, // 2018.12.03 / hanb / 추가

    'age-detail': { h: 1830, desc: '측정기 이벤트 상세 팝' },
    'age-checklist-intro': {
      mode: '100%',
      h: 1840,
      desc: '측정기 이벤트 체크리스트 인트로 영상'
    },
    'age-checklist': { h: 1576, desc: '측정기 이벤트 체크리스트' },
    'age-skin-test': { h: 1576, desc: '측정기 이벤트 스킨 사진 업로드 팝' },
    'age-result': { h: 1337, desc: '측정기 이벤트 결과' },
    'age-winners': { h: 1400, desc: '측정기 이벤트 당첨자 리스트' }, // 2018.12.03 / hanb / 추가

    'challenge-detail': { h: 3445, desc: '챌린지 이벤트 상세 팝' },
    'challenge-winners': { h: 1400, desc: '챌린지 이벤트 당첨자 리스트' }, // 2018.12.03 / hanb / 추가

    'user-info': { mode: 'full', h: 1840, desc: '개인정보 입력 창' },
    'share-sns': {
      mode: 'full',
      h: 1840,
      desc: 'sns 공유, 구매하러 가기, 매장체험 하기'
    },
    store: { mode: 'full', h: 1840, desc: '매장 체험하기 팝' },
    complete: {
      mode: 'full',
      h: 1840,
      desc: '이벤트 참여 완료 멘트가 있는 단순 팝'
    },
    'info-clause': {
      mode: 'full',
      h: 1840,
      desc: '약관 자세히 보기'
    }
  },
  winnersInfo: {
    calculator: [
      '최*(8279),박*지(3341),김*호(3510),김*림(4198),김*름(5642)',
      '이*렬(9776),박*리(4038),김*점(2537),김*희(5912),김*늘(5287)',
      '김*숙(3091),최*정(6286),김*애(5391),서*(8739),서*옥(4660),김*은(3917),이*옥(3039),성*린(7122),이*채(9972),조*라(9389),이*나(6478),오*민(8262),박*희(9923),박*진(2007),최*경(6866),이*영(9422),김*지(3854),권*자(5135),홍*선(4018),정*주(9136)'
    ],
    age: [
      '김*정(5076)',
      '김*주(5092),장*미(6676),김*기(6830),김*혜(2465),박*규(4280),김*재(4621),김*령(3365),최*예(3148),홍*복(4698),김*랑(5516)',
      '김*연(9742),홍*기(9278),송*순(6263),임*성(2748),박*희(5400),이*영(2287),정*진(3589),권*원(5809),고*원(4106),황*정(2468),차*윤(6213),김*민(2237),우*경(3570),배*란(6448),박*슬(5655),주*정(5035),박*연(9224),최*민(2510),조*호(2931),김*술6401'
    ],
    challenge: [
      '임*순(5252)',
      '유*란(3311),이*호(7916),김*진(9627),박*환(2727),이*지(7158),이*아(9088),문*영(5844),이*현(6617),김*대(4913),정*나(8262)'
    ]
  }
};
