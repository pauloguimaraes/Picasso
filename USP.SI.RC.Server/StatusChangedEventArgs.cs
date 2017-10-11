using System;

namespace Server
{
    public class StatusChangedEventArgs : EventArgs
    {


        #region [Atributos]

        public string EventMessage { get; set; }

        #endregion


        #region [Métodos]

        /// <summary>
        /// Evento de mudança de status
        /// </summary>
        /// <param name="strEventMsg">Mensagem</param>
        public StatusChangedEventArgs(string strEventMsg)
        {
            EventMessage = strEventMsg;
        }

        #endregion


    }
}
