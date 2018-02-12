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

namespace Okna.formsy
{
    public partial class klienci : Form
    {
        public klienci()
        {
            InitializeComponent();
            Text = "Kartoteka klientów";
        }

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        private string server;
        private string database;
        private string uid;
        private string password;
        private string connectionString;

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

        private void klienci_Load(object sender, EventArgs e)
        {
            var MyIni = new INIFile("WektorSettings.ini");
            server = MyIni.Read("server", "Okna");
            database = MyIni.Read("database", "Okna");
            uid = MyIni.Read("login", "Okna");
            password = Decrypt(MyIni.Read("pass", "Okna"));
            var zera = MyIni.Read("zera", "wyceny");
            var przed = MyIni.Read("przed", "wyceny");
            try
            {
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "Convert Zero Datetime = True;";
                MySqlConnection obj_Conn = new MySqlConnection();
                obj_Conn.ConnectionString = connectionString;

                obj_Conn.Open();
                MySqlCommand obj_Cmd = new MySqlCommand("SELECT id,symbol,nazwa,ulica,nr,kod,miasto,nip FROM klienci", obj_Conn);
                MySqlDataReader obj_Reader = obj_Cmd.ExecuteReader();

                dt.Columns.Add("Lp");
                dt.Columns.Add("Symbol");
                dt.Columns.Add("Nazwa");
                dt.Columns.Add("Adres");
                dt.Columns.Add("Kod pocztowy");
                dt.Columns.Add("Miasto");
                dt.Columns.Add("NIP");

                while (obj_Reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row[0] = obj_Reader[0];
                    row[1] = obj_Reader[1];
                    row[2] = obj_Reader[2];
                    row[3] = obj_Reader[3] + " " + obj_Reader[4];
                    row[4] = obj_Reader[5];
                    row[5] = obj_Reader[6];
                    row[6] = obj_Reader[7];
                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                obj_Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormSearchClient frm = new FormSearchClient();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
