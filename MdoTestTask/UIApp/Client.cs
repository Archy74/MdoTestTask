using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIApp
{
    public partial class Client : Form, IUAppView
    {
        private string fileName;
        UAppPresenter uAppPresenter;
        public Client()
        {
            InitializeComponent();
            uAppPresenter = new UAppPresenter(this, new UAppModel(Properties.Settings.Default.ConnectionString));
        }

        private void LoadPictureMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "jpeg files (*.jpg)|*.jpg";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;

                }
                if (fileName != null)
                {
                    uAppPresenter.SavePicture(fileName);
                }
            }

        }

        public void ViewPictureThumbnail(Picture picture)
        {
            pictureBox1.Image = picture.GetThumbnail(200, 250);
        }

        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message);
        }
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            uAppPresenter.TransferPicture();
        }

        private void BtnShowPicture_Click(object sender, EventArgs e)
        {
            uAppPresenter.ViewPicture();
        }
    }
}
