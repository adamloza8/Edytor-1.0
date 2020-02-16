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
using System.Net;

namespace EdytorHTML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            webBrowser2.Url = new Uri(PlikTymczasowy);
            ZapiszDoPlikuTekstowego(PlikTymczasowy, new string[0]);
        }
        private int PozycjaKursora;
        string PlikTymczasowy = Environment.CurrentDirectory + "\\tmp.html";
        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void ZapiszDoPlikuTekstowego(string NazwaPliku, string[] tekst)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(NazwaPliku))
                {
                    foreach (string linia in tekst)
                        sw.Write(linia);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd zapisu pliku: " + NazwaPliku + "(" + ex.Message + (")"));
            }
        }
        private string[] OdczytajZPlikuTekstowego(string NazwaPliku)
        {
            List<string> tekst = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(NazwaPliku))
                {
                    string linia;
                    while ((linia = sr.ReadLine()) != null)
                        tekst.Add(linia);
                }
                return tekst.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd odczytu pliku:  " + NazwaPliku + "(" + ex.Message + ")");
                return null;
            }

            
        }

        private void OtwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() ==DialogResult.OK)
            {
                string NazwaPliku = openFileDialog1.FileName;
                textBox1.Lines = OdczytajZPlikuTekstowego(NazwaPliku);
                toolStripStatusLabel1.Text = "Otwarty plik: " + NazwaPliku.Substring(NazwaPliku.LastIndexOf("\\") + 1, NazwaPliku.Length - NazwaPliku.LastIndexOf("\\") - 1);

            }
        }

        private void ZapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NazwaPliku = openFileDialog1.FileName;
            if (NazwaPliku != String.Empty) saveFileDialog1.FileName = NazwaPliku;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                NazwaPliku = saveFileDialog1.FileName;
                ZapiszDoPlikuTekstowego(NazwaPliku, textBox1.Lines);
                toolStripStatusLabel1.Text = "Otwarty plik: " +
                NazwaPliku.Substring(NazwaPliku.LastIndexOf("\\") + 1,
                NazwaPliku.Length - NazwaPliku.LastIndexOf("\\") - 1);
            }

        }

        private void CofnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void WytnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }

        private void KopiujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }

        private void WklejToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void UsuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "";
        }

        private void ZaznaczWszystkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }
        private void WprowadzTag(string tag)
        {
            string kod = textBox1.Text;
            textBox1.Text = kod.Insert(PozycjaKursora, tag);
            textBox1.Focus();
            if (tag == "<br>" || tag == "<hr>")
            {
                textBox1.Select(PozycjaKursora + tag.Length, 0);
                PozycjaKursora += tag.Length;
            }
            else
            {
                textBox1.Select(PozycjaKursora + tag.Length / 2, 0);
                PozycjaKursora += tag.Length / 2;
            }
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            PozycjaKursora = textBox1.SelectionStart;
        }

        private void TextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            PozycjaKursora = textBox1.SelectionStart;
        }

        private void TagB_Click(object sender, EventArgs e)
        {
            WprowadzTag("<b></b>");
        }

        private void TagI_Click(object sender, EventArgs e)
        {
            WprowadzTag("<i></i>");
        }

        private void TagTable_Click(object sender, EventArgs e)
        {
            WprowadzTag("<table></table>");

        }

        private void TagTr_Click(object sender, EventArgs e)
        {
            WprowadzTag("<tr></tr>");

        }

        private void TagTd_Click(object sender, EventArgs e)
        {
            WprowadzTag("<td></td>");

        }

        private void TagBr_Click(object sender, EventArgs e)
        {
            WprowadzTag("<br></br>");

        }

        private void TagHr_Click(object sender, EventArgs e)
        {
            WprowadzTag("<hr></hr>");

        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            ZapiszDoPlikuTekstowego(PlikTymczasowy, textBox1.Lines);
            webBrowser2.Refresh();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void BToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WprowadzTag("<b></b>");
        }

        private void IToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WprowadzTag("<i></i>");

        }

        private void TableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WprowadzTag("<table></table>");
        }

        private void TrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WprowadzTag("<tr></tr>");

        }

        private void TdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WprowadzTag("<td></td>");

        }

        private void BrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WprowadzTag("<br></br>");

        }

        private void HrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WprowadzTag("<hr></hr>");

        }

        private void KoniecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
    }

        private void PomocToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private  void ObjaśnienieTagówToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void DrukujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser2.Print();
        }
    }
    }
