using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueMoon
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ProgBar.Increment(4);
            ProgBar.ForeColor = Color.Red;
            if (ProgBar.Value == ProgBar.Maximum)
            {
                timer1.Stop();
                this.Hide();
                Interfaz frminter = new Interfaz();
                frminter.ShowDialog();
            }
        }

        private void PicBox_Click(object sender, EventArgs e)
        {

        }

        private void labT1_Click(object sender, EventArgs e)
        {

        }
    }
}
