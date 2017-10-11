using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System;
using Newtonsoft.Json;
using System.Drawing.Imaging;

namespace USP.SI.RC.Client
{
    public partial class FrmClient : Form
    {


        #region [Atributos]

        private string Username = "Desconhecido";
        private bool Connected;
        private string[] ClientsList;

        private StreamWriter StwSender;
        private StreamReader StrReceiver;
        private TcpClient TcpServer;
        private Thread ThrMessage;
        private IPAddress IpAddress;
        UdpClient Socket;
        UdpClient SenderSocket;

        #endregion


        #region [Eventos]

        /// <summary>
        /// Atualiza o log
        /// </summary>
        /// <param name="_par_strMessage">Mensagem trafegada</param>
        private delegate void UpdateLogCallback(string _par_strMessage);

        /// <summary>
        /// Fecha a conexão
        /// </summary>
        /// <param name="_par_strReason">Razão do fechamento da conexão</param>
        private delegate void CloseConnectionCallback(string _par_strReason);

        /// <summary>
        /// Evento de click no botão conectar
        /// </summary>
        /// <param name="_par_objSender">Objeto que disparou o evento</param>
        /// <param name="_par_objEventArgs">Argumentos enviados</param>
        private void BtnConnect_Click(object _par_objSender, EventArgs _par_objEventArgs)
        {
            if (!Connected)
                BeginConnection();
            else
                CloseConnection("Desconectado a pedido do usuário.");
        }

        /// <summary>
        /// Evento de envio
        /// </summary>
        /// <param name="_par_objSender">Objeto que disparou o evento</param>
        /// <param name="_par_objEventArgs">Argumentos recebidos</param>
        private void BtnSend_Click(object _par_objSender, EventArgs _par_objEventArgs)
        {
            SendMessage();
        }

        /// <summary>
        /// Saindo da aplicação
        /// </summary>
        /// <param name="_par_objSender">Objeto que disparou o evento</param>
        /// <param name="_par_objEventArgs">Argumentos recebidos</param>
        public void OnApplicationExit(object _par_objSender, EventArgs _par_objEventArgs)
        {
            if (Connected)
            {
                Connected = false;
                StwSender.Close();
                StrReceiver.Close();
                TcpServer.Close();
            }
        }

        /// <summary>
        /// Evento de carregamento do formulário
        /// </summary>
        /// <param name="_par_objSender">Objeto que disparou o evento</param>
        /// <param name="_par_objEventArgs">Argumentos recebidos</param>
        private void FrmClient_Load(object _par_objSender, EventArgs _par_objEventArgs)
        {
            drawingBoard1.EnableEdit = true;
        }

        #endregion


        #region [Inicialização]

        /// <summary>
        /// Construtor
        /// </summary>
        public FrmClient()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            Directory.CreateDirectory("C:\\temp");
            InitializeComponent();
        }

        #endregion


        #region [Interações com o servidor]

        /// <summary>
        /// Inicia a conexão com o servidor
        /// </summary>
        private void BeginConnection()
        {
            try
            {
                IpAddress = IPAddress.Parse(txtServidorIP.Text);

                TcpServer = new TcpClient();
                TcpServer.Connect(IpAddress, Convert.ToInt32(txtPort.Text));
                
                Connected = true;
                
                Username = txtUsuario.Text;
                
                txtServidorIP.Enabled = false;
                txtPort.Enabled = false;
                txtUsuario.Enabled = false;
                txtMensagem.Enabled = true;
                btnEnviar.Enabled = true;
                BtnConnect.Text = "Desconectar";
                
                StwSender = new StreamWriter(TcpServer.GetStream());
                StwSender.WriteLine(txtUsuario.Text);
                StwSender.Flush();
                
                ThrMessage = new Thread(new ThreadStart(ReceiveMessages));
                ThrMessage.Start();
            }
            catch (Exception _par_objException)
            {
                MessageBox.Show("Erro : " + _par_objException.Message, "Erro na conexão com servidor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Método responsável por receber as mensagens
        /// </summary>
        private void ReceiveMessages()
        {
            StrReceiver = new StreamReader(TcpServer.GetStream());

            string ConResposta = JsonConvert.DeserializeObject<USP.SI.RC.Util.Message>(StrReceiver.ReadLine()).Text;
            
            if (ConResposta[0] == '1')
                Invoke(new UpdateLogCallback(this.UpdateLog), new object[] {
                    JsonConvert.SerializeObject(new USP.SI.RC.Util.Message() {
                        Text = "Conectado com sucesso!",
                        Type = USP.SI.RC.Util.Type.Control
                    }) });
            else
            {
                string Motivo = "Não Conectado: ";
                Motivo += ConResposta.Substring(2, ConResposta.Length - 2);
                this.Invoke(new CloseConnectionCallback(this.CloseConnection), new object[] {
                    JsonConvert.SerializeObject(new USP.SI.RC.Util.Message() {
                        Text = Motivo,
                        Type = USP.SI.RC.Util.Type.Control
                    }) });
                return;
            }
            
            while (Connected)
                Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { StrReceiver.ReadLine() });
        }

        /// <summary>
        /// Envia a mensagem para o servidor
        /// </summary>
        private void SendMessage()
        {
            if (txtMensagem.Lines.Length >= 1)
            {
                StwSender.WriteLine(txtMensagem.Text);
                StwSender.Flush();
                txtMensagem.Lines = null;
            }
            txtMensagem.Text = "";
        }
                
        /// <summary>
        /// Fehca a conexão
        /// </summary>
        /// <param name="_par_strMessage">Mensagem de fechamento</param>
        private void CloseConnection(string _par_strMessage)
        {
            String Motivo = JsonConvert.DeserializeObject<USP.SI.RC.Util.Message>(_par_strMessage).Text;
            txtLog.AppendText(Motivo + "\r\n");
            txtServidorIP.Enabled = true;
            txtUsuario.Enabled = true;
            txtMensagem.Enabled = false;
            btnEnviar.Enabled = false;
            BtnConnect.Text = "Conectado";
            
            Connected = false;
            StwSender.Close();
            StrReceiver.Close();
            TcpServer.Close();
        }

        #endregion


        #region [Métodos do Broadcast]

        /// <summary>
        /// Responsável por iniciar a thread do broadcast
        /// </summary>
        public void BroadcastControl()
        {
            Thread t = new Thread(BroadcastBoard);
            t.Start();
        }

        /// <summary>
        /// Trata da interação com o painel de desenho pelo broadcast
        /// </summary>
        private void BroadcastBoard()
        {
            SenderSocket = new UdpClient(5395);

            if (drawingBoard1.EnableEdit)
            {
                foreach (string s in ClientsList)
                {
                    IPEndPoint target = new IPEndPoint(IPAddress.Parse(s), 5394);
                    
                    byte[] message;

                    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                    
                    Encoder myEncoder = Encoder.Quality;

                    EncoderParameters myEncoderParameters = new EncoderParameters(1);

                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    
                    using (MemoryStream ms = new MemoryStream())
                    {
                        drawingBoard1.GetBitmap().Save(ms, jpgEncoder, myEncoderParameters);
                        message = ms.ToArray();
                    }

                    SenderSocket.Send(message, message.Length, target);
                }
            }

            SenderSocket.Close();
        }

        /// <summary>
        /// Método que trata do trafego através de UDP
        /// </summary>
        /// <param name="_par_objAsyncResult">Resultado</param>
        private void OnUdpData(IAsyncResult _par_objAsyncResult)
        {
            UdpClient socket = _par_objAsyncResult.AsyncState as UdpClient;
            IPEndPoint source = new IPEndPoint(IPAddress.Any, 0);
            Byte[] buffer = ((UdpClient)_par_objAsyncResult.AsyncState).EndReceive(_par_objAsyncResult, ref source);

            try
            {
                SaveData(buffer);
                drawingBoard1.SetBitmap("C:\\temp\\temp.jpg");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            socket.BeginReceive(new AsyncCallback(OnUdpData), socket);
        }

        #endregion


        #region [Métodos]

        /// <summary>
        /// Atualiza o log do cliente
        /// </summary>
        /// <param name="_par_strMessage">Mensagem a atualizar</param>
        private void UpdateLog(string _par_strMessage)
        {
            var msg = JsonConvert.DeserializeObject<USP.SI.RC.Util.Message>(_par_strMessage);

            if (msg.Type == USP.SI.RC.Util.Type.NewTurn)
            {
                drawingBoard1.Clean();
                drawingBoard1.EnableEdit = msg.IsPlayerTurn;
                if (msg.IsPlayerTurn)
                {
                    lblPalavra.Text = msg.Word;
                    lblPlayer.Visible = true;
                    lblPalavra.Visible = true;
                    lblWatcher.Visible = false;
                    txtMensagem.Enabled = false;
                    ClientsList = msg.ConnectedClients;
                    drawingBoard1.Clean();
                    drawingBoard1.BroadCastMethod = BroadcastControl;
                }
                else
                {
                    drawingBoard1.Clean();

                    try
                    {
                        Socket = new UdpClient(5394);
                        Socket.BeginReceive(new AsyncCallback(OnUdpData), Socket);
                    }
                    catch { }

                    lblPlayer.Visible = false;
                    lblPalavra.Visible = false;
                    lblWatcher.Visible = true;
                    txtMensagem.Enabled = true;
                }
            }
            else
                txtLog.AppendText(msg.Text + "\r\n");
        }

        /// <summary>
        /// Salva a imagem trafegada em um arquivo temporário
        /// </summary>
        /// <param name="_par_arrDados">Array a ser salvo</param>
        /// <returns></returns>
        protected bool SaveData(byte[] _par_arrDados)
        {
            BinaryWriter Writer = null;
            string Name = @"C:\temp\temp.jpg";

            try
            {
                Writer = new BinaryWriter(File.OpenWrite(Name));             
                Writer.Write(_par_arrDados);
                Writer.Flush();
                Writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Decoficador de imagem
        /// </summary>
        /// <param name="_par_objFormat">Formato da decodificação</param>
        /// <returns></returns>
        private ImageCodecInfo GetEncoder(ImageFormat _par_objFormat)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == _par_objFormat.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        #endregion


    }
}
