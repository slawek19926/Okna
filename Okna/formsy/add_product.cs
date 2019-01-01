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
using System.Data.SqlClient;
using System.Data.Sql;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Okna
{
    public partial class add_product : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        DataSet ds = new DataSet();
        private wycena wycena;
        public add_product(wycena wycena)
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            
            this.wycena = wycena;
            Text = "Dodaj produkt";
        }

        private string server;
        private string database;
        private string uid;
        private string password;
        private MySqlConnection connection;
        private MySqlDataAdapter mySqlDataAdapter;
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
        private void add_product_Load(object sender, EventArgs e)
        {
            szukajka.DisplayMember = "Text";
            szukajka.ValueMember = "Value";
            var items = new[]
            {
                new { Text = "Indeks", Value = "reference" },
                new { Text = "Nazwa", Value = "nazwa" }
            };
            szukajka.DataSource = items;
            szukajka.SelectedIndex = 0;

            var MyIni = new INIFile("WektorSettings.ini");
            server = MyIni.Read("server", "Okna");
            database = MyIni.Read("database", "Okna");
            uid = MyIni.Read("login", "Okna");
            password = Decrypt(MyIni.Read("pass", "Okna"));
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            if (OpenConnection() == true)
            {
                mySqlDataAdapter = new MySqlDataAdapter("SELECT reference, nazwa, cena, rabat FROM cenniki", connection);
                //DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(ds);
                bindingSource1.DataSource = ds.Tables[0];
                dataGridView1.DataSource = bindingSource1;
                bindingNavigator1.BindingSource = bindingSource1;
                CloseConnection();
            }
            ds.Tables[0].Columns[0].ColumnName = "Indeks";
            ds.Tables[0].Columns[1].ColumnName = "Nazwa";
            dataGridView1.Columns[1].Width = 525;
            dataGridView1.Columns[2].DefaultCellStyle.Format = "c";
            
        }
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Nie można połączyć się z bazą danych. Skontaktuj się z administratorem!");
                        break;
                    case 1045:
                        MessageBox.Show("Nieprawidłowa nazwa użytkownika/hasło, spróbuj ponownie");
                        break;
                    default:
                        MessageBox.Show(ex.Message);
                        break;
                }
                return false;
            }
        }
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
            var indeks = dataGridView1.SelectedCells[0].Value.ToString();
            var nazwa = dataGridView1.SelectedCells[1].Value.ToString();
            var netto = dataGridView1.SelectedCells[2].Value.ToString();
            var rabat = dataGridView1.SelectedCells[3].Value.ToString();
            decimal r = Decimal.Parse(rabat);
            decimal n = Decimal.Parse(netto);
            var po_r =Math.Round( n-(n*(r/100)),2);
            var vat = "1,23";
            decimal v = Decimal.Parse(vat);
            var brutto = Math.Round(po_r*v, 2);
            object ile;
            string prompt;
            string title;
            string defaultIle;

            prompt = "Podaj ilość";
            title = "Wprowadź ilośc sztuk";
            defaultIle = "1";
            ile = Microsoft.VisualBasic.Interaction.InputBox(prompt, title, defaultIle);

            if(ile.ToString() == "")
            {
                ile = defaultIle;
            }

            Boolean found = false;
            foreach (DataGridViewRow rows in wycena.metroGrid1.Rows)
            {
                if (rows.Cells[0].Value.ToString() == indeks)
                {
                    found = true;
                    MessageBox.Show("Taki indeks już istnieje", "Wektor Okna", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
            }
            if (!found)
            {
                object[] row = new object[] { indeks, nazwa, netto, rabat + " %", po_r, ile };
                wycena.metroGrid1.Rows.Add(row);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                label1.Text = row.Cells[0].Value.ToString();
            }
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //szukanie po indeksie
                if (szukajka.SelectedIndex == 0)
                {
                    var bd = (BindingSource)dataGridView1.DataSource;
                    var dt = (DataTable)bd.DataSource;
                    dt.DefaultView.RowFilter = string.Format("Indeks like '%{0}%'", searchBox.Text.Trim().Replace("'", "''"));
                    dataGridView1.Refresh();
                }
                //szukanie po nazwie
                else if (szukajka.SelectedIndex == 1)
                {
                    var bd = (BindingSource)dataGridView1.DataSource;
                    var dt = (DataTable)bd.DataSource;
                    dt.DefaultView.RowFilter = string.Format("Nazwa like '%{0}%'", searchBox.Text.Trim().Replace("'", "''"));
                    dataGridView1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button1_Click(e,e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formsy.spoza_katalogu frm = new formsy.spoza_katalogu(this,wycena);
            frm.Show();
            Close();
        }
    }
}
