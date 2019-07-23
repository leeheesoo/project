using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Abstract {
    public enum SharingType {
        [Display(Name = "페이스북")]
        Facebook = 1,
        [Display(Name = "트위터")]
        Twitter = 2,
        [Display(Name = "블로그")]
        Blog = 3,
        [Display(Name = "카카오스토리")]
        KakaoStory = 4,
        [Display(Name = "카카오톡")]
        Kakaotalk = 5
    }

    public enum ChannelType {
        [Display(Name = "PC")]
        PC = 1,
        [Display(Name = "Mobile")]
        Mobile = 2
    }
}
