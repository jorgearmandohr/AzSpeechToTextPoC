namespace SpeechDemo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.txbPath = new System.Windows.Forms.TextBox();
            this.rtxbTranscript = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.rtxbTranslate = new System.Windows.Forms.RichTextBox();
            this.rdbtnEn = new System.Windows.Forms.RadioButton();
            this.rdbtnEs = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(448, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "Select";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txbPath
            // 
            this.txbPath.BackColor = System.Drawing.Color.White;
            this.txbPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbPath.Location = new System.Drawing.Point(13, 30);
            this.txbPath.Name = "txbPath";
            this.txbPath.ReadOnly = true;
            this.txbPath.Size = new System.Drawing.Size(429, 27);
            this.txbPath.TabIndex = 1;
            // 
            // rtxbTranscript
            // 
            this.rtxbTranscript.BackColor = System.Drawing.Color.White;
            this.rtxbTranscript.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtxbTranscript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxbTranscript.Location = new System.Drawing.Point(0, 0);
            this.rtxbTranscript.Name = "rtxbTranscript";
            this.rtxbTranscript.ReadOnly = true;
            this.rtxbTranscript.Size = new System.Drawing.Size(385, 399);
            this.rtxbTranscript.TabIndex = 2;
            this.rtxbTranscript.Text = "";
            this.rtxbTranscript.TextChanged += new System.EventHandler(this.rtxbTranscript_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Audio file | *.wav";
            // 
            // rtxbTranslate
            // 
            this.rtxbTranslate.BackColor = System.Drawing.Color.White;
            this.rtxbTranslate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtxbTranslate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxbTranslate.Location = new System.Drawing.Point(0, 0);
            this.rtxbTranslate.Name = "rtxbTranslate";
            this.rtxbTranslate.ReadOnly = true;
            this.rtxbTranslate.Size = new System.Drawing.Size(388, 399);
            this.rtxbTranslate.TabIndex = 2;
            this.rtxbTranslate.Text = "";
            // 
            // rdbtnEn
            // 
            this.rdbtnEn.AutoSize = true;
            this.rdbtnEn.Checked = true;
            this.rdbtnEn.Location = new System.Drawing.Point(705, 33);
            this.rdbtnEn.Name = "rdbtnEn";
            this.rdbtnEn.Size = new System.Drawing.Size(49, 24);
            this.rdbtnEn.TabIndex = 3;
            this.rdbtnEn.TabStop = true;
            this.rdbtnEn.Text = "EN";
            this.rdbtnEn.UseVisualStyleBackColor = true;
            // 
            // rdbtnEs
            // 
            this.rdbtnEs.AutoSize = true;
            this.rdbtnEs.Location = new System.Drawing.Point(751, 33);
            this.rdbtnEs.Name = "rdbtnEs";
            this.rdbtnEs.Size = new System.Drawing.Size(46, 24);
            this.rdbtnEs.TabIndex = 3;
            this.rdbtnEs.Text = "ES";
            this.rdbtnEs.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.rtxbTranscript);
            this.panel1.Location = new System.Drawing.Point(13, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(385, 399);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.rtxbTranslate);
            this.panel2.Location = new System.Drawing.Point(406, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(388, 399);
            this.panel2.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 474);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rdbtnEs);
            this.Controls.Add(this.rdbtnEn);
            this.Controls.Add(this.txbPath);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Speech to Text Demo";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txbPath;
        private System.Windows.Forms.RichTextBox rtxbTranscript;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox rtxbTranslate;
        private System.Windows.Forms.RadioButton rdbtnEn;
        private System.Windows.Forms.RadioButton rdbtnEs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

