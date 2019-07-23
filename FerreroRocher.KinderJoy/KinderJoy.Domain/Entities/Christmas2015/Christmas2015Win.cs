using KinderJoy.Domain.Abstract;
using KinderJoy.Domain.Abstract.Christmas2015;
using System.ComponentModel.DataAnnotations.Schema;

namespace KinderJoy.Domain.Entities.Christmas2015 {
    /// <summary>
    /// 2015년 크리스마스 이벤트
    /// 즉석당첨 이벤트
    /// </summary>
    public class Christmas2015Win : EventNaverSearching<Christmas2015EnumConst.Christmas2015WinPrize> {

    }
}
