using Data;
using System;
using System.Drawing;
using System.IO;
using UIApp.TransferService;

namespace UIApp
{
    class UAppPresenter
    {
        private readonly IUAppView uAppView;
        private readonly IUAppModel uAppModel;
        private readonly ReciverClient reciverClient;
        private Picture lastPicture;
        public UAppPresenter(IUAppView uAppView, IUAppModel uAppModel)
        {
            this.uAppModel = uAppModel;
            this.uAppView = uAppView;
            reciverClient = new ReciverClient();
            reciverClient.ReceiveMsg += ReciverClient_ReceiveMsg; 
        }

        private void ReciverClient_ReceiveMsg(string message)
        {
            uAppView.ShowMessage(message);
        }

        public void SavePicture(string fileName)
        {
            try
            {
                Picture picture = new Picture()
                {
                    Name = Path.GetFileName(fileName),
                    Format = Path.GetExtension(fileName),
                    Data = Picture.ToByte(Image.FromFile(fileName))
                };
                uAppModel.SavePicture(picture);
            }
            catch (Exception ex)
            {
                uAppView.ShowErrorMessage(ex.Message);
            }
        }

        public void ViewPicture()
        {
            try
            {
                var newPicture = uAppModel.GetPicture();
                if (lastPicture == null)
                {
                    lastPicture = newPicture;
                    uAppView.ViewPictureThumbnail(lastPicture);
                }
                else if (lastPicture.id != newPicture.id)
                {
                    lastPicture = newPicture;
                    uAppView.ViewPictureThumbnail(lastPicture);
                }
                else uAppView.ShowErrorMessage("Картинка та же самая");                
            }
            catch(Exception ex)
            {
                uAppView.ShowErrorMessage(ex.Message);
            }
        }

        public void TransferPicture()
        {
            try
            {
                if (lastPicture != null)
                    reciverClient.SavePicture(lastPicture);
                else uAppView.ShowErrorMessage("Нечего отправлять");
            }
            catch(Exception ex)
            {
                uAppView.ShowErrorMessage(ex.Message);
            }

        }
    }
}
