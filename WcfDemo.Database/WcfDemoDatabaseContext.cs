namespace WcfDemo.Database
{
    using System.Data.Entity;

    public class WcfDemoDatabaseContext : DbContext
    {
        public WcfDemoDatabaseContext()
            : base("name=WcfDemoDatabaseContext")
        {
        }

        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<MessageRequestModel> MessageRequests { get; set; }
        public DbSet<MessageResponseModel> MessageResponses { get; set; }
        public DbSet<ContactTypeModel> ContactTypes { get; set; }
        public DbSet<LegalFormModel> LegalForms { get; set; }
    }
}