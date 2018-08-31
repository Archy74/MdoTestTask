using Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Transfer;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(TransferService)))
            {
                host.Open();
                TransferService.SendPicture += TransferService_SendPicture;
                Console.WriteLine("Server satrted...");
                Console.ReadKey();
            }
        }

        private static void TransferService_SendPicture(Picture picture, object sender, IReceiveTransferService receive)
        {
            try
            {
                string Path = GetPathFolder();
                Path = Path.TrimEnd('\\');
                System.IO.Directory.CreateDirectory(Path);
                using (System.IO.FileStream fs = new System.IO.FileStream(GetPathFolder() + "\\" + GetRandomNameFile(), FileMode.OpenOrCreate))
                {
                    var codeFile = Convert.ToBase64String(picture.Data);

                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(codeFile);
                }
                Console.WriteLine("Изображение '{0}' сохранено", picture.Name);
                receive?.ReceiveMessage("Файл сохранен в папку: " + Path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static string GetRandomNameFile()
        {
            Random rand = new Random();
            int num_letters = rand.Next(10, 20);

            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

            StringBuilder Name = new StringBuilder("");
            for (int j = 1; j <= num_letters; j++)
            {
                int letter_num = rand.Next(0, letters.Length - 1);
                Name.Append(letters[letter_num]);
            }
            return Name.ToString();
        }

        private static string GetPathFolder()
        {
            string config = Properties.Settings1.Default.PathFolder;
            string[] path = config.Split(';');
            
            Random rand = new Random();
            return path[rand.Next(0, path.Count() - 1)];
        }
    }
}
