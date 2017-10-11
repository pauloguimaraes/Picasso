namespace USP.SI.RC.Util
{
    public class Message
    {
        public Type Type { get; set; }
        public string Text { get; set; }
        public bool IsPlayerTurn { get; set; }
        public string Word { get; set; }
        public string[] ConnectedClients { get; set; }

    }
}
