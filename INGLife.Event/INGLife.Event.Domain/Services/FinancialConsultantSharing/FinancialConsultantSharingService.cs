using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INGLife.Event.Domain.Entities.FinancialConcertMarketingAgree;
using INGLife.Event.Domain.Repositories.FinancialConsultantSharing;
using INGLife.Event.Domain.Exceptions;
using INGLife.Event.Domain.Entities.FinancialConsultantSharing;
using System.Collections;
using INGLife.Event.Domain.Entities.Abstract;

namespace INGLife.Event.Domain.Services.FinancialConsultantSharing
{
    public class FinancialConsultantSharingService : IFinancialConsultantSharingService
    {
        #region variables
        IFinancialConsultantRepository fcRepository;
        IFinancialConsultantOriginCustomerRepository fcCustomerOriginRepository;
        IFinancialConsultantNewCustomerRepository fcCustomerNewRepository;
        #endregion

        #region constructor
        public FinancialConsultantSharingService(IFinancialConsultantRepository fcRepository, IFinancialConsultantOriginCustomerRepository fcCustomerOriginRepository, IFinancialConsultantNewCustomerRepository fcCustomerNewRepository)
        {
            this.fcRepository = fcRepository;
            this.fcCustomerOriginRepository = fcCustomerOriginRepository;
            this.fcCustomerNewRepository = fcCustomerNewRepository;
        }
        #endregion

        #region isExistFC
        public Boolean isExistFC(string financialConsultantCode)
        {
            var selectEntry = fcRepository.SingleOrDefault(x => x.FcCode.Equals(financialConsultantCode));
            if (selectEntry != null)
            {
                return true;
            }
            else
            {
                throw new EventServiceException("400", "FC코드를 다시 한번 확인해주세요", financialConsultantCode);
            }
        }
        #endregion



        /// ##################################################################################################################

        #region depulicateOriginCustomer
        public Boolean depulicateOriginCustomer(string mobile)
        {
            var selectEntry = fcCustomerOriginRepository.SingleOrDefault(x => x.Mobile.Equals(mobile));
            if (selectEntry != null)
            {
                return false;                
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region limitOriginCustomer
        public bool limitOriginCustomer()
        {
            if (4001 > fcCustomerOriginRepository.GetAll().Count())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region existOriginCustomer
        public FinancialConsultantOriginalCustomerEntry existOriginCustomer(FinancialConsultantOriginalCustomerEntry financialConsultantOriginalCustomerEntry)
        {
            return fcCustomerOriginRepository.FirstOrDefault(x =>
               x.FcCode.Equals(financialConsultantOriginalCustomerEntry.FcCode) &&
               x.CustomerRandomNum.Equals(financialConsultantOriginalCustomerEntry.CustomerRandomNum)
           );
        }
        #endregion

        #region existOriginCustomer
        public FinancialConsultantOriginalCustomerEntry existOriginCustomer(FinancialConsultantNewCustomerEntry financialConsultantNewCustomerEntry)
        {
            return fcCustomerOriginRepository.SingleOrDefault(x => x.Mobile.Equals(financialConsultantNewCustomerEntry.Mobile));
        }
        #endregion

        #region saveOriginEntry
        public FinancialConsultantOriginalCustomerEntry saveOriginEntry(FinancialConsultantOriginalCustomerEntry financialConsultantOriginalEntry)
        {
            var selectEntry = fcCustomerOriginRepository.SingleOrDefault(x => x.Mobile.Equals(financialConsultantOriginalEntry.Mobile));
            if (selectEntry != null)
            {
                throw new EventServiceException("400", "이미 참여하셨습니다.", financialConsultantOriginalEntry);
            }
            var result = fcCustomerOriginRepository.Add(financialConsultantOriginalEntry);
            fcCustomerOriginRepository.Save();

            return result;
        }
        #endregion

        #region updateOriginEntry
        public void updateOriginEntry(FinancialConsultantOriginalCustomerEntry financialConsultantOriginalEntry)
        {
            var selectEntry = fcCustomerOriginRepository.SingleOrDefault(x => x.Id.Equals(financialConsultantOriginalEntry.Id));
            if (selectEntry != null)
            {
                fcCustomerOriginRepository.Update(financialConsultantOriginalEntry);
                fcCustomerOriginRepository.Save();
            }
            fcCustomerOriginRepository.Update(financialConsultantOriginalEntry);
            fcCustomerOriginRepository.Save();
        }
        #endregion



        /// ##################################################################################################################

        #region saveNewEntry
        public FinancialConsultantNewCustomerEntry saveNewEntry(FinancialConsultantNewCustomerEntry financialConsultantNewEntry)
        {
            var selectEntry = fcCustomerNewRepository.SingleOrDefault(x => x.Mobile.Equals(financialConsultantNewEntry.Mobile));
            if (selectEntry != null)
            {
                throw new EventServiceException("400", "이미 참여하셨습니다.", financialConsultantNewEntry);
            }

            var result = fcCustomerNewRepository.Add(financialConsultantNewEntry);
            fcCustomerNewRepository.Save();

            return result;
        }
        #endregion

        #region updateNewEntry
        public void updateNewEntry(FinancialConsultantNewCustomerEntry financialConsultantNewEntry)
        {
            var selectEntry = fcCustomerNewRepository.SingleOrDefault(x => x.Id.Equals(financialConsultantNewEntry.Id));
            if (selectEntry != null)
            {
                fcCustomerNewRepository.Update(financialConsultantNewEntry);
                fcCustomerNewRepository.Save();
            }

            fcCustomerNewRepository.Update(financialConsultantNewEntry);
            fcCustomerNewRepository.Save();
        }
        #endregion

        #region depulicateNewCustomer
        public bool depulicateNewCustomer(string mobile)
        {
            var selectEntry = fcCustomerNewRepository.SingleOrDefault(x => x.Mobile.Equals(mobile));
            if (selectEntry != null)
            {
                return false;                
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region limitNewCustomer
        public bool limitNewCustomer()
        {
            if (3263 > fcCustomerNewRepository.GetAll().Count())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion  


        /// ##################################################################################################################                
        public IQueryable<OriginalStatusModel> GetAdminFinancialConsultantOriginalEntryList()
        {
            var subQuery = (
                                from m in fcCustomerOriginRepository.GetAll()
                                join s in fcCustomerNewRepository.GetAll()
                                on m.CustomerRandomNum equals s.OriginCustomerRandomNum                                
                                where
                                    m.FcCode == s.FcCode
                                group m by new { m.CustomerRandomNum, m.FcCode } into t
                                select new
                                {
                                    CustomerRandomNum = t.Key.CustomerRandomNum,
                                    FcCode = t.Key.FcCode,                                    
                                    Cnt = t.Count()
                                }
                            );


            var query = (
                           from master in fcCustomerOriginRepository.GetAll()
                           join sub in subQuery
                           on new {A = master.CustomerRandomNum, B = master.FcCode } equals new { A = sub.CustomerRandomNum, B = sub.FcCode } into gj
                           from test in gj.DefaultIfEmpty()
                           select new OriginalStatusModel
                           {
                               Id = master.Id,
                               Cnt = test == null ? 0 : test.Cnt,
                               CreateDate = master.CreateDate,
                               Channel = master.Channel,
                               IpAddress = master.IpAddress,
                               Name = master.Name,
                               Mobile = master.Mobile,
                               Gender = master.Gender,
                               BirthDay = master.BirthDay,
                               PhoneCheck = master.PhoneCheck,
                               MessageCheck = master.MessageCheck,
                               PostCheck = master.PostCheck,
                               EmailCheck = master.EmailCheck,
                               RetentionPeriodOriginType = master.RetentionPeriodOriginType,
                               Email = master.Email,
                               ZipCode = master.ZipCode,
                               Address = master.Address,
                               AddressDetail = master.AddressDetail,
                               FcCode = master.FcCode,
                               CustomerRandomNum = master.CustomerRandomNum,
                               IsMessage = master.IsMessage,
                           }
                       );
            return query.OrderByDescending(x => x.Id);            
        }

        public IQueryable<FinancialConsultantNewCustomerEntry> GetAdminFinancialConsultantNewEntryList()
        {
            return fcCustomerNewRepository.GetAll().OrderByDescending(x => x.Id);
        }

        public string getUrlToOriginCustomerByPhone(string phone)
        {
            var entry = fcCustomerOriginRepository.SingleOrDefault(x => x.Mobile.Equals(phone));
            if(entry == null)
            {
                throw new EventServiceException("400", "이벤트 참여 후 공유해주세요.", null);
            }
            return "fc-sharing-new?rid=" + entry.CustomerRandomNum + "&fc=" + entry.FcCode;                
        }

        public FinancialConsultantNewCustomerEntry existNewCustomer(string mobile)
        {
            return fcCustomerNewRepository.SingleOrDefault(x => x.Mobile.Equals(mobile));
        }
    }
}
