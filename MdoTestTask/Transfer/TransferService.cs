using Data;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Transfer
{
    public delegate void MessageFromClient(Picture picture, object sender, IReceiveTransferService receive);

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TransferService : ITransferService
    {
        Dictionary<string, IReceiveTransferService> names = new Dictionary<string, IReceiveTransferService>();
        IReceiveTransferService callback = null;

        public static event MessageFromClient SendPicture;
        public void SavePicture(Picture picture)
        {
            callback = OperationContext.Current.GetCallbackChannel<IReceiveTransferService>();
            SendPicture?.Invoke(picture, this, callback);
        }
    }
}
