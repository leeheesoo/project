// 카카오톡 스크랩
function kakaoTalkShare(data,callback,failCallback) {
//	console.log(data);
//	
//    Kakao.Link.sendDefault({
//        objectType: 'feed',
//        content: {
//            title: '테스트',
//            description: '이렇게',
//            imageUrl: 'http://lgcare-babience.pentacle.co.kr/images/kabrita/survey-event/sns/kakaotalk.jpg',
//            imageWidth: 200,
//            imageHeight: 200,
//            link: {
//                mobileWebUrl: 'http://lgcare-babience.pentacle.co.kr/event02',
//                webUrl: 'http://lgcare-babience.pentacle.co.kr/event02'
//            },
//            
//        },
//        buttons:[
//        	{
//        		title: '이벤트페이지로 가기',
//        		link:{
//        			mobileWebUrl: 'http://lgcare-babience.pentacle.co.kr/event02',
//                    webUrl: 'http://lgcare-babience.pentacle.co.kr/event02'
//        		}
//        	}
//        ],
//        fail: function (res) {
//        	console.log('fail');
//        	console.log(res);
//        },
//        success:function(res){
//        	console.log('success');
//        	console.log(res);
//        },
//        callback:function(res){
//        	console.log('callback');
//        	console.log(res);
//        }
//    });
}

function kakaoTalkShareTemplate(data,successCallback,failCallback,callback){

	Kakao.Link.sendCustom({
		templateId: data.templateId,
		// templateArgs 적용안됨
//		templateArgs:{
//			'title': data.title,
//			'description': data.descrption
//		},
		fail: function (res) {
//			alert(JSON.stringify(res));
			if(typeof failCallback == 'function'){
				failCallback();
			}
	    },
		success:function(res){
//			alert(JSON.stringify(res));
			if(typeof successCallback == 'function'){
				successCallback();
			}
		},
		callback:function(res){
//			alert(JSON.stringify(res));
			if(typeof callback == 'function'){
				callback();
			}
		}
	});
}