using INGLife.Event.Domain.Entities.KidsNote;
using INGLife.Event.Domain.Exceptions;
using INGLife.Event.Domain.Repositories.KidsNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INGLife.Event.Domain.Entities.MamboEvent;

namespace INGLife.Event.Domain.Services.Nilririmambo
{
    public class NilririmamboService : INilririmamboService {
        private INilririmamboEntryRepository repo;

        public NilririmamboService(INilririmamboEntryRepository repo) {
            this.repo = repo;
        }

        public NilririmanboEntry CreateNilririmamboEntry(NilririmanboEntry entry)
        {
            var isEntry = repo.Any(x => x.FcCode == entry.FcCode);
            if (isEntry)
            {
                throw new NilririmamboServiceException("400", "이미 등록된 fc코드입니다.", entry);
            }
            var result = repo.Add(entry);
            repo.Save();
            return result;
        }

        IQueryable<NilririmanboEntry> INilririmamboService.GetAdminNilririmamboEntryList()
        {
           return repo.GetAll().OrderByDescending(x => x.CreateDate);
        }
    }
    public class NilririmamboServiceException : EventServiceException {
        public NilririmamboServiceException(string code, string message, object data)
            : base(code, message, data) {
        }
    }
}
