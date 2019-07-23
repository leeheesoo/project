using KinderJoy.Domain.Entities.ChildrensDay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Repository.ChildrensDay {
    public interface IChildrensDayRepository {
        /// <summary>
        /// 개인정보저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        ChildrensDayHiddenPicture CreateChildrensDayHiddenPicture(ChildrensDayHiddenPicture entry);

        /// <summary>
        /// Sns 공유저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        void CreateChildrensDayHiddenPictureSns(ChildrensDayHiddenPictureSns entry);

        IQueryable<ChildrensDayHiddenPicture> ChildrensDayHiddenPicture { get; }
        IQueryable<ChildrensDayHiddenPictureSns> ChildrensDayHiddenPictureSns { get; }

    }
}
