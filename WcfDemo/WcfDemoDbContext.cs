namespace WcfDemo
{
    using System.Data.Entity;

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
    }
}