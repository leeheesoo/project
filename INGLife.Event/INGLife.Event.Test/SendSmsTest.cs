using INGLife.Event.Message.Repositories.Message;
using INGLife.Event.Message.Services.Message;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace INGLife.Event.Test {
    public class SendSmsTest {
        private Mock<ISmsRepository> repository;
        private SmsService service;
        public SendSmsTest() {
            repository = new Mock<ISmsRepository>();
            service = new SmsService(repository.Object);
        }
        [Fact(DisplayName = "Send SMS 테스트")]
        public void TrySendSms() {
            string phone = "01099779790";
            string callback = "01099779790";
            string msg = "테스트입니다";
            repository.Setup(x => x.SendSMS(phone, callback, msg)).Returns(true);

            var sendSMS = service.SendSMS(phone, callback, msg);

            Assert.True(sendSMS);
            repository.Verify(x => x.SendSMS(phone, callback, msg), Times.Once);
            repository.Verify(x => x.SendMMS("", phone, callback, msg, new List<string>()), Times.Never);
        }
        [Fact(DisplayName = "Send MMS 테스트")]
        public void TrySendMms() {
            string subject = "테스트";
            string phone = "01099779790";
            string callback = "01099779790";
            string msg = "테스트입니다";
            List<string> files = new List<string>();

            repository.Setup(x => x.SendMMS(subject, phone, callback, msg, files)).Returns(true);

            var sendMMS = service.SendMMS(subject, phone, callback, msg, files);

            Assert.True(sendMMS);
            repository.Verify(x => x.SendSMS(phone, callback, msg), Times.Never);
            repository.Verify(x => x.SendMMS(subject, phone, callback, msg, files), Times.Once);
        }
    }
}
