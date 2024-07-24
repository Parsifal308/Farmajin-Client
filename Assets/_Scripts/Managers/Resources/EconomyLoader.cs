using System;
using System.Collections;
using UnityEngine;

namespace Farmanji.Data
{
    public class EconomyLoader : ResourceLoader
    {
        [SerializeField] private int coins;
        [SerializeField] private int gems;
        public int Gems { get { return gems; } }
        public int Coins { get { return coins; } }

        public event Action OnEconomyLoaded;

        public override IEnumerator Load()
        {
            yield return StartCoroutine(_jsonLoader.LoadFromWeb<EconomyCollection>());

            yield return StartCoroutine(CreateConcreteData());

            Debug.Log("CURRENCIES LOADED");
            OnEconomyLoaded?.Invoke();
        }

        public override IEnumerator CreateConcreteData()
        {
            if (_jsonLoader.ResponseInfo is EconomyCollection response)
            {
                coins = response.data.coins;
                gems = response.data.gems;
            }
            yield return null;
        }

        public void PostWeeklyChellengeEconomy()
        {
            EconomyBody body = EconomyBody.CreateCurrenciesBody(0, 1000);
            StartCoroutine(GetComponent<EconomyPost>().Post(body));
        }
    }
}