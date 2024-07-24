using Farmanji.Data;
using Farmanji.Managers;
using UnityEngine;
using UnityEngine.Events;


namespace Farmanji.Game
{
    public class ContactsPanel : MonoBehaviour
    {
        [SerializeField] private FriendSlot _friendSlotPrefab;
        [SerializeField] private Transform _contactsPanelContent;

        private FriendsLoader _friendsLoader;

        private void Start()
        {
            _friendsLoader = ResourcesLoaderManager.Instance.Friends;
            foreach (var friend in _friendsLoader.Friends)
            {
                CreateFriendSlot(friend);
            }
        }

        private void CreateFriendSlot(FriendData data)
        {
            FriendSlot friendSlot = Instantiate(_friendSlotPrefab, _contactsPanelContent);
            friendSlot.InitializeSlot(data);

            string friendName = data.Name;
            UnityAction updateFriendName = () => { TabsManager.Instance.Chat.GetComponent<ChatTab>().FriendName.text = friendName; };

            friendSlot.MensagueButton.onClick.AddListener(updateFriendName);
        }

        public void MoveUpSlot(FriendSlot slot)
        {
            _contactsPanelContent.GetChild(FindSlotIndexByUserID(slot.Data.Id)).transform.SetSiblingIndex(0);
        }
        public void MoveUpSlot(string id)
        {
            _contactsPanelContent.GetChild(FindSlotIndexByUserID(id)).transform.SetSiblingIndex(0);
        }
        public int FindSlotIndexByUserID(string id)
        {
            for (int i = 0; i < _contactsPanelContent.childCount; i++)
            {
                if (_contactsPanelContent.GetChild(i).GetComponent<FriendSlot>().Data.Id == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}