using Samsonite.Mall.Domain.Entities.BagtotheFuture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsonite.Mall.Domain.Services.BagtotheFuture {
    public interface IBagtotheFutureService {
        #region create
        BagtotheFutureEntry CreateBagtotheFutureEntry(BagtotheFutureEntry entity);
        BagtotheFutureSnsUser CreateBagtotheFutureSnsUser(BagtotheFutureSnsUser entity);
        BagtotheFutureSns CreateBagtotheFutureSns(BagtotheFutureSns entity);
        #endregion

        #region read
        BagtotheFutureEntry GetBagtotheFutureEntryById(long id);
        IList<BagtotheFutureEntry> GetBagtotheFutureEntries();
        BagtotheFutureSnsUser GetBagtotheFutureSnsUserById(long id);
        IList<BagtotheFutureSnsUser> GetBagtotheFutureSnsUsers();
        IList<BagtotheFutureSns> GetBagtotheFutureSnsByUserId(long userId);
        IList<BagtotheFutureSns> GetBagtotheFutureSns();
        #endregion

        #region update
        #endregion

        #region delete
        #endregion
    }
}
