using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.IO;
using System.Security.Cryptography;

namespace Okna.produkcja
{
    public partial class prod_zlec : Form
    {
        public prod_zlec()
        {
            InitializeComponent();
            Text = "Zlecenia produkcyjne";
            WindowState = FormWindowState.Maximized;
        }
        private string server;
        private string database;
        private string uid;
        private string password;
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

        private void prod_zlec_Load(object sender, EventArgs e)
        {
            var MyIni = new INIFile("WektorSettings.ini");
            server = MyIni.Read("server", "postgres");
            port = MyIni.Read("port", "postgres");
            uid = MyIni.Read("user", "postgres");
            password = Decrypt(MyIni.Read("pass", "postgres"));
            database = MyIni.Read("db", "postgres");
            string connectionString;
            connectionString = String.Format("Server={0};Port={1};" +
                    "User Id={2};Password={3};Database={4};",
                    server, port, uid,
                    password, database);
            NpgsqlConnection obj_Conn = new NpgsqlConnection();
            obj_Conn.ConnectionString = connectionString;

            obj_Conn.Open();
            NpgsqlCommand obj_Cmd = new NpgsqlCommand("SELECT td.idxtradedoc as id,td.tradedocid AS zlec, td.datecreation AS utw, " +
                "CASE " +
                "WHEN td.cntorderstatus = 1 THEN 'Otwarte' " +
                "WHEN td.cntorderstatus = 2 THEN 'Zablokowane' " +
                "WHEN td.cntorderstatus = 3 THEN 'W produkcji' " +
                "WHEN td.cntorderstatus = 4 THEN 'Ukończone' " +
                "END AS status, " +
                "td.datestartingproduction AS start,td.datefinishingproduction AS finish, " +
                "COALESCE (get_td_value_sale_netto(td.idxtradedoc), '0.00') AS wart, " +
                "COALESCE (get_td_vatrates_projects_str(td.idxtradedoc), '0.00') AS vat, " +
                "COALESCE (get_td_value_sale_brutto(td.idxtradedoc), '0.00') AS brutto, " +
                "tdi_m.quantity AS ilosc, td.reference AS ref, cli.ne AS klient, usr.ne AS user " +
                "FROM tradedocs td " +
                "LEFT JOIN clients cli ON (cli.idxclient = td.idxclient) " +
                "LEFT JOIN users usr ON (usr.idxuser = td.idxuser_master) " +
                "LEFT JOIN (" +
                "   SELECT SUM(quantity) as quantity,tdi.idxtradedoc " +
                "   FROM tradedocsitems tdi " +
                "   LEFT JOIN tradedocs td ON (td.idxtradedoc = tdi.idxtradedoc) " +
                "   WHERE td.cntdoctype = 3 AND cnttradedocitem = 1 " +
                "   GROUP BY tdi.idxtradedoc " +
                ") tdi_m ON (tdi_m.idxtradedoc = td.idxtradedoc) " +
                "WHERE td.cntdoctype = 3 " +
                "ORDER BY td.datecreation DESC ", obj_Conn);

            NpgsqlDataReader obj_Reader = obj_Cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("Lp");
            dt.Columns.Add("Numer");
            dt.Columns.Add("Utworzono");
            dt.Columns.Add("Rozpoczęto");
            dt.Columns.Add("Zakończono");
            dt.Columns.Add("Status");
            dt.Columns.Add("Wprowadził");
            dt.Columns.Add("Klient");
            dt.Columns.Add("Referencje");
            dt.Columns.Add("Sztuk");
            dt.Columns.Add("Wartość netto");
            dt.Columns.Add("VAT");
            dt.Columns.Add("Wartość brutto");
            dt.Columns.Add("id");

            while (obj_Reader.Read())
            {
                DataRow row = dt.NewRow();
                var data = Convert.ToDateTime(obj_Reader["utw"]);
                if(obj_Reader["start"] == DBNull.Value)
                {
                    row["Rozpoczęto"] = "Jeszcze nie rozpoczęto";
                }
                else
                {
                    var start = Convert.ToDateTime(obj_Reader["start"]);
                    row["Rozpoczęto"] = start.ToString("dd") + "/" + start.ToString("MM") + "/" + start.ToString("yyyy");
                }
                if(obj_Reader["finish"] == DBNull.Value)
                {
                    row["Zakończono"] = "Jeszcze nie ukończono";
                }
                else
                {
                    var finish = Convert.ToDateTime(obj_Reader["finish"]);
                    row["Zakończono"] = finish.ToString("dd") + "/" + finish.ToString("MM") + "/" + finish.ToString("yyyy");
                }
                row["Lp"] = dt.Rows.Count + 1;
                row["Numer"] = obj_Reader["zlec"];
                row["Utworzono"] = data.ToString("dd") + "/" + data.ToString("MM") + "/" + data.ToString("yyyy");
                row["Status"] = obj_Reader["status"];
                row["Wprowadził"] = obj_Reader["user"];
                row["Klient"] = obj_Reader["klient"];
                row["Referencje"] = obj_Reader["ref"];
                row["Sztuk"] = obj_Reader["ilosc"] + " szt.";
                row["Wartość netto"] = string.Format("{0:c}", obj_Reader["wart"]);
                row["VAT"] = obj_Reader["vat"] + " %";
                row["Wartość brutto"] = string.Format("{0:c}", obj_Reader["brutto"]);
                row["id"] = obj_Reader["id"];
                dt.Rows.Add(row);
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void det_BTN_Click(object sender, EventArgs e)
        {
            zlec_det form = new zlec_det(this);
            form.Show();
        }
    }
}
