using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using MySql.Data.MySqlClient;
using System.Xml;

namespace Okna
{
    public partial class frmLogin : Form
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + "\\logged.ext";
        public frmLogin()
        {
            InitializeComponent();
            Text = "Logowanie";
            txtPassword.PasswordChar = '*';

            if (IsKeyLocked(Keys.CapsLock))
            {
                MessageBox.Show("Caps lock jest włączony");
            }
        }

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        BindingSource source1 = new BindingSource();
        private string server;
        private string database;
        private string uid;
        private string password;
        private string connectionString;
        private MySqlConnection connect;
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

        private void db_connection()
        {
            try
            {
                var MyIni = new INIFile("WektorSettings.ini");
                server = MyIni.Read("server", "Okna");
                database = MyIni.Read("database", "Okna");
                uid = MyIni.Read("login", "Okna");
                password = Decrypt(MyIni.Read("pass", "Okna"));

                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "Convert Zero Datetime = True;";
                connect = new MySqlConnection(connectionString);
                connect.Open();
            }
            catch (MySqlException ex)
            {
                throw;
            }
        }

        private bool validate_login(string user, string pass)
        {
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from uzytkownicy where username=@user and password=MD5(@pass)";
            cmd.Parameters.AddWithValue("@user", txtLogin.Text);
            cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
            cmd.Connection = connect;
            MySqlDataReader login = cmd.ExecuteReader();
            if (login.Read())
            {
                connect.Close();
                return true;
            }
            else
            {
                connect.Close();
                return false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtLogin.Text;
            string pass = txtPassword.Text;
            if (user == "" || pass == "")
            {
                MessageBox.Show("Podaj nazwę użytkownika i hasło!");
                return;
            }
            bool r = validate_login(user, pass);
            if (r)
            {
                db_connection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "Select * from uzytkownicy where username=@user and password=MD5(@pass)";
                cmd.Parameters.AddWithValue("@user", txtLogin.Text);
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
                cmd.Connection = connect;
                MySqlDataReader login = cmd.ExecuteReader();

                while (login.Read())
                {
                    string l = login.GetString("allowed");

                    if (l == "1")
                    {
                        MessageBox.Show("Zalogowano pomyślnie","Sukces",MessageBoxButtons.OK,MessageBoxIcon.Information);

                        var MyIni = new INIFile("WektorSettings.ini");
                        MyIni.Write("user", user, "logged");
                        
                        Close();
                    }
                    else
                    {
                        logedError frm = new logedError();
                        frm.Show();
                    }
                }   
            }
            else
            {
                MessageBox.Show("Nieprawidłowa nazwa użytkownika lub hasło","Błąd",MessageBoxButtons.OK,MessageBoxIcon.Error);
            } 
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            string user = txtLogin.Text;
            string pass = txtPassword.Text;
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from uzytkownicy where username=@user and password=MD5(@pass)";
            cmd.Parameters.AddWithValue("@user", txtLogin.Text);
            cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
            cmd.Connection = connect;
            MySqlDataReader login = cmd.ExecuteReader();

            while (login.Read())
            {
                string l = login.GetString("allowed");

                if (l == "0")
                {
                    Application.Exit();
                }
            }
            if (user == "" || pass == "")
            {
                Application.Exit();
            }
        }
    }
}
