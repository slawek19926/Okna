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

        private void fb_faktury_Load(object sender, EventArgs e)
        {
            server = "localhost";
            uid = "SYSDBA";
            password = "masterkey";
            string database = @"C:\Users\Admin\AppData\Roaming\Faktura-NT\Bazy danych\ALLEGRO2018.fdb";
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
