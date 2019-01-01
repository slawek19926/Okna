using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using System.Security.Cryptography;
using System.IO;
using MySql.Data.MySqlClient;

namespace Okna.print
{
    public partial class wycena_prt : Form
    {
        private wycena wycena;
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

        public wycena_prt(wycena wycena,Form1 Form1)
        {
            this.wycena = wycena;
            this.Form1 = Form1;
            InitializeComponent();
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
        }
        private void wycena_prt_Load(object sender, EventArgs e)
        {
            GetData();
            GetClient();
            GetDataCompany();
            GetUser();
            Parameters();
            reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        void GetData()
        {
            string fblocation = AppDomain.CurrentDomain.BaseDirectory + "DBAPP.fdb";
            wyc_tmp tmp = new wyc_tmp();
            string cs = @"UserID=SYSDBA;Password=masterkey;Database=" + fblocation + "";
            FbConnection cn = new FbConnection(cs);
            FbDataAdapter da = new FbDataAdapter("SELECT * FROM wyc_temp", cn);
            da.Fill(tmp, tmp.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("wyc_temp", tmp.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.Refresh();
        }

        void Parameters()
        {
            ReportParameter[] param = new ReportParameter[]
            {
                new ReportParameter("kwotaSlownie", wycena.slownie.Text),
            };
            reportViewer1.LocalReport.SetParameters(param);
        }

        void ConnSettings()
        {
            var MyIni = new INIFile("WektorSettings.ini");
            server = MyIni.Read("server", "Okna");
            database = MyIni.Read("database", "Okna");
            uid = MyIni.Read("login", "Okna");
            password = Decrypt(MyIni.Read("pass", "Okna"));
        }

        void GetClient()
        {
            ConnSettings();

            wyc_details wd = new wyc_details();
            string cs = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "Convert Zero Datetime = True;";
            MySqlConnection cn = new MySqlConnection(cs);
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM klienci WHERE nazwa = '" + wycena.klientTXT.Text + "' AND is_deleted = 0", cn);
            da.Fill(wd, wd.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("klient", wd.Tables[0]);
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
    }
}
