using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Farmanji.Data;

namespace Farmanji.Game
{
    public class MsgSlot : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private ConversationData conversationData;
        [SerializeField] private TextMeshProUGUI msg;
        [SerializeField] private TextMeshProUGUI time;
        [SerializeField] private TextMeshProUGUI date;
        #endregion

        #region UNITY METHODS
        #endregion

        #region PUBLIC METHODS
        public ConversationData ConversationData { get { return conversationData; } set { conversationData = value; } }
        public TextMeshProUGUI Msg { get { return msg; } set { msg = value; } }
        #endregion

        #region PRIVATE METHODS
        public void LoadData(MsgData data)
        {
            msg.text = data.msg;
            time.text = data.time;
            date.text = data.date;
        }
        public void LoadData(MessageData data)
        {
            msg.text = data.Text;
        }
        public void LoadData(string data)
        {
            msg.text = data;
        }
        #endregion
    }
}