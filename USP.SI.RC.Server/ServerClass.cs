using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;
using Newtonsoft.Json;
using Server;

namespace USP.SI.RC.Server
{
    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);
    
    public class ServerClass
    {


        #region [Atributos]

        public static event StatusChangedEventHandler StatusChanged;

        public static Hashtable Users = new Hashtable(30);
        public static Hashtable Connections = new Hashtable(30);
        public static bool IsRight = false;
        public static ArrayList Words = new ArrayList();
        public static string TurnWord;

        private static StatusChangedEventArgs e;

        private IPAddress ipAddress;
        private Thread thrListener;
        private TcpClient tcpClient;
        private TcpListener tlsClient;

        bool IsServerRunning = false;

        #endregion


        #region [Inicialização]

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="_par_objIpAddress">IP de conexão</param>
        public ServerClass(IPAddress _par_objIpAddress)
        {
            ipAddress = _par_objIpAddress;
        }

        #endregion


        #region [Métodos com usuário]

        /// <summary>
        /// Método que adiciona um usuário na lista de conectados
        /// </summary>
        /// <param name="_par_objTcpClient">Cliente conectando</param>
        /// <param name="_par_strUsername">Nome do usuário</param>
        public static void IncluiUsuario(TcpClient _par_objTcpClient, string _par_strUsername)
        {
            Users.Add(_par_strUsername, _par_objTcpClient);
            Connections.Add(_par_objTcpClient, _par_strUsername);
            
            SendMessageAsAdmin(Connections[_par_objTcpClient] + " entrou..");
        }
        
        /// <summary>
        /// Método que remove um determinado cliente da lista de conectados
        /// </summary>
        /// <param name="_par_objTcpClient">Cliente que será removido</param>
        public static void RemoveUsuario(TcpClient _par_objTcpClient)
        {
            var _strUser = Connections[_par_objTcpClient];

            if (Connections[_par_objTcpClient] != null)
            {
                Users.Remove(Connections[_par_objTcpClient]);
                Connections.Remove(_par_objTcpClient);

                SendMessageAsAdmin(_strUser + " saiu...");
            }
        }

        #endregion


        #region [Métodos de envios de mensagem]

        /// <summary>
        /// Método que envia mensagem como administrador para os usuários
        /// </summary>
        /// <param name="_par_strMessage">Mensagem a ser enviada</param>
        public static void SendMessageAsAdmin(string _par_strMessage)
        {
            e = new StatusChangedEventArgs("Administrador: " + _par_strMessage);
            OnStatusChanged(e);

            var tcpClientes = new TcpClient[Users.Count];
            Users.Values.CopyTo(tcpClientes, 0);
            for (int i = 0; i < tcpClientes.Length; i++)
            {
                try
                {
                    if (_par_strMessage.Trim() == "" || tcpClientes[i] == null)
                        continue;

                    var swSenderSender = new StreamWriter(tcpClientes[i].GetStream());
                    swSenderSender.WriteLine(JsonConvert.SerializeObject(new Util.Message { Type = Util.Type.Control, Text = "Administrador: " + _par_strMessage }));
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch
                {
                    RemoveUsuario(tcpClientes[i]);
                }
            }
        }

        /// <summary>
        /// Método que trafega mensagens para os usuários
        /// </summary>
        /// <param name="_par_strFrom">Usuário enviando a mensagem</param>
        /// <param name="_par_strMessage">Mensagem que está sendo enviada</param>
        public static void SendMessage(string _par_strFrom, string _par_strMessage)
        {
            e = new StatusChangedEventArgs(_par_strFrom + " disse : " + _par_strMessage);
            OnStatusChanged(e);

            var tcpClientes = new TcpClient[Users.Count];
            Users.Values.CopyTo(tcpClientes, 0);
            for (int i = 0; i < tcpClientes.Length; i++)
            {
                try
                {
                    if (_par_strMessage.Trim() == "" || tcpClientes[i] == null)
                        continue;

                    var swSenderSender = new StreamWriter(tcpClientes[i].GetStream());
                    swSenderSender.WriteLine(JsonConvert.SerializeObject(
                        new Util.Message {
                            Type = Util.Type.Control,
                            Text = _par_strFrom + " disse: " + _par_strMessage }));
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch
                {
                    RemoveUsuario(tcpClientes[i]);
                }
            }
        }


        #endregion


        #region [Eventos]

        /// <summary>
        /// Evento que notifica a mudança de status
        /// </summary>
        /// <param name="_par_objEventArgs">Argumentos do evento</param>
        public static void OnStatusChanged(StatusChangedEventArgs _par_objEventArgs)
        {
            StatusChanged?.Invoke(null, _par_objEventArgs);
        }

        #endregion


        #region [Ações do servidor]


        /// <summary>
        /// Método que inicia o trabalho do servidor
        /// </summary>
        /// <param name="_par_intPort">Porta a qual o servidor estará ouvindo</param>
        public void StartToListen(int _par_intPort)
        {
            try
            {
                Words.Add("VACA");
                Words.Add("CACHORRO");
                Words.Add("TUCANO");
                Words.Add("LEÃO");
                Words.Add("ARANHA");
                Words.Add("FORMIGA");
                Words.Add("CARANGUEIJO");

                IPAddress ipaLocal = ipAddress;
                tlsClient = new TcpListener(ipaLocal, _par_intPort);
                tlsClient.Start();

                IsServerRunning = true;

                thrListener = new Thread(KeepAlive);
                thrListener.Start();

                Thread temporizados = new Thread(Temporizador);
                temporizados.Start();
            }
            catch (Exception _par_objException)
            {
                throw _par_objException;
            }
        }


        /// <summary>
        /// Temporizador que controla a palavra da vez e o usuário que deve desenhar
        /// </summary>
        private void Temporizador()
        {
            while (IsServerRunning)
            {
                int playerDaVez = -1;
                while (Users.Count > 1)
                {
                    playerDaVez++;
                    TurnWord = (string)Words[new Random().Next(Words.Count)];
                    BeginTurn(playerDaVez);
                    while (!IsRight && Users.Count > 1)
                        Thread.Sleep(1000);

                    if (IsRight)
                        IsRight = false;
                }

                Thread.Sleep(2000);
            }
        }

        /// <summary>
        /// Inicia o turno para determinado usúário
        /// </summary>
        /// <param name="_par_intTurnPlayer">Índice do usuário</param>
        private void BeginTurn(int _par_intTurnPlayer)
        {
            StatusChangedEventArgs ePlayerDaVez = new StatusChangedEventArgs("servidor disse : " + TurnWord);
            OnStatusChanged(e);

            var tcpClientes = new TcpClient[Users.Count];
            Users.Values.CopyTo(tcpClientes, 0);

            var ips = new string[tcpClientes.Length];
            for (int i = 0; i < tcpClientes.Length; i++)
                ips[i] = ((IPEndPoint)tcpClientes[i].Client.RemoteEndPoint).Address.ToString();

            for (int i = 0; i < tcpClientes.Length; i++)
            {
                try
                {
                    if (tcpClientes[i] == null)
                        continue;

                    var swSenderSender = new StreamWriter(tcpClientes[i].GetStream());
                    if (i == _par_intTurnPlayer)
                        swSenderSender.WriteLine(JsonConvert.SerializeObject(new Util.Message { Type = Util.Type.NewTurn, Word = TurnWord, IsPlayerTurn = true, ConnectedClients = ips }));
                    else
                        swSenderSender.WriteLine(JsonConvert.SerializeObject(new Util.Message { Type = Util.Type.NewTurn, IsPlayerTurn = false }));

                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch
                {
                    RemoveUsuario(tcpClientes[i]);
                }
            }
        }

        /// <summary>
        /// Mantém a conexão aberta
        /// </summary>
        private void KeepAlive()
        {
            while (IsServerRunning)
            {
                tcpClient = tlsClient.AcceptTcpClient();
                new Connection(tcpClient);
            }
        }

        #endregion


    }
    
}
