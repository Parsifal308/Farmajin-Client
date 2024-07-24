using System;
using System.Collections;
using System.Collections.Generic;
using Farmaji.Data;
using Farmanji.Auth;
using Farmanji.Data;
using Farmanji.Game;
using Farmanji.Managers;
using Farmanji.Ws;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Farmanji.UI
{
    public class NotificationPanel : MonoBehaviour
    {
        #region FIELDS
        [Header("PREFABS:")]
        [SerializeField] private MsgNotification msgPrefab;
        [SerializeField] private BadgeNotification achievementPrefab;
        [SerializeField] private ChallengeNotification friendChallengePrefab;
        [SerializeField] private ItemNotification itemNotiPrefab;
        [SerializeField] private ProductNotification productNotiPrefab;
        [Header("CONTAINERS:")]
        [SerializeField] private Transform content;
        [Header("BUTTONS:")]
        [SerializeField] private Button chatButton;
        [SerializeField] private Button challengesButton;

        public event Action<ItemData> OnItemBuyed;
        public event Action<ProductData> OnProductUnlocked; 
        #endregion

        #region PROPERTIES
        public Transform Content { get { return content; } }
        #endregion

        #region UNITY METHODS
        void Start()
        {
            SubcribeChatEvents();
        }
        void OnDestroy()
        {
            UnsubcribeChatEvents();
        }
        #endregion

        #region PRIVATE METHODS

        private void SubcribeChatEvents()
        {
            Debug.Log("[NotificationPanel] SUBSCRIBIENDO A EVENTOS");
            // TabsManager.Instance.Chat.GetComponent<ChatTab>().OnChatMessageReceivedEvent += MsgNotification;
            // TabsManager.Instance.Achievements.GetComponent<AchievementsTab>().OnBadgeUnlockedEvent += BadgeNotification;
            WebSocketMsgsHandler.Instance.OnChatMsgReceivedEvent += MsgNotification;
            WebSocketMsgsHandler.Instance.OnBadgeUnlockedMsgReceivedEvent += BadgeNotification;
            WebSocketMsgsHandler.Instance.OnChallengeMsgReceivedEvent += ChallengeNotification;
            WebSocketMsgsHandler.Instance.OnItemUnlockedMsgReceivedEvent += ItemNotification;
            WebSocketMsgsHandler.Instance.OnProductUnlockedMsgReceivedEvent += ProductNotification;
        }
        private void UnsubcribeChatEvents()
        {
            Debug.Log("[NotificationPanel] UNSUBSCRIBIENDO A EVENTOS");
            WebSocketMsgsHandler.Instance.OnChatMsgReceivedEvent -= MsgNotification;
            WebSocketMsgsHandler.Instance.OnBadgeUnlockedMsgReceivedEvent -= BadgeNotification;
            WebSocketMsgsHandler.Instance.OnChallengeMsgReceivedEvent -= ChallengeNotification;
            WebSocketMsgsHandler.Instance.OnItemUnlockedMsgReceivedEvent -= ItemNotification;
            WebSocketMsgsHandler.Instance.OnProductUnlockedMsgReceivedEvent -= ProductNotification;
        }
        private void AddMsgNotification(string msg, FriendData friend)
        {
            MsgNotification msgNoti = Instantiate(msgPrefab, content);
            msgNoti.FriendNameText.text = friend?.Name;
            msgNoti.MessageText.text = msg;
            UnityAction goToChatAction = () =>
            {
                TabsManager.Instance.Chat.GetComponent<ChatTab>().FriendName.text = friend?.Name;
                TabsManager.Instance.ChangeTab("ChatCanvas");
                TabsManager.Instance.HideCanvas(TabsManager.Instance.Navigation);
                TabsManager.Instance.HideCanvas(TabsManager.Instance.Information);
                TabsManager.Instance.Chat.GetComponent<ChatTab>().LoadChat(SessionManager.Instance.UserData.UserInfo.Id, friend?.Id);
                Destroy(msgNoti.gameObject);
            };
            msgNoti.GoToButton.onClick.AddListener(goToChatAction);
        }
        private void AddBadgeNotification(string msg, AchievementData achievement)
        {
            Debug.Log("creando notificacion de insignia");
            BadgeNotification newBadge = Instantiate(achievementPrefab, content);
            newBadge.BadgeNameText.text = achievement?.Name;
            newBadge.MessageText.text = achievement?.Description;
            newBadge.Icon.sprite = achievement?.Image;
            AchievementsTab achievements = TabsManager.Instance.Achievements.GetComponent<AchievementsTab>();
            Instantiate(achievements.AchievementSlotPrefab, achievements.AchievementsSlotContainer).GetComponent<AchievementSlot>().InitializeSlot(achievement);
            Instantiate(achievements.DetailSlotPrefab, achievements.DetailsSlotsContainer).GetComponent<AchievementDetailSlot>().InitializeDetail(achievement);
            UnityAction goToAchievementsAction = () =>
            {
                TabsManager.Instance.ChangeTab("AchievementsCanvas");
                Destroy(newBadge.gameObject);
            };
            newBadge.GoToButton.onClick.AddListener(goToAchievementsAction);
        }
        private void AddChallengeNotification(string msg, ChallengeData challenge)
        {
            ChallengeNotification newChallenge = Instantiate(friendChallengePrefab, content);
            newChallenge.ChallengeNameText.text = msg;
            newChallenge.MessageText.text = "Rewards: " + challenge?.CoinsReward + " coins & " + challenge?.GemsReward + " gems";
            UnityAction goToChallengesAction = () =>
            {
                TabsManager.Instance.ChangeTab("GamesCanvas");
                Destroy(newChallenge.gameObject);
            };
            newChallenge.GoToButton.onClick.AddListener(goToChallengesAction);
        }
        private void AddItemNotification(string msg, ItemData item)
        {
            Debug.Log("ITEM NOTIFICATION");
            ItemNotification newItem = Instantiate(itemNotiPrefab, content);
            newItem.ItemNameText.text = msg;
            newItem.MessageText.text = item?.Name;
            newItem.Icon.sprite = item?.MainSprite;
            UnityAction goToItemsAction = () =>
            {
                Debug.Log("New Item Created");
                OnItemBuyed?.Invoke(item);
                Destroy(newItem.gameObject);
            };
            newItem.GoToButton.onClick.AddListener(goToItemsAction);
        }
        private void AddProductNotification(string msg, ProductData product)
        {
            ProductNotification newProduct = Instantiate(productNotiPrefab, content);
            newProduct.ProductNameText.text = msg;
            newProduct.MessageText.text = product?.Name;
            newProduct.Icon.sprite = product?.images[0];
            ShopTab shop = TabsManager.Instance.Shop.GetComponent<ShopTab>();
            Instantiate(shop.UnlockeableSlotPrefab, shop.UnlockeablesSlotsContainer).GetComponent<StoreSlot>().InitializeSlot(product);
            UnityAction goToProductAction = () =>
            {
                OnProductUnlocked?.Invoke(product);
                TabsManager.Instance.ChangeTab("ShopCanvas");
                Destroy(newProduct.gameObject);
            };
            newProduct.GoToButton.onClick.AddListener(goToProductAction);
        }
        #endregion

        #region PUBLIC METHODS
        public void CleanNotifications()
        {
            for (int i = content.childCount - 1; i >= 0; i--)
            {
                Destroy(content.GetChild(i).gameObject);
            }
        }
        #endregion

        #region EVENTS CALLBACKS
        private void MsgNotification(object sender, EventArgs e)
        {
            Debug.Log("WebSocketMsgsHandler.Instance: " + WebSocketMsgsHandler.Instance);
            Debug.Log("WebSocketMsgsHandler.Instance.Msg: "+ WebSocketMsgsHandler.Instance.Msg);
            Debug.Log("(sender as ChatTab).SenderId): " + (sender as WebSocketMsgsHandler).SenderID);
            Debug.Log("ResourcesLoaderManager.Instance.Friends.FindFriendByID((sender as ChatTab).SenderId): " + ResourcesLoaderManager.Instance.Friends.FindFriendByID((sender as WebSocketMsgsHandler).SenderID));
            AddMsgNotification(WebSocketMsgsHandler.Instance.Msg, ResourcesLoaderManager.Instance.Friends.FindFriendByID((sender as WebSocketMsgsHandler).SenderID));
        }
        private void BadgeNotification(object sender, EventArgs e)
        {
            string msg = (sender as WebSocketMsgsHandler).BadgeMsg;
            AddBadgeNotification(WebSocketMsgsHandler.TrimServerMsg(msg, "message", 3, "\",\"", 0), ResourcesLoaderManager.Instance.Achievements.FindBadgeByID(WebSocketMsgsHandler.TrimServerMsg(msg, "badgeId", 3, "\"", 0)));
        }
        private void ChallengeNotification(object sender, EventArgs e)
        {
            string msg = (sender as WebSocketMsgsHandler).ChallengeMsg;
            Debug.Log("[Notification Panel] -> ChallengeNotification \nChallenge ID: "+ WebSocketMsgsHandler.TrimServerMsg(msg, "challengeId", 3, "\"", 0));
            AddChallengeNotification(WebSocketMsgsHandler.TrimServerMsg(msg, "message", 3, "\",\"", 0), ResourcesLoaderManager.Instance.Challenges.FindChallengesByID(WebSocketMsgsHandler.TrimServerMsg(msg, "challengeId", 3, "\"", 0)));
        }
        private void ItemNotification(object sender, EventArgs e)
        {
            string msg = (sender as WebSocketMsgsHandler).ItemsUnlocledMsg;
            Debug.Log("[Notification Panel] -> ItemNotification \nItem ID: " + WebSocketMsgsHandler.TrimServerMsg(msg, "itemId", 3, "\"", 0));
            AddItemNotification(WebSocketMsgsHandler.TrimServerMsg(msg, "message", 3, "\",\"", 0), ResourcesLoaderManager.Instance.Items.FindItemByID(WebSocketMsgsHandler.TrimServerMsg(msg, "itemId", 3, "\"", 0)));
        }
        private void ProductNotification(object sender, EventArgs e)
        {
            string msg = (sender as WebSocketMsgsHandler).ProductsUnlockedMsg;
            Debug.Log("[Notification Panel] -> ProductNotification \nProduct ID: " + WebSocketMsgsHandler.TrimServerMsg(msg, "productId", 3, "\"", 0));
            AddProductNotification(WebSocketMsgsHandler.TrimServerMsg(msg, "message", 3, "\",\"", 0), ResourcesLoaderManager.Instance.Products.FindProductByID(WebSocketMsgsHandler.TrimServerMsg(msg, "productId", 3, "\"", 0)));
        }
        #endregion

    }
}