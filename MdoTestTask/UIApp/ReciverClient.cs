using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UIApp.TransferService;

namespace UIApp
{
    public delegate void ReceviedMessage(string message);

    class ReciverClient : ITransferServiceCallback
    {
        public event ReceviedMessage ReceiveMsg;

        private readonly InstanceContext inst = null;
        private TransferServiceClient transferServiceClient = null;

        public ReciverClient()
        {
            inst = new InstanceContext(this);
            transferServiceClient = new TransferService.TransferServiceClient(inst);
        }
        public void ReceiveMessage(string msg)
        {
            ReceiveMsg?.Invoke(msg);
        }

        public void SavePicture(Picture picture)
        {
            transferServiceClient.SavePicture(picture);
        }
    }
}
