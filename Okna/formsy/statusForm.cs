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
using System.IO;
using System.Security.Cryptography;

namespace Okna.formsy
{
    public partial class statusForm : Form
    {
        private zamowienia zamowienia;
        private Form1 Form1;
        public statusForm(zamowienia zamowienia,Form1 Form1)
        {
            InitializeComponent();
            this.zamowienia = zamowienia;
            this.Form1 = Form1;
            Text = "Status zamówienia";
        }
        private string server;
        private string database;
        private string uid;
        private string password;
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
        private void statusForm_Load(object sender, EventArgs e)
        {
            var MyIni = new INIFile("WektorSettings.ini");
            server = MyIni.Read("server", "Okna");
            database = MyIni.Read("database", "Okna");
            uid = MyIni.Read("login", "Okna");
            password = Decrypt(MyIni.Read("pass", "Okna"));

            var connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT name FROM status ORDER BY id ASC";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statusBOX.Items.Add(reader.GetString("name"));
                        }
                    }
                }
            }
            statusBOX.SelectedIndex = 0;
        }

        private void selectBTN_Click(object sender, EventArgs e)
        {
            if(statusBOX.SelectedIndex == 3)
            {
                var MyIni = new INIFile("WektorSettings.ini");
                server = MyIni.Read("server", "Okna");
                database = MyIni.Read("database", "Okna");
                uid = MyIni.Read("login", "Okna");
                password = Decrypt(MyIni.Read("pass", "Okna"));
                var connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                using (var connection = new MySqlConnection(connectionString))
                {
                    var data = DateTime.Now;
                    var dat = data.ToString("yyyy-MM-dd HH:mm:ss");
                    var query = "UPDATE zamowienia SET status = '" + statusBOX.SelectedIndex + "', " +
                        "data_r = '" + dat + "',przyjol=(SELECT name FROM uzytkownicy WHERE username ='" + Form1.logged.Text + "') " +
                        "WHERE zamowienie_id = '" + zamowienia.dataGridView1.SelectedCells[9].Value.ToString() + "'";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Status zmieniono pomyślnie" , "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        command.Connection.Close();
                        zamowienia.PerformRefresh(e, e);
                        Close();
                    }
                }
            }
            else
            {
                var MyIni = new INIFile("WektorSettings.ini");
                server = MyIni.Read("server", "Okna");
                database = MyIni.Read("database", "Okna");
                uid = MyIni.Read("login", "Okna");
                password = Decrypt(MyIni.Read("pass", "Okna"));
                var connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                using (var connection = new MySqlConnection(connectionString))
                {
                    var data = DateTime.Now;
                    var dat = data.ToString("yyyy-MM-dd");
                    var query = "UPDATE zamowienia SET status = '" + statusBOX.SelectedIndex + "',przyjol=(SELECT name FROM uzytkownicy WHERE username ='" + Form1.logged.Text + "') WHERE zamowienie_id = '" + zamowienia.dataGridView1.SelectedCells[9].Value.ToString() + "'";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Status zmieniono pomyślnie", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        command.Connection.Close();
                        zamowienia.PerformRefresh(e, e);
                        Close();
                    }
                }
            }
        }
    }
}
