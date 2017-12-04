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
    public partial class towarAdd : Form
    {
        fakt_det fakt_det;
        public towarAdd(fakt_det fakt_det)
        {
            InitializeComponent();
        }

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

        private void towarAdd_Load(object sender, EventArgs e)
        {
            filtrBox.SelectedIndex = 0;
            try
            {
                var MyIni = new INIFile("WektorSettings.ini");
                server = MyIni.Read("server", "Okna");
                database = MyIni.Read("database", "Okna");
                uid = MyIni.Read("login", "Okna");
                password = Decrypt(MyIni.Read("pass", "Okna"));

                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "Convert Zero Datetime = True;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT id,reference,nazwa,cena FROM cenniki ORDER BY id ASC", conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                dt.Columns.Add("Lp");
                dt.Columns.Add("Indeks");
                dt.Columns.Add("Nazwa");
                dt.Columns.Add("Detal netto");
                dt.Columns.Add("Detal brutto");

                while (rdr.Read())
                {
                    DataRow row = dt.NewRow();

                    decimal narzut = Decimal.Parse("1,2");
                    decimal vat = Decimal.Parse("1,23");
                    decimal netto = Decimal.Parse(rdr[3].ToString());
                    decimal detaln = Math.Round(netto * narzut, 2);
                    decimal detalb = Math.Round(detaln * vat, 2);

                    row[0] = rdr[0];
                    row[1] = rdr[1];
                    row[2] = rdr[2];
                    row[3] = string.Format("{0:c}", detaln);
                    row[4] = string.Format("{0:c}", detalb);

                    dt.Rows.Add(row);
                }
                source1.DataSource = dt;
                dataGridView1.DataSource = source1;
                dataGridView1.Columns[0].Width = 40;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void phrase_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //szukanie po indeksie
                if (filtrBox.SelectedIndex == 0)
                {
                    var bd = (BindingSource)dataGridView1.DataSource;
                    var dt = (DataTable)bd.DataSource;
                    dt.DefaultView.RowFilter = string.Format("Indeks like '%{0}%'", phrase.Text.Trim().Replace("'", "''"));
                    dataGridView1.Refresh();
                }
                //szukanie po nazwie
                else if (filtrBox.SelectedIndex == 1)
                {
                    var bd = (BindingSource)dataGridView1.DataSource;
                    var dt = (DataTable)bd.DataSource;
                    dt.DefaultView.RowFilter = string.Format("Nazwa like '%{0}%'", phrase.Text.Trim().Replace("'", "''"));
                    dataGridView1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
