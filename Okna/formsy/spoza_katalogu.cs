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
        public spoza_katalogu(add_product add_Product)
        {
            InitializeComponent();
            this.add_Product = add_Product;
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

        private void spoza_katalogu_Load(object sender, EventArgs e)
        {
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
                            stawkiVAT.Items.Add(reader.GetString("name"));
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

            
        }

        private void nettoTXT_TextChanged(object sender, EventArgs e)
        {
            if(nettoTXT.Text == "")
            {
                decimal O = Decimal.Parse("0,00");
                decimal v = Decimal.Parse("1,23");
                decimal b = Math.Round(O * v, 2);
                bruttoTXT.Text = b.ToString("c");
            }
            else
            {
                decimal n = Decimal.Parse(nettoTXT.Text.Replace("zł", ""));
                decimal v = Decimal.Parse("1,23");
                decimal b = Math.Round(n * v, 2);
                bruttoTXT.Text = b.ToString("c");
            }
        }

        public void stawkiVAT_SelectedIndexChanged(object sender, EventArgs e)
        {
            int[] vat = new int[5];
            vat[0] = stawkiVAT.SelectedIndex;
        }
        
    }
}
