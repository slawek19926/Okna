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
    public partial class edit_klient : Form
    {
        klienci klienci;
        public edit_klient(klienci klienci)
        {
            InitializeComponent();
            Text = "Edycja klienta " + klienci.dataGridView1.SelectedCells[2].Value.ToString();
            nazwaTXT.Text = klienci.dataGridView1.SelectedCells[2].Value.ToString();
            miastoTXT.Text = klienci.dataGridView1.SelectedCells[5].Value.ToString();
            ulicaTXT.Text = klienci.dataGridView1.SelectedCells[3].Value.ToString();
            pocztaTXT.Text = klienci.dataGridView1.SelectedCells[4].Value.ToString();
            panstwoTXT.Text = "Polska";
            nipTXT.Text = klienci.dataGridView1.SelectedCells[6].Value.ToString();
            klientID.Text = klienci.dataGridView1.SelectedCells[0].Value.ToString();
        }

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

        private void saveBTN_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Ju siur rze ales gut?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                var MyIni = new INIFile("WektorSettings.ini");
                server = MyIni.Read("server", "Okna");
                database = MyIni.Read("database", "Okna");
                uid = MyIni.Read("login", "Okna");
                password = Decrypt(MyIni.Read("pass", "Okna"));
                try
                {
                    connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "Convert Zero Datetime = True;";
                    MySqlConnection obj_Conn = new MySqlConnection();
                    obj_Conn.ConnectionString = connectionString;

                    obj_Conn.Open();
                    MySqlCommand obj_Cmd = new MySqlCommand("UPDATE klienci SET nazwa = '" + nazwaTXT.Text + "', ulica = '" + ulicaTXT.Text + "',kod = '" + pocztaTXT.Text + "',miasto = '" + miastoTXT.Text + "',nip = '" + nipTXT.Text + "' WHERE id = " + klientID.Text + "", obj_Conn);
                    obj_Cmd.ExecuteNonQuery();
                    var res = MessageBox.Show("Operacja przebiegła pomyslnie", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (res == DialogResult.OK)
                    {
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Nic z tego :(");
            }
        }
    }
}
