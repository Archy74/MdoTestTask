using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIApp
{
    interface IUAppModel
    {
        void SavePicture(Picture picture);
        Picture GetPicture();
    }
}
