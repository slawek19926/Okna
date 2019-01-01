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

namespace Okna
{
    public partial class wyceny : Form
    {
        Form1 Form1;
        public wyceny(Form1 Form1)
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            this.Form1 = Form1;
        }
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
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

        public void wyceny_Load(object sender, EventArgs e)
        {
            var MyIni = new INIFile("WektorSettings.ini");
            server = MyIni.Read("server", "Okna");
            database = MyIni.Read("database", "Okna");
            uid = MyIni.Read("login", "Okna");
            password = Decrypt(MyIni.Read("pass", "Okna"));
            var zera = MyIni.Read("zera", "wyceny");
            var przed = MyIni.Read("przed", "wyceny");

            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "Convert Zero Datetime = True;" ;
            MySqlConnection obj_Conn = new MySqlConnection();
            obj_Conn.ConnectionString = connectionString;

            obj_Conn.Open();
            string zalogowany = Form1.logged.Text;
            if(zalogowany == "admin")
            {
                MySqlCommand obj_Cmd = new MySqlCommand("SELECT nrw as nra,numer,lpad(nrw," + zera + ",0) as nrw,data,b.id as idk,b.nazwa as klient,kwota," +
                "case " +
                "when realizacja = '0' then 'Nie' " +
                "when realizacja = '1' then 'Tak' " +
                "END AS gotowe " +
                "FROM wyceny a " +
                "LEFT JOIN klienci b ON (a.klient = b.id) WHERE a.zamow = '0'", obj_Conn);

                MySqlDataReader obj_Reader = obj_Cmd.ExecuteReader();

                dt.Columns.Add("Lp");
                dt.Columns.Add("Numer");
                dt.Columns.Add("Klient");
                dt.Columns.Add("Wartość brutto");
                dt.Columns.Add("Z dnia");
                dt.Columns.Add("Zrealizowano");
                dt.Columns.Add("Data realizacji");
                dt.Columns.Add("id_klient");
                dt.Columns.Add("add");

                while (obj_Reader.Read())
                {
                    DataRow row = dt.NewRow();
                    int index = dt.Rows.Count + 1;
                    var data = Convert.ToDateTime(obj_Reader["data"]);
                    var real = obj_Reader["gotowe"];
                    row["Lp"] = index;
                    row["Numer"] = obj_Reader["numer"];
                    row["Klient"] = obj_Reader["klient"];
                    row["Wartość brutto"] = string.Format("{0:c}", obj_Reader["kwota"]);
                    row["Z dnia"] = data.ToString("dd") + "/" + data.ToString("MM") + "/" + data.ToString("yyyy");
                    row["Zrealizowano"] = real;
                    if (real.ToString() == "Nie")
                    {
                        row["Data realizacji"] = "";
                    }
                    else
                    {
                        row["Data realizacji"] = data;
                    }
                    row[7] = obj_Reader["idk"];
                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;
            }
            else
            {
                string klient = Form1.logged.Text;
                MySqlCommand obj_Cmd = new MySqlCommand("SELECT nrw as nra,numer,lpad(nrw," + zera + ",0) as nrw,data,b.id as idk,b.nazwa as klient,kwota,wycena_user," +
                "case " +
                "when realizacja = '0' then 'Nie' " +
                "when realizacja = '1' then 'Tak' " +
                "END AS gotowe " +
                "FROM wyceny a " +
                "LEFT JOIN klienci b ON (a.klient = b.id) WHERE a.zamow = '0' and user_id = (SELECT id FROM uzytkownicy WHERE username = '" + klient + "') GROUP BY a.wyc_id", obj_Conn);

                MySqlDataReader obj_Reader = obj_Cmd.ExecuteReader();

                dt.Columns.Add("Lp");
                dt.Columns.Add("Numer");
                dt.Columns.Add("Klient");
                dt.Columns.Add("Wartość brutto");
                dt.Columns.Add("Z dnia");
                dt.Columns.Add("Zrealizowano");
                dt.Columns.Add("Data realizacji");
                dt.Columns.Add("id_klient");
                dt.Columns.Add("wycena_user");

                while (obj_Reader.Read())
                {
                    DataRow row = dt.NewRow();
                    int index = dt.Rows.Count + 1;
                    var data = Convert.ToDateTime(obj_Reader["data"]);
                    var real = obj_Reader["gotowe"];
                    row["Lp"] = index;
                    row["Numer"] = obj_Reader["numer"];
                    row["Klient"] = obj_Reader["klient"];
                    row["Wartość brutto"] = string.Format("{0:c}", obj_Reader["kwota"]);
                    row["Z dnia"] = data.ToString("dd") + "/" + data.ToString("MM") + "/" + data.ToString("yyyy");
                    row["Zrealizowano"] = real;
                    if (real.ToString() == "Nie")
                    {
                        row["Data realizacji"] = "";
                    }
                    else
                    {
                        row["Data realizacji"] = data;
                    }
                    row[7] = obj_Reader["idk"];
                    row[8] = obj_Reader["wycena_user"];
                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;
            }

            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
        }

        private void detailBTN_Click(object sender, EventArgs e)
        {
            try
            {
                var MyIni = new INIFile("WektorSettings.ini");
                server = MyIni.Read("server", "Okna");
                database = MyIni.Read("database", "Okna");
                uid = MyIni.Read("login", "Okna");
                password = Decrypt(MyIni.Read("pass", "Okna"));
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                using (var connection = new MySqlConnection(connectionString))
                {
                    var data = DateTime.Now;
                    var dat = data.ToString("yyyy-MM-dd HH:mm:ss");
                    var wart = dataGridView1.SelectedCells[3].Value.ToString().Replace(",",".");
                    var klient = dataGridView1.SelectedCells[7].Value.ToString();
                    var id = dataGridView1.SelectedCells[0].Value.ToString();
                    var query1 = "INSERT INTO zamowienia SET klient_id = '" + klient + "',wartosc = '" + wart + "',data = '" + dat + "',status='1',pay='1';";
                    var query2 = "SELECT a.id_product,b.reference,a.ilosc,a.cena,a.rabat FROM wyceny_detail a" +
                        " LEFT JOIN cenniki b ON(a.id_product = b.id)" +
                        " WHERE a.id ='" + id + "';";

                    connection.Open();

                    using (var command1 = new MySqlCommand(query1, connection))
                    {
                        command1.ExecuteNonQuery();
                        using (var com2 = new MySqlCommand(query2, connection))
                        {
                            DataTable tb = new DataTable();
                            tb.Columns.Add("id");
                            tb.Columns.Add("indeks");
                            tb.Columns.Add("ilosc");
                            tb.Columns.Add("cena");
                            tb.Columns.Add("rabat");

                            using (MySqlDataReader rdr = com2.ExecuteReader())
                            { 
                                while (rdr.Read())
                                {
                                    DataRow row = tb.NewRow();
                                    row[0] = rdr[0];
                                    row[1] = rdr[1];
                                    row[2] = rdr[2];
                                    row[3] = rdr[3].ToString().Replace(",",".");
                                    row[4] = rdr[4].ToString().Replace(",", ".");
                                    tb.Rows.Add(row);
                                }
                            }
                            foreach (DataRow r in tb.Rows)
                            {
                                com2.CommandText = "INSERT INTO zam SET order_id = (SELECT max(zamowienie_id) FROM zamowienia),id = '" + r[0] + "',product_id='" + r[1] + "'," +
                                    "quantity='" + r[2] + "',cena='" + r[3] + "',rabat='" + r[4] + "';";
                                com2.ExecuteNonQuery();
                            }
                            MessageBox.Show("Zamówienie utworzono pomyślnie!");
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void detailBTN_Click_1(object sender, EventArgs e)
        {
            formsy.wyceny_detail form = new formsy.wyceny_detail(this,Form1);
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            print.pirnt_wycena frm = new print.pirnt_wycena(this,Form1);
            frm.Show();
        }
    }
}
