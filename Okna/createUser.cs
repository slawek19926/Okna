using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.IO;

namespace Okna
{
    public partial class createUser : Form
    {
        private Form1 Form1;
        public createUser(Form1 Form1)
        {
            InitializeComponent();
            Text = "Tworzenie nowego użytkownika";
            password.PasswordChar = '*';
            password2.PasswordChar = '*';

            this.Form1 = Form1;
        }

        private string server;
        private string database;
        private string uid;
        private string pass;
        private string port;
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(password.Text == "" || password2.Text == "")
            {
                MessageBox.Show("Wprowadź hasło");
                password.Focus();
                return;
            }
            if (password.Text != password2.Text)
            {
                MessageBox.Show("Hasła do siebie nie pasują");
                password2.Focus();
                return;
            }

            var MyIni = new INIFile("WektorSettings.ini");
            server = MyIni.Read("server", "Okna");
            database = MyIni.Read("database", "Okna");
            uid = MyIni.Read("login", "Okna");
            pass = Decrypt(MyIni.Read("pass", "Okna"));

            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + pass + ";" + "Convert Zero Datetime = True;";
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                var command = conn.CreateCommand();
                command.CommandText = "INSERT INTO uzytkownicy (username, password,allowed) VALUES ('" + loginName.Text + "','" + password.Text + "','1')";
                conn.Open();
                command.ExecuteNonQuery();

                MessageBox.Show("Zapisanie danych było udane");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Zapisanie danych nie było udane");
            }
        }
    }
}