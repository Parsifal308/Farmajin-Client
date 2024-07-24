using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Farmanji.Ws;
using WebSocketSharp;
using System;
using UnityEngine.Events;
using Farmanji.Data;
using Farmanji.Managers;
using Farmanji.Auth;
using Newtonsoft.Json;

namespace Farmanji.Game
{
    public class ChatTab : BaseTab
    {
        #region FIELDS
        public string msgReceived;
        public string textMsg;

        private MsgData dataReceived;
        private CreateConversation conversationCreator;

        [Header("CONTAINERS")]
        [SerializeField] RectTransform msgsPanel;
        [SerializeField] private TextMeshProUGUI friendName;
        [Header("PREFABS:")]
        [SerializeField] private GameObject incMsg;
        [SerializeField] private GameObject sndMsg;
        [Header("INPUTS")]
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button sndButton;

        [SerializeField] private string conversationId;
        [SerializeField] private string recipientId;
        [SerializeField] private string senderId;

        [SerializeField] private event EventHandler OnChatMessageReceived, OnChatMessageSended, OnChallengeReceived, OnChallengeSended;
        #endregion

        #region PROPERTIES
        public string MsgReceived { get { return msgReceived; } }
        public string SenderId { get { return senderId; } }
        public EventHandler OnChallengeReceivedEvent { get { return OnChallengeReceived; } set { OnChallengeReceived = value; } }
        public EventHandler OnChallengeSendedEvent { get { return OnChallengeSended; } set { OnChallengeSended = value; } }
        public EventHandler OnChatMessageReceivedEvent { get { return OnChatMessageReceived; } set { OnChatMessageReceived = value; } }
        public EventHandler OnChatMessageSendedEvent { get { return OnChatMessageSended; } set { OnChatMessageSended = value; } }
        public TextMeshProUGUI FriendName { get { return friendName; } }
        #endregion

        #region UNITY METHODS
        void Start()
        {
            conversationCreator = GetComponent<CreateConversation>();
            UnityAction sendAction = () => { SendMsgToServer(inputField.text); inputField.text = ""; };
            UnityAction<string> checkInputFieldAction = (string value) => { CheckInputFields(value); };

            inputField.onValueChanged.AddListener(checkInputFieldAction);
            sndButton.onClick.AddListener(sendAction);

            WebSocketMsgsHandler.Instance.OnChatMsgReceivedEvent += SetMsg;
        }

        private void SetMsg(object sender, EventArgs e)
        {
            msgReceived = (sender as WebSocketMsgsHandler).Msg;
            senderId = (sender as WebSocketMsgsHandler).SenderID;
        }

        void Update()
        {
            //ESTA GILADA TAN ASQUEROSA SOLUCIONA EL PROBLEMITA DE LLAMAR AL METODO DESDE UN THREAD QUE NO ES EL MAIN
            if (msgReceived != "")
            {
                ReceiveChatMsg(msgReceived);
                msgReceived = "";
            }
        }
        #endregion

        #region METHODS


        #region OLDS SHITS
        public void OpenChat(List<MsgData> history)
        {
            ClearChat();
            foreach (var msg in history)
            {
                if (msg.from == "ME")//BUSCAR SI HAY REFERENCIA AL USUARIO LOCAL PARA ESTA COMPARACION
                {
                    SendMsgToServer(msg);
                }
                else
                {
                    ReceiveMsgFromServer(msg);
                }
            }
        }
        public void CloseChat()
        {
            WebSocketClient.Instance.Close();
        }
        public void ClearChat()
        {
            for (int i = 0; i < msgsPanel.childCount; i++)
            {
                GameObject.Destroy(msgsPanel.GetChild(i).gameObject);
            }
        }
        public void SendMsgToServer(MsgData data)
        {
            if (WebSocketClient.Instance.IsConnected())
            {
                WebSocketClient.Instance.SendMsg(data.msg);
                MsgSlot sended = GameObject.Instantiate(sndMsg, msgsPanel.transform).GetComponent<MsgSlot>();
                sended.LoadData(data);
            }
            else
            {
                Debug.Log("No existe conexion al servidor web socket");
            }
        }
        public void ReceiveMsgFromServer(MsgData data)
        {
            dataReceived = data;
            MsgSlot sended = GameObject.Instantiate(incMsg, msgsPanel.transform).GetComponent<MsgSlot>();
            sended.LoadData(dataReceived);
        }
        #endregion


        private void ClearChatPanel()
        {
            for (int i = 0; i < msgsPanel.childCount; i++)
            {
                Destroy(msgsPanel.GetChild(i).gameObject);
            }
        }
        public void LoadChat(string userId, string friendId)
        {
            StartCoroutine(InitializeChatPanel(userId, friendId));
        }
        private void CheckInputFields(string value)
        {
            if (value != "") { sndButton.interactable = true; }
            else { sndButton.interactable = false; }
        }

        public void SendMsgToServer(MessageData data)
        {
            if (WebSocketClient.Instance.IsConnected())
            {
                WebSocketClient.Instance.SendMsg(data.Text);
                MsgSlot sended = GameObject.Instantiate(sndMsg, msgsPanel.transform).GetComponent<MsgSlot>();
                sended.LoadData(data);
            }
            else
            {
                Debug.Log("No existe conexion al servidor web socket");

            }
        }
        public void SendMsgToServer(string msg)
        {
            if (WebSocketClient.Instance.IsConnected())
            {
                WebSocketMessage message = WebSocketMessage.Create(msg, SessionManager.Instance.UserData.UserInfo.Id, recipientId, conversationId);
                Debug.Log("Json web socket message:\n{\"message\":" + JsonConvert.SerializeObject(message) + "}");
                WebSocketClient.Instance.SendMsg("{\"message\":" + JsonConvert.SerializeObject(message) + "}");
                MsgSlot sended = GameObject.Instantiate(sndMsg, msgsPanel.transform).GetComponent<MsgSlot>();
                sended.LoadData(msg);
                OnChatMessageSended?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Debug.Log("No existe conexion al servidor web socket");
            }
        }
        public void ReceiveChatMsg(string msg)
        {
            MsgSlot sended = GameObject.Instantiate(incMsg, msgsPanel.transform).GetComponent<MsgSlot>();
            sended.LoadData(msgReceived);
            TabsManager.Instance.Friends.GetComponent<FriendsTab>().Contacts.MoveUpSlot(senderId);
            OnChatMessageReceived?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region COROUTINES
        public IEnumerator InitializeChatPanel(string userId, string friendId)
        {
            conversationId = ResourcesLoaderManager.Instance.Conversations.GetConversationId(userId, friendId);
            recipientId = friendId;
            ClearChatPanel();
            if (conversationId != "")
            {
                yield return StartCoroutine(ResourcesLoaderManager.Instance.Messages.Load(conversationId));
                ClearChatPanel();
                for (int i = ResourcesLoaderManager.Instance.Messages.Messages.Count - 1; i >= 0; i--)
                {
                    if (ResourcesLoaderManager.Instance.Messages.Messages[i].SenderId == SessionManager.Instance.UserData.UserInfo.Id)
                    {
                        MsgSlot msgPrefab = GameObject.Instantiate(sndMsg, msgsPanel.transform).GetComponent<MsgSlot>();
                        msgPrefab.Msg.text = ResourcesLoaderManager.Instance.Messages.Messages[i].Text;
                    }
                    else
                    {
                        MsgSlot msgPrefab = GameObject.Instantiate(incMsg, msgsPanel.transform).GetComponent<MsgSlot>();
                        msgPrefab.Msg.text = ResourcesLoaderManager.Instance.Messages.Messages[i].Text;
                    }
                }
            }
            else
            {
                CreateConversationBody body = CreateConversationBody.Create(userId, friendId);
                StartCoroutine(conversationCreator.Post(body));
                yield return new WaitForSeconds(2f);
                conversationId = conversationCreator.response._id;
            }
        }
        #endregion
    }
    public class MsgData
    {
        public string from;
        public string to;
        public string msg;
        public string time;
        public string date;
    }
}