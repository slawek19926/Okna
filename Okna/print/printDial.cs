using Microsoft.Reporting.WinForms;
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
using System.IO;
using System.Security.Cryptography;

namespace Okna.print
{
    public partial class printDial : Form
    {
        wycena wycena;
        Form1 Form1;
        public printDial(wycena wycena,Form1 Form1)
        {
            InitializeComponent();
            this.wycena = wycena;
            this.Form1 = Form1;
        }

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

        private void printDial_Load(object sender, EventArgs e)
        {
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.PrinterSettings.Copies = 1;
            reportViewer1.PrinterSettings.Collate = true;
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;

            DataTable dt1 = new DataTable();

            var MyIni = new INIFile("WektorSettings.ini");
            server = MyIni.Read("server", "Okna");
            database = MyIni.Read("database", "Okna");
            uid = MyIni.Read("login", "Okna");
            password = Decrypt(MyIni.Read("pass", "Okna"));

            var connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM temp WHERE wycena_nr = " + wycena.numerek.Text + "";
                try
                {
                    MySqlDataAdapter adr = new MySqlDataAdapter(query, connection);
                    adr.SelectCommand.CommandType = CommandType.Text;
                    adr.Fill(dt1);
                }
                catch
                {

                }
                connection.Close();
            }
            DataTable dt2 = new DataTable();

            var connectionString2 = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (var connection2 = new MySqlConnection(connectionString2))
            {
                connection2.Open();
                var query2 = "SELECT * FROM firma";
                try
                {
                    MySqlDataAdapter adr2 = new MySqlDataAdapter(query2, connection2);
                    adr2.SelectCommand.CommandType = CommandType.Text;
                    adr2.Fill(dt2);
                }
                catch
                {

                }
                connection2.Close();
            }
            DataTable dt3 = new DataTable();

            var connectionString3 = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (var connection3 = new MySqlConnection(connectionString3))
            {
                connection3.Open();
                var query3 = "SELECT * FROM uzytkownice WHERE username = '" + Form1.logged.Text + "'";
                try
                {
                    MySqlDataAdapter adr3 = new MySqlDataAdapter(query3, connection3);
                    adr3.SelectCommand.CommandType = CommandType.Text;
                    adr3.Fill(dt3);
                }
                catch
                {

                }
                connection3.Close();
            }
            var data = DateTime.Now.ToString("dd/MM/yyyy");
            ReportParameter[] param = new ReportParameter[]
            {
                new ReportParameter("wycenaNR", wycena.wycena_nr.Text),
                new ReportParameter("wystawiona", data),
                //new ReportParameter("przygotowal", Form1.logged.Text)
            }; 
            reportViewer1.LocalReport.SetParameters(param);
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt1));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dt2));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", dt3));
            reportViewer1.RefreshReport();
        }
    }
}
