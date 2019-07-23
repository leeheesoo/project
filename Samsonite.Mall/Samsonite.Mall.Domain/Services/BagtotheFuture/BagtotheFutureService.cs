using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Samsonite.Mall.Domain.Entities.BagtotheFuture;
using Samsonite.Mall.Domain.Infrastructures;
using Samsonite.Mall.Domain.Repositories.BagtotheFuture;
using Samsonite.Mall.Domain.Exceptions;

namespace Samsonite.Mall.Domain.Services.BagtotheFuture {
    public class BagtotheFutureService : IBagtotheFutureService {
        #region variables
        IBagtotheFutureEntryRepository bttfEntryRepo;
        IBagtotheFutureSnsUserRepository bttfSnsUserRepo;
        IBagtotheFutureSnsRepository bttfSnsRepo;
        #endregion

        #region constructor
        public BagtotheFutureService(IBagtotheFutureEntryRepository bttfEntryRepo, 
            IBagtotheFutureSnsUserRepository bttfSnsUserRepo, IBagtotheFutureSnsRepository bttfSnsRepo) {
            this.bttfEntryRepo = bttfEntryRepo;
            this.bttfSnsUserRepo = bttfSnsUserRepo;
            this.bttfSnsRepo = bttfSnsRepo;
        }
        #endregion


        #region create
        public BagtotheFutureEntry CreateBagtotheFutureEntry(BagtotheFutureEntry entity) {
            bttfEntryRepo.Add(entity);
            bttfEntryRepo.Save();
            return entity;
        }
        public BagtotheFutureSnsUser CreateBagtotheFutureSnsUser(BagtotheFutureSnsUser entity) {
            bttfSnsUserRepo.Add(entity);
            bttfSnsUserRepo.Save();
            return entity;
        }

        public BagtotheFutureSns CreateBagtotheFutureSns(BagtotheFutureSns entity) {
            if(this.GetBagtotheFutureSnsUserById(entity.UserId) == null) {
                throw new BagtotheFutureServiceException("400", "sns공유 절차가 잘못되었습니다. 처음부터 정상적인 방법으로 참여해주시길 바랍니다.", entity);
            }
            bttfSnsRepo.Add(entity);
            bttfSnsRepo.Save();
            return entity;
        }
        #endregion

        #region read
        public IList<BagtotheFutureEntry> GetBagtotheFutureEntries() {
            return bttfEntryRepo.GetAll().ToList();
        }
        public BagtotheFutureEntry GetBagtotheFutureEntryById(long id) {
            return bttfEntryRepo.SingleOrDefault(x => x.Id == id);
        }


        public IList<BagtotheFutureSnsUser> GetBagtotheFutureSnsUsers() {
            return bttfSnsUserRepo.GetAll().ToList();
        }

        public BagtotheFutureSnsUser GetBagtotheFutureSnsUserById(long id) {
            return bttfSnsUserRepo.SingleOrDefault(x => x.Id == id);
        }

        public IList<BagtotheFutureSns> GetBagtotheFutureSns() {
            return bttfSnsRepo.GetAll().ToList();
        }

        public IList<BagtotheFutureSns> GetBagtotheFutureSnsByUserId(long userId) {
            return bttfSnsRepo.Filter(x => x.UserId == userId).ToList();
        }
        #endregion

        #region update
        #endregion

        #region delete
        #endregion
    }
    public class BagtotheFutureServiceException : EventServiceException {
        public BagtotheFutureServiceException(string code, string message, object data) 
            : base(code, message, data) {
        }
    }
}
