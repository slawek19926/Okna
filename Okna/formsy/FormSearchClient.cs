using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NIP24;
using System.Security.Cryptography;
using System.IO;
using MySql.Data.MySqlClient;

namespace Okna.formsy
{
    public partial class FormSearchClient : Form
    {
        DialogResult result;
        INIP24Client nip24 = new NIP24Client("FOcZer5yilvG", "PgAoumN9ZOzL");

        public FormSearchClient()
        {
            InitializeComponent();
            buttonReset.Enabled = false;
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

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRegon.Text) && string.IsNullOrEmpty(textBoxNip.Text) && string.IsNullOrEmpty(textBoxKrs.Text))
            {
                const string message = "Wypełnij przynajmniej jedno pole wyszukiwania z bazy CEIDG!";
                const string caption = "Komunikat";
                result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                InvoiceData invoice = nip24.GetInvoiceData(Number.NIP, textBoxNip.Text, false);
                if (invoice != null)
                {
                    textBoxNazwa.Text = invoice.Name;
                    textBoxMiejscowosc.Text = invoice.City;
                    textBoxUlica.Text = invoice.Street + " " + invoice.StreetNumber;
                    textBoxPoczta.Text = invoice.PostCity;
                    textBox1.Text = invoice.PostCode;
                    textBoxPanstwo.Text = "Polska";
                    textBoxN.Text = invoice.NIP;
                }
                else
                {
                    MessageBox.Show(nip24.LastError);
                }
                buttonSearch.Enabled = false;
                buttonReset.Enabled = true;
                AcceptButton = button9;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            textBoxNip.Clear();
            textBoxRegon.Clear();
            textBoxKrs.Clear();
            buttonReset.Enabled = false;
            buttonSearch.Enabled = true;
            AcceptButton = buttonSearch;
        }

        private void textBoxRegon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        private void textBoxNip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        private void textBoxKrs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        //CZYSZCZENIE PÓL
        private void buttonCzNazwa_Click(object sender, EventArgs e) { textBoxNazwa.Clear(); }

        private void buttonCzMiejscowosc_Click(object sender, EventArgs e) { textBoxMiejscowosc.Clear(); }

        private void buttonCzUlica_Click(object sender, EventArgs e) { textBoxUlica.Clear(); }

        private void buttonCzPoczta_Click(object sender, EventArgs e) { textBoxPoczta.Clear(); }

        private void buttonCzPanstwo_Click(object sender, EventArgs e) { textBoxPanstwo.Clear(); }

        private void buttonCzNip_Click(object sender, EventArgs e) { textBoxN.Clear(); }

        private void buttonCzKod_Click(object sender, EventArgs e) { textBox1.Clear(); }

        private void buttonCzWszystko_Click(object sender, EventArgs e)
        {
            const string message = "Czy na pewno chcesz wyczyścić wszystkie pola?";
            const string caption = "Komunikat";
            result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                textBoxNazwa.Clear();
                textBoxMiejscowosc.Clear();
                textBoxUlica.Clear();
                textBoxPoczta.Clear();
                textBoxPanstwo.Clear();
                textBoxN.Clear();
                textBox1.Clear();
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
                //InvoiceData invoice = nip24.GetInvoiceData(Number.NIP, textBoxNip.Text, false);
                if (textBoxNazwa.Text != null)
                {
                    string nazwa = textBoxNazwa.Text;
                    string miasto = textBoxMiejscowosc.Text;
                    string ulica = textBoxUlica.Text; 
                    //string numer = invoice.StreetNumber;
                    string kod = textBox1.Text;
                    string nip = textBoxNip.Text;

                    var MyIni = new INIFile("WektorSettings.ini");
                    server = MyIni.Read("server", "Okna");
                    database = MyIni.Read("database", "Okna");
                    uid = MyIni.Read("login", "Okna");
                    password = Decrypt(MyIni.Read("pass", "Okna"));

                    string MyConnectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "Convert Zero Datetime = True;";
                    MySqlConnection connection = new MySqlConnection(MyConnectionString);
                try
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = $"INSERT INTO klienci (nazwa,ulica,kod,miasto,nip,firma) VALUES ('{nazwa}','{ulica}','{kod}','{miasto}','{nip}',1)";
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Dodano klienta do bazy danych");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Klient o podanym numerze NIP: " + nip + " już istnieje!","Błąd",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                    
                }
                else
                {
                    MessageBox.Show(nip24.LastError);
                }
        }
    }
}
