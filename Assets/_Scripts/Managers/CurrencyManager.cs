using System;
using UnityEngine;

namespace Farmanji.Managers
{
    public class CurrencyManager : SingletonPersistent<CurrencyManager>
    {
        [SerializeField] private int Coins;
        [SerializeField] private int Gems;
        //[SerializeField] private CurrenciesPost _currenciesPost;
        [SerializeField] private string coinsKey = "coins";
        [SerializeField] private string gemsKey = "gems";

        public event Action<int> OnCoinsChanged;
        public event Action<int> OnGemsChanged;

        //public event Action<int> OnItemBuyed;
        //public event Action<int> OnActivityCompleted;

        public int GetCoins { get { return Coins; } }
        public int GetGems { get { return Gems; } }

        private void Start()
        {
            ResourcesLoaderManager.Instance.Economy.OnEconomyLoaded += LoadCurrencies;
        }

        private void LoadCurrencies()
        {
            Coins = ResourcesLoaderManager.Instance.Economy.Coins;
            Gems = ResourcesLoaderManager.Instance.Economy.Gems;

            OnCoinsChanged?.Invoke(Coins);
            OnGemsChanged?.Invoke(Gems);
        }

        private void PostCurrenciesChange(int currencie)
        {
            //var currenciesBody = CurrenciesBody.CreateCurrenciesBody(Coins, Gems);
            //_currenciesPost.Post(currenciesBody);
        }

        public void AddCoins(int coins)
        {
            Coins += coins;
            OnCoinsChanged?.Invoke(Coins);
        }

        public void SpendCoins(int coins)
        {
            Coins = Mathf.Clamp(Coins - coins, 0, 10000000);
            OnCoinsChanged?.Invoke(Coins);
        }

        public void AddGems(int gems)
        {
            Gems += gems;
            Debug.Log("Adding gems");
            OnGemsChanged?.Invoke(Gems);
        }

        public void SpendGems(int gems)
        {
            Gems = Mathf.Clamp(Gems - gems, 0, 10000000);
            OnGemsChanged?.Invoke(Gems);
        }

        public bool CanBuyWithCoins(int price)
        {
            return ResourcesLoaderManager.Instance.Economy.Coins >= price;
        }

        public bool CanBuyWithGems(int price)
        {
            return ResourcesLoaderManager.Instance.Economy.Gems >= price;
        }
    }
}