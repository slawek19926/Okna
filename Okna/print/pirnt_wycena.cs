using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Reporting.WinForms;

namespace Okna.print
{
    public partial class pirnt_wycena : Form
    {
        private wyceny wyceny;
        private Form1 Form1;
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

        public pirnt_wycena(wyceny wyceny, Form1 Form1)
        {
            this.wyceny = wyceny;
            this.Form1 = Form1;
            InitializeComponent();
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
        }

        private void pirnt_wycena_Load(object sender, EventArgs e)
        {
            GetDataDetails();
            GetDataCompany();
            GetClient();
            GetUser();
            Parameters();
            reportViewer1.RefreshReport();
        }

        void ConnSettings()
        {
            var MyIni = new INIFile("WektorSettings.ini");
            server = MyIni.Read("server", "Okna");
            database = MyIni.Read("database", "Okna");
            uid = MyIni.Read("login", "Okna");
            password = Decrypt(MyIni.Read("pass", "Okna"));
        }

        void GetDataDetails()
        {
            ConnSettings();

            string id = wyceny.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string user_id = Form1.logged.Text;

            wyc_details wd = new wyc_details();
            string cs = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "Convert Zero Datetime = True;";
            MySqlConnection cn = new MySqlConnection(cs);
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT b.nazwa as nazwa,b.reference as indeks,a.ilosc as ilosc,a.cena as cena,a.rabat as rabat" +
                " FROM wyceny_detail a" +
                " LEFT JOIN cenniki b ON (a.id_product = b.id)" +
                " WHERE a.wycena_user = '" + wyceny.dataGridView1.CurrentRow.Cells[8].Value + "' AND a.user_id = (SELECT id FROM uzytkownicy WHERE username = '" + user_id + "')", cn);
            da.Fill(wd, wd.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("wyc_det", wd.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
        }

        void GetDataCompany()
        {
            ConnSettings();

            wyc_details wd = new wyc_details();
            string cs = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "Convert Zero Datetime = True;";
            MySqlConnection cn = new MySqlConnection(cs);
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM firma", cn);
            da.Fill(wd, wd.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("firma_det", wd.Tables[0]);
            reportViewer1.LocalReport.DataSources.Add(rds);
        }

        void GetClient()
        {
            ConnSettings();
            string user_id = Form1.logged.Text;

            wyc_details wd = new wyc_details();
            string cs = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "Convert Zero Datetime = True;";
            MySqlConnection cn = new MySqlConnection(cs);
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT a.nazwa as nazwa,a.ulica as ulica,a.kod as kod,a.miasto as miasto,a.nip as nip FROM klienci a" +
                " LEFT JOIN wyceny b ON (a.id = b.klient)" +
                " WHERE b.wycena_user = '" + wyceny.dataGridView1.CurrentRow.Cells[8].Value + "' AND b.user_id = (SELECT id FROM uzytkownicy WHERE username = '" + user_id + "')", cn);
            da.Fill(wd, wd.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("klient_det", wd.Tables[0]);
            reportViewer1.LocalReport.DataSources.Add(rds);
        }

        void GetUser()
        {
            ConnSettings();
            string user_id = Form1.logged.Text;

            wyc_details wd = new wyc_details();
            string cs = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "Convert Zero Datetime = True;";
            MySqlConnection cn = new MySqlConnection(cs);
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT a.name as name FROM uzytkownicy a " +
                " LEFT JOIN wyceny b ON (b.user_id = a.id)" +
                " WHERE a.username  = '" + user_id + "'", cn);
            da.Fill(wd, wd.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("user", wd.Tables[0]);
            reportViewer1.LocalReport.DataSources.Add(rds);
        }

        void Parameters()
        {
            ReportParameter[] param = new ReportParameter[]
            {
                new ReportParameter("wystawiona", wyceny.dataGridView1.CurrentRow.Cells[4].Value.ToString()),
                new ReportParameter("wycenaNR" ,wyceny.dataGridView1.CurrentRow.Cells[1].Value.ToString()),
                new ReportParameter("sporzadzil",Form1.logged.Text),
            };
            reportViewer1.LocalReport.SetParameters(param);
        }
    }
}
