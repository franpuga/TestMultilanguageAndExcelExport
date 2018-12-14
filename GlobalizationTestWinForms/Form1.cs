using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel.Application;
using System.Drawing;

namespace GlobalizationTestWinForms
{
    public partial class Form1 : Form
    {
        public string language = Properties.Settings.Default.Lenguaje;
        public Form1()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            InitializeComponent();
        }

        private void ChangeLanguage(string lang)
        {
            foreach(Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
                resources.ApplyResources(c, c.Name, new CultureInfo(lang));
            }
        }

        private void inglésToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            language = "en-GB";
            inglésToolStripMenuItem.Checked = true;
            francésToolStripMenuItem.Checked = false;
            españolToolStripMenuItem.Checked = false;

            Properties.Settings.Default.Lenguaje = language;
            Properties.Settings.Default.Save();
            ChangeLanguage(language);
            System.Windows.Forms.Application.Restart();
        }

        private void francésToolStripMenuItem_Click_1(object sender, System.EventArgs e)
        {
            language = "fr-FR";
            inglésToolStripMenuItem.Checked = false;
            francésToolStripMenuItem.Checked = true;
            españolToolStripMenuItem.Checked = false;

            Properties.Settings.Default.Lenguaje = language;
            Properties.Settings.Default.Save();
            ChangeLanguage(language);
            System.Windows.Forms.Application.Restart();
        }

        private void españolToolStripMenuItem_Click_1(object sender, System.EventArgs e)
        {
            language = "es-ES";
            inglésToolStripMenuItem.Checked = false;
            francésToolStripMenuItem.Checked = false;
            españolToolStripMenuItem.Checked = true;

            Properties.Settings.Default.Lenguaje = language;
            Properties.Settings.Default.Save();
            ChangeLanguage(language);
            System.Windows.Forms.Application.Restart();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("¿Está seguro que desea cerrar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                if (MessageBox.Show("¿Está seguro que desea cerrar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    e.Cancel = false;
                }
                else
                    e.Cancel = true;
            }
        }

        private void toolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            object opc = Type.Missing;

            // Creamos app excel
            Excel app = new Excel();
            // Libro
            Workbook book = app.Workbooks.Add(opc);
            app.DisplayAlerts = false;
            // Hoja
            Worksheet hoja = new Worksheet();
            hoja = (Worksheet)book.Sheets.Add(opc, opc, opc, opc);
            hoja.Activate();
            hoja.Cells[1, 1] = "Nombre";
            hoja.Cells[1, 2] = "Apellidos";
            hoja.Cells[1, 3] = "Teléfono";
            // Cambiar color cabecera y ajustar texto
            Range cabecera = hoja.get_Range("A1:C1");
            cabecera.Interior.Color = Color.Aquamarine;

            // Info
            hoja.Cells[2, 1] = !string.IsNullOrEmpty(this.textBox1.Text) ? this.textBox1.Text : string.Empty;
            hoja.Cells[2, 2] = !string.IsNullOrEmpty(this.textBox2.Text) ? this.textBox2.Text : string.Empty;
            hoja.Cells[2, 3] = !string.IsNullOrEmpty(this.textBox3.Text) ? this.textBox3.Text : string.Empty;

            // Fit
            Range columns = hoja.get_Range("A:C");
            columns.EntireColumn.AutoFit();

            // Shox Excel
            app.Visible = true;

        }
    }
}