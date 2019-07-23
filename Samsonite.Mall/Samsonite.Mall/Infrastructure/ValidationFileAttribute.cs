using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Samsonite.Mall.Infrastructure {
    public class ValidationFileAttribute: ValidationAttribute {
        public bool Required { get; set; }

        public long ContentLength { get; set; }
        public override bool IsValid(object value) {
            var file = value as HttpPostedFileBase;
            /* 필수여부 체크 */
            if(file == null) {
                if (!Required) {
                    return true;
                } else {
                    return false;
                }
            }
            // 용량 체크
            if (file.ContentLength > ContentLength) {
                return false;
            }
            return true;
        }
    }
}