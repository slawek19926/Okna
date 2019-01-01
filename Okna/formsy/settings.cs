using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Npgsql;
using FirebirdSql.Data.FirebirdClient;

namespace Okna.formsy
{
    public partial class settings : Form
    {
        public settings(Form1 Form1)
        {
            InitializeComponent();
            WindowState = FormWindowState.Normal;
            Text = "Ustawienia";
            haslo.PasswordChar = '*';
            passP.PasswordChar = '*';
            fbPass.PasswordChar = '*';
        }
        private string server;
        private string database;
        private string uid;
        private string password;
        private string port;
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }
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
        private void settings_Load(object sender, EventArgs e)
        {
            var MyIni = new INIFile("WektorSettings.ini");
            serwer.Text = MyIni.Read("server", "Okna");
            login.Text = MyIni.Read("login", "Okna");
            haslo.Text = Decrypt(MyIni.Read("pass", "Okna"));
            baza.Text = MyIni.Read("database", "Okna");
            zera.Text = MyIni.Read("zera", "wyceny");
            przedTXT.Text = MyIni.Read("przed", "wyceny");
            servP.Text = MyIni.Read("server", "postgres");
            portP.Text = MyIni.Read("port", "postgres");
            userP.Text = MyIni.Read("user", "postgres");
            passP.Text = Decrypt(MyIni.Read("pass", "postgres"));
            dbP.Text = MyIni.Read("db", "postgres");
            przed2TXT.Text = MyIni.Read("przed", "faktury");
            zera2.Text = MyIni.Read("zera", "faktury");
            numerFV.Value = Convert.ToDecimal(MyIni.Read("numer", "faktury"));
            fbServer.Text = MyIni.Read("server", "firebird");
            fbPath.Text = MyIni.Read("dblocation", "firebird");
            fbUser.Text = MyIni.Read("user", "firebird");
            fbPass.Text = MyIni.Read("pass", "firebird");
        }

        private void testCONN_Click(object sender, EventArgs e)
        {
            server = serwer.Text;
            database = baza.Text;
            uid = login.Text;
            password = haslo.Text;
            try
            {
                var connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT @@VERSION";
                        var result = (string)command.ExecuteScalar();
                        MessageBox.Show("Połączenie przebiegło pomyślnie","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połącznia z bazą danych","Błąd",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        
        private void saveBTN_Click(object sender, EventArgs e)
        {
            const string REGISTRY_KEY1 = @"HKEY_CURRENT_USER\Software\WektorApp";
            const string REGISTY_VALUE1 = "server";
            Microsoft.Win32.Registry.SetValue(REGISTRY_KEY1, REGISTY_VALUE1, serwer.Text, Microsoft.Win32.RegistryValueKind.String);
            const string REGISTRY_KEY2 = @"HKEY_CURRENT_USER\Software\WektorApp";
            const string REGISTY_VALUE2 = "uid";
            Microsoft.Win32.Registry.SetValue(REGISTRY_KEY2, REGISTY_VALUE2, login.Text, Microsoft.Win32.RegistryValueKind.String);
            const string REGISTRY_KEY3 = @"HKEY_CURRENT_USER\Software\WektorApp";
            const string REGISTY_VALUE3 = "pass";
            Microsoft.Win32.Registry.SetValue(REGISTRY_KEY3, REGISTY_VALUE3, Encrypt(haslo.Text), Microsoft.Win32.RegistryValueKind.String);
            const string REGISTRY_KEY4 = @"HKEY_CURRENT_USER\Software\WektorApp";
            const string REGISTY_VALUE4 = "db";
            Microsoft.Win32.Registry.SetValue(REGISTRY_KEY4, REGISTY_VALUE4, baza.Text, Microsoft.Win32.RegistryValueKind.String);

            var MyIni = new INIFile("WektorSettings.ini");
            MyIni.Write("server", serwer.Text, "Okna");
            MyIni.Write("login", login.Text, "Okna");
            MyIni.Write("pass", Encrypt(haslo.Text), "Okna");
            MyIni.Write("database", baza.Text, "Okna");
            MessageBox.Show("Ustawienia zostały zapisane", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            settings_Load(e, e);
        }

        private void save2BTN_Click(object sender, EventArgs e)
        {
            var MyIni = new INIFile("WektorSettings.ini");
            MyIni.Write("zera", zera.Text, "wyceny");
            MyIni.Write("przed", przedTXT.Text, "wyceny");
            MessageBox.Show("Ustawienia zostały zapisane", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            settings_Load(e, e);
        }

        private void savePbtn_Click(object sender, EventArgs e)
        {
            var MyIni = new INIFile("WektorSettings.ini");
            MyIni.Write("server", servP.Text, "postgres");
            MyIni.Write("port", portP.Text, "postgres");
            MyIni.Write("user", userP.Text, "postgres");
            MyIni.Write("pass", Encrypt(passP.Text), "postgres");
            MyIni.Write("db", dbP.Text, "postgres");
            MessageBox.Show("Ustawienia zostały zapisane", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            settings_Load(e, e);
        }

        private void testPbtn_Click(object sender, EventArgs e)
        {
            server = servP.Text;
            database = dbP.Text;
            uid = userP.Text;
            password = passP.Text;
            port = portP.Text;
            try
            {
                var connectionString = String.Format("Server={0};Port={1};" +
                    "User Id={2};Password={3};Database={4};",
                    server, port, uid,
                    password, database);
                NpgsqlConnection conn = new NpgsqlConnection(connectionString);
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                    MessageBox.Show("Połączenie przebiegło pomyślnie", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połącznia z bazą danych", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void save3BTN_Click(object sender, EventArgs e)
        {
            var MyIni = new INIFile("WektorSettings.ini");
            MyIni.Write("zera", zera2.Text, "faktury");
            MyIni.Write("przed", przed2TXT.Text, "faktury");
            MyIni.Write("numer", numerFV.Value.ToString(), "faktury");
            MessageBox.Show("Ustawienia zostały zapisane", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            settings_Load(e, e);
        }

        private void saveFB_Click(object sender, EventArgs e)
        {
            var MyIni = new INIFile("WektorSettings.ini");
            MyIni.Write("server", fbServer.Text, "firebird");
            MyIni.Write("dblocation", fbPath.Text, "firebird");
            MyIni.Write("user", fbUser.Text, "firebird");
            MyIni.Write("pass", Encrypt(fbPass.Text), "firebird");
            MessageBox.Show("Ustawienia zaostały zapisane", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void testFB_Click(object sender, EventArgs e)
        {
            server = fbServer.Text;
            database = fbPath.Text;
            uid = fbUser.Text;
            password = fbPass.Text;
            try
            {
                var connectionString = String.Format("Datasource={0};Database={1};" +
                    "User={2};Password={3};",
                    server,database,uid,password);
                FbConnection conn = new FbConnection(connectionString);
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                    MessageBox.Show("Połączenie przebiegło pomyślnie", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połącznia z bazą danych", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
