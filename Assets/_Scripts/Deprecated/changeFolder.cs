using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class changeFolder : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject fPanel;

    public void mainLoad()
    {
        SceneManager.LoadScene("main");
    }

    public void loadLoginPanel()
    {
        loginPanel.SetActive(true);
        fPanel.SetActive(false);
    }
}
