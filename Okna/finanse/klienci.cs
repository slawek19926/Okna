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
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace Okna.finanse
{
    public partial class klienci : Form
    {
        public klienci()
        {
            InitializeComponent();
            Text = "Kontrahenci";
        }
        public string ico1 = "C:\\img\\usun_159x41.png";
        public string ico2 = "C:\\img\\usun159x41 grey.png";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        BindingSource source1 = new BindingSource();
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
            
            Form f1 = Application.OpenForms["fakt_det"];
            if (((fakt_det)f1) != null)
            {
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                button3.Image = Image.FromFile(ico2);
                button4.Enabled = false;
                button5.Visible = true;
                button6.Visible = false;
            }
            else
            {
                button2.Enabled = true;
                button3.Enabled = true;
                //button3.Image = Image.FromFile(ico1);
                button4.Enabled = true;
                button5.Visible = false;
                button6.Visible = true;
            }

            try
            {
                var MyIni = new INIFile("WektorSettings.ini");
                server = MyIni.Read("server", "Okna");
                database = MyIni.Read("database", "Okna");
                uid = MyIni.Read("login", "Okna");
                password = Decrypt(MyIni.Read("pass", "Okna"));

                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "Convert Zero Datetime = True;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;

                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT nazwa,ulica,nr,kod,miasto,nip,odbiorca FROM klienci", conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                dt.Columns.Add("Nazwa klienta");
                dt.Columns.Add("Adres");
                dt.Columns.Add("Kod pocztowy");
                dt.Columns.Add("Miejscowość");
                dt.Columns.Add("Nip");
                dt.Columns.Add("Odbiorca/dostawca");

                while (rdr.Read())
                {
                    DataRow row = dt.NewRow();

                    row[0] = rdr[0];
                    row[1] = rdr[1] + " " + rdr[2];
                    row[2] = rdr[3];
                    row[3] = rdr[4];
                    row[4] = rdr[5];
                    if(rdr[6].ToString() == "0")
                    {
                        row[5] = "Dostawca";
                    }
                    else
                    {
                        row[5] = "Odbiorca";
                    }

                    dt.Rows.Add(row);
                }
                source1.DataSource = dt;
                dataGridView1.DataSource = source1;
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
