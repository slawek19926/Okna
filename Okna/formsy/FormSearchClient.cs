using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Okna.formsy
{
    public partial class FormSearchClient : Form
    {
        DialogResult result;
        int varString;
        string wrt;
        string varNow;
        string varMoment;
        string imie;
        string nazwisko;
        string adres;
        string miejscowosc;
        string ulica;
        string poczta;

        public FormSearchClient()
        {
            InitializeComponent();
            webBrowser.Navigate("https://prod.ceidg.gov.pl/CEIDG/CEIDG.Public.UI/Search.aspx");
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRegon.Text) && string.IsNullOrEmpty(textBoxNip.Text) && string.IsNullOrEmpty(textBoxKrs.Text))
            {
                const string message = "Wypełnij przynajmniej jedno pole wyszukiwania z bazy CEIDG!";
                const string caption = "Komunikat";
                result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                pictureBoxLoad.Visible = true;
                labelLoad.Visible = true;
                webBrowser.Document.GetElementById("MainContent_txtNip").SetAttribute("value", textBoxNip.Text);
                webBrowser.Document.GetElementById("MainContent_txtRegon").SetAttribute("value", textBoxRegon.Text);
                webBrowser.Document.GetElementById("MainContent_txtKrs").SetAttribute("value", textBoxKrs.Text);
                webBrowser.Document.GetElementById("MainContent_btnInputSearch").InvokeMember("click");
            }
        }

        public string CutPart(string str, string cut)
        {
            char[] charArray;

            varString = str.LastIndexOf(cut);
            if (varString > 0)
                varNow = str.Remove(varString);

            charArray = varNow.ToCharArray();
            Array.Reverse(charArray);

            return new string(charArray);
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser.Document.ForeColor = Color.Black;
            //https://www.youtube.com/watch?v=YEROx7EjPWg
            webBrowser.Document.Body.Style = "zoom:100%;";
            HtmlElement tbCaptcha_0 = webBrowser.Document.GetElementById("tbCaptcha_0");
            HtmlElement MainContent_DataListEntities_linkHref2_0 = webBrowser.Document.GetElementById("MainContent_DataListEntities_linkHref2_0");
            HtmlElement MainContent_lblName = webBrowser.Document.GetElementById("tbCaptcha_0");

            if (tbCaptcha_0 != null) webBrowser.Document.GetElementById("MainContent_tdCaptcha2").Focus();
            if (MainContent_DataListEntities_linkHref2_0 != null) webBrowser.Document.GetElementById("MainContent_DataListEntities_linkHref2_0").InvokeMember("click");

            if (MainContent_lblName != null)
            {
                adres = webBrowser.Document.GetElementById("MainContent_lblPlaceOfBusinessAddress").InnerHtml;

                //MIEJSCOWOŚĆ
                if (adres.Contains(", ul.")) wrt = ", ul.";
                else wrt = ", nr";
                varMoment = CutPart(adres, wrt);
                miejscowosc = CutPart(varMoment, " .csjeim");

                //ULICA
                if (adres.Contains(", ul."))
                {
                    varMoment = CutPart(adres, ", nr");
                    ulica = CutPart(varMoment, " .lu");
                }
                else ulica = miejscowosc;

                varMoment = CutPart(adres, ", ");
                ulica = ulica + CutPart(varMoment, "rn ,");

                varString = ulica.LastIndexOf(", ");
                if (varString > 0)
                    ulica = ulica.Remove(varString);

                //POCZTA
                varMoment = CutPart(adres + ", p", ", p");
                poczta = CutPart(varMoment, " rn ,");
                poczta = poczta.Replace("poczta ", "");
                varMoment = CutPart(poczta + ", p", ", p");
                poczta = CutPart(varMoment, " ,");

                //INNE PRZYPADKI
                if (miejscowosc.Contains(", "))
                {
                    varString = miejscowosc.LastIndexOf(", ");
                    varNow = miejscowosc.Remove(varString);
                    miejscowosc = varNow;
                }

                if (ulica.Contains(", "))
                {
                    varMoment = CutPart(ulica + ", p", ", p");
                    ulica = CutPart(varMoment, " ,");
                }

                if (adres.Contains("lok") && !ulica.Contains("lok"))
                {
                    varMoment = CutPart(adres, ", ");
                    varMoment = CutPart(varMoment, "rn ,");
                    varMoment = CutPart(varMoment + ", p", ", p");
                    ulica = ulica + CutPart(varMoment, ",");
                }

                if (adres.Contains("lok"))
                {
                    varMoment = CutPart(poczta + ", p", ", p");
                    poczta = CutPart(varMoment, " ,");
                }

                if (poczta.Contains("opis"))
                {
                    varString = poczta.LastIndexOf(", opis");
                    if (varString > 0)
                        poczta = poczta.Remove(varString);
                }

                if (!poczta.Contains(" ")) { poczta += ", " + miejscowosc; }



                imie = webBrowser.Document.GetElementById("MainContent_lblFirstName").InnerHtml;
                nazwisko = webBrowser.Document.GetElementById("MainContent_lblLastName").InnerHtml;

                //PRZYPISANIE WARTOŚCI
                textBoxNazwa.Text = webBrowser.Document.GetElementById("MainContent_lblName").InnerHtml;
                if (textBoxNazwa.Text == "-" || string.IsNullOrEmpty(textBoxNazwa.Text)) textBoxNazwa.Text = imie + " " + nazwisko;
                textBoxMiejscowosc.Text = miejscowosc;
                textBoxUlica.Text = ulica;
                textBoxPoczta.Text = poczta;
                textBoxPanstwo.Text = webBrowser.Document.GetElementById("MainContent_lblCitizenship").InnerHtml;
                textBoxN.Text = webBrowser.Document.GetElementById("MainContent_lblNip").InnerHtml;

                webBrowser.Visible = false;
                buttonCEIDG.Visible = true;
                buttonSearch.Enabled = false;
            }

            pictureBoxLoad.Visible = false;
            labelLoad.Visible = false;

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            webBrowser.Refresh();
            webBrowser.Visible = true;
            pictureBoxLoad.Visible = true;
            labelLoad.Visible = true;
            buttonCEIDG.Visible = false;
            buttonSearch.Enabled = true;
        }

        private void buttonCEIDG_Click(object sender, EventArgs e)
        {
            //Komunikat.Console("Kliknięto łącze do strony www.ceidg.gov.pl");
            System.Diagnostics.Process.Start("https://prod.ceidg.gov.pl/ceidg.cms.engine/");
        }

        private void textBoxRegon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        private void textBoxNip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        private void textBoxKrs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        //CZYSZCZENIE PÓL
        private void buttonCzNazwa_Click(object sender, EventArgs e) { textBoxNazwa.Clear(); }

        private void buttonCzMiejscowosc_Click(object sender, EventArgs e) { textBoxMiejscowosc.Clear(); }

        private void buttonCzUlica_Click(object sender, EventArgs e) { textBoxUlica.Clear(); }

        private void buttonCzPoczta_Click(object sender, EventArgs e) { textBoxPoczta.Clear(); }

        private void buttonCzPanstwo_Click(object sender, EventArgs e) { textBoxPanstwo.Clear(); }

        private void buttonCzNip_Click(object sender, EventArgs e) { textBoxN.Clear(); }

        private void buttonCzPesel_Click(object sender, EventArgs e) { textBoxPESEL.Clear(); }

        private void buttonCzWszystko_Click(object sender, EventArgs e)
        {
            const string message = "Czy na pewno chcesz wyczyścić wszystkie pola?";
            const string caption = "Komunikat";
            result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                textBoxNazwa.Clear();
                textBoxMiejscowosc.Clear();
                textBoxUlica.Clear();
                textBoxPoczta.Clear();
                textBoxPanstwo.Clear();
                textBoxN.Clear();
                textBoxPESEL.Clear();
            }

        }

        private void numericUpDownZoom_ValueChanged(object sender, EventArgs e)
        {
            //Properties.Settings.Default.zoom = Convert.ToInt32(numericUpDownZoom.Value);
            //Properties.Settings.Default.Save();
        }

        private void buttonDodaj_Click(object sender, EventArgs e)
        {

        }
    }
}
