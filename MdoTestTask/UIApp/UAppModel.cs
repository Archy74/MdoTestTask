using Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIApp
{
    class UAppModel : IUAppModel
    {
        private readonly string connectionString;

        public UAppModel(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public Picture GetPicture()
        {
            Picture picture = new Picture();
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.ConnectionString)) // строка подключения к БД
            {               
                string commandText = "select top 1 [id] ,[Image] ,[Name] ,[Format] from [Images] Order by newid()"; // запрос на вставку
                SqlCommand command = new SqlCommand(commandText, sqlConnection);
                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                picture.id = reader.GetInt32(0);
                picture.Data = (byte[])reader.GetValue(1);
                picture.Name = reader.GetString(2);
                picture.Format = reader.GetString(3);
                sqlConnection.Close();
            }
            return picture;
        }

        public void SavePicture(Picture picture)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.ConnectionString)) // строка подключения к БД
            {
                string commandText = "INSERT INTO Images ([Image], [Format], [Name]) VALUES(@Image, @Format, @Name)"; // запрос на вставку
                SqlCommand command = new SqlCommand(commandText, sqlConnection);
                command.Parameters.AddWithValue("@Image", picture.Data); // записываем само изображение
                command.Parameters.AddWithValue("@Format", picture.Format); // записываем расширение изображения
                command.Parameters.AddWithValue("@Name", picture.Name); // записываем название файла
                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
    }
}
