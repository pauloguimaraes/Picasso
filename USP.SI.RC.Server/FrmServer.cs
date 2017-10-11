using System.Windows.Forms;
using System.Net;
using System;
using Server;

namespace USP.SI.RC.Server
{
    public partial class FrmServer : Form
    {


        #region [Atributos]

        private delegate void AtualizaStatusCallback(string _par_strMessage);

        #endregion


        #region [Inicialização]

        /// <summary>
        /// Construtor
        /// </summary>
        public FrmServer()
        {
            InitializeComponent();
        }

        #endregion


        #region [Eventos]

        /// <summary>
        /// Evento de click no botão de conectar
        /// </summary>
        /// <param name="_par_objSender">Objeto que disparou o evento</param>
        /// <param name="_par_objEventArgs">Argumentos enviados</param>
        private void BtnConectar_Click(object _par_objSender, EventArgs _par_objEventArgs)
        {
            if (txtIP.Text.Equals(string.Empty))
            {
                MessageBox.Show("Informe o endereço IP.");
                txtIP.Focus();
                return;
            }

            try
            {
                var enderecoIP = IPAddress.Parse(txtIP.Text);
                var mainServidor = new ServerClass(enderecoIP);
                ServerClass.StatusChanged += new StatusChangedEventHandler(StatusChanged);

                mainServidor.StartToListen(Convert.ToInt32(TxtPort.Text));

                txtLog.AppendText("Monitorando as conexões...\r\n");
            }
            catch (Exception _par_objException)
            {
                MessageBox.Show("Erro de conexão : " + _par_objException.Message);
            }
        }

        /// <summary>
        /// Atualização de status
        /// </summary>
        /// <param name="_par_objSender">Objeto que disparou o evento</param>
        /// <param name="_par_objEventArgs">Argumentos enviados</param>
        public void StatusChanged(object _par_objSender, StatusChangedEventArgs _par_objEventArgs)
        {
            Invoke(new AtualizaStatusCallback(AtualizaStatus), new object[] { _par_objEventArgs.EventMessage });
        }

        #endregion


        #region [Métodos]

        /// <summary>
        /// Atualiza o campo de log
        /// </summary>
        /// <param name="_par_strMessage">Mensagem logada</param>
        private void AtualizaStatus(string _par_strMessage)
        {
            txtLog.AppendText(_par_strMessage + "\r\n");
        }

        #endregion


    }
}
