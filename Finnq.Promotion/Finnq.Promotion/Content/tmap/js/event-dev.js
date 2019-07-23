var tmapEventEntryModel = new Vue({
    el: '#tmapEventEntry',
    data: {
        showLoading: false
    },
    methods: {
        openEntry: function () {
            $('#frmTmapEventEntry').clearForm();
            openPop('pop-info');
        },
        beginEntry: function () {
            if (confirm('이름: ' + $('#Name').val() + '\n휴대폰: ' + $('#PhoneA').val() + '-' + $('#PhoneB').val() + '-' + $('#PhoneC').val() + '\n입력하신 정보가 맞습니까? 맞으면 확인 버튼 클릭 후 참여가 완료 되며,\n틀리면 취소 버튼 클릭 후 개인 정보를 다시 입력 해 주세요.')) {
                this.showLoading = true;
                return true;
            } else {
                return false;
            }
        },
        completeEntry: function () {
            this.showLoading = false;
        },
        successEntry: function (res) {
            this.showLoading = false;
            if (res) {                
                ga('send', 'event', 'T map 이벤트', '이벤트 응모 완료');
                closePop('pop-info');
                window.location = 'https://finnq.onelink.me/YERF?pid=Tmap';
            } else {
                alert('이벤트 응모 도중 오류가 발생하였습니다.\n다시 시도해주시길 바랍니다.');
            }
        },
        failEntry: function (xhr, err, obj) {
            this.showLoading = false;
            if (err !== 'abort') {
                alert(xhr.responseJSON.Message);
            }
        }
    }
});