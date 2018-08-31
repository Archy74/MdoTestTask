using Data;
using System.ServiceModel;

namespace Transfer
{
    [ServiceContract(CallbackContract = typeof(IReceiveTransferService))]
    interface ITransferService
    {
        [OperationContract(IsOneWay = true)]
        void SavePicture(Picture picture);
    }


    public interface IReceiveTransferService
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(string msg);
    }

}
