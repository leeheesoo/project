using INGLife.Event.Domain.Entities.FinancialConcertMarketingAgree;
using INGLife.Event.Domain.Entities.FinancialConsultantSharing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static INGLife.Event.Domain.Services.FinancialConsultantSharing.FinancialConsultantSharingService;

namespace INGLife.Event.Domain.Services.FinancialConsultantSharing
{
    public interface IFinancialConsultantSharingService
    {

        /// <summary>
        /// FC 존재여부 조회
        /// </summary>
        Boolean isExistFC(string financialConsultantCode);


        /// ##################################################################################################################
        /// <summary>
        /// 기존 고객 중복 확인
        /// </summary>
        Boolean depulicateOriginCustomer(string mobile);
        /// <summary>
        /// 기존 고객 4000명 참여 제한
        /// </summary>
        Boolean limitOriginCustomer();
        /// <summary>
        /// 기존 고객 조회 -  기존고객 정보로 기존고객 조회
        /// </summary>
        string getUrlToOriginCustomerByPhone(string phone);
        /// <summary>
        /// 기존 고객 조회 -  기존고객 정보로 기존고객 조회
        /// </summary>
        FinancialConsultantOriginalCustomerEntry existOriginCustomer(FinancialConsultantOriginalCustomerEntry financialConsultantOriginalCustomerEntry);
        /// <summary>
        /// 기존 고객 조회 -  신규고객정보로 기존고객 조회
        /// </summary>
        FinancialConsultantOriginalCustomerEntry existOriginCustomer(FinancialConsultantNewCustomerEntry financialConsultantNewCustomerEntry);
        /// <summary>
        /// 기존 고객 조회 -  기존고객 정보로 신규고객 조회
        /// </summary>
        FinancialConsultantNewCustomerEntry existNewCustomer(string mobile);
        /// <summary>
        /// 기존고객 데이터 저장
        /// </summary>
        FinancialConsultantOriginalCustomerEntry saveOriginEntry(FinancialConsultantOriginalCustomerEntry financialConsultantOriginalEntry);
        /// <summary>
        /// 기존고객 데이터 업데이트
        /// </summary>
        void updateOriginEntry(FinancialConsultantOriginalCustomerEntry financialConsultantOriginalEntry);


        /// ##################################################################################################################
        /// <summary>
        /// 신규고객 데이터 저장
        /// </summary>
        FinancialConsultantNewCustomerEntry saveNewEntry(FinancialConsultantNewCustomerEntry financialConsultantNewEntry);
        /// <summary>
        /// 신규고객 데이터 업데이트
        /// </summary>
        void updateNewEntry(FinancialConsultantNewCustomerEntry financialConsultantNewEntry);
        /// <summary>
        /// 중복 신규 중복 확인
        /// </summary>
        Boolean depulicateNewCustomer(string mobile);        
        /// <summary>
        /// 신규 고객 4000명 참여 제한
        /// </summary>
        Boolean limitNewCustomer();


        /// ##################################################################################################################                
        IQueryable<OriginalStatusModel> GetAdminFinancialConsultantOriginalEntryList();
        IQueryable<FinancialConsultantNewCustomerEntry> GetAdminFinancialConsultantNewEntryList();

    }
}
