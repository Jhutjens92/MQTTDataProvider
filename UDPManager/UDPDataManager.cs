using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ESPDataProvider.ViewModel;
using Newtonsoft.Json.Linq;
using ESPDataProvider.MQTTManager;

namespace ESPDataProvider.UDPManager
{
    public class UDPDataManager
    {
        MqttDataManager mdmanager = new MqttDataManager();
        string ReceivedMessage;
        dynamic Parsed_ReceivedMessage; //JSON Parser UDP message
        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private const int bufSize = 8 * 1024;
        private State state = new State();
        private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;

        public class State
        {
            public byte[] buffer = new byte[bufSize];
        }

        public void Client(string address, int port)
        {
            _socket.Connect(IPAddress.Parse(address), port);
            Receive();
        }

        private void Receive()
        {
            _socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _socket.EndReceiveFrom(ar, ref epFrom);
                _socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);
                Console.WriteLine("RECV: {0}: {1}, {2}", epFrom.ToString(), bytes, Encoding.ASCII.GetString(so.buffer, 0, bytes));
            }, state);
        }


        private string _txtReceived = " ";
        public string TxtReceived
        {
            get { return _txtReceived; }
            set
            {
                _txtReceived = value;

            }
        }

        public event EventHandler<TextReceivedEventArgs> NewUDPStringReceived;
        protected virtual void OnNewTextReceived(TextReceivedEventArgs e)
        {
            EventHandler<TextReceivedEventArgs> handler = NewUDPStringReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public class TextReceivedEventArgs : EventArgs
        {
            public string GSR { get; set; }
            public string TextReceived { get; set; }
        }

        #region Methods
        // this code runs when a message was received
        void Client_UDPMessageReceived()
        {

            //ReceivedMessage = Encoding.UTF8.GetString();

            if (Globals.IsRecordingMqtt == true)
            {
                JSONParse_ReceivedMessage();
                mdmanager.Publish_Data();
                TextReceivedEventArgs args = new TextReceivedEventArgs();
                args.TextReceived = ReceivedMessage;
                OnNewTextReceived(args);
            }

        }

        public String UpdateText()
        {
            return TxtReceived;
        }

        //parse MQTT JSON String
        public void JSONParse_ReceivedMessage()
        {
            Parsed_ReceivedMessage = JObject.Parse(ReceivedMessage);
        }



        #endregion
    }

}   