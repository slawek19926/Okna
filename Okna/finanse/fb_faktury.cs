using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebirdSql;
using FirebirdSql.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;

namespace Okna.finanse
{
    public partial class fb_faktury : Form
    {
        private Form1 Form1;
        public fb_faktury(Form1 Form1)
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            Text = "Faktury Sokaris";
            this.Form1 = Form1;
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

        private void fb_faktury_Load(object sender, EventArgs e)
        {
            var MyIni = new INIFile("WektorSettings.ini");
            server = MyIni.Read("server", "firebird");
            database = MyIni.Read("dblocation", "firebird");
            uid = MyIni.Read("user", "firebird");
            password = Decrypt(MyIni.Read("pass", "firebird"));

            connectionString = "User=" + uid + ";Password=" + password + ";Database=" + database + ";DataSource=" + server + ";Charset=NONE;";
            FbConnection conn = new FbConnection();
            conn.ConnectionString = connectionString;

            conn.Open();

            FbCommand cmd = new FbCommand("SELECT NUMER, DATA_WYSTAWIENIA,NAZWA_AD,RODZAJ_PLATNOSCI,ZAPLACONA,WARTOSCZPODATKIEM,ZALICZKA,ZAPLACONO FROM FAKTURY p " +
                "LEFT JOIN KONTRAHENCI d ON (KONTRAHENT = d.KOD)" +
                "ORDER BY p.KOD DESC", conn);

            FbDataReader rdr = cmd.ExecuteReader();

            dt.Columns.Add("Numer");
            dt.Columns.Add("Data wystawienia");
            dt.Columns.Add("Klient");
            dt.Columns.Add("Rodzaj płatności");
            dt.Columns.Add("Zapłacona");
            dt.Columns.Add("Wartość");
            dt.Columns.Add("Zaliczka");
            dt.Columns.Add("Zapłacono");

            while (rdr.Read())
            {
                DataRow row = dt.NewRow();
                var data = Convert.ToDateTime(rdr[1]);
                CultureInfo cult = new CultureInfo("en-US");
                row[0] = rdr[0];
                row[1] = data.ToString("dd.MM.yyyy", cult);
                row[2] = rdr[2];
                row[3] = rdr[3];
                if(rdr[4].ToString() == "1")
                {
                    row[4] = "tak";
                }
                else
                {
                    row[4] = "nie";
                }
                row[5] = string.Format("{0:c}",rdr[5]);
                row[6] = string.Format("{0:c}",rdr[6]);
                row[7] = string.Format("{0:c}",rdr[7]);
                dt.Rows.Add(row);
            }
            source1.DataSource = dt;
            dataGridView1.DataSource = source1;
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].Width = 120;

            dataOd.Enabled = false;
            dataDo.Enabled = false;
            textBox1.Enabled = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton4.Checked == true)
            {
                dataOd.Enabled = true;
                dataDo.Enabled = true;
            }
            else
            {
                dataOd.Enabled = false;
                dataDo.Enabled = false;
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton8.Checked == true)
            {
                textBox1.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
            }
        }
        //sortowanie malejąco
        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
        }
        //sortowanie rosnąco
        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
        }

        private void detailsFbFakt_Click(object sender, EventArgs e)
        {
            var reult = MessageBox.Show("Ju siur rze ales gut?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(DialogResult == DialogResult.Yes)
            {
                MessageBox.Show("Narazie nie da się obejżeć szczegółów tych faktur :(", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
