using System;
using System.Collections;
using System.Collections.Generic;
using Farmanji.Auth;
using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

namespace Farmanji.Ws
{
    public class WebSocketClient : SingletonPersistent<WebSocketClient>
    {
        #region FIELDS
        [Header("DEVELOPMENT:")]
        [SerializeField] private bool debug;
        [SerializeField] private bool localHost;
        [SerializeField] private int pingRate = 540;
        private WebSocket ws;
        [SerializeField] private string url = "//localhost";
        [SerializeField] private int port = 8080;

        [SerializeField] public event EventHandler<CloseEventArgs> OnClose;
        [SerializeField] public event EventHandler OnConnect, OnSendMsg, OnSendMsgFail, OnStartListening, OnStopListening;
        [SerializeField] public event EventHandler<MessageEventArgs> OnReceiveMsg;
        [SerializeField] public event EventHandler<ErrorEventArgs> OnError;
        #endregion

        #region PROPERTIES
        #endregion

        #region UNITY METHODS
        private void OnDestroy()
        {
            Close();
        }
        #endregion


        #region PUBLIC METHODS
        public void Connect()
        {
            if (localHost)
            {
                ws = new WebSocket("ws://localhost:8080");
            }
            else
            {
                ws = new WebSocket(url + "?authorization=" + SessionManager.Instance.UserData.Token);
                //ws = new WebSocket("wss://ccqhn29yne.execute-api.us-east-1.amazonaws.com/dev?authorization=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjM0dTR0Yno4bGtpa3M3MTgiLCJuZXdFeHBpcmF0aW9uIjo2MDQ4MDAwMDAsInVzZXJJZCI6IjYzYzdmNDIxYTE5MTI4YjgyOWY1NTcwOCIsImlhdCI6MTY5MDMwNjM5MSwiZXhwIjoxNjkwOTExMTkxfQ.0yD0PLvSY79CUhhiUWGiBRT4uAttXQGIdSf9FmudVu4");
            }

            ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;

            ws.OnClose += OnCloseCallback;
            ws.OnOpen += OnConnectCallback;
            ws.OnError += OnErrorCallback;

            if (debug) Debug.Log("[Web Socket Client] Intentando conectarse al servidor.....");
            ws.Connect();
            if (debug) Debug.Log("[Web Socket Client] .....connection status: " + ws.IsAlive + "");
            StartCoroutine(KeepAlive(pingRate));
        }

        public bool IsConnected()
        {
            return ws.IsAlive;
        }
        public void Close()
        {
            if (ws != null)
            {
                ws.OnClose -= OnCloseCallback;
                ws.OnOpen -= OnConnectCallback;
                ws.OnError -= OnErrorCallback;
                try
                {
                    ws.Close();
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }
        public void StartListening()
        {
            if (ws != null)
            {
                try
                {
                    if (debug) Debug.Log("Starting Listening For Messages...");
                    ws.OnMessage += OnReceiveMsgCallback;
                    OnStartListening?.Invoke(this, EventArgs.Empty);
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }



        public void StopListening()
        {
            if (ws != null)
            {
                try
                {
                    if (debug) Debug.Log("Stopping Listening For Messages...");
                    ws.OnMessage -= OnReceiveMsgCallback;
                    OnStopListening?.Invoke(this, EventArgs.Empty);
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }
        public void SendMsg(string msg)
        {
            if (ws == null) return;
            if (!ws.IsAlive) return;
            try
            {
                ws.Send(msg);
                OnSendMsg?.Invoke(this, EventArgs.Empty);
            }
            catch (System.Exception)
            {
                OnSendMsgFail?.Invoke(this, EventArgs.Empty);
                throw;
            }
        }
        #endregion

        #region EVENTS CALLBACKS
        private void OnReceiveMsgCallback(object sender, MessageEventArgs e)
        {
            if (debug) Debug.Log("OnReceiveMsgCallback: " + e.Data);
            OnReceiveMsg?.Invoke(sender, e);
        }
        private void OnErrorCallback(object sender, ErrorEventArgs e)
        {
            if (debug) Debug.Log("OnErrorCallback: " + e.Message+", exception: "+e.Exception);
            OnError?.Invoke(sender, e);
        }

        private void OnConnectCallback(object sender, EventArgs e)
        {
            if (debug) Debug.Log("OnConnectCallback: " + e.ToString());
            OnConnect?.Invoke(sender, e);
        }

        private void OnCloseCallback(object sender, CloseEventArgs e)
        {
            if (debug) Debug.Log("OnCloseCallback: " + e.Reason + ", code: " + e.Code + ", was clean: " + e.WasClean);
            OnClose?.Invoke(sender, e);
        }
        #endregion

        #region COROUTINES
        IEnumerator KeepAlive(int seconds)
        {
            while (ws.IsAlive) {
                string msg = JsonConvert.SerializeObject(WebSocketPing.CreateWebSocketPing("alive", "ping"));
                Debug.Log("PING: " + msg);
                ws.Send(msg);
                yield return new WaitForSecondsRealtime(seconds); 
            }
        }
        #endregion
    }
    [System.Serializable]
    public class WebSocketMessage
    {
        #region FIELDS
        public string text;
        public string senderId;
        public string recipientId;
        public string conversationId;
        #endregion

        public static WebSocketMessage Create(string text, string senderId, string recipientId, string conversationId)
        {
            WebSocketMessage message = new WebSocketMessage();
            message.text = text;
            message.senderId = senderId;
            message.recipientId = recipientId;
            message.conversationId = conversationId;
            return message;
        }
        // "message": {
        //     "text": "Hola buenos dias, soy un dev xd",
        //     "senderId": "63c7f421a19128b829f55708",
        //     "recipientId": "63c7ef693f355422b395a96d",
        //     "conversationId": "64d267dd36de51f544a87877"
        // }
    }
    public class WebSocketPing
    {
        public string action;
        public List<WebSocketMessageComponent> message = new List<WebSocketMessageComponent>();
        //"text": "ping",
        public static WebSocketPing CreateWebSocketPing(string action,string message)
        {
            WebSocketPing wsPing = new WebSocketPing();
            wsPing.action = action;
            wsPing.message.Add(new WebSocketMessageComponent(message));
            return wsPing;
        }
        public class WebSocketMessageComponent
        {
            public string text;
            public WebSocketMessageComponent(string TextMessage) { text = TextMessage; }
        }
    }
}
