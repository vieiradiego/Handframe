namespace Test
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cepButton = new System.Windows.Forms.Button();
            this.cepLabel = new System.Windows.Forms.Label();
            this.cepText = new System.Windows.Forms.TextBox();
            this.testTab = new System.Windows.Forms.TabControl();
            this.HCepTab = new System.Windows.Forms.TabPage();
            this.erroLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ibgeLabel = new System.Windows.Forms.Label();
            this.estadoLabel = new System.Windows.Forms.Label();
            this.Label = new System.Windows.Forms.Label();
            this.enderecoLabel = new System.Windows.Forms.Label();
            this.cepErroText = new System.Windows.Forms.TextBox();
            this.ibgeText = new System.Windows.Forms.TextBox();
            this.estadoText = new System.Windows.Forms.TextBox();
            this.cidadeText = new System.Windows.Forms.TextBox();
            this.complementoText = new System.Windows.Forms.TextBox();
            this.enderecoText = new System.Windows.Forms.TextBox();
            this.HFipePage = new System.Windows.Forms.TabPage();
            this.combustivelCombo = new System.Windows.Forms.ComboBox();
            this.anoCombo = new System.Windows.Forms.ComboBox();
            this.veiculoCombo = new System.Windows.Forms.ComboBox();
            this.modeloCombo = new System.Windows.Forms.ComboBox();
            this.marcaCombo = new System.Windows.Forms.ComboBox();
            this.tipoCombo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.fipeErroText = new System.Windows.Forms.TextBox();
            this.tipoLabel = new System.Windows.Forms.Label();
            this.testTab.SuspendLayout();
            this.HCepTab.SuspendLayout();
            this.HFipePage.SuspendLayout();
            this.SuspendLayout();
            // 
            // cepButton
            // 
            this.cepButton.Location = new System.Drawing.Point(129, 29);
            this.cepButton.Name = "cepButton";
            this.cepButton.Size = new System.Drawing.Size(36, 23);
            this.cepButton.TabIndex = 0;
            this.cepButton.Text = "OK";
            this.cepButton.UseVisualStyleBackColor = true;
            this.cepButton.Click += new System.EventHandler(this.cepButton_Click);
            // 
            // cepLabel
            // 
            this.cepLabel.AutoSize = true;
            this.cepLabel.Location = new System.Drawing.Point(20, 13);
            this.cepLabel.Name = "cepLabel";
            this.cepLabel.Size = new System.Drawing.Size(28, 13);
            this.cepLabel.TabIndex = 1;
            this.cepLabel.Text = "CEP";
            // 
            // cepText
            // 
            this.cepText.Location = new System.Drawing.Point(23, 29);
            this.cepText.Name = "cepText";
            this.cepText.Size = new System.Drawing.Size(100, 20);
            this.cepText.TabIndex = 2;
            this.cepText.Text = "95095-495";
            // 
            // testTab
            // 
            this.testTab.Controls.Add(this.HCepTab);
            this.testTab.Controls.Add(this.HFipePage);
            this.testTab.Location = new System.Drawing.Point(12, 12);
            this.testTab.Name = "testTab";
            this.testTab.SelectedIndex = 0;
            this.testTab.Size = new System.Drawing.Size(377, 274);
            this.testTab.TabIndex = 3;
            // 
            // HCepTab
            // 
            this.HCepTab.Controls.Add(this.erroLabel);
            this.HCepTab.Controls.Add(this.label5);
            this.HCepTab.Controls.Add(this.ibgeLabel);
            this.HCepTab.Controls.Add(this.estadoLabel);
            this.HCepTab.Controls.Add(this.Label);
            this.HCepTab.Controls.Add(this.enderecoLabel);
            this.HCepTab.Controls.Add(this.cepErroText);
            this.HCepTab.Controls.Add(this.ibgeText);
            this.HCepTab.Controls.Add(this.estadoText);
            this.HCepTab.Controls.Add(this.cidadeText);
            this.HCepTab.Controls.Add(this.complementoText);
            this.HCepTab.Controls.Add(this.enderecoText);
            this.HCepTab.Controls.Add(this.cepText);
            this.HCepTab.Controls.Add(this.cepButton);
            this.HCepTab.Controls.Add(this.cepLabel);
            this.HCepTab.Location = new System.Drawing.Point(4, 22);
            this.HCepTab.Name = "HCepTab";
            this.HCepTab.Padding = new System.Windows.Forms.Padding(3);
            this.HCepTab.Size = new System.Drawing.Size(369, 248);
            this.HCepTab.TabIndex = 0;
            this.HCepTab.Text = "HCep";
            this.HCepTab.UseVisualStyleBackColor = true;
            // 
            // erroLabel
            // 
            this.erroLabel.AutoSize = true;
            this.erroLabel.Location = new System.Drawing.Point(244, 142);
            this.erroLabel.Name = "erroLabel";
            this.erroLabel.Size = new System.Drawing.Size(26, 13);
            this.erroLabel.TabIndex = 14;
            this.erroLabel.Text = "Erro";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(135, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Cidade";
            // 
            // ibgeLabel
            // 
            this.ibgeLabel.AutoSize = true;
            this.ibgeLabel.Location = new System.Drawing.Point(135, 142);
            this.ibgeLabel.Name = "ibgeLabel";
            this.ibgeLabel.Size = new System.Drawing.Size(32, 13);
            this.ibgeLabel.TabIndex = 12;
            this.ibgeLabel.Text = "IBGE";
            // 
            // estadoLabel
            // 
            this.estadoLabel.AutoSize = true;
            this.estadoLabel.Location = new System.Drawing.Point(20, 142);
            this.estadoLabel.Name = "estadoLabel";
            this.estadoLabel.Size = new System.Drawing.Size(40, 13);
            this.estadoLabel.TabIndex = 11;
            this.estadoLabel.Text = "Estado";
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Location = new System.Drawing.Point(20, 103);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(71, 13);
            this.Label.TabIndex = 10;
            this.Label.Text = "Complemento";
            // 
            // enderecoLabel
            // 
            this.enderecoLabel.AutoSize = true;
            this.enderecoLabel.Location = new System.Drawing.Point(20, 64);
            this.enderecoLabel.Name = "enderecoLabel";
            this.enderecoLabel.Size = new System.Drawing.Size(53, 13);
            this.enderecoLabel.TabIndex = 9;
            this.enderecoLabel.Text = "Endereço";
            // 
            // cepErroText
            // 
            this.cepErroText.Location = new System.Drawing.Point(244, 158);
            this.cepErroText.Name = "cepErroText";
            this.cepErroText.Size = new System.Drawing.Size(100, 20);
            this.cepErroText.TabIndex = 8;
            // 
            // ibgeText
            // 
            this.ibgeText.Location = new System.Drawing.Point(138, 158);
            this.ibgeText.Name = "ibgeText";
            this.ibgeText.Size = new System.Drawing.Size(100, 20);
            this.ibgeText.TabIndex = 7;
            // 
            // estadoText
            // 
            this.estadoText.Location = new System.Drawing.Point(23, 158);
            this.estadoText.Name = "estadoText";
            this.estadoText.Size = new System.Drawing.Size(100, 20);
            this.estadoText.TabIndex = 6;
            // 
            // cidadeText
            // 
            this.cidadeText.Location = new System.Drawing.Point(138, 119);
            this.cidadeText.Name = "cidadeText";
            this.cidadeText.Size = new System.Drawing.Size(100, 20);
            this.cidadeText.TabIndex = 5;
            // 
            // complementoText
            // 
            this.complementoText.Location = new System.Drawing.Point(23, 119);
            this.complementoText.Name = "complementoText";
            this.complementoText.Size = new System.Drawing.Size(100, 20);
            this.complementoText.TabIndex = 4;
            // 
            // enderecoText
            // 
            this.enderecoText.Location = new System.Drawing.Point(23, 80);
            this.enderecoText.Name = "enderecoText";
            this.enderecoText.Size = new System.Drawing.Size(100, 20);
            this.enderecoText.TabIndex = 3;
            // 
            // HFipePage
            // 
            this.HFipePage.Controls.Add(this.combustivelCombo);
            this.HFipePage.Controls.Add(this.anoCombo);
            this.HFipePage.Controls.Add(this.veiculoCombo);
            this.HFipePage.Controls.Add(this.modeloCombo);
            this.HFipePage.Controls.Add(this.marcaCombo);
            this.HFipePage.Controls.Add(this.tipoCombo);
            this.HFipePage.Controls.Add(this.label8);
            this.HFipePage.Controls.Add(this.label1);
            this.HFipePage.Controls.Add(this.label3);
            this.HFipePage.Controls.Add(this.label4);
            this.HFipePage.Controls.Add(this.label6);
            this.HFipePage.Controls.Add(this.label7);
            this.HFipePage.Controls.Add(this.fipeErroText);
            this.HFipePage.Controls.Add(this.tipoLabel);
            this.HFipePage.Location = new System.Drawing.Point(4, 22);
            this.HFipePage.Name = "HFipePage";
            this.HFipePage.Padding = new System.Windows.Forms.Padding(3);
            this.HFipePage.Size = new System.Drawing.Size(369, 248);
            this.HFipePage.TabIndex = 1;
            this.HFipePage.Text = "HFipe";
            this.HFipePage.UseVisualStyleBackColor = true;
            // 
            // combustivelCombo
            // 
            this.combustivelCombo.FormattingEnabled = true;
            this.combustivelCombo.Location = new System.Drawing.Point(22, 224);
            this.combustivelCombo.Name = "combustivelCombo";
            this.combustivelCombo.Size = new System.Drawing.Size(142, 21);
            this.combustivelCombo.TabIndex = 42;
            // 
            // anoCombo
            // 
            this.anoCombo.FormattingEnabled = true;
            this.anoCombo.Location = new System.Drawing.Point(22, 184);
            this.anoCombo.Name = "anoCombo";
            this.anoCombo.Size = new System.Drawing.Size(142, 21);
            this.anoCombo.TabIndex = 41;
            // 
            // veiculoCombo
            // 
            this.veiculoCombo.FormattingEnabled = true;
            this.veiculoCombo.Location = new System.Drawing.Point(22, 143);
            this.veiculoCombo.Name = "veiculoCombo";
            this.veiculoCombo.Size = new System.Drawing.Size(142, 21);
            this.veiculoCombo.TabIndex = 40;
            // 
            // modeloCombo
            // 
            this.modeloCombo.FormattingEnabled = true;
            this.modeloCombo.Location = new System.Drawing.Point(22, 106);
            this.modeloCombo.Name = "modeloCombo";
            this.modeloCombo.Size = new System.Drawing.Size(142, 21);
            this.modeloCombo.TabIndex = 39;
            // 
            // marcaCombo
            // 
            this.marcaCombo.FormattingEnabled = true;
            this.marcaCombo.Location = new System.Drawing.Point(22, 66);
            this.marcaCombo.Name = "marcaCombo";
            this.marcaCombo.Size = new System.Drawing.Size(142, 21);
            this.marcaCombo.TabIndex = 38;
            // 
            // tipoCombo
            // 
            this.tipoCombo.FormattingEnabled = true;
            this.tipoCombo.Location = new System.Drawing.Point(22, 27);
            this.tipoCombo.Name = "tipoCombo";
            this.tipoCombo.Size = new System.Drawing.Size(142, 21);
            this.tipoCombo.TabIndex = 37;
            this.tipoCombo.SelectedIndexChanged += new System.EventHandler(this.tipoCombo_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Marca";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(260, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Erro";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Combustível";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Ano";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Modelo";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Veículo";
            // 
            // fipeErroText
            // 
            this.fipeErroText.Location = new System.Drawing.Point(263, 28);
            this.fipeErroText.Multiline = true;
            this.fipeErroText.Name = "fipeErroText";
            this.fipeErroText.Size = new System.Drawing.Size(100, 136);
            this.fipeErroText.TabIndex = 23;
            // 
            // tipoLabel
            // 
            this.tipoLabel.AutoSize = true;
            this.tipoLabel.Location = new System.Drawing.Point(19, 11);
            this.tipoLabel.Name = "tipoLabel";
            this.tipoLabel.Size = new System.Drawing.Size(28, 13);
            this.tipoLabel.TabIndex = 16;
            this.tipoLabel.Text = "Tipo";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 298);
            this.Controls.Add(this.testTab);
            this.Name = "TestForm";
            this.Text = "Test - Handframe";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.testTab.ResumeLayout(false);
            this.HCepTab.ResumeLayout(false);
            this.HCepTab.PerformLayout();
            this.HFipePage.ResumeLayout(false);
            this.HFipePage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cepButton;
        private System.Windows.Forms.Label cepLabel;
        private System.Windows.Forms.TextBox cepText;
        private System.Windows.Forms.TabControl testTab;
        private System.Windows.Forms.TabPage HCepTab;
        private System.Windows.Forms.TabPage HFipePage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label ibgeLabel;
        private System.Windows.Forms.Label estadoLabel;
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.Label enderecoLabel;
        private System.Windows.Forms.TextBox cepErroText;
        private System.Windows.Forms.TextBox ibgeText;
        private System.Windows.Forms.TextBox estadoText;
        private System.Windows.Forms.TextBox cidadeText;
        private System.Windows.Forms.TextBox complementoText;
        private System.Windows.Forms.TextBox enderecoText;
        private System.Windows.Forms.Label erroLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox fipeErroText;
        private System.Windows.Forms.Label tipoLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox tipoCombo;
        private System.Windows.Forms.ComboBox marcaCombo;
        private System.Windows.Forms.ComboBox veiculoCombo;
        private System.Windows.Forms.ComboBox modeloCombo;
        private System.Windows.Forms.ComboBox combustivelCombo;
        private System.Windows.Forms.ComboBox anoCombo;
    }
}

