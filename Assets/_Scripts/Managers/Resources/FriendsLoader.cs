using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    public class FriendsLoader : ResourceLoader
    {
        public List<FriendData> Friends;

        #region PUBLIC METHODS
        public FriendData FindFriendByID(string id)
        {
            foreach (var friend in Friends)
            {
                if (friend.Id == id)
                {
                    return friend;
                }
            }
            Debug.Log("NO SE ENCONTRO AMIGO CON DICHO ID");
            return null;
        }
        #endregion

        #region COROUTINES
        public override IEnumerator Load()
        {
            Friends.Clear();
            yield return StartCoroutine(_jsonLoader.LoadFromWeb<FriendsCollection>());

            yield return StartCoroutine(CreateConcreteData());
        }
        public override IEnumerator CreateConcreteData()
        {
            FriendsCollection response = _jsonLoader.ResponseInfo as FriendsCollection;

            foreach (var friend in response.data)
            {
                Friends.Add(FriendData.Create(friend._id, friend.name, friend.email));
            }
            yield return null;
        }
        #endregion
    }

    [System.Serializable]
    public class FriendData : IFriendData
    {
        #region FIELDS
        [SerializeField] private string _name;
        [SerializeField] private string _id;
        [SerializeField] private string _email;
        [SerializeField] private string _score;
        [SerializeField] private Sprite _icon;
        #endregion

        #region PROPERTIES
        public string Name { get { return _name; } set { _name = value; } }
        public string Id { get { return _id; } set { _id = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Score { get { return _score; } set { _score = value; } }
        public Sprite Icon { get { return _icon; } set { _icon = value; } }
        #endregion

        public static FriendData Create(string id, string name, string email)
        {
            FriendData friend = new FriendData();
            friend._id = id;
            friend.Name = name;
            friend.Email = email;
            return friend;
        }
    }

    public interface IFriendData
    {
        public string Name { get; }
        public string Id { get; }
        public string Score { get; }
        public Sprite Icon { get; }
    }
}