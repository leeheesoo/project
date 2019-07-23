using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using INGLife.Event.Domain.Repositories.Rebranding;
using Moq;
using INGLife.Event.Domain.Services.Rebranding;

namespace INGLife.Event.Test.RebrandingEventTest {
    public class RebrandingEventServiceTest {
        private Mock<IRebrandingMarketingAgreeEntryRepository> marketingRepo;
        private Mock<IRebrandingConsultingAgreeEntryRepository> consultingRepo;

        private IRebrandingEventService service;

        public RebrandingEventServiceTest() {
           
        }
    }
}
