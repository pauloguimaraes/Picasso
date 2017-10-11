using Newtonsoft.Json;
using Server;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace USP.SI.RC.Server
{
    public class Connection
    {


        #region [Atributos]

        TcpClient tcpCliente;

        private Thread thrSender;
        private StreamReader srReceptor;
        private StreamWriter swEnviador;
        private string usuarioAtual;
        private string strResposta;

        #endregion


        #region [Inicialização]

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="_par_objTcpClient">Servidor abrindo para conexão</param>
        public Connection(TcpClient _par_objTcpClient)
        {
            tcpCliente = _par_objTcpClient;
            thrSender = new Thread(AcceptClient);
            thrSender.Start();
        }

        #endregion


        #region [Métodos]

        /// <summary>
        /// Encerra a conexão aberta
        /// </summary>
        private void CloseConnection()
        {
            tcpCliente.Close();
            srReceptor.Close();
            swEnviador.Close();
        }

        /// <summary>
        /// Aceita o usuário que está tentando conectar
        /// </summary>
        private void AcceptClient()
        {
            srReceptor = new StreamReader(tcpCliente.GetStream());
            swEnviador = new StreamWriter(tcpCliente.GetStream());

            usuarioAtual = srReceptor.ReadLine();

            if (usuarioAtual != "")
            {
                if (ServerClass.Users.Contains(usuarioAtual))
                {
                    swEnviador.WriteLine(JsonConvert.SerializeObject(new USP.SI.RC.Util.Message { Type = USP.SI.RC.Util.Type.Control, Text = "0|Este nome de usuário já existe." }));
                    swEnviador.Flush();
                    CloseConnection();
                    return;
                }
                else if (usuarioAtual == "Administrator")
                {
                    swEnviador.WriteLine(JsonConvert.SerializeObject(new Util.Message { Type = Util.Type.Control, Text = "0|Este nome de usuário é reservado." }));
                    swEnviador.Flush();
                    CloseConnection();
                    return;
                }
                else
                {
                    swEnviador.WriteLine(JsonConvert.SerializeObject(new Util.Message { Type = Util.Type.Control, Text = "1|Conectou com sucesso." }));
                    swEnviador.Flush();

                    ServerClass.IncluiUsuario(tcpCliente, usuarioAtual);
                }
            }
            else
            {
                CloseConnection();
                return;
            }

            try
            {
                while ((strResposta = srReceptor.ReadLine()) != "")
                {
                    if (strResposta == null)
                        ServerClass.RemoveUsuario(tcpCliente);
                    else
                    {
                        if (strResposta.ToUpper().Equals(ServerClass.TurnWord))
                        {
                            ServerClass.IsRight = true;
                            ServerClass.SendMessageAsAdmin(usuarioAtual + " acertou!");
                        }
                        ServerClass.SendMessage(usuarioAtual, strResposta);
                    }
                }
            }
            catch
            {
                ServerClass.RemoveUsuario(tcpCliente);
            }
        }

        #endregion


    }
}
