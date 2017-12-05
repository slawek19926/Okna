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
using System.Timers;

namespace Okna.finanse
{
    public partial class rabaty : Form
    {
        public rabaty()
        {
            InitializeComponent();
            Text = "Edycja grup rabatowych";

            dt.Columns.Add("Lp");
            dt.Columns.Add("Symbol");
            dt.Columns.Add("Nazwa");
            dt.Columns.Add("Wartość");
        }

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        BindingSource source1 = new BindingSource();
        private string server;
        private string database;
        private string uid;
        private string password;

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

        private void rabaty_Load(object sender, EventArgs e)
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
                MySqlCommand cmd = new MySqlCommand("SELECT id,ref,nazwa,wart FROM rabaty ORDER BY id ASC", conn);

                MySqlDataReader rdr = cmd.ExecuteReader();



                while (rdr.Read())
                {
                    DataRow row = dt.NewRow();
                    row[0] = rdr[0];
                    row[1] = rdr[1];
                    row[2] = rdr[2];
                    row[3] = rdr[3];

                    dt.Rows.Add(row);
                }
                conn.Close();
                source1.DataSource = dt;
                dataGridView1.DataSource = source1;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
       
        private void editRabat_Click(object sender, EventArgs e)
        {
            editRabat frm = new editRabat(this);
            frm.Show();
        }

        private void addBTN_Click(object sender, EventArgs e)
        {
            
        }

        private void delBTN_Click(object sender, EventArgs e)
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

            MySqlCommand cmd = new MySqlCommand("DELETE FROM rabaty WHERE id='" + dataGridView1.CurrentRow.Cells[0].Value + "'");

            cmd.ExecuteNonQuery();
            MessageBox.Show("Rekord został usunięty");

            conn.Close();
        }
    }
}
