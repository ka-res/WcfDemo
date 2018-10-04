using System.ServiceModel;

namespace WcfDemo.Contracts
{
    [ServiceContract]
    public interface IMessageService
    {
        [OperationContract]
        MessageResponse Send(MessageRequest message);
    }
}
