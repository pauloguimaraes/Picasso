namespace USP.SI.RC.Client
{
    partial class FrmClient
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
            this.txtServidorIP = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.txtMensagem = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.lblPlayer = new System.Windows.Forms.Label();
            this.lblPalavra = new System.Windows.Forms.Label();
            this.lblWatcher = new System.Windows.Forms.Label();
            this.drawingBoard1 = new USP.SI.RC.Components.DrawingBoard();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtServidorIP
            // 
            this.txtServidorIP.Location = new System.Drawing.Point(12, 23);
            this.txtServidorIP.Name = "txtServidorIP";
            this.txtServidorIP.Size = new System.Drawing.Size(94, 20);
            this.txtServidorIP.TabIndex = 0;
            this.txtServidorIP.Text = "127.0.0.1";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(177, 23);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(94, 20);
            this.txtUsuario.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Usuário:";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(10, 78);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(238, 296);
            this.txtLog.TabIndex = 4;
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(177, 49);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(94, 23);
            this.BtnConnect.TabIndex = 5;
            this.BtnConnect.Text = "&Conectar";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // txtMensagem
            // 
            this.txtMensagem.Location = new System.Drawing.Point(10, 380);
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(148, 20);
            this.txtMensagem.TabIndex = 6;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(164, 378);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(84, 23);
            this.btnEnviar.TabIndex = 7;
            this.btnEnviar.Text = "Enviar Palpite";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // lblPlayer
            // 
            this.lblPlayer.AutoSize = true;
            this.lblPlayer.Location = new System.Drawing.Point(385, 29);
            this.lblPlayer.Name = "lblPlayer";
            this.lblPlayer.Size = new System.Drawing.Size(183, 13);
            this.lblPlayer.TabIndex = 10;
            this.lblPlayer.Text = "DESENHA PARA QUE ADIVINHEM:";
            this.lblPlayer.Visible = false;
            // 
            // lblPalavra
            // 
            this.lblPalavra.AutoSize = true;
            this.lblPalavra.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPalavra.Location = new System.Drawing.Point(573, 29);
            this.lblPalavra.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPalavra.Name = "lblPalavra";
            this.lblPalavra.Size = new System.Drawing.Size(107, 13);
            this.lblPalavra.TabIndex = 11;
            this.lblPalavra.Text = "NOME DESENHO";
            this.lblPalavra.Visible = false;
            // 
            // lblWatcher
            // 
            this.lblWatcher.AutoSize = true;
            this.lblWatcher.Location = new System.Drawing.Point(388, 49);
            this.lblWatcher.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWatcher.Name = "lblWatcher";
            this.lblWatcher.Size = new System.Drawing.Size(128, 13);
            this.lblWatcher.TabIndex = 12;
            this.lblWatcher.Text = "ADIVINHE O DESENHO!";
            this.lblWatcher.Visible = false;
            // 
            // drawingBoard1
            // 
            this.drawingBoard1.BroadCastMethod = null;
            this.drawingBoard1.EnableEdit = false;
            this.drawingBoard1.Location = new System.Drawing.Point(253, 78);
            this.drawingBoard1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.drawingBoard1.Name = "drawingBoard1";
            this.drawingBoard1.Size = new System.Drawing.Size(450, 298);
            this.drawingBoard1.TabIndex = 8;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(109, 23);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(49, 20);
            this.txtPort.TabIndex = 13;
            this.txtPort.Text = "2502";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(106, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Porta:";
            // 
            // FrmClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(709, 417);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lblWatcher);
            this.Controls.Add(this.lblPalavra);
            this.Controls.Add(this.lblPlayer);
            this.Controls.Add(this.drawingBoard1);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.txtMensagem);
            this.Controls.Add(this.BtnConnect);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.txtServidorIP);
            this.Name = "FrmClient";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.FrmClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServidorIP;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.TextBox txtMensagem;
        private System.Windows.Forms.Button btnEnviar;
        private USP.SI.RC.Components.DrawingBoard drawingBoard1;
        private System.Windows.Forms.Label lblPlayer;
        private System.Windows.Forms.Label lblPalavra;
        private System.Windows.Forms.Label lblWatcher;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label3;
    }
}

