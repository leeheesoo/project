var marketingAgreeEventModel = new Vue({
    el: '#marketing-agree-event',
    methods: {
        beginEntry:function(){
            $('.dimm__loading').show();
        },
        completeEntry:function(){
            $('.dimm__loading').hide();
        },
        saveEntry: function (res) {
            $('.dimm__loading').hide();
            if (res.Result === true) {
                alert('이벤트 응모가 완료되었습니다.');
                $('#frmMarketingAgreeEntry').clearForm();
                $('.dimm__block').attr('onclick', 'alert("휴대폰 인증을 진행해 주세요");');

            } else {
                if (!res.IsRequiredAllCheck) {
                    openPopup('Alert');
                } else {
                    alert(res.Message);
                }
            }
        },
        failureEntry: function (xhr, err) {
            $('.dimm__loading').hide();
            alert('다시 시도해주세요.');
        }
    }
});

function setKmcData(data) {
    $('#MarketingAgreeModel_Name').val(data.Name);
    $('#MarketingAgreeModel_Mobile').val(data.Mobile);
    $('#MarketingAgreeModel_BirthDay').val(data.BirthDay);
    if (data.Gender === "0") {
        $("#male").prop("checked", true);
    } else {
        $("#female").prop("checked", true);
    }
    $('.dimm__block').attr('onclick', '');
}