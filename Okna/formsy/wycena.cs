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
using System.Collections;
using MaterialSkin;
using MaterialSkin.Controls;
using MetroFramework;
using MetroFramework.Controls;
using PrintDataGrid;

namespace Okna

{

    public partial class wycena : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string strCurrency;
        bool acceptableKey = false;
        Form1 Form1;
        public static string klients;
        public wycena(Form1 Form1)
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            this.Form1 = Form1;
            Text = "Wycena " + klientTXT.Text;
            FormClosing += wycena_FormClosing;

            double sum = 0;
            for (int i =0; i < metroGrid1.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(metroGrid1.Rows[i].Cells[7].Value);
            }
            sumaTXT.Text = sum.ToString("c");
            editBTN.Enabled = false;
            deleteBTN.Enabled = false;
            saveBTN.Enabled = false;
            print_btn.Enabled = true;
            InitTimer();
        }
        private static DataGrid dg;
        public static string numer_wyceny;
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


        public void wycena_Load(object sender, EventArgs e)
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
                string klient = Form1.logged.Text;
                var query = $"{$"SELECT lpad(max(wycena+1),"}{MyIni.Read("zera","wyceny")},0) as numer,wycena,id FROM uzytkownicy WHERE id = (SELECT id FROM uzytkownicy WHERE username = '{klient}')";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        //wycena numer
                        while (reader.Read())
                        {
                            if (MyIni.Read("przed","wyceny") == "")
                            {
                                numer_wyceny = "wyc/" + reader.GetString("numer") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Today.Year;
                                wycena_nr.Text = numer_wyceny;
                                numberBAZA.Text = reader.GetString("wycena");
                                userID.Text = reader.GetString("id");
                            }
                            else
                            {
                                numer_wyceny = "" + MyIni.Read("przed", "wyceny") + "/" + reader.GetString("numer") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Today.Year;
                                wycena_nr.Text = numer_wyceny;
                                numberBAZA.Text = reader.GetString("wycena");
                                userID.Text = reader.GetString("id");
                            }
                            numerek.Text = reader.GetString("numer");
                        }
                    }
                }
            }


            metroGrid1.Columns[1].Width = 325;
            metroGrid1.Columns[2].DefaultCellStyle.Format = "c";
            decimal kwota = Convert.ToDecimal(suma());
            int zlote = (int)kwota;
            int grosze = (int)(100 * kwota) % 100;

            slownie.Text = String.Format("{0} {1} {2} {3}",
                Formatowanie.LiczbaSlownie(zlote),
                Formatowanie.WalutaSlownie(zlote, "złote"),
                Formatowanie.LiczbaSlownie(grosze),
                Formatowanie.WalutaSlownie(grosze, "grosze"));

            if (klientTXT.Text == "")
            {
                userBTN.Text = "Wybierz klienta";
            }
            else
            {
                userBTN.Text = "Zmień klienta";
            }

            //this.reportViewer1.RefreshReport();
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
                klients = klientTXT.Text;
                Text = "Wycena: " + klients ;
            }
        }
        private void addBTN_Click(object sender, EventArgs e)
        {
            add_product form = new add_product(this);
            form.Show();
        }

        private void editBTN_Click(object sender, EventArgs e)
        {
            formsy.edit_towar form = new formsy.edit_towar(this);
            form.Show();
        }
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for(int i = 0; i < metroGrid1.Rows.Count; ++i)
            {
                decimal n = Decimal.Parse(metroGrid1.Rows[i].Cells[4].Value.ToString().Replace("zł","")); //netto
                decimal ile = Decimal.Parse(metroGrid1.Rows[i].Cells[5].Value.ToString()); //ilość
                decimal vat = Decimal.Parse("1,23");
                metroGrid1.Rows[i].Cells[6].Value = n * ile;
                metroGrid1.Rows[i].Cells[7].Value = Math.Round((n*ile)*vat,2);
                metroGrid1.Rows[i].Cells[8].Value = "0 %";
            }
            if (metroGrid1.RowCount >= 1)
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
                //8 - narzut
                ChangedRow = false;
                decimal n = Decimal.Parse(metroGrid1.Rows[e.RowIndex].Cells[2].Value.ToString()); //netto
                decimal r = Decimal.Parse(metroGrid1.Rows[e.RowIndex].Cells[3].Value.ToString().Replace("%","")); //rabat
                decimal szt = Decimal.Parse(metroGrid1.Rows[e.RowIndex].Cells[5].Value.ToString()); //ilość
                decimal vat = Decimal.Parse("1,23"); 
                metroGrid1.Rows[e.RowIndex].Cells[4].Value = Math.Round((n-(n*(r/100))),2) ; //po rabacie netto
                decimal netto = Decimal.Parse(metroGrid1.Rows[e.RowIndex].Cells[4].Value.ToString()); //po rabacie netto
                metroGrid1.Rows[e.RowIndex].Cells[6].Value = Math.Round(netto * szt, 2); //wartość netto
                decimal wartn = Decimal.Parse(metroGrid1.Rows[e.RowIndex].Cells[6].Value.ToString());
                metroGrid1.Rows[e.RowIndex].Cells[7].Value = Math.Round(wartn*vat, 2); //wartość brutto
                decimal narzut = Decimal.Parse(metroGrid1.Rows[e.RowIndex].Cells[8].Value.ToString()); //narzut

                metroGrid1.Rows[e.RowIndex].Cells[4].Value = Math.Round(netto + (netto * (narzut / 100)), 2); //netto po narzucie
                decimal nettoN = Decimal.Parse(metroGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                metroGrid1.Rows[e.RowIndex].Cells[6].Value = Math.Round(nettoN * szt, 2); //wartosc netto po narzucie
                decimal wnnetoN = Decimal.Parse(metroGrid1.Rows[e.RowIndex].Cells[6].Value.ToString()); //wartosc netto po narzucie
                metroGrid1.Rows[e.RowIndex].Cells[7].Value = Math.Round(wnnetoN * vat, 2); //wartosc netto po narzucie

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
            for (int i = 0; i < metroGrid1.Rows.Count; ++i)
            {
                double d = 0;
                Double.TryParse(metroGrid1.Rows[i].Cells[7].Value.ToString().Replace("zł", ""), out d);
                sum += d;
            }
            return sum;
        }
        private void deleteBTN_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in metroGrid1.SelectedRows)
            {
                metroGrid1.Rows.RemoveAt(row.Index);
            }
            if (metroGrid1.RowCount == 1)
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
                //zaplaconoTXT.Select(zaplaconoTXT.Text.Length, 0);
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
                var result = MessageBox.Show("Przed zapisem musisz wybrać klient!");
                if(result == DialogResult.OK)
                {
                    formsy.klients form = new formsy.klients(this);
                    form.Show();
                }
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

                foreach (DataGridViewRow row in metroGrid1.Rows)
                {
                    try
                    {
                        int id_user = Convert.ToInt32(userID.Text);
                        string klient = Form1.logged.Text;
                        int nr = Convert.ToInt32(numberBAZA.Text);
                        int numer = nr + 1;
                        string str = numer.ToString();
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
                        cmd.CommandText = $"INSERT IGNORE INTO wyceny (nrw,user_id,wycena_user,numer,klient,kwota,data) VALUES (@id,'{id_user}','{nr}','{wycena_nr.Text}',(SELECT id FROM klienci WHERE nazwa = '{klientTXT.Text}'),@kwota,@data); " +
                            $"INSERT INTO wyceny_detail (id,user_id,wycena_user,id_product,id_klient,rabat,ilosc,cena) VALUES (@id,'{id_user}','{nr}',(SELECT id FROM cenniki WHERE reference = @indeks),(SELECT id FROM klienci WHERE nazwa ='{klientTXT.Text}'),@rabat,@ilosc,@cena);" +
                            $"UPDATE uzytkownicy SET wycena = '{str}' WHERE username = '{klient}';";
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                var result = MessageBox.Show("Wycena została zapisana pod numerem " + wycena_nr.Text);
                if (result == DialogResult.OK)
                {
                    Close();
                }
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            editBTN_Click(e, e);
        }

        private void zapisDoBazy()
        {
            var MyIni = new INIFile("WektorSettings.ini");
            server = MyIni.Read("server", "Okna");
            database = MyIni.Read("database", "Okna");
            uid = MyIni.Read("login", "Okna");
            password = Decrypt(MyIni.Read("pass", "Okna"));

            string MyConnectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            MySqlConnection connection = new MySqlConnection(MyConnectionString);

            foreach (DataGridViewRow row in metroGrid1.Rows)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd = connection.CreateCommand();
                    cmd.Parameters.AddWithValue("@nr", numerek.Text);
                    cmd.Parameters.AddWithValue("@indeks", row.Cells[0].Value.ToString());
                    cmd.Parameters.AddWithValue("@nazwa", row.Cells[1].Value.ToString());
                    cmd.Parameters.AddWithValue("@rabat", row.Cells[3].Value.ToString());
                    cmd.Parameters.AddWithValue("@narzut", row.Cells[8].Value.ToString());
                    cmd.Parameters.AddWithValue("@cena", row.Cells[4].Value.ToString());
                    cmd.Parameters.AddWithValue("@ilosc", row.Cells[5].Value.ToString());
                    decimal netto = Decimal.Parse(row.Cells[4].Value.ToString().Replace("zł", ""));
                    decimal ilosc = Decimal.Parse(row.Cells[5].Value.ToString());
                    decimal vat = Decimal.Parse("1,23");
                    decimal wartN = Math.Round(netto * ilosc);
                    decimal wartB = Math.Round(wartN * vat);
                    decimal wartV = Math.Round(wartB - wartN);
                    cmd.Parameters.AddWithValue("@wartN", wartN);
                    cmd.Parameters.AddWithValue("@wartB", wartB);
                    cmd.Parameters.AddWithValue("@wartV", wartV);
                    cmd.CommandText = "INSERT IGNORE INTO temp (indeks,nazwa,rabat,narzut,cena,wartn,wart_v,wartb,ilosc,wycena_nr) VALUES (@indeks,@nazwa,@rabat,@narzut,@cena,@wartN,@wartV,@wartB,@ilosc,@nr)";
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void print_btn_Click(object sender, EventArgs e)
        {
            zapisDoBazy();
            print.printDial frm = new print.printDial(this,Form1);
            frm.Show();
        }
            
        public void print_btn_Click_1(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Narazie da się drukować na domyślnej drukarce. Kontynuować?", "Informacja", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(result == DialogResult.Yes)
            {
                //Ładuje okno konfiguracji wydruku
                PrintDGV.Print_DataGridView(metroGrid1);
            }
        }
    }
}
