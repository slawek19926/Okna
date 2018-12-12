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
using MaterialSkin;
using MaterialSkin.Controls;
using MetroFramework;
using AutoUpdaterDotNET;

namespace Okna
{
    public partial class frmLogin : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string path = AppDomain.CurrentDomain.BaseDirectory + "\\logged.ext";
        public frmLogin()
        {
            InitializeComponent();

            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

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

        private bool Zalogowany(string user, string pass)
        {
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from uzytkownicy where username=@user and password=MD5(@pass)";
            cmd.Parameters.AddWithValue("@user", txtLogin.Text);
            cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
            cmd.Connection = connect;
            MySqlDataReader zalogowany = cmd.ExecuteReader();
            if (zalogowany.Read())
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

        private void zapis_login()
        {
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Update uzytkownicy set logged='1' WHERE username=@user and password=MD5(@pass)";
            cmd.Parameters.AddWithValue("@user", txtLogin.Text);
            cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtLogin.Text;
            string pass = txtPassword.Text;
            if (user == "" || pass == "")
            {
                boxes.frmOK frm = new boxes.frmOK();
                frm.Show();
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
                        bool log = Zalogowany(user,pass);
                        if (log)
                        {
                            db_connection();
                            MySqlCommand cmd2 = new MySqlCommand();
                            cmd2.CommandText = "Select * from uzytkownicy where username=@user and password=MD5(@pass)";
                            cmd2.Parameters.AddWithValue("@user", txtLogin.Text);
                            cmd2.Parameters.AddWithValue("@pass", txtPassword.Text);
                            cmd2.Connection = connect;
                            MySqlDataReader login1 = cmd2.ExecuteReader();
                            while (login1.Read())
                            {
                                string jest = login1.GetString("logged");
                                if(jest == "1")
                                {
                                    //boxes.frmUser frm = new boxes.frmUser();
                                    //frm.Show();
                                }
                                else
                                {
                                    zapis_login();
                                    boxes.frmZalogowany frms = new boxes.frmZalogowany();
                                    frms.Show();

                                    var MyIni = new INIFile("WektorSettings.ini");
                                    MyIni.Write("user", user, "logged");
                                    Hide();
                                }
                            }
                        }
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
                MetroMessageBox.Show(this,"Nieprawidłowa nazwa użytkownika lub hasło","Błąd",MessageBoxButtons.OK,MessageBoxIcon.Error);
            } 
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = MetroMessageBox.Show(this,"Napewno chcesz zakończyć działanie programu?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                e.Cancel = false;
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            
        }
    }
}
