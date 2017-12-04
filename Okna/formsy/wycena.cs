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

    public partial class wycena : Form
    {
        string strCurrency;
        Boolean acceptableKey = false;
        public wycena()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            Text = "Wycena " + klientTXT.Text;
            FormClosing += wycena_FormClosing;

            double sum = 0;
            for (int i =0; i < dataGridView1.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[7].Value);
            }
            sumaTXT.Text = sum.ToString("c");
            editBTN.Enabled = false;
            deleteBTN.Enabled = false;
            saveBTN.Enabled = false;
            print_btn.Enabled = false;
            InitTimer();
        }
        private Timer tajmer;
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
        private void wycena_Load(object sender, EventArgs e)
        {
            //pobiera numer wyceny
            var MyIni = new INIFile("WektorSettings.ini");
            server = MyIni.Read("server", "Okna");
            database = MyIni.Read("database", "Okna");
            uid = MyIni.Read("login", "Okna");
            password = Decrypt(MyIni.Read("pass", "Okna"));

            var connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT lpad(max(id+1)," + MyIni.Read("zera","wyceny") + ",0) as numer FROM wyceny_detail";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        //wycena numer
                        while (reader.Read())
                        {
                            if (MyIni.Read("przed","wyceny") == "")
                            {
                                wycena_nr.Text = "Wycena nr: wyc/" + reader.GetString("numer") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Today.Year;
                            }
                            else
                            {
                                wycena_nr.Text = "Wycena nr: " + MyIni.Read("przed", "wyceny") + "/" + reader.GetString("numer") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Today.Year;
                            }
                            numerek.Text = reader.GetString("numer");
                        }
                    }
                }
            }


            dataGridView1.Columns[1].Width = 325;
            dataGridView1.Columns[2].DefaultCellStyle.Format = "c";
            decimal kwota = Convert.ToDecimal(suma());
            int zlote = (int)kwota;
            int grosze = (int)(100 * kwota) % 100;

            slownie.Text = String.Format("{0} {1} {2} {3}",
                Formatowanie.LiczbaSlownie(zlote),
                Formatowanie.WalutaSlownie(zlote, "złote"),
                Formatowanie.LiczbaSlownie(grosze),
                Formatowanie.WalutaSlownie(grosze, "grosze"));

            if(klientTXT.Text == "")
            {
                userBTN.Text = "Wybierz klienta";
            }
            else
            {
                userBTN.Text = "Zmień klienta";
            }
            
        }
        private void wycena_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (DialogResult == DialogResult.Cancel)
            {
                switch (MessageBox.Show(this, "Jesteś pewnien?","Czekaj...",MessageBoxButtons.YesNo,MessageBoxIcon.Question))
                {
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }
            }
        }
        public void InitTimer()
        {
            tajmer = new Timer();
            tajmer.Tick += new EventHandler(tajmer_tick);
            tajmer.Interval = 1000;
            tajmer.Start();
        }
        private void tajmer_tick(object sender, EventArgs e)
        {
            wyliczenie_kwoty();
        }
        public void wyliczenie_kwoty()
        {
            sumaTXT.Text = suma().ToString("c");

            decimal kwota = Convert.ToDecimal(suma());
            int zlote = (int)kwota;
            int grosze = (int)(100 * kwota) % 100;

            slownie.Text = String.Format("{0} {1} {2} {3}",
                Formatowanie.LiczbaSlownie(zlote),
                Formatowanie.WalutaSlownie(zlote, "złote"),
                Formatowanie.LiczbaSlownie(grosze),
                Formatowanie.WalutaSlownie(grosze, "grosze"));
        }
        private void klients_TextChanged(object sender, EventArgs e)
        {
            if (klientTXT.Text == "")
            {
                userBTN.Text = "Wybierz klienta";
            }
            else
            {
                userBTN.Text = "Zmień klienta";
                Text = "Wycena: " + klientTXT.Text ;
            }
        }
        private void addBTN_Click(object sender, EventArgs e)
        {
            add_product form = new add_product(this);
            form.Show();
            int i = 0;
            progressBar.Visible = true;
            progressBar.Value = 0;
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Step = 10;

            for (i=0; i <= 100; ++i)
            {
                progressBar.PerformStep();
            }
        }

        private void editBTN_Click(object sender, EventArgs e)
        {
            formsy.edit_towar form = new formsy.edit_towar(this);
            form.Show();
        }
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for(int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                decimal n = Decimal.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString().Replace("zł","")); //netto
                decimal ile = Decimal.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()); //ilość
                decimal vat = Decimal.Parse("1,23");
                dataGridView1.Rows[i].Cells[6].Value = n * ile;
                dataGridView1.Rows[i].Cells[7].Value = Math.Round((n*ile)*vat,2);
            }
            if (dataGridView1.RowCount >= 1)
            {
                editBTN.Enabled = true;
                deleteBTN.Enabled = true;
                saveBTN.Enabled = true;
                print_btn.Enabled = true;
            }
            wyliczenie_kwoty();
        }
        public void DynList_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (ChangedRow == true)
            {
                //numeracja kolumn
                //0 - indeks
                //1 - nazwa
                //2 - netto przed rabatem
                //3 - rabat %
                //4 - netto po rabacie
                //5 - ilość
                //6 - wartość netto
                //7 - wartość brutto
                ChangedRow = false;
                decimal n = Decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()); //netto
                decimal r = Decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString().Replace("%","")); //rabat
                decimal szt = Decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()); //ilość
                decimal vat = Decimal.Parse("1,23"); 
                dataGridView1.Rows[e.RowIndex].Cells[4].Value = Math.Round((n-(n*(r/100))),2) ; //po rabacie netto
                decimal netto = Decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()); //po rabacie netto
                dataGridView1.Rows[e.RowIndex].Cells[6].Value = Math.Round(netto * szt, 2); //wartość netto
                decimal wartn = Decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
                dataGridView1.Rows[e.RowIndex].Cells[7].Value = Math.Round(wartn*vat, 2); //wartość brutto

                wyliczenie_kwoty();
            }

        }
        bool ChangedRow;
        private void DynList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ChangedRow = true;
        }
        public double suma()
        {
            double sum = 0;
            for(int i = 0; i <dataGridView1.Rows.Count;++i)
            {
                double d = 0;
                Double.TryParse(dataGridView1.Rows[i].Cells[7].Value.ToString().Replace("zł",""), out d);
                sum += d;
            }
            return sum;
        }
        private void deleteBTN_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(row.Index);
            }
            if (dataGridView1.RowCount == 1)
            {
                editBTN.Enabled = false;
                deleteBTN.Enabled = false;
                saveBTN.Enabled = false;
                print_btn.Enabled = false;
            }
            wyliczenie_kwoty();
        }
        public class Formatowanie
        {
            private static string zero = "zero";
            private static string[] jednosci = { "", " jeden ", " dwa ", " trzy ",
        " cztery ", " pięć ", " sześć ", " siedem ", " osiem ", " dziewięć " };
            private static string[] dziesiatki = { "", " dziesięć ", " dwadzieścia ",
        " trzydzieści ", " czterdzieści ", " pięćdziesiąt ",
        " sześćdziesiąt ", " siedemdziesiąt ", " osiemdziesiąt ",
        " dziewięćdziesiąt "};
            private static string[] nascie = { "dziesięć", " jedenaście ", " dwanaście ",
        " trzynaście ", " czternaście ", " piętnaście ", " szesnaście ",
        " siedemnaście ", " osiemnaście ", " dziewiętnaście "};
            private static string[] setki = { "", " sto ", " dwieście ", " trzysta ",
        " czterysta ", " pięćset ", " sześćset ",
        " siedemset ", " osiemset ", " dziewięćset " };
            private static string[,] rzedy = {{" tysiąc ", " tysiące ", " tysięcy "},
                            {" milion ", " miliony ", " milionów "},
                            {" miliard ", " miliardy ", " miliardów "}};

            private static Dictionary<string, string[]> Waluty = new Dictionary<string, string[]>() {
        //Formy podawane według wzorca: jeden-dwa-pięć, np.
        //(jeden) złoty, (dwa) złote, (pięć) złotych
        { "złote", new string[]{ "złoty", "złote", "złotych" } },
        { "grosze", new string[]{ "grosz", "grosze", "groszy" } }
    };

            public static string LiczbaSlownie(int liczba)
            {
                return LiczbaSlownieBase(liczba).Replace("  ", " ").Trim();
            }

            public static string WalutaSlownie(int liczba, string kodWaluty)
            {
                var key = Waluty[kodWaluty];
                return key[DeklinacjaWalutyIndex(liczba)];
            }

            private static string LiczbaSlownieBase(int wartosc)
            {
                StringBuilder sb = new StringBuilder();
                //0-999
                if (wartosc == 0)
                    return zero;
                int jednosc = wartosc % 10;
                int para = wartosc % 100;
                int set = (wartosc % 1000) / 100;
                if (para > 10 && para < 20)
                    sb.Insert(0, nascie[jednosc]);
                else
                {
                    sb.Insert(0, jednosci[jednosc]);
                    sb.Insert(0, dziesiatki[para / 10]);
                }
                sb.Insert(0, setki[set]);

                //Pozostałe rzędy wielkości
                wartosc = wartosc / 1000;
                int rzad = 0;
                while (wartosc > 0)
                {
                    jednosc = wartosc % 10;
                    para = wartosc % 100;
                    set = (wartosc % 1000) / 100;
                    bool rzadIstnieje = wartosc % 1000 != 0;
                    if ((wartosc % 1000) / 10 == 0)
                    {
                        //nie ma dziesiątek i setek
                        if (jednosc == 1)
                            sb.Insert(0, rzedy[rzad, 0]);
                        else if (jednosc >= 2 && jednosc <= 4)
                            sb.Insert(0, rzedy[rzad, 1]);
                        else if (rzadIstnieje)
                            sb.Insert(0, rzedy[rzad, 2]);
                        if (jednosc > 1)
                            sb.Insert(0, jednosci[jednosc]);
                    }
                    else
                    {
                        if (para >= 10 && para < 20)
                        {
                            sb.Insert(0, rzedy[rzad, 2]);
                            sb.Insert(0, nascie[para % 10]);
                        }
                        else
                        {
                            if (jednosc >= 2 && jednosc <= 4)
                                sb.Insert(0, rzedy[rzad, 1]);
                            else if (rzadIstnieje)
                                sb.Insert(0, rzedy[rzad, 2]);
                            sb.Insert(0, jednosci[jednosc]);
                            sb.Insert(0, dziesiatki[para / 10]);
                        }
                        sb.Insert(0, setki[set]);
                    }
                    rzad++;
                    wartosc = wartosc / 1000;
                }
                return sb.ToString();
            }

            private static int DeklinacjaWalutyIndex(int liczba)
            {
                if (liczba == 1)
                    return 0;

                int para = liczba % 100;
                if (para >= 10 && para < 20)
                    return 2;

                int jednosc = liczba % 10;
                if (jednosc >= 2 && jednosc <= 4)
                    return 1;

                return 2;
            }
        }

        private void zaplaconoTXT_TextChanged(object sender, EventArgs e)
        {
            if (zaplaconoTXT.Text == "")
            {

            }
            else
            {
                string oblicz;
                string su = sumaTXT.Text.Replace("zł", "");
                string za = zaplaconoTXT.Text.Replace("zł", "");
                decimal suma = Decimal.Parse(su);
                decimal zal = Decimal.Parse(za);
                oblicz = Math.Round(suma - zal,2).ToString("c");
                resztaTXT.Text = oblicz;
            }
        }
        private void zaplaconoTXT_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) || e.KeyCode == Keys.Back)
            {
                acceptableKey = true;
            }
            else
            {
                acceptableKey = false;
            }
        }
        private void zaplaconoTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (acceptableKey == false)
            {
                e.Handled = true;
                return;
            }
            else
            {
                if (e.KeyChar == Convert.ToChar(Keys.Back))
                {
                    if (strCurrency.Length > 0)
                    {
                        strCurrency = strCurrency.Substring(0, strCurrency.Length - 1);
                    }
                }
                else
                {
                    strCurrency = strCurrency + e.KeyChar;
                }

                if (strCurrency.Length == 0)
                {
                    zaplaconoTXT.Text = "0,00 zł";
                }
                else if (strCurrency.Length == 1)
                {
                    zaplaconoTXT.Text = "0,0" + strCurrency + " zł";
                }
                else if (strCurrency.Length == 2)
                {
                    zaplaconoTXT.Text = "0," + strCurrency + " zł";
                }
                else if (strCurrency.Length > 2)
                {
                    zaplaconoTXT.Text = strCurrency.Substring(0, strCurrency.Length - 2) + "," + strCurrency.Substring(strCurrency.Length - 2) + " zł";
                }
                zaplaconoTXT.Select(zaplaconoTXT.Text.Length, 0);
            }
            e.Handled = true;
        }

        private void userBTN_Click(object sender, EventArgs e)
        {
            formsy.klients form = new formsy.klients(this);
            form.Show();
        }

        private void saveBTN_Click(object sender, EventArgs e)
        {
            //sprawdzamy czy klient został wybrany
            if (klientTXT.Text == "")
            {
                MessageBox.Show("Przed zapisem musisz wybrać klient!");
            }
            else
            {
                //zapis do bazy danych
                var MyIni = new INIFile("WektorSettings.ini");
                server = MyIni.Read("server", "Okna");
                database = MyIni.Read("database", "Okna");
                uid = MyIni.Read("login", "Okna");
                password = Decrypt(MyIni.Read("pass", "Okna"));

                string MyConnectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                MySqlConnection connection = new MySqlConnection(MyConnectionString);

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    try
                    {
                        MySqlCommand cmd = new MySqlCommand();
                        cmd = connection.CreateCommand();
                        if (row.IsNewRow) continue;
                        cmd.Parameters.AddWithValue("@id", numerek.Text);
                        cmd.Parameters.AddWithValue("@indeks", row.Cells[0].Value);
                        cmd.Parameters.AddWithValue("@rabat", row.Cells[3].Value.ToString().Replace("%", "").Replace(",", "."));
                        cmd.Parameters.AddWithValue("@ilosc", row.Cells[5].Value);
                        cmd.Parameters.AddWithValue("@cena", row.Cells[4].Value.ToString().Replace("zł", "").Replace(",", "."));
                        cmd.Parameters.AddWithValue("@data", DateTime.Now);
                        cmd.Parameters.AddWithValue("@kwota", sumaTXT.Text.Replace("zł", "").Replace(",", "."));
                        cmd.CommandText = "INSERT IGNORE INTO wyceny (nrw,klient,kwota,data) VALUES (@id,3,@kwota,@data); " +
                                          "INSERT INTO wyceny_detail (id,id_product,id_klient,rabat,ilosc,cena) VALUES (@id,(SELECT id FROM cenniki WHERE reference = @indeks),3,@rabat,@ilosc,@cena);";
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                MessageBox.Show("Wycena została zapisana pod numerem " + wycena_nr.Text);
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            editBTN_Click(e, e);
        }
    }
}
