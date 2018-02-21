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
    public partial class spoza_katalogu : Form
    {
        add_product add_Product;
        wycena wycena;
        public spoza_katalogu(add_product add_Product,wycena wycena)
        {
            InitializeComponent();
            this.add_Product = add_Product;
            this.wycena = wycena;
            Text = "Dodawanie produktu spoza katalogu";
        }
        private string server;
        private string database;
        private string uid;
        private string pass;
        private string port;
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

        public class Stawki
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public override string ToString() { return this.Name; }
        }

        private void spoza_katalogu_Load(object sender, EventArgs e)
        {
            ActiveControl = indeksTXT;
            iloscTXT.Text = "1";

            stawkiVAT.DisplayMember = "Text";
            stawkiVAT.ValueMember = "Value";

            var MyIni = new INIFile("WektorSettings.ini");
            server = MyIni.Read("server", "Okna");
            database = MyIni.Read("database", "Okna");
            uid = MyIni.Read("login", "Okna");
            pass = Decrypt(MyIni.Read("pass", "Okna"));

            var connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + pass + ";";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM podatki";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        //dodaje stawki VAT
                        while (reader.Read())
                        {
                            stawkiVAT.Items.Add(new Stawki { Name = reader.GetString("name"), Value = reader.GetString("wart") });
                        }
                    }
                }
                connection.Close();
                connection.Open();
                var query2 = "SELECT * FROM jednostki";
                using (var command = new MySqlCommand(query2, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        //dodaje jednostki miar
                        while (reader.Read())
                        {
                            jednostki.Items.Add(reader.GetString("nazwa"));
                        }
                    }
                }
                connection.Close();
            }
            stawkiVAT.SelectedIndex = 0;
            jednostki.SelectedIndex = 0;
            decimal netto = Decimal.Parse("0,00");
            decimal brutto = Decimal.Parse("0,00");
            nettoTXT.Text = netto.ToString("c");
            bruttoTXT.Text = brutto.ToString("c");

            decimal stawka = Convert.ToDecimal(((Stawki)stawkiVAT.SelectedItem).Value);
        }

        private void nettoTXT_TextChanged(object sender, EventArgs e)
        {
            nettoTXT.BackColor = Color.White;
            decimal stawka = Convert.ToDecimal(((Stawki)stawkiVAT.SelectedItem).Value);
            if (nettoTXT.Text == "")
            {
                decimal O = Decimal.Parse("0,00");
                decimal i = Decimal.Parse(iloscTXT.Text);
                decimal v = stawka;
                decimal b = Math.Round(O * v, 2);
                decimal nw = Math.Round(O * i, 2);
                decimal bw = Math.Round(b * i, 2);
                bruttoTXT.Text = b.ToString("c");
                nettoW.Text = nw.ToString("c");
                bruttoW.Text = bw.ToString("c");
            }
            else
            {
                decimal n = Decimal.Parse(nettoTXT.Text.Replace("zł", ""));
                decimal i = Decimal.Parse(iloscTXT.Text);
                decimal v = stawka;
                decimal b = Math.Round(n * v, 2);
                decimal nw = Math.Round(n * i, 2);
                decimal bw = Math.Round(b * i, 2);
                bruttoTXT.Text = b.ToString("c");
                nettoW.Text = nw.ToString("c");
                bruttoW.Text = bw.ToString("c");
            }
        }

        public void stawkiVAT_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal stawka = Convert.ToDecimal(((Stawki)stawkiVAT.SelectedItem).Value);
            if (nettoTXT.Text == "")
            {
                decimal O = Decimal.Parse("0,00");
                decimal v = stawka;
                decimal b = Math.Round(O * v, 2);
                bruttoTXT.Text = b.ToString("c");
            }
            else
            {
                decimal n = Decimal.Parse(nettoTXT.Text.Replace("zł", ""));
                decimal v = stawka;
                decimal b = Math.Round(n * v, 2);
                bruttoTXT.Text = b.ToString("c");
            }
            if(stawka.ToString() == "0,00")
            {
                bruttoTXT.Text = nettoTXT.Text;
            }
        }

        private void nettoTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void anulujBTN_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Napewno anulować?", "info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                Close();
            }
            if(result == DialogResult.No)
            {

            }
        }

        public bool SprawdzamFormularz()
        {
                if (String.IsNullOrEmpty(indeksTXT.Text))
                {
                    MessageBox.Show("Indeks jest wymagany!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ActiveControl = indeksTXT;
                    indeksTXT.BackColor = Color.Red;
                    return false;
                }
                else if (String.IsNullOrEmpty(nazwaTXT.Text))
                {
                    MessageBox.Show("Nazwa jest wymagana!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ActiveControl = nazwaTXT;
                    nazwaTXT.BackColor = Color.Red;
                    return false;
                }
                else if (Decimal.Parse(nettoTXT.Text.Replace("zł", "")).ToString() == "0,00")
                {
                    MessageBox.Show("Cena nie może wynosić 0,00 zł", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ActiveControl = nettoTXT;
                    nettoTXT.BackColor = Color.Red;
                    return false;
                }
                else if (String.IsNullOrEmpty(iloscTXT.Text))
                {
                    MessageBox.Show("Ilość nie może wynosić 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ActiveControl = iloscTXT;
                    iloscTXT.BackColor = Color.White;
                    return false;
                }
            return true;
        }

        private void dodajBTN_Click(object sender, EventArgs e)
        {
            if (!SprawdzamFormularz())
            {
                return;
            }
            //dodawanie towaru do bazy danych
            var indeks = indeksTXT.Text;
            var nazwa = nazwaTXT.Text;
            var netto = nettoTXT.Text;
            string rabat = "0,00";
            string vat = stawkiVAT.SelectedText;
            var ilosc = iloscTXT.Text;

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
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"INSERT INTO cenniki (reference,nazwa,cena,podatek) VALUES ('{indeks}','{nazwa}','{netto.Replace("zł","").Replace(",",".")}',1)";
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Zapisanie danych było udane");
                object[] row = new object[] { indeks, nazwa, netto, rabat, netto, ilosc };
                wycena.metroGrid1.Rows.Add(row);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void indeksTXT_TextChanged(object sender, EventArgs e)
        {
            indeksTXT.BackColor = Color.White;
        }

        private void nazwaTXT_TextChanged(object sender, EventArgs e)
        {
            nazwaTXT.BackColor = Color.White;
        }

        private void iloscTXT_TextChanged(object sender, EventArgs e)
        {
            iloscTXT.BackColor = Color.White;
        }

        private void iloscTXT_Validated(object sender, EventArgs e)
        {
            
        }

        private void nettoTXT_Leave(object sender, EventArgs e)
        {
            Double value;
            if (Double.TryParse(nettoTXT.Text, out value))
                nettoTXT.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:c}", value);
            else
                nettoTXT.Text = String.Empty;
        }

        private void indeksTXT_MouseEnter(object sender, EventArgs e)
        {
            indeksTXT.SelectAll();
        }
        private void Select_nazwa(object sender,EventArgs e)
        {
            nazwaTXT.SelectAll();
        }
        private void Select_netto(object sender,EventArgs e)
        {
            nettoTXT.SelectAll();
        }
    }
}
