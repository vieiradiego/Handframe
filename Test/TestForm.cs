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

namespace Test
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void cepButton_Click(object sender, EventArgs e)
        {
            
            string[] cep = new HCep().Get(this.cepText.Text);
            this.enderecoText.Text = cep[0];
            this.cidadeText.Text = cep[1];
            this.complementoText.Text = cep[2];
            this.estadoText.Text = cep[3];
            this.ibgeText.Text = cep[4];
            this.erroText.Text = cep[5];
        }
    }
}
