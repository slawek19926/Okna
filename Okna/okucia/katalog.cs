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
using Npgsql;
using System.Net;

namespace Okna.okucia
{
    public partial class katalog : Form
    {
        private Form1 Form1;
        bool on = true;
        bool toggleLight = true;
        Timer t = new Timer();
        public katalog(Form1 Form1)
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            Text = "Katalog okuć";
            this.Form1 = Form1;
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

        public class ComboboxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void katalog_Load(object sender, EventArgs e)
        {

            fillterView.ExpandAll();
            systemPRO.ExpandAll();

            button1.Text = "Cena zakupu";
            t.Interval = 500;
            t.Tick += new EventHandler(t_Tick);

            searchMethod.DisplayMember = "Text";
            searchMethod.ValueMember = "Value";

            var items = new[]
            {
                new { Text = "Indeks", Value = "reference" },
                new { Text = "Nazwa", Value = "nazwa" }
            };
            searchMethod.DataSource = items;
            searchMethod.SelectedIndex = 0;

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
                MySqlCommand cmd = new MySqlCommand("SELECT a.id,a.reference,a.nazwa,a.cena,a.grupa,a.system,a.opis,b.ref,a.rabat FROM cenniki a " +
                    "LEFT JOIN rabaty b ON (b.id = a.gr) ORDER BY id ASC", conn);

                MySqlDataReader rdr = cmd.ExecuteReader();
               
                dt.Columns.Add("Lp");
                dt.Columns.Add("Indeks");
                dt.Columns.Add("Nazwa");
                dt.Columns.Add("Zakup netto");
                dt.Columns.Add("Grupa");
                dt.Columns.Add("Zakup brutto");
                dt.Columns.Add("Detal netto");
                dt.Columns.Add("Detal brutto");
                dt.Columns.Add("System");
                dt.Columns.Add("Opis");
                dt.Columns.Add("G rab");

                Bitmap b = new Bitmap(50, 15);
                using(Graphics g = Graphics.FromImage(b))
                {
                    g.DrawString("Loading...", Font, new SolidBrush(Color.Black), 0f, 0f);
                }
                while (rdr.Read())
                {
                    DataRow row = dt.NewRow();

                    decimal vat = Decimal.Parse("1,23");
                    decimal narzut = Decimal.Parse("1,2");
                    decimal netto = Decimal.Parse(rdr[3].ToString());
                    decimal brutto = Math.Round(netto * vat, 2);
                    decimal detaln = Math.Round(netto * narzut, 2);
                    decimal brutton = Math.Round(detaln * vat, 2);
                    decimal r = Decimal.Parse(rdr[8].ToString());
                    decimal zakupN = Math.Round(netto - (netto * (r / 100)), 2);
                    decimal zakupB = Math.Round(zakupN * vat, 2);
                    decimal klientN = Math.Round(zakupN * narzut, 2);
                    decimal klientB = Math.Round(klientN * vat, 2);

                    row[0] = rdr[0];
                    row[1] = rdr[1];
                    row[2] = rdr[2];
                    row[3] = string.Format("{0:c}",zakupN);
                    row[4] = rdr[4];
                    row[5] = string.Format("{0:c}", zakupB);
                    row[6] = string.Format("{0:c}", klientN);
                    row[7] = string.Format("{0:c}", klientB);
                    row[8] = rdr[5];
                    row[9] = rdr[6];
                    row[10] = rdr[7];

                    dt.Rows.Add(row);
                }
                source1.DataSource = dt;
                dataGridView1.DataSource = source1;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                if (on)
                {
                    dataGridView1.Columns[3].Visible = true;
                    dataGridView1.Columns[5].Visible = true;
                    dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[7].Visible = false;
                }
                
                //for (int i = 0; i < dataGridView1.Columns.Count - 1; i++)
                //{
                //    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //}
                //dataGridView1.Columns[dataGridView1.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                //for (int i = 0; i < dataGridView1.Columns.Count; i++)
                //{
                //    int colw = dataGridView1.Columns[i].Width;
                //    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //    dataGridView1.Columns[i].Width = colw;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void t_Tick(object sender, EventArgs e)
        {
            if (toggleLight)
            {
                button1.BackColor = Color.LimeGreen;
                toggleLight = false;
            }
            else
            {
                button1.BackColor = Color.Gray;
                toggleLight = true;
            }
        }
        //Szukajka
        private void searchText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //szukanie po indeksie
                if (searchMethod.SelectedIndex == 0)
                {
                    var bd = (BindingSource)dataGridView1.DataSource;
                    var dt = (DataTable)bd.DataSource;
                    dt.DefaultView.RowFilter = string.Format("Indeks like '%{0}%'", searchText.Text.Trim().Replace("'", "''"));
                    dataGridView1.Refresh();
                }
                //szukanie po nazwie
                else if (searchMethod.SelectedIndex == 1)
                {
                    var bd = (BindingSource)dataGridView1.DataSource;
                    var dt = (DataTable)bd.DataSource;
                    dt.DefaultView.RowFilter = string.Format("Nazwa like '%{0}%'", searchText.Text.Trim().Replace("'", "''"));
                    dataGridView1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        //Cena malejąco
        private void priceDesc_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Descending);
        }
        //Cena rosnąco
        private void priceAsc_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Ascending);
        }
        //Indeks malejąco
        private void indeksDesc_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
        }
        //Indeks rosnąco
        private void indeksAsc_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
        }
        //Nazwa Z-A
        private void nameDesc_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Descending);
        }
        //Nazwa A-Z
        private void nameAsc_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Ascending);
        }
        //Filtrowanie produktów
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                foreach (TreeNode childNode in e.Node.Nodes)
                {
                    childNode.Checked = e.Node.Checked;
                }
            }
            //filtr elementów
            string nazwaEl = string.Empty;
            foreach (TreeNode node in fillterView.Nodes)
            {

                if (node.Nodes.Count > 0)
                {
                    foreach (TreeNode childNode in node.Nodes)
                    {
                        if (childNode.Checked)
                        {
                            if (nazwaEl.Length > 0)
                            {
                                nazwaEl += " OR ";
                            }
                            nazwaEl += "[Grupa] LIKE '%" + childNode.Text + "%'";
                        }
                    }
                }
            }
            //filtr systemu profili
            string sysPro = string.Empty;
            foreach (TreeNode node in systemPRO.Nodes)
            {
                if (node.Nodes.Count > 0)
                {
                    foreach (TreeNode childNode in node.Nodes)
                    {
                        if (childNode.Checked)
                        {
                            if (sysPro.Length > 0)
                            {
                                sysPro += " OR ";
                            }
                            sysPro += "[System] LIKE '%" + childNode.Text + "%'";
                        }
                    }
                }
            }

            string rowFilter = string.Empty;
            if (nazwaEl.Length > 0)
            {
                nazwaEl = "(" + nazwaEl + ")";
                rowFilter = nazwaEl;
            }
            if (sysPro.Length > 0)
            {
                sysPro = "(" + sysPro + ")";
                if (rowFilter.Length > 0)
                {
                    rowFilter += " AND " + sysPro;
                }
                else
                {
                    rowFilter = sysPro;
                }
            }

            try
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = rowFilter;
                dataGridView1.DataSource = dv;
            }
            catch (Exception)
            {
                MessageBox.Show("Nie działa");
            }
        }
        //Zaznaczenie wszystkich filtrów pojedyńczo
        public void CheckAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = true;
                CheckChildren(node, true);
            }
        }
        //Odznaczenie wszystkich filtrów pojedyńczo
        public void UncheckAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = false;
                CheckChildren(node, false);
            }
        }
        //Zaznaczenie wszystkiego
        private void CheckChildren(TreeNode rootNode, bool isChecked)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                CheckChildren(node, isChecked);
                node.Checked = isChecked;
            }
        }
        //Zmiana cen klient / zakup
        private void button1_Click(object sender, EventArgs e)
        {
            if (on)
            {
                button1.Text = "Ceny dla klienta";
                Text = "Katalog okuć - tryb klienta";
                t.Start();
                on = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = true;
                dataGridView1.Columns[7].Visible = true;
            }
            else
            {
                button1.Text = "Ceny zakupu";
                Text = "Katalog okuć";
                button1.BackColor = Color.Gray;
                t.Stop();
                on = true;
                dataGridView1.Columns[3].Visible = true;
                dataGridView1.Columns[5].Visible = true;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
            }
        }
        //Szczegóły produktu
        private void detailBTN_Click(object sender, EventArgs e)
        {
            try
            {
                detailPROD form = new detailPROD(this);
                form.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
