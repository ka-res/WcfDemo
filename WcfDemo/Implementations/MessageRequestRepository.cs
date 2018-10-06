namespace WcfDemo
{
    public class MessageRequestRepository : IMessageRequestRepository
    {
        private readonly IWcfDemoDbContext _wcfDemoDbContext;

        public MessageRequestRepository(IWcfDemoDbContext wcfDemoDatabaseContext)
        {
            _wcfDemoDbContext = wcfDemoDatabaseContext;
        }

        public void Add(MessageRequestModel messageRequestModel)
        {
            _wcfDemoDbContext.Set<MessageRequestModel>().Add(messageRequestModel);
            _wcfDemoDbContext.SaveChanges();
        }
    }
}