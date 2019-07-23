var bagtoTheFutureModel = new Vue({
    el: '#bagtothefuture',
    data: {
        isLoading: 'none',
        isClosed: 'N'
    },
    methods: {
        openEntryPopup: function () {
            if (bagtoTheFutureModel.isClosed === 'Y') {
                alert('접수가 마감되었습니다.');
                return false;
            }
        },
        saveCheckEntry: function () {
            $('#frmIdeaCompetition').ajaxSubmit({
                cache: false,
                dataType: 'json',
                beforeSend: this.beginIdeaCompetition,
                complete: this.completeIdeaCompetition,
                success: this.successCheckEntry,
                error: this.failureIdeaCompetition
            });
        },
        beginIdeaCompetition: function () {
            this.isLoading = 'block';
        },
        completeIdeaCompetition: function () {
            this.isLoading = 'none';
        },
        successIdeaCompetition: function (res) {
            alert('응모되었습니다.');
            closePop('personal');
            $('#frmIdeaCompetition').clearForm();
            $('#frmIdeaCompetitionPersonalInfo').clearForm();
        },
        failureIdeaCompetition: function (xhr, err) {
            this.isLoading = 'none';
            if (err !== 'abort') {
                alert(xhr.responseJSON.Message);
            }
        },
        successCheckEntry: function (res) {
            if (res.result != null && !res.result) {
                alert(res.message);
                return;
            }
            closePop('yesno');
            openPop('personal');
            $('#IdeaName').val(res.IdeaName);
            $('#IdeaDescription').val(res.IdeaDescription);
            $('#File').val(res.File);
            $('#frmIdeaCompetitionPersonalInfo').clearForm();
            this.isLoading = 'none';
        },
        checkInapp: function () {
            if (isInapp) {
                openPop('pop-inapp');
                return false;
            }
            return true;
        },
        /* start sns */
        openSnsPopup: function () {
            if (bagtoTheFutureModel.isClosed === 'Y') {
                alert('이벤트가 종료되었습니다.');
                return false;
                }
            if (!bagtoTheFutureModel.checkInapp()) {
                return false;
            }
            $('#frmSnsPersonalInfo').clearForm();
            openPop('sns-share-personal');
        },
        saveSnsPersonalInfo: function (res) {
            $('#hidUserId').val(res.Id);
            closePop('sns-share-personal');
            openPop('sns-share');
        },
        shareFacebook: function () {
            var data = {};
            data.link = 'https://samsonite.pentacle.co.kr/bag-to-the-future?utm_source=Promotion&utm_campaign=bagtothefuture&utm_medium=Facebook_share';
            data.picture = 'https://samsonite.pentacle.co.kr/Content/images/bag-to-the-future/sns/sns-facebook.jpg';
            data.name = 'BAG 투 더 퓨쳐 총 상금 1,000만원의 주인공은?';
            data.description = '하늘을 나는 드론 캐리어? 낙하산이 되는 백팩? 톡톡 튀는 아이디어만 있으면 누구나 참여 가능!';
            data.dialogStyleTop = 3300;
            if (confirm('공유를 완료하셔야 정상적으로 참여됩니다.')) {
                facebookShare(data, function (snsId, snsName, postId, scrapURL) {
                    this.saveSnsShareInfo('Facebook', snsId, snsName, scrapURL);
                }, function (err) {
                    alert('공유완료 하셔야 이벤트에 정상적으로 참여됩니다.');
                });
            }
        },
        shareKakaostory: function () {
            var data = {};
            data.link = 'https://samsonite.pentacle.co.kr/bag-to-the-future?utm_source=Promotion&utm_campaign=bagtothefuture&utm_medium=KakaoStory_share&is_rd=y';
            data.description = 'BAG 투 더 퓨쳐 총 상금 1,000만원의 주인공은?\r\n하늘을 나는 드론 캐리어? 낙하산이 되는 백팩?\r\n톡톡 튀는 아이디어만 있으면 누구나 참여 가능!\r\n\r\n공모전 참여하기 >> https://goo.gl/dAJCra';
            kakaostoryShareVideo(data, function (snsId, snsNickName, scrapURL) {
                this.saveSnsShareInfo('KakaoStory', snsId, snsNickName, scrapURL);
            }, function (e) {
                alert(e.responseJSON.message);
            });

        },
        shareKakaotalk: function () {
            var data = {};
            data.btnText = '공모전 응모하기';
            data.link = 'https://www.samsonitemall.co.kr/bagtothefuture.html?utm_source=Promotion&utm_campaign=bagtothefuture&utm_medium=KakaoTalk_share&is_rd=y';
            data.picture = 'https://samsonite.pentacle.co.kr/Content/images/bag-to-the-future/sns/sns-kakaotalk.jpg';
            data.description = '쌤소나이트 BAG 투 더 퓨쳐 아이디어 공모전!\r\n\r\n당신의 이노베이션 아이디어는?\r\n총 상금 1,000만원의 주인공을 찾습니다!\r\n\r\n지금 바로 확인하기 ▼';
            if (confirm('공유를 완료하셔야 정상적으로 참여됩니다.')) {
                kakaotalkShare(data, function () {
                    this.saveSnsShareInfo('Kakaotalk', null, null, null);
                }, function () {
                    alert('모바일에서 참여가 가능합니다 ^^');
                });
            }
        }
    }
});

var saveSnsShareInfo = function (snsType, snsId, snsName, scrapURL) {
    $.post('/api/bag-to-the-future/sns/post', {
        UserId: $('#hidUserId').val(),
        SnsType: snsType,
        SnsId: snsId,
        SnsName: snsName,
        Post: scrapURL
    }, function (res) {
        if(snsType =='KakaoStory'){
            alert('공유가 완료되었습니다.');
        }
    }).error(function (xhr, err) {
        alert(xhr.responseJSON.message);
    });
}
$(function () {
    $('#frmIdeaCompetition').data('validator').settings.submitHandler = function (e) {
        if (!checkMobile.iOS() && $('#BagtotheFutureEntryCheckModel_Attachment').val().length === 0) {
            openPop('yesno');
            return;
        }
        bagtoTheFutureModel.saveCheckEntry();
    }

    //$('.btn-join').click(function () {
    //    if (!bagtoTheFutureModel.checkInapp()) {
    //        return false;
    //    }
    //});

    $('#BagtotheFutureEntryCheckModel_Attachment').click(function () {
        if (checkMobile.iOS()) {
            alert('아이폰 사용자는 파일 첨부가 불가합니다.\n파일 첨부 시 PC 버전으로 응모 부탁 드립니다.');
            return false;
        }
    });
    $('#BagtotheFutureEntryCheckModel_IdeaDescription').on('keyup', function (element){
        chkTextLength(element.currentTarget, element.currentTarget.dataset.valMaxlengthMax, element.currentTarget.dataset.valMaxlength);
    });

    $('#BagtotheFutureEntryCheckModel_Attachment').on('change', function (element) {
        if (element.currentTarget.value != '') {
            alert('첨부파일이 업로드 되었습니다.');
        }
    });
})