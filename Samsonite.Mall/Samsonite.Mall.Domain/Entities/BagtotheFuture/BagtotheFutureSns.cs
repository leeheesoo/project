using Samsonite.Mall.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsonite.Mall.Domain.Entities.BagtotheFuture {
    /* 백투더퓨처 sns 공유 */
    public class BagtotheFutureSns : EventSnsSharing<BagtotheFutureSnsUser> {
    }
}
