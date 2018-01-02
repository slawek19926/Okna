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

namespace Okna.formsy
{
    public partial class wyceny_detail : Form
    {
        private wyceny wyceny;
        public wyceny_detail(wyceny wyceny)
        {
            InitializeComponent();
            this.wyceny = wyceny;
            Text = "Wycena: " + wyceny.dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
        DataSet ds = new DataSet();
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
        private void wyceny_detail_Load(object sender, EventArgs e)
        {
            wycena_nr.Text = "Wycena nr: " + wyceny.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            var MyIni = new INIFile("WektorSettings.ini");
            server = MyIni.Read("server", "Okna");
            database = MyIni.Read("database", "Okna");
            uid = MyIni.Read("login", "Okna");
            password = Decrypt(MyIni.Read("pass", "Okna"));
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "Convert Zero Datetime = True;";
            MySqlConnection obj_Conn = new MySqlConnection();
            obj_Conn.ConnectionString = connectionString;

            obj_Conn.Open();
            MySqlCommand obj_Cmd = new MySqlCommand("SELECT a.id_wyc, d.nrw as nrwyc, c.nazwa as nazwa,a.ilosc as ilosc,a.rabat as rabat,a.cena as cena,b.nazwa as klient " +
                "FROM wyceny_detail a " +
                "LEFT JOIN klienci b ON (a.id_klient = b.id) " +
                "LEFT JOIN cenniki c ON (a.id_product = c.id) " +
                "LEFT JOIN wyceny d ON (a.id_wyc = d.nrw)" +
                "WHERE a.id = '" + wyceny.dataGridView1.CurrentRow.Cells[0].Value + "'", obj_Conn);

            MySqlDataReader obj_Reader = obj_Cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("Lp");
            dt.Columns.Add("Produkt");
            dt.Columns.Add("Ilość");
            dt.Columns.Add("Rabat");
            dt.Columns.Add("Netto");
            dt.Columns.Add("Wartość netto");
            dt.Columns.Add("Vat");
            dt.Columns.Add("Wartość brutto");

            while (obj_Reader.Read())
            {
                DataRow row = dt.NewRow();
                row["Lp"] = dt.Rows.Count +1;
                row["Produkt"] = obj_Reader["nazwa"];
                row["Ilość"] = obj_Reader["ilosc"];
                row["Rabat"] = obj_Reader["rabat"];
                row["Netto"] = obj_Reader["cena"] + " zł";
                decimal ile = Decimal.Parse(obj_Reader["ilosc"].ToString());
                decimal net = Decimal.Parse(obj_Reader["cena"].ToString().Replace("zł",""));
                decimal vat = Decimal.Parse("1,23");
                row["Wartość netto"] = Math.Round(ile * net, 2) + " zł";
                row["Vat"] = Math.Round(((ile*net)*vat)-(ile*net), 2) + " zł";
                row["Wartość brutto"] = Math.Round((ile*net)*vat, 2) + " zł";
                dt.Rows.Add(row);
                klient.Text = obj_Reader["klient"].ToString();
            }
            obj_Conn.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 35;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void changeKLI_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Napewno chcesz zmienić klienta?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Narazie nie zmienisz klienta :)");
            }
        }
    }
}
