namespace USP.SI.RC.Server
{
    partial class FrmServer
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
            this.txtLog = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.BtnConectar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtPort = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(0, 47);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(430, 199);
            this.txtLog.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Servidor:";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(64, 14);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(110, 20);
            this.txtIP.TabIndex = 2;
            this.txtIP.Text = "127.0.0.1";
            // 
            // BtnConectar
            // 
            this.BtnConectar.Location = new System.Drawing.Point(346, 12);
            this.BtnConectar.Name = "BtnConectar";
            this.BtnConectar.Size = new System.Drawing.Size(72, 23);
            this.BtnConectar.TabIndex = 3;
            this.BtnConectar.Text = "Iniciar";
            this.BtnConectar.UseVisualStyleBackColor = true;
            this.BtnConectar.Click += new System.EventHandler(this.BtnConectar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Porta:";
            // 
            // TxtPort
            // 
            this.TxtPort.Location = new System.Drawing.Point(221, 14);
            this.TxtPort.Name = "TxtPort";
            this.TxtPort.Size = new System.Drawing.Size(54, 20);
            this.TxtPort.TabIndex = 5;
            this.TxtPort.Text = "2502";
            // 
            // FrmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(430, 244);
            this.Controls.Add(this.TxtPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnConectar);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLog);
            this.Name = "FrmServer";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button BtnConectar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtPort;
    }
}

