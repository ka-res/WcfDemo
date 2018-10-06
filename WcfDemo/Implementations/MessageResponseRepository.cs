namespace WcfDemo
{
    public class MessageResponseRepository : IMessageResponseRepository
    {
        private readonly IWcfDemoDbContext _wcfDemoDbContext;

        public MessageResponseRepository(IWcfDemoDbContext wcfDemoDatabaseContext)
        {
            _wcfDemoDbContext = wcfDemoDatabaseContext;
        }

        public void Add(MessageResponseModel messageResponseModel)
        {
            _wcfDemoDbContext.Set<MessageResponseModel>().Add(messageResponseModel);
            _wcfDemoDbContext.SaveChanges();
        }
    }
}