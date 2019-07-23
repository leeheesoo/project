var utils = (function(){
  function getShuffledTgNum(tgArr, tgNum){
  var arr = tgArr.concat();
  var results = [];
  var counter = tgNum;  
  var idxRange = 0;  
  while( counter > 0 ){     
    // random 한 값의 추출범위
    idxRange = arr.length;  
    // 랜덤한 index 값을 구한다.
    var idx = Math.floor( Math.random()*idxRange);    
    //console.log( idxRange, idx )
    // 해당 원소는 빼서 결과리스트에 담는다.
    var val = arr.splice(idx,1)[0];
    results.push( val );        
    counter--;
  }
  return results;  
}
  
  return {
    getShuffledTgNum: getShuffledTgNum
    
  } 
})()