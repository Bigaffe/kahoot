namespace kahoot
{
    partial class Form1
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnAnslut = new System.Windows.Forms.Button();
            this.tbxStart = new System.Windows.Forms.TextBox();
            this.tbxAnslut = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbxSend = new System.Windows.Forms.TextBox();
            this.lbxLista = new System.Windows.Forms.ListBox();
            this.lbxInput = new System.Windows.Forms.ListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(179, 11);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 28);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Starta";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnAnslut
            // 
            this.btnAnslut.Location = new System.Drawing.Point(179, 50);
            this.btnAnslut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAnslut.Name = "btnAnslut";
            this.btnAnslut.Size = new System.Drawing.Size(100, 28);
            this.btnAnslut.TabIndex = 1;
            this.btnAnslut.Text = "Anslut";
            this.btnAnslut.UseVisualStyleBackColor = true;
            this.btnAnslut.Click += new System.EventHandler(this.btnAnslut_Click);
            // 
            // tbxStart
            // 
            this.tbxStart.Location = new System.Drawing.Point(285, 18);
            this.tbxStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxStart.Name = "tbxStart";
            this.tbxStart.Size = new System.Drawing.Size(132, 22);
            this.tbxStart.TabIndex = 2;
            // 
            // tbxAnslut
            // 
            this.tbxAnslut.Location = new System.Drawing.Point(285, 58);
            this.tbxAnslut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxAnslut.Name = "tbxAnslut";
            this.tbxAnslut.Size = new System.Drawing.Size(132, 22);
            this.tbxAnslut.TabIndex = 3;
            this.tbxAnslut.Text = "127.0.0.1";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(179, 92);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(132, 28);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbxSend
            // 
            this.tbxSend.Location = new System.Drawing.Point(329, 95);
            this.tbxSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbxSend.Name = "tbxSend";
            this.tbxSend.Size = new System.Drawing.Size(132, 22);
            this.tbxSend.TabIndex = 5;
            // 
            // lbxLista
            // 
            this.lbxLista.FormattingEnabled = true;
            this.lbxLista.ItemHeight = 16;
            this.lbxLista.Location = new System.Drawing.Point(3, 11);
            this.lbxLista.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbxLista.Name = "lbxLista";
            this.lbxLista.Size = new System.Drawing.Size(169, 500);
            this.lbxLista.TabIndex = 6;
            // 
            // lbxInput
            // 
            this.lbxInput.FormattingEnabled = true;
            this.lbxInput.ItemHeight = 16;
            this.lbxInput.Location = new System.Drawing.Point(467, 18);
            this.lbxInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbxInput.Name = "lbxInput";
            this.lbxInput.Size = new System.Drawing.Size(1028, 484);
            this.lbxInput.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1507, 554);
            this.Controls.Add(this.lbxInput);
            this.Controls.Add(this.lbxLista);
            this.Controls.Add(this.tbxSend);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbxAnslut);
            this.Controls.Add(this.tbxStart);
            this.Controls.Add(this.btnAnslut);
            this.Controls.Add(this.btnStart);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnAnslut;
        private System.Windows.Forms.TextBox tbxStart;
        private System.Windows.Forms.TextBox tbxAnslut;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbxSend;
        private System.Windows.Forms.ListBox lbxLista;
        private System.Windows.Forms.ListBox lbxInput;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

