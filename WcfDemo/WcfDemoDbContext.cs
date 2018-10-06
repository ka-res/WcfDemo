using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace WcfDemo
{
    public interface IWcfDemoDbContext
    {
        Database Database { get; }
        DbEntityEntry Entry(object entity);
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    }

    public class WcfDemoDbContext : DbContext, IWcfDemoDbContext
    {
        public WcfDemoDbContext()
            : base("WcfDemoDbContext")
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists<WcfDemoDbContext>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<WcfDemoDbContext>());
        }

        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<MessageRequestModel> MessageRequests { get; set; }
        public DbSet<MessageResponseModel> MessageResponses { get; set; }
        public DbSet<ContactTypeModel> ContactTypes { get; set; }
        public DbSet<LegalFormModel> LegalForms { get; set; }
        public DbSet<ReturnCodeModel> ReturnCodes { get; set; }

        IDbSet<TEntity> IWcfDemoDbContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries<IBaseModel>();
            if (entities != null)
            {
                foreach (var entry in entities.Where(x => x.State != EntityState.Unchanged))
                {
                    entry.Entity.IsSoftDeleted = false;
                    entry.Entity.SaveDate = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }
}