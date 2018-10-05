using System.Data.Entity;

namespace WcfDemo.Database
{
    public class WcfDemoDbContext : DbContext
    {
        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<MessageRequestModel> MessageRequests { get; set; }
        public DbSet<MessageResponseModel> MessageResponses { get; set; }
    }
}
