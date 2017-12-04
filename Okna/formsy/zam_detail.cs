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
    public partial class zam_detail : Form
    {
        private zamowienia zamowienia;
        public zam_detail(zamowienia zamowienia)
        {
            InitializeComponent();
            this.zamowienia = zamowienia;
            Text = "Zamówienie: " + zamowienia.dataGridView1.CurrentRow.Cells[1].Value.ToString();
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
        private void zam_detail_Load(object sender, EventArgs e)
        {
            wycena_nr.Text = "Zamówienie nr: " + zamowienia.dataGridView1.CurrentRow.Cells[1].Value.ToString();
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
            MySqlCommand obj_Cmd = new MySqlCommand("SELECT product_id,quantity,a.cena as netto,b.nazwa as nazwa,a.rabat as rab,a.narzut as nar " +
                "FROM zam a " +
                "LEFT JOIN cenniki b ON(a.product_id = b.id)" +
                "WHERE order_id = '" + zamowienia.dataGridView1.CurrentRow.Cells[9].Value + "'", obj_Conn);

            MySqlDataReader obj_Reader = obj_Cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("Lp");
            dt.Columns.Add("Indeks");
            dt.Columns.Add("Nazwa");
            dt.Columns.Add("Ilość");
            dt.Columns.Add("Netto j.");
            dt.Columns.Add("Rabat");
            dt.Columns.Add("Narzut");
            dt.Columns.Add("Netto");
            dt.Columns.Add("Wartość netto");
            dt.Columns.Add("Wartość brutto");

            while (obj_Reader.Read())
            {
                DataRow row = dt.NewRow();
                row["Lp"] = dt.Rows.Count + 1;
                row["Indeks"] = obj_Reader["product_id"];
                row["Nazwa"] = obj_Reader["nazwa"];
                row["Ilość"] = obj_Reader["quantity"];
                row["Netto j."] = obj_Reader["netto"];
                row["Rabat"] = obj_Reader["rab"];
                row["Narzut"] = obj_Reader["nar"];
                decimal n = Decimal.Parse(obj_Reader["netto"].ToString());
                decimal r = Decimal.Parse(obj_Reader["rab"].ToString());
                decimal na = Decimal.Parse(obj_Reader["nar"].ToString());
                var npr = Math.Round(n - (n * (r / 100)), 2);
                var npn = Math.Round(n + (n * (na / 100)), 2);
                decimal v = Decimal.Parse("1,23");
                decimal i = Decimal.Parse(obj_Reader["quantity"].ToString());
                if(Decimal.Parse(obj_Reader["nar"].ToString()) > 0)
                {
                    row["Netto"] = npn;
                    var wn = Math.Round(npn * i, 2);
                    var wb = Math.Round(wn * v, 2);
                    row["Wartość netto"] = string.Format("{0:c}", wn);
                    row["Wartość brutto"] = string.Format("{0:c}", wb);
                }
                else
                {
                    row["Netto"] = npr;
                    var wn = Math.Round(npr * i, 2);
                    var wb = Math.Round(wn * v, 2);
                    row["Wartość netto"] = string.Format("{0:c}", wn);
                    row["Wartość brutto"] = string.Format("{0:c}", wb);
                }
                dt.Rows.Add(row);
                //klient.Text = obj_Reader["klient"].ToString();
            }
            obj_Conn.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 35;
            dataGridView1.Columns[1].Width = 200;
        }
    }
}
