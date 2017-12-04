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
using Npgsql;

namespace Okna.produkcja
{
    public partial class zlec_det : Form
    {
        private prod_zlec prod_zlec;
        public zlec_det(prod_zlec prod_zlec)
        {
            InitializeComponent();
            this.prod_zlec = prod_zlec;
            Text = "Zlecenie nr: " + prod_zlec.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            FormBorderStyle = FormBorderStyle.Fixed3D;
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

        private void zlec_det_Load(object sender, EventArgs e)
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
            NpgsqlCommand obj_Cmd = new NpgsqlCommand("SELECT td.numberindoc as poz,td.ne as ne,td.quantity as quantity,td.discountrate as discountrate,td.width as width,td.height as height," +
                "COALESCE (get_td_value_sale_netto(idxtradedoc), '0.00') AS wart, " +
                "COALESCE (get_td_vatrates_projects_str(idxtradedoc), '0.00') AS vat, " +
                "COALESCE (get_td_value_sale_brutto(idxtradedoc), '0.00') AS brutto," +
                "td.idxtradedocitem AS item,pro.ne as kolor " +
                "FROM tradedocsitems td " +
                "LEFT JOIN profilecolors pro ON (td.idxprofilecolor = pro.idxprofilecolor) " +
                "WHERE idxtradedoc = '" + prod_zlec.dataGridView1.CurrentRow.Cells[13].Value.ToString() + "' AND cnttradedocitem = 1 " +
                "ORDER BY numberindoc ASC", obj_Conn);

            NpgsqlDataReader obj_Reader = obj_Cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("Lp");
            dt.Columns.Add("Nazwa");
            dt.Columns.Add("Ilość");
            dt.Columns.Add("Rabat");
            dt.Columns.Add("Szerokość");
            dt.Columns.Add("Wysokość");
            dt.Columns.Add("Wartość netto");
            dt.Columns.Add("VAT");
            dt.Columns.Add("Wartość brutto");
            dt.Columns.Add("item");
            dt.Columns.Add("Kolor");

            while (obj_Reader.Read())
            {
                DataRow row = dt.NewRow();
                row["Lp"] = obj_Reader["poz"];
                row["Nazwa"] = obj_Reader["ne"];
                row["Ilość"] = obj_Reader["quantity"] + " szt.";
                row["Rabat"] = obj_Reader["discountrate"] + " %";
                row["Szerokość"] = obj_Reader["width"] + " mm";
                row["Wysokość"] = obj_Reader["height"] + " mm";
                row["Wartość netto"] = string.Format("{0:c}", obj_Reader["wart"]);
                row["VAT"] = obj_Reader["vat"] + " %";
                row["Wartość brutto"] = string.Format("{0:c}", obj_Reader["brutto"]);
                row["item"] = obj_Reader["item"];
                row["Kolor"] = obj_Reader["kolor"];
                dt.Rows.Add(row);
            }
            obj_Conn.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns["item"].Visible = false;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Rows[0].Selected = true;
            img_Load(e, e);
        }

        private void img_Load(object sender, EventArgs e)
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

            NpgsqlCommand cmd = new NpgsqlCommand("select emf as rys FROM tradedocsitems where idxtradedocitem = '" + dataGridView1.CurrentRow.Cells["item"].Value + "'", obj_Conn);
            obj_Conn.Open();
            NpgsqlDataReader dr;

            try
            {
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["rys"];
                    MemoryStream ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                    pictureBox1.Image = Image.FromStream(ms);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                obj_Conn.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
            NpgsqlCommand obj_Cmd = new NpgsqlCommand("SELECT td.ne as ne,value_sale,amount, " +
                "(value_sale+(value_sale*(taxrate/100))) as value_brutto " +
                "FROM tradedocsitems td " +
                "WHERE idxtradedocitem_parent = '" + dataGridView1.CurrentRow.Cells["item"].Value + "' and isinsideparentprice is false", obj_Conn);

            NpgsqlDataReader obj_Reader = obj_Cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("Nazwa");
            dt.Columns.Add("Ilość");
            dt.Columns.Add("Netto");
            dt.Columns.Add("Brutto");

            while (obj_Reader.Read())
            {
                DataRow row = dt.NewRow();                
                row["Nazwa"] = obj_Reader["ne"];
                row["Ilość"] = obj_Reader["amount"];
                row["Netto"] = string.Format("{0:c}", obj_Reader["value_sale"]);
                row["Brutto"] = string.Format("{0:c}", obj_Reader["value_brutto"]);
                dt.Rows.Add(row);
            }
            dataGridView2.DataSource = dt;
            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            img_Load(e, e);
        }
    }
}
