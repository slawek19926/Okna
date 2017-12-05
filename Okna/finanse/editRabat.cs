using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace Okna.finanse
{
    public partial class editRabat : Form
    {
        private rabaty rabaty;
        public editRabat(rabaty rabaty)
        {
            InitializeComponent();
            this.rabaty = rabaty;
            string id = rabaty.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string symbol = rabaty.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string nazwa = rabaty.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            string wartosc = rabaty.dataGridView1.CurrentRow.Cells[3].Value.ToString().Replace(".",",");
            Text = "Edytujesz grupę rabatową: " + symbol;

            idN.Text = id;
            nazwaTXT.Text = nazwa;
            symbolTXT.Text = symbol;
            wartoscTXT.Text = wartosc;
        }

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        BindingSource source1 = new BindingSource();
        private string server;
        private string database;
        private string uid;
        private string pass;

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

        private void saveBtn_Click(object sender, EventArgs e)
        {
            decimal wart = Decimal.Parse(wartoscTXT.Text);

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
                command.CommandText = "UPDATE rabaty SET ref='" + symbolTXT.Text + "',nazwa='" + nazwaTXT.Text + "',wart='" + wart + "' WHERE id='" + idN.Text + "'";
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Zapisanie danych było udane");
                rabaty.dataGridView1.CurrentRow.Cells[1].Value = symbolTXT.Text;
                rabaty.dataGridView1.CurrentRow.Cells[2].Value = nazwaTXT.Text;
                rabaty.dataGridView1.CurrentRow.Cells[3].Value = wartoscTXT.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
