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
    public partial class edit_towar : Form
    {
        private wycena wycena;
        public edit_towar(wycena wycena)
        {
            InitializeComponent();
            this.wycena = wycena;
        }
        
        private void edit_towar_Load(object sender, EventArgs e)
        {
            //przypisanie pobieranych danych
            var indeks = wycena.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            var nazwa = wycena.dataGridView1.CurrentRow.Cells[1].Value.ToString();

            //wyświetlenie podbranych danych
            Text = "Edytuj towar " + wycena.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            indeksBOX.Text = indeks;
            nazwa_BOX.Text = nazwa;
            netto_przed.Text= wycena.dataGridView1.CurrentRow.Cells[2].Value.ToString() + " zł";
            rabat_BOX.Text = wycena.dataGridView1.CurrentRow.Cells[3].Value.ToString().Replace("%","");
            netto_po.Text = wycena.dataGridView1.CurrentRow.Cells[4].Value.ToString();
            ilosc_BOX.Text = wycena.dataGridView1.CurrentRow.Cells[5].Value.ToString();
            wart_netto.Text = wycena.dataGridView1.CurrentRow.Cells[6].Value.ToString();
            wart_brutto.Text = wycena.dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        private void ilosc_BOX_TextChanged(object sender, EventArgs e)
        {
            if (ilosc_BOX.Text == "")
            {
                decimal n = Decimal.Parse(netto_po.Text.Replace("zł", ""));
                decimal i = 1;
                decimal wart_n = Math.Round(n * i, 2);
                decimal vat = Decimal.Parse("1,23");
                decimal wart_b = Math.Round(wart_n * vat, 2);
                wart_netto.Text = wart_n.ToString("c");
                wart_brutto.Text = wart_b.ToString("c");
            }
            else
            {
                decimal n = Decimal.Parse(netto_po.Text.Replace("zł", ""));
                decimal i = Decimal.Parse(ilosc_BOX.Text);
                decimal wart_n = Math.Round(n * i, 2);
                decimal vat = Decimal.Parse("1,23");
                decimal wart_b = Math.Round(wart_n * vat, 2);
                wart_netto.Text = wart_n.ToString("c");
                wart_brutto.Text = wart_b.ToString("c");
            }
        }

        private void netto_po_TextChanged(object sender, EventArgs e)
        {
            if (netto_po.Text == "")
            {
                decimal i = Decimal.Parse(ilosc_BOX.Text);
                decimal np = Decimal.Parse(netto_przed.Text.Replace("zł", ""));
                decimal wart_n = Math.Round(np * i, 2);
                decimal vat = Decimal.Parse("1,23");
                decimal wart_b = Math.Round(wart_n * vat, 2);
                wart_netto.Text = wart_n.ToString("c");
                wart_brutto.Text = wart_b.ToString("c");
            }
            else
            { 
                decimal i = Decimal.Parse(ilosc_BOX.Text);
                decimal np = Decimal.Parse(netto_po.Text.Replace("zł",""));
                decimal wart_n = Math.Round(np * i, 2);
                decimal vat = Decimal.Parse("1,23");
                decimal wart_b = Math.Round(wart_n * vat, 2);
                wart_netto.Text = wart_n.ToString("c");
                wart_brutto.Text = wart_b.ToString("c");
            }
        }

        private void rabat_BOX_TextChanged(object sender, EventArgs e)
        {
            if(rabat_BOX.Text == "")
            {
                decimal r = Decimal.Parse("0");
                decimal np = Decimal.Parse(netto_przed.Text.Replace("zł", ""));
                decimal vat = Decimal.Parse("1,23");
                decimal po = Math.Round(np-(np*(r/100)),2);
                decimal br = Math.Round(po * vat);
                netto_po.Text= po.ToString("c");
                //wart_brutto.Text = br.ToString("c");
            }
            else
            {
                decimal r = Decimal.Parse(rabat_BOX.Text.Replace("%", ""));
                decimal np = Decimal.Parse(netto_przed.Text.Replace("zł", ""));
                decimal vat = Decimal.Parse("1,23");
                decimal po = Math.Round(np - (np * (r / 100)), 2);
                decimal br = Math.Round(po * vat);
                netto_po.Text = po.ToString("c");
                //wart_brutto.Text = br.ToString("c");
            }
        }

        private void save_BTN_Click(object sender, EventArgs e)
        {
            wycena.dataGridView1.CurrentRow.Cells[3].Value = rabat_BOX.Text + "%";
            wycena.dataGridView1.CurrentRow.Cells[4].Value = netto_po.Text;
            wycena.dataGridView1.CurrentRow.Cells[5].Value = ilosc_BOX.Text;
            wycena.dataGridView1.CurrentRow.Cells[6].Value = wart_netto.Text;
            wycena.dataGridView1.CurrentRow.Cells[7].Value = wart_brutto.Text;
            Close();
        }
    }
}
