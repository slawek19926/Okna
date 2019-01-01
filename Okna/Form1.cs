using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Deployment.Application;
using System.IO;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using MaterialSkin;
using MaterialSkin.Controls;
using MetroFramework;
using MetroFramework.Controls;
using System.Net;
using AutoUpdaterDotNET;
using System.Xml;

namespace Okna
{
    public partial class Form1 : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string path = AppDomain.CurrentDomain.BaseDirectory + "\\logged.ext";
        public Form1()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            Text = "Wektor wersja " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            const string REGISTRY_KEY = @"HKEY_CURRENT_USER\Software\WektorApp";
            const string REGISTY_VALUE = "FirstRun";

            if (Convert.ToInt32(Microsoft.Win32.Registry.GetValue(REGISTRY_KEY, REGISTY_VALUE, 0)) == 0)
            {
                //komikat o potrzebie utworzenia nowego użytkownika
                MessageBox.Show("Teraz musisz utworzyć użytkownika i hasło!!!");

                formsy.settings form = new formsy.settings(this);
                form.MdiParent = this;
                form.Show();
                Microsoft.Win32.Registry.SetValue(REGISTRY_KEY, REGISTY_VALUE, 1, Microsoft.Win32.RegistryValueKind.DWord);
            }
        }

        private void fORM1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wycena form = new wycena(this);
            //form.MdiParent = this;
            form.Show();
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var res = MetroMessageBox.Show(this, "Napewno chcesz zakończyć działanie aplikacji?","Zakończyć?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(res == DialogResult.Yes)
            {
                zapis_login();
                Environment.Exit(0);
            }
            else
            {

            }
        }

        private void wycenyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wyceny form = new wyceny(this);
            //form.MdiParent = this;
            form.Show();
        }

        private void zamówieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zamowienia form = new zamowienia(this);
            //form.MdiParent = this;
            form.Show();
        }

        private void ustawieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formsy.settings form = new formsy.settings(this);
            //form.MdiParent = this;
            form.Show();
        }

        private void zleceniaProdukcyjneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            produkcja.prod_zlec form = new produkcja.prod_zlec();
            //form.MdiParent = this;
            form.Show();
        }

        private void oProgramiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About form = new About();
            form.Show();
        }

        private void rysujOknotestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            test.newWindow form = new test.newWindow(this);
            //form.MdiParent = this;
            form.Show();
        }

        private void fakturyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finanse.faktury form = new finanse.faktury(this);
            //form.MdiParent = this;
            form.Show();
        }

        private void katalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            okucia.katalog form = new okucia.katalog(this);
            //form.MdiParent = this;
            form.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmLogin f = new frmLogin();
            var res = MetroMessageBox.Show(this, "Napewno chcesz zakończyć działanie aplikacji?", "Zakończyć?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                zapis_login();
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void zapis_login()
        {
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Update uzytkownicy set logged='0' where username=@user";
            cmd.Parameters.AddWithValue("@user", logged.Text);
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
        }

        private void kontrahenciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finanse.klienci form = new finanse.klienci();
            form.Show();
        }

        private void fbFakturyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finanse.fb_faktury frm = new finanse.fb_faktury(this);
            frm.Show();
        }

        private void stwórzUżytkownikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createUser frm = new createUser(this);
            frm.Show();
        }

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        BindingSource source1 = new BindingSource();
        private string server;
        private string database;
        private string uid;
        private string password;
        private string connectionString;
        private MySqlConnection connect;
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

        private void db_connection()
        {
            try
            {
                var MyIni = new INIFile("WektorSettings.ini");
                server = MyIni.Read("server", "Okna");
                database = MyIni.Read("database", "Okna");
                uid = MyIni.Read("login", "Okna");
                password = Decrypt(MyIni.Read("pass", "Okna"));

                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "Convert Zero Datetime = True;";
                connect = new MySqlConnection(connectionString);
                connect.Open();
            }
            catch (MySqlException e)
            {
                throw;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AutoUpdater.Start("http://stylwektor.nazwa.pl/wektorssl/version.xml");
            AutoUpdater.ShowSkipButton = false;
            AutoUpdater.ShowRemindLaterButton = false;
            System.Timers.Timer timer = new System.Timers.Timer
            {
                Interval = 2 * 60 * 1000,
                SynchronizingObject = this
            };
            timer.Elapsed += delegate
            {
                AutoUpdater.Start("http://stylwektor.nazwa.pl/wektorssl/version.xml");
            };
            timer.Start();

            var MyIni = new INIFile("WektorSettings.ini");
            logged.Text = MyIni.Read("user", "logged");
            
            try
            {
                db_connection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "Select * from uzytkownicy where username=@user";
                cmd.Parameters.AddWithValue("@user", logged.Text);
                cmd.Connection = connect;
                MySqlDataReader login = cmd.ExecuteReader();

                while (login.Read())
                {
                    string l = login.GetString("admin");

                    if(l == "0")
                    {
                        narzędziaAdministratorToolStripMenuItem.Visible = true;
                        rysujOknotestToolStripMenuItem.Visible = true;
                        fbFakturyToolStripMenuItem.Visible = true;
                    }
                    else
                    {
                        narzędziaAdministratorToolStripMenuItem.Visible = true;
                        rysujOknotestToolStripMenuItem.Visible = true;
                        fORM1ToolStripMenuItem.Visible = true;
                        fbFakturyToolStripMenuItem.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rabatyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finanse.rabaty frm = new finanse.rabaty();
            frm.Show();
        }

        private void klienciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formsy.klienci frm = new formsy.klienci();
            frm.Show();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            plikStrip.Show(materialFlatButton1,0, materialFlatButton1.Height);
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            finanseStrip.Show(materialRaisedButton1, 0, materialRaisedButton1.Height);
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            towaryStrip.Show(materialRaisedButton2, 0, materialRaisedButton2.Height);
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            produkcjaStrip.Show(materialRaisedButton3, 0, materialRaisedButton3.Height);
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            konfiguracjaStrip.Show(materialRaisedButton4, 0, materialRaisedButton4.Height);
        }

        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {
            pomocStrip.Show(materialRaisedButton5, 0, materialRaisedButton5.Height);
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            wycena form = new wycena(this);
            form.Show();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            wyceny form = new wyceny(this);
            form.Show();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            zamowienia form = new zamowienia(this);
            form.Show();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            okucia.katalog form = new okucia.katalog(this);
            form.Show();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            finanse.fb_faktury frm = new finanse.fb_faktury(this);
            frm.Show();
        }
    }
}
