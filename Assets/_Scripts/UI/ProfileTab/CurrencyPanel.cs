using Farmanji.Managers;
using TMPro;
using UnityEngine;

namespace Farmanji.Game
{
    public class CurrencyPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinsAmountText;
        [SerializeField] private TextMeshProUGUI _gemsAmountText;

        private void Start()
        {
            CurrencyManager.Instance.OnCoinsChanged += UpdateCoinsText;
            CurrencyManager.Instance.OnGemsChanged += UpdateGemsText;
            
            UpdateCoinsText(CurrencyManager.Instance.GetCoins);
            UpdateGemsText(CurrencyManager.Instance.GetGems);
        }

        private void UpdateCoinsText(int amount)
        {
            _coinsAmountText.text = amount.ToString();
        }

        private void UpdateGemsText(int value)
        {
            _gemsAmountText.text = value.ToString();
        }
    }
}