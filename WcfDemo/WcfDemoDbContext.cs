namespace WcfDemo
{
    using System.Data.Entity;

    public class WcfDemoDbContext : DbContext, IWcfDemoDbContext
    {
        public WcfDemoDbContext()
            : base("name=WcfDemoDbContext")
        {

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<WcfDemoDbContext, Configuration>());
            Database.SetInitializer(new CreateDatabaseIfNotExists<WcfDemoDbContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<WcfDemoDbContext>());
        }

        public virtual DbSet<ContactModel> Contacts { get; set; }
        public virtual DbSet<MessageRequestModel> MessageRequests { get; set; }
        public virtual DbSet<MessageResponseModel> MessageResponses { get; set; }
        public virtual DbSet<ContactTypeModel> ContactTypes { get; set; }
        public virtual DbSet<LegalFormModel> LegalForms { get; set; }

        IDbSet<TEntity> IWcfDemoDbContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }
    }
}