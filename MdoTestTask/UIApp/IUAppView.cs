using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIApp
{
    interface IUAppView
    {
        void ViewPictureThumbnail(Picture picture);
        void ShowErrorMessage(string message);
        void ShowMessage(string message);
    }
}
