namespace WcfDemo
{
    public class ContactRepository : IContactRepository
    {
        private readonly IWcfDemoDbContext _wcfDemoDbContext;

        public ContactRepository(IWcfDemoDbContext wcfDemoDatabaseContext)
        {
            _wcfDemoDbContext = wcfDemoDatabaseContext;
        }

        public void Add(ContactModel contactModel)
        {
            _wcfDemoDbContext.Set<ContactModel>().Add(contactModel);
            _wcfDemoDbContext.SaveChanges();
        }
    }
}