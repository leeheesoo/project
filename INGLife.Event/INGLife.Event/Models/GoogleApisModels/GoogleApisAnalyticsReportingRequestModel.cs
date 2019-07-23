using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INGLife.Event.Models.GoogleApisModels {
    /// <summary>
    /// GA(PV/UV) 관리 페이지
    /// </summary>
    public class GoogleApisAnalyticsReportingRequestModel {
        public GoogleApisAnalyticsReportingRequestModel() {
            this.StartDate = DateTime.Now.AddDays(-10);
            this.EndDate = DateTime.Now;
        }
        /// <summary>
        /// 시작일
        /// </summary>
        [Required(ErrorMessage = "기간 필터를 선택해주세요. (시작일)")]
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 종료일
        /// </summary>
        [Required(ErrorMessage = "기간 필터를 선택해주세요. (종료일)")]
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 확인할 페이지 Path
        /// </summary>
        [Required(ErrorMessage = "확인할 이벤트를 선택해주세요.")]
        public string PagePath { get; set; }
    }
}