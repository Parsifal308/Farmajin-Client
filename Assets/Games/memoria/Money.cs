using System.Collections;
using System.Collections.Generic;
using Farmanji.Data;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Farmanji.Managers;

public class Money : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI moneyText2;
    public GameController gameController;

    //private int startMoney = 0;
    //public int rewardMoney = 100;
    // Start is called before the first frame update
    public TextMeshProUGUI recomText;
    public void Penalize()
    {
        moneyText.text = "+" + gameController.reward.ToString();
        //moneyText2.text = "+" + gameController.reward.ToString();
        recomText.text = "Sigue intentando para más monedas!";
    }

    // Update is called once per frame
    public void Reward()
    {
        // EconomyBody body = EconomyBody.CreateCurrenciesBody(gameController.reward, 0);
        // StartCoroutine(ResourcesLoaderManager.Instance.Economy.GetComponent<EconomyPost>().Post(body));
        CurrencyManager.Instance.AddCoins(gameController.reward);
        moneyText.text = "+" + gameController.reward.ToString() ;
        recomText.text = "Has completado tu reto, ten tu recompensa!";
    }

}
