
$(function () {
	//바로체험
	$('#survey01FormPersonal').validate({
		rules: {
			name: "required",
			mobile: {
				required: true,
				minlength: 10,
				maxlength: 11
			},
			zipcode: "required",
			address: "required",
			addressDetail: "required",
			babyBirthYear: "required",
			babyBirthMonth: "required",
			proudctSteps: "required",
			agree2: "required",
			agree3: "required"
		},
		messages: {
			name: "이름을 입력해주세요",
			mobile:  {
                required: "휴대폰번호를 입력해주세요",
                minlength: "휴대폰번호를 정확히 입력해주세요",
                maxlength: "휴대폰번호를 정확히 입력해주세요"
            },
			zipcode: "주소를 입력해주세요",
			address: "주소를 입력해주세요",
			addressDetail: "주소를 입력해주세요",
			babyBirthYear: "아이생일을 선택해주세요",
			babyBirthMonth: "아이생일을 선택해주세요",
			proudctSteps: "제품단계를 선택해주세요",
			agree2: "개인정보 활용에 동의해주세요",
			agree3: "경품 배송 및 이벤트 관련 위탁에 동의해주세요"
		},
		submitHandler: function() {
			surveyEvent.experienceUserInfo();
		}
	});     
	
	//사전체험
	$('#survey02FormPersonal').validate({
		rules: {
			name: "required",
			mobile: {
				required: true,
				minlength: 10,
				maxlength: 11
			},
			zipcode: "required",
			address: "required",
			addressDetail: "required",
			babyBirthYear: "required",
			babyBirthMonth: "required",
			agree2: "required",
			agree3: "required"
		},
		messages: {
			name: "이름을 입력해주세요",
			mobile:  {
                required: "휴대폰번호를 입력해주세요",
                minlength: "휴대폰번호를 정확히 입력해주세요",
                maxlength: "휴대폰번호를 정확히 입력해주세요"
            },
			zipcode: "주소를 입력해주세요",
			address: "주소를 입력해주세요",
			addressDetail: "주소를 입력해주세요",
			babyBirthYear: "아이생일을 선택해주세요",
			babyBirthMonth: "아이생일을 선택해주세요",
			agree2: "개인정보 활용에 동의해주세요",
			agree3: "경품 배송 및 이벤트 관련 위탁에 동의해주세요"
		},
		submitHandler: function() {
			surveyEvent.advanceApplicationUserInfo();
		}
	});
	
	//후기이벤트
	$('#survey03FormPersonal').validate({
		rules: {
			name: "required",
			mobile: {
				required: true,
				minlength: 10,
				maxlength: 11
			},
			zipcode: "required",
			address: "required",
			addressDetail: "required",
			agree2: "required",
			agree3: "required"
		},
		messages: {
			name: "이름을 입력해주세요",
			mobile:  {
                required: "휴대폰번호를 입력해주세요",
                minlength: "휴대폰번호를 정확히 입력해주세요",
                maxlength: "휴대폰번호를 정확히 입력해주세요"
            },
			zipcode: "주소를 입력해주세요",
			address: "주소를 입력해주세요",
			addressDetail: "주소를 입력해주세요",
			agree2: "개인정보 활용에 동의해주세요",
			agree3: "경품 배송 및 이벤트 관련 위탁에 동의해주세요"
		},
		submitHandler: function() {
			surveyEvent.reviewUserInfo();
		}
	});
})

var clipboard = null;
var selectArr = [];

function surveyEventData() {
    return {
        testSj : [],
        QuestionList: [
            //사전신청 질문
            ['임산부', '0개월 ~ 6개월'],
            [{ questionAlt: '소화력' }, { questionAlt: '황금변' }, { questionAlt: '영양성분' }, { questionAlt: '브랜드' }, { questionAlt: '가격' }, { questionAlt: '', inputString: '' }],
            ['조제식 수유', '모유수유', '혼합수유']
        ],
        experienceData: { //바로체험
            name: '',
            mobile: '',
            zipCode: '',
            address: '',
            addressDetail: '',
            babyBirthYear: '',
            babyBirthMonth: '',
            proudctSteps: '',
            feedingType: '',
            usedProduct: '',
            selectPoint: '',
            selectPoint2: '',
            childcareWorry: '',
            agree: false,
            agree2: false,
            agree3: false
        },
        advanceApplicationSendData: { //사전신청
            name: '',
            mobile: '',
            zipCode: '',
            address: '',
            addressDetail: '',
            babyBirthYear:'',
            babyBirthMonth:'',
            babyAge: '',
            usedProduct: '',
            feedingType: '',
            childcareWorry: '',
            agree: false,
            agree2: false,
            agree3: false
        },
        reviewData: {
            name: '',
            mobile: '',
            zipCode: '',
            address: '',
            addressDetail: '',
            reviewUrl: '',
            agree: false,
            agree2: false,
            agree3: false
        },
        loading: false,
        popName: '',
        popBefore: '',
        prizewinnerTabNumber: '4', //1주차 = 0
        prizewinnerTab: [{ tab: 0, active: false }, { tab: 1, active: false }, { tab: 2, active: false }, { tab: 3, active: false, }, { tab: 4, active: true, }],
        prizWinner01Step02: [ // 바로체험 당첨자 2단계 [1주차],[2주차]....
            ['김*지(4679)', '최*희(0167)', '박*희(9031)', '오*혜(4787)', '김*정(0596)', '이*미(4581)', '장*별(0402)', '이*나(4000)', '윤*숙(2193)', '이*경(9236)', '윤*옥(6434)', '이*연(4691)', '조*진(1079)', '최*정(5576)', '진*경(2705)', '박*용(9612)', '정*희(4455)', '이*진(7156)', '황*경(0188)', '안*(5315)', '신*균(2827)', '이*현(2632)', '좌*혜(0215)', '강*신(8305)', '이*원(5933)', '황*경(9089)', '임*희(2221)', '박*미(5117)', '김*계(7676)', '유*현(8305)'],
            ['윤*나(7053)','천*희(0188)','원*선(7090)','정*희(0331)','강*리(7304)','박*진(2425)','김*영(9089)','김*희(0574)','정*경(2856)','지*정(4215)','여*감(1678)','최*민(2904)','박*희(1177)','김*애(5904)','이*미(7362)','주*희(8031)','조*서(8756)','김*화(2791)','정*정(5070)','최*연(6184)','최*랑(9521)','손*은(0304)','이*희(9726)','이*미(0715)','김*용(4859)','전*순(3204)','김*매(5999)','박*영(6443)','최*아(7686)','김*영(8115)','송*지(9603)','김*화(4333)','서*나(2587)','김*화(1888)','김*철(2230)','정*경(8040)','강*원(2615)','임*정(4679)','김*숙(7741)','윤*운(4581)'],
            ['박*정(6657)','장*순(1923)','박*희(3537)','김*구(3909)','김*은(3763)','박*연(6567)','정*연(0375)','한*영(8987)','백*정(9829)','강*슬(0056)','이*리(5372)','김*정(0189)','김*영(3917)','조*선(9273)','권*아(2907)','장*기(2183)','이*미(4985)','차*화(2097)','이*진(8269)','김*선(6251)','제*진(1748)','장*룡(8030)','김*재(1128)','조*일(5169)','최*정(8916)','오*빈(2615)','허*숙(4634)','곽*혁(9122)','이*니(1028)','지*웅(5906)','권*선(5203)'],
            ["권*아(2907)", "신*현(4975)", "김*미(8579)", "백*진(1937)", "천*주(5775)", "황*미(1107)", "최*영(2242)", "장*옥(3735)", "이*이(6622)", "김*정(1241)", "김*명(0529)", "임*미(8311)", "이*요(7896)", "김*영(3917)", "황*민(4445)", "권*린(9991)", "서*이(7507)", "박*희(5312)", "오*화(0531)", "이*정(0150)", "정*안(8257)", "최*영(3748)", "김*정(0547)", "조*란(8437)", "황*애(2308)", "김*순(3963)", "홍*숙(1102)", "이*리(5372)", "이*은(4684)", "권*현(3726)"],
			      ["황*희(1223)", "심*혜(6797)", "김*미(6684)", "이*영(6232)", "허*(1405)", "박*기(8953)", "노*라(0372)", "박*남(5445)", "이*숙(6110)", "김*하(6650)", "박*현(6449)", "허*윤(0294)", "양*란(8783)", "허*희(8656)", "손*희(2216)", "김*인(4410)", "김*지(6840)", "김*미(6000)", "김*미(8756)", "임*은(9791)", "정*희(0747)", "심*윤(7775)", "설*연(7002)", "정*진(6877)", "김*연(1905)", "김*선(5142)", "방*리(0308)", "박*민(9331)", "박*희(7980)", "정*주(0222)"]
        ],
        prizWinner01Step03: [ // 바로체험 당첨자 3단계 [1주차],[2주차]....
            ['최*름(4152)', '박*영(2466)', '김*미(2959)', '배*미(3663)', '최*규(1157)', '정*우(2932)', '박*주(3845)', '안*슬(3318)', '김*선(8526)', '김*은(0646)', '강*름(7300)', '정*진(6183)', '강*하(7919)', '경*연(8045)', '이*림(4455)', '이*영(9977)', '김*희(1020)', '신*수(0369)', '윤*경(0970)', '김*진(2796)'],
            ['김*진(9213)','김*현(8486)','최*정(8317)','황*현(1486)','정*선(4305)','이*림(1298)','김*민(1201)','전*애(4778)','유*현(2065)','이*름(2565)'],
            ['임*영(3789)','윤*선(2463)','백*희(0105)','서*(7955)','손*지(6484)','박*영(8099)','박*주(8596)','강*미(8200)','박*영(3262)','신*혜(8098)','윤*윤(4860)','유*영(8331)','공*영(6713)','소*희(4568)','이*기(8212)','박*혜(4853)','신*현(6994)','강*경(9395)','이*현(2677)'],
            ["최*화(2781)", "하*숙(9630)", "박*영(0765)", "정*영(1311)", "이*롱(3764)", "유*혜(1384)", "전*나(7465)", "이*준(7465)", "김*영(3581)", "김*은(0429)", "백*운(4619)", "백*연(3359)", "박*의(1257)", "박*실(3511)", "이*진(1267)", "정*영(8216)", "김*진(7759)", "문*옥(5049)", "김*진(2236)", "김*미(8404)"],
			      ["김*민(1157)", "김*매(2587)", "김*조(3412)", "전*(8839)", "류*름(3183)", "강*남(8090)", "장*아(7008)", "남*선(7885)", "홍*민(5831)", "이*영(7673)", "홍*나(3741)", "박*정(8710)", "김*윤(2996)", "장*진(6427)", "조*화(6625)", "형*우(2186)", "박*지(0047)", "황*서(7735)", "안*옥(9666)", "윤*현(2670)"]
        ],
        prizWinner02: [ //사전신청 당첨자 [1주차],[2주차]....
            ['김*빈(6253)', '손*영(5242)', '김*정(3944)', '김*향(1895)', '박*영(1277)', '김*경(9661)', '김*미(3113)', '김*현(8992)', '이*선(6009)', '남*우(4705)', '김*희(2773)', '조*진(4188)', '강*두(0125)', '설*혜(9604)', '김*영(8252)', '홍*비(8163)', '이*경(1206)', '추*원(5888)', '김*양(3046)', '이*철(7788)', '한*영(6848)', '이*나(9995)', '김*순(1017)', '신*희(7750)', '김*민(6217)', '김*정(6106)', '강*진(7290)', '최*아(7075)', '장*선(9577)', '박*선(0627)', '이*민(0617)', '이*영(6807)', '조*정(7756)', '김*미(9434)', '조*원(0415)', '민*령(0111)', '노*우(6500)', '박*희(4865)', '김*래(9394)', '김*희(6747)', '김*지(3182)', '김*섭(0115)', '주*미(3264)', '김*미(1775)', '김*지(2895)', '김*희(0503)', '김*옥(6779)', '김*정(4126)', '박*영(7209)', '박*운(5241)'],
            ['김*주(8389)','신*선(2292)','손*영(5242)','채*희(0060)','김*자(2025)','이*미(4821)','이*은(8968)','김*혜(2611)','염*경(3523)','하*나(0510)','이*주(1200)','전*은(6424)','한*혜(9842)','최*준(9919)','정*재(6654)','왕*숙(8440)','이*훈(2559)','강*희(0541)','임*화(9513)','박*령(9114)','차*이(2082)','김*영(8457)','채*희(1115)','이*아(4474)','정*희(0690)','김*라(3468)','강*빈(3032)','우*영(1163)','남*숙(8013)','한*혜(0293)','남*구(2134)','원*라(3329)','심*수(0060)','한*인(2620)','김*정(3954)','백*경(9314)','김*유(1020)','정*미(8090)','주*영(2653)','권*람(4701)','안*영(8744)','유*라(2923)','하*민(0946)','이*은(5246)','이*영(8481)','김*미(3146)','안*숙(4679)','박*정(8602)','홍*경(0725)','정*현(3852)'],
            ['신*화(3443)','육*선(3759)','권*래(2818)','임*연(9860)','지*숙(0756)','채*경(3907)','김*선(6973)','옥*혜(3383)','공*미(6514)','임*선(8908)','엄*희(9203)','백*선(2672)','서*미(3463)','김*라(7001)','박*진(5792)','신*리(9014)','정*석(8311)','이*희(1014)','김*미(9166)','김*신(5336)','김*학(3234)','이*희(3599)','김*유(0813)','김*현(7329)','하*정(3030)','정*운(4740)','임*(1763)','최*혜(3113)','공*연(2717)','임*이(6807)','최*진(2813)','이*리(8824)','조*화(2531)','고*람(0438)','안*은(7947)','이*경(8456)','이*연(3599)','김*영(8308)','장*솔(3216)','한*헌(6134)','주*화(7555)','윤*영(1192)','박*연(6486)','김*인(2250)','신*현(1658)','김*경(3321)','김*연(4573)','이*나(2235)','최*미(5277)','장*정(1203)'],
            ["이*영(9723)", "김*은(5163)", "김*정(2928)", "최*희(6235)", "유*미(1809)", "김*은(6307)", "조*연(1087)", "이*엽(4930)", "천*로미(7597)", "김*정(6455)", "정*경(5676)", "노*나(7443)", "손*은(0206)", "김*나(2946)", "이*아(6893)", "박*성(1051)", "이*연(7557)", "최*나(7795)", "정*정(1453)", "김*현(6005)", "여*화(3544)", "정*나(0675)", "이*미(3669)", "이*경(8905)", "안*숙(7544)", "황*년(7694)", "김*민(2831)", "박*현(9611)", "조*언(3315)", "하*미(0412)", "이*영(0310)", "이*정(9008)", "장*화(4883)", "이*연(2642)", "박*은(8214)", "허*윤(8400)", "유*미(4915)", "조*경(0058)", "공*매(2555)", "연*영(7792)", "김*라(9934)", "조*주(0502)", "이*훈(9426)", "이*빈(9122)", "이*영(3535)", "강*영(8273)", "박*미(1932)", "윤*현(3814)", "김*영(8736)", "박*진(5775)"],
			      ["박*진(8771)", "신*화(3264)", "권*영(3787)", "전*미(4774)", "최*호(8764)", "양*갑(2305)", "김*옥(6548)", "이*혜(5079)", "박*란(1291)", "곽*혜(0052)", "이*영(0729)", "김*숙(6662)", "박*음(7741)", "황*인(8852)", "이*나(9439)", "엄*용(6836)", "안*성(0716)", "김*정(7563)", "이*연(8227)", "이*환(7131)", "심*지(3817)", "최*선(3059)", "정*종(6476)", "김*임(9634)", "이*현(6704)", "김*연(7009)", "김*하(6668)", "강*림(6575)", "임*야(7898)", "홍*화(2849)", "염*영(9206)", "임*은(2226)", "손*미(6660)", "박*숙(3062)", "서*진(4865)", "김*름(4857)", "엄*옴(8084)", "성*희(1022)", "김*륜(4700)", "한*희(2462)", "이*진(0907)", "노*진(7593)", "정*선(4262)", "이*지(5408)", "김*(1981)", "박*영(1328)", "김*진(2518)", "김*지(7914)", "권*라(2982)", "정*주(0403)"]
        ],
        snsData: {
            eventType: null,
            url: null,
            isSave: false
        },
        sharePopupText :true
    }
};



var surveyEvent = new Vue({
    el: '#surveyEvent',
    data: surveyEventData(),
    created: function () {
    	setClipboard();
    	openPopup('review-winner');
    },
    methods: {
        openPostCode: function (popName, zipCode, address) { // 우편번호팝업
            var self = this;

            this.popName = popName;
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

                    //var addressDetailId = 'event-one-addressdetail';
                    if (popName == 'survey01-personal') {
                        self.experienceData.zipCode = data.zonecode;
                        self.experienceData.address = fullAddr
                    }  else if (popName == 'survey02-personal') {
                        //addressDetailId = 'event-two-addressdetail';
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
            return false;
        },
        advanceQuestionCheck:function(){ //사전신청
            var babyAge = this.advanceApplicationSendData.babyAge;
            var usedProduct = this.advanceApplicationSendData.usedProduct;

            var feedingType = this.advanceApplicationSendData.feedingType;
            var self = this;

            //사전신청 설문 data check
            if (babyAge === '' || usedProduct === '' || feedingType === '') {
                alert('설문을 모두 입력해 주세요');
                return false;
            } else {
                openPopup('survey02-personal');
                closePopup('survey02', false);
            }
        },
        experienceQuestionCheck: function () { //바로채험
            selectArr = [];
            $('input[name=survey01-q03]:checked').each(function (selectPoint) {
                //console.log($(this).attr('id'));
                selectArr.push($(this).val());
            });
            //alert(JSON.stringify(selectArr));
            
            var self = this;
            this.experienceData.selectPoint = selectArr.map(function (item) {
                return item || self.QuestionList[1][5].inputString;
            });

            var feedingType = this.experienceData.feedingType;
            var usedProduct = this.experienceData.usedProduct;
            var selectPoint = selectArr.length;
            //var childcareWorry = this.experienceData.childcareWorry;

            //사전신청 설문 data check
            if (feedingType === '' || usedProduct === '' || selectPoint === 0 ) {
                alert('설문을 모두 입력해 주세요');
                return false;
            } else {
                this.experienceData.selectPoint = selectArr[0];
                this.experienceData.selectPoint2 = selectArr[1];

                openPopup('survey01-personal');
                closePopup('survey01', false);
            }
        },
        advanceApplicationUserInfo: function (e) { //사전신청 data
            var self = this;
            
			$.ajax({
                url: '/api/kabrita/advance-application',
                type: 'POST',
                // dataType: 'json',
                data: this.advanceApplicationSendData,
                success: function (res) {
                    closePopup('survey02-personal');

                    $('#survey02Form').clearForm();
                    $('#survey02FormPersonal').clearForm();
                    self.$data.advanceApplicationSendData = surveyEventData().advanceApplicationSendData;
                    //Object.assign(self.$data.advanceApplicationSendData, surveyEventData().advanceApplicationSendData);
                    surveyEvent.openShare('survey02');
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
        experienceUserInfo: function (e) { // 바로체험data        	
            var self = this;
			

					
			$.ajax({
                url: '/api/kabrita/experience',
                type: 'POST',
                data: this.experienceData,

                success: function (res) {
                    closePopup('survey01-personal');
					$('#survey01Form').clearForm();
					$('#survey01FormPersonal').clearForm();
					//self.dataReset();
					self.$data.experienceData = surveyEventData().experienceData;
					//Object.assign(self.$data.experienceData, surveyEventData().experienceData);
                    surveyEvent.openShare('survey01');
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
            })         
        },
        getQuestioinClass: function(idx){
            return idx+1
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
                url: '/api/kabrita/review',
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
        winnerTab: function (tab) { //당첨자발표 tab
            this.prizewinnerTabNumber = tab.tab;
            this.prizewinnerTab.forEach(function (e) {
                e.active = false;
            });
            tab.active = !tab.active;
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
            //selectArr = checkedInputs.map(function (item) {
            //    return this.value;
            //    console.log(item)
            //}).get();

            if ($('#survey01-q03-a06').is(':checked')) {
                $etcInput.removeAttr('readonly');
                $etcInput.focus();
            } else {
                $etcInput.attr('readonly', 'readonly');
                this.QuestionList[1][5].inputString = '';
            }
        },
        popupControl: function (popBefore) { //자세히보기 버튼 click 이전팝업 저장
            var self = this;
            this.popBefore = popBefore;
            closePopup(popBefore);
        },
        kakaotalkShare:function(){
        	var data={};
        	
        	// 사전신청 : 11138 , 바로체험 : 11155 , 단순공유 : 11169
        	data.templateId = 11169;
        	
            if(this.snsData.eventType == 'survey02'){
        		data.templateId = 11138;
        	}else if(this.snsData.eventType == 'survey01'){
        		data.templateId = 11155;
        	}

        	kakaoTalkShareTemplate(data,function(){
        		if(surveyEvent.snsData.isSave){
        			var shareData = {};
        			shareData.sharingType = 'kakao';
        			shareData.url = surveyEvent.snsData.url;
        			
        			saveSnsShare(shareData,function(){
        				if(isMobile.any()){
        					alert('이벤트 응모가 완료되었습니다.');
        				}
            		})        			
        		}
        	},function(){
        	},function(){
        	});
        },
        openShare:function(eventType){
        	this.snsData.eventType = eventType;
        	this.snsData.isSave = false;
        	
        	if(eventType == 'survey02'){
               	this.snsData.isSave = true;
               	this.snsData.url = '/api/kabrita/advance-application/sharing'        		
        	}else if(eventType == 'survey01'){
               	this.snsData.isSave = true;
               	this.snsData.url = '/api/kabrita/experience/sharing'
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

function saveSnsShare(data,successCallback){
	 $.ajax({
         url: data.url,
         type: 'POST',
         data: {
        	 sharingType : data.sharingType
         },
         success: function (res) {
        	 if(typeof successCallback == 'function'){
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
        if (surveyEvent.snsData.isSave) {
            var snsData = {};
            snsData.sharingType = 'url';
            snsData.url = surveyEvent.snsData.url;

            saveSnsShare(snsData, function () {
                alert('복사되었습니다');
            })
        } else {
            alert('복사되었습니다');
        }
    });
    clipboard.on('error', function (e) {
        alert('복사실패')
    });
}



