using KinderJoy.Domain.Entities.ChildrensDay;
using KinderJoy.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Repository.ChildrensDay {
    public class ChildrensDayRepository : IChildrensDayRepository {

        private AppDbContext db;

        public IQueryable<ChildrensDayHiddenPicture> ChildrensDayHiddenPicture {
            get {
                return db.ChildrensDayHiddenPicture;
            }
        }

        public IQueryable<ChildrensDayHiddenPictureSns> ChildrensDayHiddenPictureSns {
            get {
                return db.ChildrensDayHiddenPictureSns;
            }
        }

        public ChildrensDayRepository(AppDbContext db) {
            this.db = db;
        }

        public ChildrensDayRepository() {
            db = AppDbContext.Create();
        }

        public ChildrensDayHiddenPicture CreateChildrensDayHiddenPicture(ChildrensDayHiddenPicture entry) {

            var result = db.ChildrensDayHiddenPicture.Add(entry);
            db.SaveChanges();

            return result;
        }

        public void CreateChildrensDayHiddenPictureSns(ChildrensDayHiddenPictureSns entry) {
            var result = db.ChildrensDayHiddenPictureSns.Add(entry);
            db.SaveChanges();
        }

    }
}
