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
using System.Globalization;

namespace Okna.finanse
{
    public partial class fakt_det : Form
    {
        private faktury faktury;
        public fakt_det(faktury faktury)
        {
            InitializeComponent();
            this.faktury = faktury;
            Text = "Faktura nr " + faktury.dataGridView1.SelectedCells[0].Value.ToString();
        }

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        BindingSource source1 = new BindingSource();
        private string server;
        private string database;
        private string uid;
        private string password;
        Boolean acceptableKey = false;
        string strCurrency = "";

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

        private void fakt_det_Load(object sender, EventArgs e)
        {
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

                var id_fakt = faktury.dataGridView1.SelectedCells[9].Value.ToString();
                MySqlCommand cmd = new MySqlCommand("SELECT a.id,b.nazwa,c.nazwa,a.ilosc,d.name,a.netto,d.ile FROM fakt_det a " +
                    " LEFT JOIN cenniki b ON(a.id_product = b.id)" +
                    " LEFT JOIN jednostki c ON(a.jm = c.id)" +
                    " LEFT JOIN podatki d ON(a.vat = d.id)" +
                    " WHERE a.id_fakt = '" + id_fakt + "' ORDER BY a.id ASC", conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                dt.Columns.Add("Lp."); //0
                dt.Columns.Add("Nazwa towatu"); //1
                dt.Columns.Add("j.m."); //2
                dt.Columns.Add("Ilość"); //3
                dt.Columns.Add("Vat"); //4
                dt.Columns.Add("c.j. Netto"); //5
                dt.Columns.Add("c.j. Brutto"); //6
                dt.Columns.Add("wart. Netto"); //7
                dt.Columns.Add("wart. Brutto"); //8
                dt.Columns.Add("Rabat"); //9

                while (rdr.Read())
                {
                    DataRow row = dt.NewRow();
                    var v = Decimal.Parse(rdr[6].ToString());
                    decimal vat = Math.Round((v / 100) + 1, 2);
                    decimal netto = Decimal.Parse(rdr[5].ToString());
                    var brutto = Math.Round(netto * vat, 2);
                    decimal ilosc = Decimal.Parse(rdr[3].ToString());
                    var wartN = Math.Round(netto * ilosc, 2);
                    var wartB = Math.Round(wartN * vat, 2);
                    row[0] = dt.Rows.Count + 1;
                    row[1] = rdr[1];
                    row[2] = rdr[2];
                    row[3] = rdr[3];
                    row[4] = rdr[4];
                    row[5] = String.Format("{0:c}", rdr[5]);
                    row[6] = String.Format("{0:c}", brutto);
                    row[7] = String.Format("{0:c}", wartN);
                    row[8] = String.Format("{0:c}", wartB);
                    row[9] = "0%";
                    dt.Rows.Add(row);
                }
                source1.DataSource = dt;
                dataGridView1.DataSource = source1;
                dataGridView1.Columns[0].Width = 40; //lp
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; //nazwa
                dataGridView1.Columns[2].Width = 50; //jm
                dataGridView1.Columns[3].Width = 50; //ilość
                dataGridView1.Columns[4].Width = 60; //vat
                dataGridView1.Columns[9].Width = 60; //rabat

                conn.Close();

                if (nettoKW.Checked == true)
                {
                    dataGridView1.Columns[6].Visible = false;
                }
                else
                {
                    dataGridView1.Columns[6].Visible = true;
                }
                numerFV.Text = faktury.dataGridView1.SelectedCells[0].Value.ToString();

                slownie.Text = KwotaSlownie((decimal)CellSum(), "PLN");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            kontrahentLoad(e, e);
            terminyLoad(e, e);
            wartFV.Text = CellSum().ToString() + " zł";
            doZap.Text = wartFV.Text;
            zalT.Text = "0,00 zł";
            rodzajFaktury.SelectedIndex = 0;
            var rfv = rodzajFaktury.SelectedIndex;
            if (rfv == 0)
            {
                numerFV.Enabled = false;
            }
            numerZamowienia.Text = faktury.dataGridView1.SelectedCells[2].Value.ToString();

            Double netto1;
            string netto23_value;

            

            dataGridView2.Rows.Add("Razem:", wartNetto().ToString() + " zł", "x", wartVat().ToString() + " zł", CellSum().ToString() + " zł");
            dataGridView2.Rows.Add("W tym:", wartNetto().ToString() + " zł", "23%", wartVat().ToString() + " zł", CellSum().ToString() + " zł");
        }

        public static string KwotaSlownie(decimal kwota, string kodWaluty)
        {
            int calosc = (int)kwota;
            int ulamki = (int)(kwota * 100) % 100;
            return string.Format("{0} {1}, {2} {3}",
                Formatowanie.LiczbaSlownie(calosc),
                Formatowanie.WalutaSlownie(calosc, kodWaluty),
                Formatowanie.LiczbaSlownie(ulamki),
                Formatowanie.WalutaSlownie(ulamki, "." + kodWaluty));
        }

        private double CellSum()
        {
            double sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                double d = 0;
                Double.TryParse(dataGridView1.Rows[i].Cells[8].Value.ToString().Replace("zł",""), out d);
                sum += d;
            }
            return sum;
        }

        private double wartNetto()
        {
            double net = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                double dd = 0;
                Double.TryParse(dataGridView1.Rows[i].Cells[7].Value.ToString().Replace("zł", ""), out dd);
                net += dd;
            }
            return net;
        }

        private double wartVat()
        {
            double wVat = 0;
            wVat = Math.Round(CellSum() - wartNetto(),2);
            return wVat;
        }

        private void kontrahentLoad(object sender, EventArgs e)
        {
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

                var id_klient = faktury.dataGridView1.SelectedCells[7].Value.ToString();
                MySqlCommand cmd = new MySqlCommand("SELECT nip,nazwa,ulica,nr,kod,miasto FROM klienci WHERE id = '" + id_klient + "'", conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    nipBox.Text = rdr[0].ToString();
                    nazwaL.Text = rdr[1].ToString();
                    ulica.Text = rdr[2].ToString();
                    ulicaNR.Text = rdr[3].ToString();
                    kod.Text = rdr[4].ToString();
                    miasto.Text = rdr[5].ToString();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void terminyLoad(object sender, EventArgs e)
        {
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

                var indeks = faktury.dataGridView1.SelectedCells[8].Value.ToString();
                MySqlCommand cmd = new MySqlCommand("SELECT a.name FROM terminy a ORDER BY a.id", conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    rodzajPlatnosci.Items.Add(rdr[0]);
                }
                conn.Close();
                rodzajPlatnosci.SelectedIndex = Convert.ToInt32(indeks);
                if(indeks == "4" | indeks == "5" | indeks == "5" | indeks == "6" | indeks == "7")
                {
                    isPay.Checked = true;
                    doZap.Text = "0,00 zł";
                }
                else
                {
                    isPay.Checked = false;
                    doZap.Text = wartFV.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bruttoKW_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Wszystkie kwoty zostały przeliczone","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nettoKW_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns[5].Visible = true;
                dataGridView1.Columns[6].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rodzajPlatnosci_SelectedIndexChanged(object sender, EventArgs e)
        {
            var indeks = rodzajPlatnosci.SelectedIndex + 1;
            if (indeks == 4 | indeks == 5 | indeks == 6 | indeks == 7)
            {
                isPay.Checked = true;
                dataPlatnosci.Value = DateTime.Now;
                doZap.Text = "0,00 zł";
            }
            else
            {
                isPay.Checked = false;
                doZap.Text = wartFV.Text;
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

                    MySqlCommand cmd = new MySqlCommand("SELECT a.dni FROM terminy a WHERE a.id = '" + indeks + "'", conn);

                    MySqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var dni = Convert.ToDouble(rdr[0]);
                        dataPlatnosci.Value = DateTime.Now.AddDays(dni);
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void anulujBTN_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Czy chcesz wyjść?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                Close();
            }
            else { }
        }

        private void printBTN_Click(object sender, EventArgs e)
        {
            formsy.druk form = new formsy.druk();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            klienci form = new klienci();
            form.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(zalT.Text))
            {
                doZap.Text = wartFV.Text;
            }
            else
            {
                try
                {
                    decimal zal = Decimal.Parse(zalT.Text.Replace("zł",""));
                    decimal wart = Decimal.Parse(wartFV.Text.Replace("zł", ""));
                    var zost = Math.Round(wart - zal, 2);
                    doZap.Text = string.Format("{0:c}", zost);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } 
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

        }

        private void TextBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if ((e.KeyCode >= Keys.D0 & e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 & e.KeyCode <= Keys.NumPad9) || e.KeyCode == Keys.Back)
            {
                acceptableKey = true;
            }
            else
            {
                acceptableKey = false;
            }
        }
        private void TextBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (acceptableKey == false)
            {
                e.Handled = true;
                return;
            }
            else
            {
                if (e.KeyChar == Convert.ToChar(Keys.Back))
                {
                    if (strCurrency.Length > 0)
                    {
                        strCurrency = strCurrency.Substring(0, strCurrency.Length - 1);
                    }
                }
                else
                {
                    strCurrency = strCurrency + e.KeyChar;
                }

                if (strCurrency.Length == 0)
                {
                    zalT.Text = "0,00 zł";
                }
                else if (strCurrency.Length == 1)
                {
                    zalT.Text = "0,0" + strCurrency + " zł";
                }
                else if (strCurrency.Length == 2)
                {
                    zalT.Text = "0," + strCurrency + " zł";
                }
                else if (strCurrency.Length > 2)
                {
                    zalT.Text = strCurrency.Substring(0, strCurrency.Length - 2) + "," + strCurrency.Substring(strCurrency.Length - 2) + " zł";
                }
                zalT.Select(zalT.Text.Length, 0);

            }
            e.Handled = true;
        }

        private void zalT_Click(object sender, EventArgs e)
        {
            zalT.SelectAll();
        }

        private void isPay_CheckedChanged(object sender, EventArgs e)
        {
            if (isPay.Checked == true)
                doZap.Text = "0,00 zł";
            else
                doZap.Text = wartFV.Text;
        }

        private void rodzajFaktury_SelectedIndexChanged(object sender, EventArgs e)
        {
            var nrfv = rodzajFaktury.SelectedIndex;
            if (nrfv == 0)
            {
                numerFV.Enabled = false;
            }
            else
            {
                numerFV.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            towarAdd form = new towarAdd(this);
            form.Show();
        }
    }
}
