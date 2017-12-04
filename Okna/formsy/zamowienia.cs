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

namespace Okna
{
    public partial class zamowienia : Form
    {
        public zamowienia()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }
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
        
        private void zamowienia_Load(object sender, EventArgs e)
        {
            var MyIni = new INIFile("WektorSettings.ini");
            server = MyIni.Read("server", "Okna");
            database = MyIni.Read("database", "Okna");
            uid = MyIni.Read("login", "Okna");
            password = Decrypt(MyIni.Read("pass", "Okna"));
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "Convert Zero Datetime = True;";
            MySqlConnection obj_Conn = new MySqlConnection();
            obj_Conn.ConnectionString = connectionString;

            obj_Conn.Open();
            MySqlCommand obj_Cmd = new MySqlCommand("SELECT zamowienie_id as nrw,c.id as id_k,c.nazwa as klient,wartosc,data,b.name as stat,pay,uwagi,data_r, " +
                "case " +
                "when przyjol = 'alleg' then 'Allegro' " +
                "when przyjol = 'Wojtekdu' then 'Wojciech Dukaczewski'" +
                "end as przyjol " +
                "FROM zamowienia a " +
                "LEFT JOIN status b ON(a.status = b.id)" +
                "LEFT JOIN klienci c ON(a.klient_id = c.id)", obj_Conn);

            MySqlDataReader obj_Reader = obj_Cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Lp");
            dt.Columns.Add("Numer");
            dt.Columns.Add("Klient");
            dt.Columns.Add("Wartość netto");
            dt.Columns.Add("Z dnia");
            dt.Columns.Add("Ostatnia zmiana");
            dt.Columns.Add("Status");
            dt.Columns.Add("Uwagi");
            dt.Columns.Add("Przyjmujący");
            dt.Columns.Add("id");
            dt.Columns.Add("idk");
            dt.Columns.Add("idz");

            while (obj_Reader.Read())
            {
                DataRow row = dt.NewRow();
                var data = Convert.ToDateTime(obj_Reader["data"]);
                var rea = Convert.ToDateTime(obj_Reader["data_r"]);
                var user = Environment.UserName;
                row["Lp"] = dt.Rows.Count + 1;
                row["Numer"] = "zam/" + obj_Reader["nrw"] + "/" + data.ToString("MM") + "/" + data.ToString("yyyy");
                row["Klient"] = obj_Reader["klient"];
                row["Wartość netto"] = string.Format("{0:c}", obj_Reader["wartosc"]);
                row["Z dnia"] = data.ToString("dd/MM/yyyy");
                row["Status"] = obj_Reader["stat"];
                row["Uwagi"] = obj_Reader["uwagi"];
                row["Ostatnia zmiana"] = rea.ToString("dd") + "/" + rea.ToString("MM") + "/" + rea.ToString("yyyy");
                row["Przyjmujący"] = obj_Reader["przyjol"];
                row["id"] = obj_Reader["nrw"];
                row["idk"] = obj_Reader["id_k"];
                row["idz"] = obj_Reader["nrw"];
                dt.Rows.Add(row);
            }
            obj_Conn.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[5].Width = 80;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[9].Visible = false;
        }

        private void changeStat_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[6].Value.ToString() == "Zrealizowane")
            {
                MessageBox.Show("Nie można zmienić statusu zrealizowanego zamówienia", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                formsy.statusForm form = new formsy.statusForm(this);
                form.Show();
            }
        }
        public void PerformRefresh(object sender, EventArgs e)
        {
            //zapisujemy pozycję
            var MyIni = new INIFile("WektorSettings.ini");
            int index = dataGridView1.CurrentRow.Index;
            MyIni.Write("poz", index.ToString(),"poz");
            //przeładowywujemy tabelę
            zamowienia_Load(e, e);
            //odczytujemy pozycję
            var poz = Convert.ToInt32(MyIni.Read("poz", "poz"));
            dataGridView1.FirstDisplayedScrollingRowIndex = poz;
            dataGridView1.Rows[0].Selected = false;
            dataGridView1.Rows[poz].Selected = true;
            MyIni.DeleteSection("poz");
        }

        private void detailBtn_Click(object sender, EventArgs e)
        {
            formsy.zam_detail form = new formsy.zam_detail(this);
            form.Show();
        }

        private void wystawFV_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[6].Value.ToString() != "Zrealizowane")
            {
                MessageBox.Show("Nie można wystawić faktury jeśli zamówienie nie jest zrealizowane", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var MyIni = new INIFile("WektorSettings.ini");
                server = MyIni.Read("server", "Okna");
                database = MyIni.Read("database", "Okna");
                uid = MyIni.Read("login", "Okna");
                password = Decrypt(MyIni.Read("pass", "Okna"));
                var przed = MyIni.Read("przed", "faktury");
                var zera = MyIni.Read("zera", "faktury");
                var data = DateTime.Now;
                var miesiac = data.ToString("MM");
                var rok = data.ToString("yyyy");
                var wyst = data.ToString("yyyy-MM-dd HH:mm:ss");
                var numer_z = dataGridView1.SelectedCells[1].Value.ToString();
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";Allow User Variables=True";
                try
                {
                    var klient = dataGridView1.SelectedCells[10].Value.ToString();
                    var id = dataGridView1.SelectedCells[11].Value.ToString();
                    var query1 = "INSERT INTO fakt SET nr = ''";
                    var query2 = "SET @ids = (SELECT MAX(id) FROM fakt);" +
                        "SET @name = (SELECT MAX(LPAD(id," + zera + ",0)) FROM fakt);" +
                        "SET @miesiac = " + miesiac + ";" +
                        "SET @rok = " + rok + ";" +
                        "SET @data = '" + wyst + "';" +
                        "SET @zam = '" + numer_z + "';" +
                        " UPDATE fakt SET nr = (@ids),klient='" + klient + "',numer=CONCAT('" + przed + "/',@name,'/',@miesiac,'/',@rok),pay='1',data=(@data),zam_nr=(@zam) WHERE id = @ids;";
                    var query3 = "SELECT id,quantity,cena FROM zam WHERE order_id = '" + id + "';";
                    using(var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        using(var cmd = new MySqlCommand(query1, connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        using(var cmd2 = new MySqlCommand(query2, connection))
                        {
                            cmd2.ExecuteNonQuery();
                        }
                        using(var cmd3 = new MySqlCommand(query3, connection))
                        {
                            DataTable tb = new DataTable();
                            tb.Columns.Add("1");
                            tb.Columns.Add("2");
                            tb.Columns.Add("3");
                            using(MySqlDataReader rdr = cmd3.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    DataRow row = tb.NewRow();
                                    row[0] = rdr[0];
                                    row[1] = rdr[1];
                                    row[2] = rdr[2];
                                    tb.Rows.Add(row);
                                }
                            }
                            foreach(DataRow r in tb.Rows)
                            {
                                cmd3.CommandText = "INSERT INTO fakt_det SET id_fakt=(SELECT MAX(nr) FROM fakt),id_product=" + r[0] + ",jm='1',ilosc='" + r[1] + "',netto='" + r[2].ToString().Replace(",",".") + "',vat='1'";
                                cmd3.ExecuteNonQuery();
                            }
                            MessageBox.Show("Faktura została utworzona pomyślnie", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        connection.Close();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
