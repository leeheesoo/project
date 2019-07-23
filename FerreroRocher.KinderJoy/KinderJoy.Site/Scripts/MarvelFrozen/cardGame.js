var cardGame = function (obj) {

    var $container = obj.container;    
    var setNum = obj.setNum;
    //
    var cardGroupList = []; // card group list

    var currentStepNum = 1;
    var cardNameList = [];
    var compareList = [];
    var rightCnt = 0; // 맞춘개수   

    var onPlay = false;
    var isEnd = false;
    var totalWidth = gameBarWidth; // progressivebar 너비    
    var progressNum = 1;
    var timerIntervalId;    // timer interval      
    var hintCnt = 0;

    //==== time values
    var intervalMiliSect = 2000; // 각 step 이동시 지연시간( 밀리세컨 )
    var hintMiliSec = 3000; // 힌트보여주는 시간    
    var userTime = 0; // 사용자가 사용한 시간
    var limitTime = 3*60; // 주어진 게임 시간( 초 ) 
    var showCardTimeSec = cardActiveDelay; // 뒤집은 카드 보는 시간   
    
    //=== ui 들
    var $uiStepList = $container.find('.stage li');
    var $uiStepImgList;
    var $uiStepPointer = $container.find('.pointer');
    var $uiTimer = $container.find('.timer');
    var $uiScoreNum = $container.find('.score-num');   
    var $uiProgress = $container.find('.bar');
    var $uiCardContainer = $('#cards'); 
    var $uiCardList; // card 들        
    var $btnHint = $container.find('.btn-hint');

    //=== 
    init();
  

    //===
    function init() {
        //=== add button event
        // start
        //$container.find('.btn-game-start').click(function () {
        //    setStep(2);
        //    return false;
        //});
        // retry
        $container.find('.btn-retry').click(function () {
            setStep(3);
            return false;
        });
        // show hint
        $btnHint.click(function () {
            if (!onPlay) return;            

            showHint();

            //--- check hint counter           
            hintCnt++;
            if (hintCnt >= 3) {
                hintCnt = 0;
                $btnHint.hide();
                return;
            }
            // console.log('hint cnt> ' + hintCnt);
            return false;
        });        
        
        if (device == 'm') $uiStepImgList = $container.find('.steps>img');
        setPointer();

        //=== 카드세팅
        initCards();              
    }

    function initCards() {
        makeCardGroupList();
        setList();        

        var cardBackUrl = cardImgUrl + 'card-back.png';
        for (i = 0; i < cardNameList.length; i++) {
            var cardId = cardNameList[i];                        
            // var cardBackUrl = cardImgUrl + cardId + '.png';            
            var stCard = '<li class="card" data-card="" data-idx="">'                        
                        + '<div class="face front"><img src="" alt="Alternate Text" /></div>'                        
                        + '<div class="face back"><img src=' + cardBackUrl + ' alt="Alternate Text" /></div>'                        
                        +'</li>';
            $uiCardContainer.append($(stCard));
        }
        $uiCardList = $uiCardContainer.find('li');        
    }       
    
    //=== 해당 step으로 가기
    function setStep(tgNum) {        
        currentStepNum = tgNum;

        $uiStepList.hide();       
        $uiStepList.eq(tgNum - 1).show();        
        
        // step3 진입후 일정시간 후  4로 간다.
        if( currentStepNum == 3) {            
            readyGame();
            setTimeout(function (){                
                setStep(4);                   
            }, hintMiliSec );
        }

        if (currentStepNum == 4) {
            startGame();
        }

        if (currentStepNum == 5) {
            $uiScoreNum.text(toTimeString(userTime));            
        }
        setPointer();
    }
    
    //===============================
    // game control part
    //===============================
    function readyGame(){
        initGameValues();
        resetCards();
        $uiCardContainer.show();
    }  

    // game 시작
    function startGame() {
        onPlay = true;
        hideImgCards();
        startTimer();
    }

    function hideImgCards() {
        $uiCardList.each(function () {
            var $this = $(this);
            if (!$this.hasClass('matched')) $this.removeClass('flipped');
        });
        setTimeout(function () {
            onPlay = true;
        }, 300);        
    }


    //=== 초기화
    function initGameValues() {
        onPlay = false;
        rightCnt = 0;
        compareList = [];
        isEnd = false;
        userTime = 0;
        hintCnt = 0;

        $btnHint.show();
        $uiProgress.css({
            'left': (-1 * totalWidth)
        });        
    }

    function resetCards() {
        initGameValues();
        shuffle(); // 카드 섞기
        $uiCardList.removeClass('matched'); // 매치됐던 카드 초기화
        $uiCardList.addClass('flipped'); // 그림이 보이게 놓는다.   

        //--- 카드게임 로직
        $uiCardList.on('click', function () {
            checkMatch(this);
        });
    }

    function checkMatch(ele) {
        if (!onPlay) return;        

        var $this = $(ele);      
        $this.addClass('flipped');
        compareList.push($this);        

        if( compareList.length == 2 ){

            //---정답 판별동안에는 막아둔다
            onPlay = false;
            var $card1 = $(compareList[0]);
            var $card2 = $(compareList[1]);

            // 같은 카드는 비교를 무효화 한다.
            if ($card1.attr('data-idx') === $card2.attr('data-idx')) {
                compareList.pop();
                onPlay = true;
                return
            }

            //--- 정답
            if ($card1.attr('data-card') === $card2.attr('data-card')) {

                $card1.addClass('matched');
                $card1.off('click');
                $card2.addClass('matched');
                $card2.off('click');
                
                compareList = [];
                //--- 카드클릭 막은거 풀기
                setTimeout(function () { onPlay = true; }, showCardTimeSec);

                rightCnt++;
                // 다 맞췄는지 확인
                if (rightCnt === setNum) {                
                    endGame();
                }

            //--- 오답
            } else {
                compareList = [];
                setTimeout(function () {                    
                    onPlay = true;                    
                    hideImgCards();
                }, showCardTimeSec);

            }
        } 
    }
    function endGame() {
        onPlay = false;
        stopTimer();        
        isEnd = true;        

        // 잠시 멈췄다가 다음으로 
        setTimeout(function () {
            $uiCardContainer.hide();
            setStep(5);
        }, 1500 );
    }
    function showHint() {
        onPlay = false;
        $uiCardList.addClass('flipped');

        setTimeout(function () {            
            hideImgCards();
        }, hintMiliSec);        
    }

    //=== timer
    function startTimer() {
        var tgPos = 0;
        timerIntervalId = setInterval(function () {
            userTime++;
            // time
            $uiTimer.text(toTimeString(userTime));
            // progress
            progressNum = ( userTime / limitTime ).toFixed(2);
            if (progressNum >= 1) {
                endGame();
            }
            tgPos = (totalWidth * -1) + (totalWidth * progressNum);
            $uiProgress.animate({
                'left': tgPos
            }, 1000, 'linear');            
        }, 1000);
     
    }
    function stopTimer() {
        clearInterval( timerIntervalId );
    }

    //=== step pointer 위치
    function setPointer() {        
        if (device == 'm') {
            $uiStepImgList.each(function(idx) {
                var $this = $(this);
                $this.attr('src', '/Images/MarvelFrozen/m/game/step-' + (idx+1) + '.png');
            });
            $($uiStepImgList.eq(currentStepNum - 1)).attr('src', '/Images/MarvelFrozen/m/game/step-' + currentStepNum + '-current.png');
        } else {
            $uiStepPointer.css({
                'left': stepPointPosList[currentStepNum-1]
            });
        }
    }

    //=== utils
    function shuffle() {
        // list 만들고        
        setList();

        // 섞인 데이타대로 이미지 입히기
        $uiCardList.each(function (idx, ele) {
            var $this = $(ele);
            var thisId = cardNameList[idx];
            var cardBackUrl = cardImgUrl + thisId + '.png';
            $this.attr('data-idx', idx);            
            $this.attr('data-card', thisId);
            $this.find('.front>img').attr('src', cardBackUrl);            
        })
    }
    // sec to time( 00:000:00 );
    function toTimeString(seconds) {
        return (new Date(seconds * 1000)).toUTCString().match(/(\d\d:\d\d:\d\d)/)[0];
    }

    // 사용될 카드 그룹 리스트를 만듬
    function makeCardGroupList() {
        cardGroupList = [];

        // console.log('start');      
        var carSetDataList = obj.carSetDataList;
        var iTotal = carSetDataList.length;
        var kTotal = 0;

        for (var i = 0; i < iTotal; i++) {
            var tgArr = carSetDataList[i];
            kTotal = tgArr.setNum;

            var resultArr = [];
            for (var k = 0; k < kTotal; k++) {                
                resultArr.push(tgArr.name + '-' + (k+1) );
            }
            cardGroupList.push(resultArr);
        }
        // console.log(cardGroupList);
    }
    // 각 그룹에서 2벌씩 랜덤으로 뽑아서 최종 카드 리스트를 만듬
    function setList() {
        var total = cardGroupList.length;
        cardNameList = [];

        var tempList = [];
        for (var i = 0; i < total; i++) {
            var tgArr = cardGroupList[i];
            tempList = tempList.concat( _.sample(tgArr, 2) );
        }
        tempList = tempList.concat(tempList);
        cardNameList = _.shuffle(tempList);        
    }

    //===== public
    return {
        setStep: setStep
    }


};