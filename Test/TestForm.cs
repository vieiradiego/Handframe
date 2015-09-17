using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Handframe.ZipCode;
using Handframe.Fipe;

namespace Test
{
    public partial class TestForm : Form
    {
        private hFipe fipe = new hFipe();
        private hCep cep = new hCep();
        public TestForm()
        {
            InitializeComponent();
        }

        private void cepButton_Click(object sender, EventArgs e)
        {
            
            string[] c = this.cep.Get(this.cepText.Text);
            this.enderecoText.Text = c[0];
            this.cidadeText.Text = c[1];
            this.complementoText.Text = c[2];
            this.estadoText.Text = c[3];
            this.ibgeText.Text = c[4];
            this.erroText.Text = c[5];
        }
        private void tipoCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.fipe.type = this.tipoCombo.SelectedText;
            String m = this.fipe.GetBrands();
        }
        private void marcaButton_Click(object sender, EventArgs e)
        {
            string marcas;
            marcas = this.fipe.GetBrands();
        }

        private void veiculoButton_Click(object sender, EventArgs e)
        {

        }

        private void modeloButton_Click(object sender, EventArgs e)
        {

        }

        private void anoButton_Click(object sender, EventArgs e)
        {

        }

        private void combustivelButton_Click(object sender, EventArgs e)
        {

        }
    }
}
