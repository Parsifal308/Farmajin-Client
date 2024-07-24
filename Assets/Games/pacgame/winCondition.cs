using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winCondition : MonoBehaviour
{
    public Text scoreText, pauseText;
    public GameObject pausemenu;
    public int score = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pvacune"))
        {
            score++;
            scoreText.text = score.ToString();
            pauseText.text = "Correcto";
            pausemenu.SetActive(true);
        }
        else if (collision.CompareTag("pvirus"))
        {
            pauseText.text = "Incorrecto";
            pausemenu.SetActive(true);
        }
    }
}
