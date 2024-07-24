using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Farmanji.Game;
using Farmanji.Managers;
using UnityEngine;
using WebSocketSharp;

namespace Farmanji.Ws
{
    public class WebSocketMsgsHandler : SingletonPersistent<WebSocketMsgsHandler>
    {
        #region FIELDS
        [Header("WEB SOCKET SERVER SYSTEM EVENTS:")]
        [SerializeField, TextArea(5, 25)] private string serverStatus;
        // [SerializeField] private event EventHandler<CloseEventArgs> OnClose;
        // [SerializeField] private event EventHandler OnConnect, OnSendMsg, OnSendMsgFail, OnStartListening, OnStopListening;
        // [SerializeField] private event EventHandler<MessageEventArgs> OnReceiveMsg;
        // [SerializeField] private event EventHandler<ErrorEventArgs> OnError;


        //WEBSOCKET FARMAJIN MESSAGES
        [Header("FARMAJIN USER CHATS:")]
        [SerializeField, TextArea(5, 25)] private string rawMsg;
        [SerializeField, TextArea(5, 25)] private string msg;
        [SerializeField] private string senderId;
        [SerializeField] private event EventHandler OnChatMsgReceived;
        [Header("FARMAJIN BADGES EVENTS:")]
        [SerializeField, TextArea(5, 25)] private string badgeMsg;
        [SerializeField] private event EventHandler OnBadgeUnlockedMsgReceived;
        [Header("FARMAJIN CHALLENGES EVENTS:")]
        [SerializeField, TextArea(5, 25)] private string challengesMsg;
        [SerializeField] private event EventHandler OnChallengeMsgReceived;
        [Header("FARMAJIN CHALLENGES UNLOCKED EVENTS:")]
        [SerializeField, TextArea(5, 25)] private string challengesUnlockedMsg;
        [SerializeField] private event EventHandler OnChallengeUnlockedMsgReceived;
        [Header("FARMAJIN ITEMS UNLOCKED EVENTS:")]
        [SerializeField, TextArea(5, 25)] private string itemsUnlocledMsg;
        [SerializeField] private event EventHandler OnItemUnlockedMsgReceived;
        [Header("FARMAJIN PRODUCTS UNLOCKED EVENTS:")]
        [SerializeField, TextArea(5, 25)] private string productsUnlockedMsg;
        [SerializeField] private event EventHandler OnProductUnlockedMsgReceived;
        [Header("SETTINGS:")]
        [SerializeField] private int reconectionTries = 2;
        [SerializeField] private bool debug;
        #endregion

        #region PROPERTIES
        public string RawMsg { get { return rawMsg; } }
        public string ChallengeMsg { get { return challengesMsg; } }
        public string BadgeMsg { get { return badgeMsg; } }
        public string ChallengesUnlockedMsg { get { return challengesUnlockedMsg; } }
        public string ItemsUnlocledMsg { get { return itemsUnlocledMsg; } }
        public string ProductsUnlockedMsg { get { return productsUnlockedMsg; } }
        // public EventHandler<CloseEventArgs> OnCloseEvent { get { return OnClose; } set { OnClose = value; } }
        // public EventHandler OnConnectEvent { get { return OnConnect; } set { OnConnect = value; } }
        // public EventHandler OnSendMsgEvent { get { return OnSendMsg; } set { OnSendMsg = value; } }
        // public EventHandler OnSendMsgFailEvent { get { return OnSendMsgFail; } set { OnSendMsgFail = value; } }
        // public EventHandler OnStartListeningEvent { get { return OnStartListening; } set { OnStartListening = value; } }
        // public EventHandler OnStopListeningEvent { get { return OnStopListening; } set { OnStopListening = value; } }
        // public EventHandler<MessageEventArgs> OnReceiveMsgEvent { get { return OnReceiveMsg; } set { OnReceiveMsg = value; } }
        // public EventHandler<ErrorEventArgs> OnErrorEvent { get { return OnError; } set { OnError = value; } }
        public string Msg { get { return msg; } }
        public string SenderID { get { return senderId; } }
        public EventHandler OnChatMsgReceivedEvent { get { return OnChatMsgReceived; } set { OnChatMsgReceived = value; } }
        public EventHandler OnChallengeMsgReceivedEvent { get { return OnChallengeMsgReceived; } set { OnChallengeMsgReceived = value; } }
        public EventHandler OnChallengeUnlockedMsgReceivedEvent { get { return OnChallengeUnlockedMsgReceived; } set { OnChallengeUnlockedMsgReceived = value; } }
        public EventHandler OnBadgeUnlockedMsgReceivedEvent { get { return OnBadgeUnlockedMsgReceived; } set { OnBadgeUnlockedMsgReceived = value; } }
        public EventHandler OnItemUnlockedMsgReceivedEvent { get { return OnItemUnlockedMsgReceived; } set { OnItemUnlockedMsgReceived = value; } }
        public EventHandler OnProductUnlockedMsgReceivedEvent { get { return OnProductUnlockedMsgReceived; } set { OnProductUnlockedMsgReceived = value; } }
        #endregion

        #region UNITY METHODS
        void Update()
        {
            if (rawMsg== "Going away")
            {
                Debug.Log("WEBSOCKET SERVER DISCONNECTED DUE TO INACTIVITY");
                rawMsg = "";
            }
            if (rawMsg!="")
            {
                Debug.Log("PROCESANDO MENSAJE");
                MessageTypeSwitch();
                rawMsg = "";
            }
            
        }
        //private void OnDisable()
        //{
        //    if (WebSocketClient.Instance.IsConnected()) UnsusbcribeToServerSystemEvents();
        //}
        #endregion

        #region PRIVATE METHODS
        private void SusbcribeToServerSystemEvents()
        {
            WebSocketClient.Instance.OnClose += OnChatClose;
            WebSocketClient.Instance.OnConnect += OnChatConnect;
            WebSocketClient.Instance.OnError += OnChatError;
            WebSocketClient.Instance.OnReceiveMsg += OnMsgReceive;
            WebSocketClient.Instance.OnSendMsg += OnMsgSended;
            WebSocketClient.Instance.OnSendMsgFail += OnMsgFail;
        }
        private void UnsusbcribeToServerSystemEvents()
        {
            WebSocketClient.Instance.OnClose -= OnChatClose;
            WebSocketClient.Instance.OnConnect -= OnChatConnect;
            WebSocketClient.Instance.OnError -= OnChatError;
            WebSocketClient.Instance.OnReceiveMsg -= OnMsgReceive;
            WebSocketClient.Instance.OnSendMsg -= OnMsgSended;
            WebSocketClient.Instance.OnSendMsgFail -= OnMsgFail;
        }
        #endregion

        #region PUBLIC METHODS
        public static string TrimServerMsg(string msg, string start, int startOffset, string end, int endOffset)
        {
            return msg.Substring(msg.IndexOf(start) + start.Length + startOffset).Substring(0, msg.Substring(msg.IndexOf(start) + start.Length + startOffset).IndexOf(end) + endOffset);
        }
        public bool ConnectToWebSocketServer()
        {
            WebSocketClient.Instance.Connect();
            WebSocketClient.Instance.StartListening();
            if (WebSocketClient.Instance.IsConnected())
            {
                if (debug) Debug.Log("trying to subscribe to websocket server events");
                SusbcribeToServerSystemEvents();
            }
            return WebSocketClient.Instance.IsConnected();
        }
        #endregion

        #region EVENTS CALLBACKS
        private void OnMsgFail(object sender, EventArgs e)
        {
            serverStatus = "";
            if (debug) Debug.Log("[ChatTab] OnMsgFail: ");
        }
        private void OnMsgSended(object sender, EventArgs e)
        {

            if (debug) Debug.Log("[ChatTab] OnMsgSended: ");
        }
        private void OnMsgReceive(object sender, MessageEventArgs e)
        {
            serverStatus = "OnMsgReceive\n IsBinary: " + e.IsBinary + "\n IsPing: " + e.IsPing + "\n IsText: " + e.IsText + "\n Data: " + e.Data;
            rawMsg = e.Data;
            if (debug) Debug.Log("[ChatTab] Msg Receive: ''" + e.Data + "''");
            //MessageTypeSwitch();
            //ReceiveMsgFromServer(msgReceived);
        }
        private void OnChatError(object sender, ErrorEventArgs e)
        {
            if (debug) Debug.Log("[ChatTab] OnChatError: " + e.Message + "\n -> Exception: " + e.Exception);
            rawMsg = e.Message;
        }
        private void OnChatConnect(object sender, EventArgs e)
        {
            if (debug) Debug.Log("[ChatTab] OnChatConnect: ");
        }
        private void OnChatClose(object sender, CloseEventArgs e)
        {
            UnsusbcribeToServerSystemEvents();
            if (!e.WasClean)
            {
                StartCoroutine(TryReconnect());
            }
            if (debug) Debug.Log("[ChatTab] Chat Closed: \n -> Reason: " + e.Reason + "\n -> Code: " + e.Code + "\n -> Was Clean: " + e.WasClean);
            rawMsg = e.Reason;
        }
        private void MessageTypeSwitch()
        {
            Debug.Log("[WebSocketMsgsHandler] Mensage received type: " + TrimServerMsg(rawMsg, "type", 3, "\",\"", 0));
            switch (TrimServerMsg(rawMsg, "type", 3, "\",\"", 0))
            {
                case "chat":
                    Debug.Log("[WebSocketMsgsHandler] SE RECIBIO UN MENSAJE DEL TIPO: chat");
                    msg = "" + TrimServerMsg(rawMsg, "text", 3, "\",\"", 0);
                    senderId = "" + TrimServerMsg(rawMsg, "senderId", 3, "\"", 0);
                    OnChatMsgReceived?.Invoke(this, EventArgs.Empty);
                    break;
                case "challenge":
                    Debug.Log("[WebSocketMsgsHandler] SE RECIBIO UN MENSAJE DEL TIPO: challenge");
                    challengesMsg = rawMsg + "";
                    OnChallengeMsgReceived?.Invoke(this, EventArgs.Empty);
                    break;
                case "challenge-unlocked":
                    Debug.Log("[WebSocketMsgsHandler] SE RECIBIO UN MENSAJE DEL TIPO: challenge-unlocked");
                    challengesUnlockedMsg = rawMsg + "";
                    OnChallengeUnlockedMsgReceived?.Invoke(this, EventArgs.Empty);
                    break;
                case "badge-unlocked":
                    Debug.Log("[WebSocketMsgsHandler] SE RECIBIO UN MENSAJE DEL TIPO: badge-unlocked");
                    //StartCoroutine(ResourcesLoaderManager.Instance.UserAchievements.Load());
                    //badgeMsg = "" + TrimServerMsg(rawMsg, "message", 3, "\"", 0);
                    badgeMsg = rawMsg+"";
                    OnBadgeUnlockedMsgReceived?.Invoke(this, EventArgs.Empty);
                    break;
                case "item-unlocked":
                    Debug.Log("[WebSocketMsgsHandler] SE RECIBIO UN MENSAJE DEL TIPO: item-unlocked");
                    //StartCoroutine(ResourcesLoaderManager.Instance.Items.Load());       
                    itemsUnlocledMsg = rawMsg + "";
                    OnItemUnlockedMsgReceived?.Invoke(this, EventArgs.Empty);
                    break;
                case "product-unlocked":
                    Debug.Log("[WebSocketMsgsHandler] SE RECIBIO UN MENSAJE DEL TIPO: product-unlocked");

                    //borrar este pingo despues
                    //StartCoroutine(ResourcesLoaderManager.Instance.Items.Load());
                    //TabsManager.Instance.Shop.GetComponent<ShopTab>().InitializeProductsSlots(ResourcesLoaderManager.Instance.Products.Products);
                    //TabsManager.Instance.Shop.GetComponent<ShopTab>().InitializeClaimSlots(ResourcesLoaderManager.Instance.Products.UserProducts);
                    //TabsManager.Instance.Shop.GetComponent<ShopTab>().InitializeClaimSlots(ResourcesLoaderManager.Instance.Products.UserOrders);
                    //TabsManager.Instance.Shop.GetComponent<ShopTab>().UpdateToClaimList();
                    //equisde
                    productsUnlockedMsg = rawMsg + "";
                    OnProductUnlockedMsgReceived?.Invoke(this, EventArgs.Empty);
                    break;

            }
            //invocar notificacion
            //OnChatMessageReceived?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region COROUTINES
        IEnumerator TryReconnect()
        { 
            int tries=0;
            while (tries < reconectionTries)
            {
                WebSocketClient.Instance.Connect();
                reconectionTries++;
                yield return new WaitForSeconds(3f);
                if (WebSocketClient.Instance.IsConnected())
                {
                    Debug.Log("RECONNECTED TO WEB SOCKET SERVER");
                    yield return null;
                }
            }
            Debug.Log("CAN RECONNECT TO WEB SOCKET SERVER");
            yield return null;
        }
        #endregion
    }
}