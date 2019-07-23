using AutoMapper;
using Finnq.Promotion.Domain.Identity;
using NLog;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace Finnq.Promotion.Domain.Infrastructures {
    public abstract class RepositoryBase<TUser, TContext, TEntity> : IRepository<TEntity> where TUser : AppUser where TContext : AppDbContext where TEntity : class {
        Logger logger = LogManager.GetCurrentClassLogger();
        private object currentUser;
        private TContext db;
        private static IMapper _mapper = null;

        public RepositoryBase(TContext db, object currentUser) {
            this.db = db;
            this.currentUser = currentUser;
        }

        public TEntity Add(TEntity entity) {
            var dbSet = db.Set<TEntity>();
            var instance = db.Set<TEntity>().Create();
            if (instance.GetType().Equals(entity.GetType())) {
                instance = entity;
            } else {
                if (_mapper == null) {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap(entity.GetType(), instance.GetType());
                    });
                    _mapper = config.CreateMapper();
                }
                instance = _mapper.Map(entity, instance);
            }
            return db.Set<TEntity>().Add(instance);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate) {
            return db.Set<TEntity>().Where(predicate).Any();
        }

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate) {
            return db.Set<TEntity>().Where(predicate).AsQueryable();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate) {
            return db.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        public IQueryable<TEntity> GetAll() {
            return db.Set<TEntity>().AsQueryable();
        }

        public void Remove(TEntity entity) {
            db.Set<TEntity>().Remove(entity);
        }

        public void Save() {
            try {
                db.SaveChanges();
            } catch (DbEntityValidationException e) {
                foreach (var eve in e.EntityValidationErrors) {
                    logger.Error("Entity of type \"{0}\" in state \"{1}\" has the f  ollowing validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors) {
                        logger.Error("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate) {
            return db.Set<TEntity>().Where(predicate).SingleOrDefault();
        }

        public TEntity Update(TEntity entity) {
            var dbSet = db.Set<TEntity>();
            var instance = db.Set<TEntity>().Create();
            if (instance.GetType().Equals(entity.GetType())) {
                instance = entity;
            } else {
                if (_mapper == null) {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap(entity.GetType(), instance.GetType());
                    });
                    _mapper = config.CreateMapper();
                }
                instance = _mapper.Map(entity, instance);
            }
            db.Entry<TEntity>(instance).State = System.Data.Entity.EntityState.Modified;
            return instance;
        }
    }
}