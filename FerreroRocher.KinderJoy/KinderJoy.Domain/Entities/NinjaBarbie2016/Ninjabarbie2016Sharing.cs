using KinderJoy.Domain.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KinderJoy.Domain.Entities.NinjaBarbie2016 {
    /// <summary>
    /// 닌자바비 2016 이벤트 SNS공유
    /// </summary>
    public class NinjaBarbie2016Sharing : EventSnsSharing<NinjaBarbie2016User> {
    }
}
