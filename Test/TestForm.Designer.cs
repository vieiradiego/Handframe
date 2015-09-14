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
            this.erroText = new System.Windows.Forms.TextBox();
            this.ibgeText = new System.Windows.Forms.TextBox();
            this.estadoText = new System.Windows.Forms.TextBox();
            this.cidadeText = new System.Windows.Forms.TextBox();
            this.complementoText = new System.Windows.Forms.TextBox();
            this.enderecoText = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.testTab.SuspendLayout();
            this.HCepTab.SuspendLayout();
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
            // 
            // testTab
            // 
            this.testTab.Controls.Add(this.HCepTab);
            this.testTab.Controls.Add(this.tabPage2);
            this.testTab.Location = new System.Drawing.Point(12, 12);
            this.testTab.Name = "testTab";
            this.testTab.SelectedIndex = 0;
            this.testTab.Size = new System.Drawing.Size(377, 237);
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
            this.HCepTab.Controls.Add(this.erroText);
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
            this.HCepTab.Size = new System.Drawing.Size(369, 211);
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
            // erroText
            // 
            this.erroText.Location = new System.Drawing.Point(244, 158);
            this.erroText.Name = "erroText";
            this.erroText.Size = new System.Drawing.Size(100, 20);
            this.erroText.TabIndex = 8;
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
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(369, 211);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 261);
            this.Controls.Add(this.testTab);
            this.Name = "TestForm";
            this.Text = "Test - Handframe";
            this.testTab.ResumeLayout(false);
            this.HCepTab.ResumeLayout(false);
            this.HCepTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cepButton;
        private System.Windows.Forms.Label cepLabel;
        private System.Windows.Forms.TextBox cepText;
        private System.Windows.Forms.TabControl testTab;
        private System.Windows.Forms.TabPage HCepTab;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label ibgeLabel;
        private System.Windows.Forms.Label estadoLabel;
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.Label enderecoLabel;
        private System.Windows.Forms.TextBox erroText;
        private System.Windows.Forms.TextBox ibgeText;
        private System.Windows.Forms.TextBox estadoText;
        private System.Windows.Forms.TextBox cidadeText;
        private System.Windows.Forms.TextBox complementoText;
        private System.Windows.Forms.TextBox enderecoText;
        private System.Windows.Forms.Label erroLabel;
    }
}

