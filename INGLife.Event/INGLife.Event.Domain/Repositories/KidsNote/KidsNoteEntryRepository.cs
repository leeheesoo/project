﻿using INGLife.Event.Domain.Entities.KidsNote;
using INGLife.Event.Domain.Identity;
using INGLife.Event.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Repositories.KidsNote {
    public class KidsNoteEntryRepository : RepositoryBase<AppUser, AppDbContext, KidsNoteEntry>, IKidsNoteEntryRepository {
        public KidsNoteEntryRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
