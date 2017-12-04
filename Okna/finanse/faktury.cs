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
using Npgsql;
using System.Net;
using System.Globalization;

namespace Okna.finanse
{
    public partial class faktury : Form
    {
        private Form1 Form1;
        public faktury(Form1 Form1)
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            Text = "Faktury";
            this.Form1 = Form1;
        }

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        BindingSource source1 = new BindingSource();
        private string server;
        private string database;
        private string uid;
        private string password;
        public static string fv_id;

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

        private void faktury_Load(object sender, EventArgs e)
        {
            try
            {
                var MyIni = new INIFile("WektorSettings.ini");
                server = MyIni.Read("server", "Okna");
                database = MyIni.Read("database", "Okna");
                uid = MyIni.Read("login", "Okna");
                password = Decrypt(MyIni.Read("pass", "Okna"));
                var przed = MyIni.Read("przed", "faktury");
                var zera = MyIni.Read("zera", "faktury");

                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "Convert Zero Datetime = True;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT b.id,a.numer,b.nazwa,a.pay,a.nr,a.zam_nr,a.data,c.dni FROM fakt a " +
                    " LEFT JOIN klienci b ON(a.klient = b.id)" +
                    " LEFT JOIN terminy c ON(a.pay = c.id)" +
                    " ORDER BY a.id ASC", conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                dt.Columns.Add("Numer faktury");
                dt.Columns.Add("Data wyst.");
                dt.Columns.Add("Do zam.");
                dt.Columns.Add("Kontrahent");
                dt.Columns.Add("Termin pł.");
                dt.Columns.Add("Na kwotę");
                dt.Columns.Add("Do zapłaty");
                dt.Columns.Add("id_klient");
                dt.Columns.Add("pay");
                dt.Columns.Add("id");
                dt.Columns.Add("Zapłacona");

                Bitmap b = new Bitmap(50, 15);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.DrawString("Loading...", Font, new SolidBrush(Color.Black), 0f, 0f);
                }

                while (rdr.Read())
                {
                    DataRow row = dt.NewRow();
                    var data = Convert.ToDateTime(rdr[6]);
                    var dni = Convert.ToDouble(rdr[7]);
                    var termin = data.AddDays(dni);
                    var teraz = DateTime.Now;
                    TimeSpan ile = termin-teraz;
                    CultureInfo cult = new CultureInfo("en-US");
                    row[0] = rdr[1];
                    row[1] = data.ToString("dd/MM/yyyy",cult);
                    row[2] = rdr[5];
                    row[3] = rdr[2];
                    row[4] = ile.Days.ToString();
                    row[7] = rdr[0];
                    row[8] = rdr[3];
                    row[9] = rdr[4];
                    if(ile.Days > 0)
                    {
                        row[10] = "Niezapłacona";
                    }
                    else
                    {
                        row[10] = "Zapłacona";
                    }

                    dt.Rows.Add(row);
                }
                source1.DataSource = dt;
                dataGridView1.DataSource = source1;
                dataGridView1.Columns[0].Width = 150;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            var termin = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);

            if (termin == 0)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                if (dataGridView.Rows[e.RowIndex].Selected)
                {
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    e.CellStyle.SelectionBackColor = Color.YellowGreen;
                }
            }
            else if (termin < 0)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                e.CellStyle.SelectionBackColor = Color.Red;
                if (dataGridView.Rows[e.RowIndex].Selected)
                {
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    e.CellStyle.SelectionBackColor = Color.YellowGreen;
                }
            }
            else
            {
                if (dataGridView.Rows[e.RowIndex].Selected)
                {
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    e.CellStyle.SelectionBackColor = Color.LightBlue;
                }
            }

        }

        private void detail_BTN_Click(object sender, EventArgs e)
        {
            fakt_det form = new fakt_det(this);
            form.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            fv_id = dataGridView1.SelectedCells[9].Value.ToString();
        }

        private void zapFV_CheckedChanged(object sender, EventArgs e)
        {
            string RowFilter = string.Empty;
            CreateOrAppendToFilter(zapFV, ref RowFilter);
            if (RowFilter.Length > 0)
            {
                try
                {
                    //Check an see what's in the dgv
                    DataView dv = new DataView(dt);
                    dv.RowFilter = RowFilter;
                    dataGridView1.DataSource = dv;
                }
                catch (Exception)
                {
                    MessageBox.Show("Can’t find the column", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void CreateOrAppendToFilter(CheckBox cb, ref string RowFilter)
        {
            if (RowFilter.Length > 0)
            {
                RowFilter += " OR ";
            }
            RowFilter += (cb.Checked) ? string.Format("[Zapłacona] = {0}", cb.Text.Trim()) : string.Empty;
        }

        private void niezapFV_CheckedChanged(object sender, EventArgs e)
        {
            string filtr = string.Empty;
            if (niezapFV.Checked)
            {
                filtr += " [Zapłacona] = " + niezapFV.Text.Trim();
            }
            try
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = filtr;
                dataGridView1.DataSource = dv;
            }
            catch (Exception)
            {
                MessageBox.Show("Brak nieopłaconych faktur", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
