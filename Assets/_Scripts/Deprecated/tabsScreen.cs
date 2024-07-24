using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tabsScreen : MonoBehaviour
{
    public GameObject fpanel;
    public GameObject fprofile;
    public GameObject flogros;
    public GameObject fshop;
    public GameObject fjuegos;
    public GameObject fmundos;
    public GameObject upPanel;
    public GameObject fetapas;
    public void fpanelOn()
    {
        fpanel.SetActive(true);
        fprofile.SetActive(false);
        flogros.SetActive(false);
        fshop.SetActive(false);
        fjuegos.SetActive(false);
        fmundos.SetActive(false);
        upPanel.SetActive(false);
        fetapas.SetActive(false);
    }

    public void fprofileOn()
    {
        fpanel.SetActive(false);
        fprofile.SetActive(true);
        flogros.SetActive(false);
        fshop.SetActive(false);
        fjuegos.SetActive(false);
        fmundos.SetActive(false);
        upPanel.SetActive(true);
        fetapas.SetActive(false);
    }
    public void flogrosOn()
    {
        fpanel.SetActive(false);
        fprofile.SetActive(false);
        flogros.SetActive(true);
        fshop.SetActive(false);
        fjuegos.SetActive(false);
        fmundos.SetActive(false);
        upPanel.SetActive(true);
        fetapas.SetActive(false);
    }
    public void fshopOn()
    {
        fpanel.SetActive(false);
        fprofile.SetActive(false);
        flogros.SetActive(false);
        fshop.SetActive(true);
        fjuegos.SetActive(false);
        fmundos.SetActive(false);
        upPanel.SetActive(true);
        fetapas.SetActive(false);
    }
    public void fjuegosOn()
    {
        fpanel.SetActive(false);
        fprofile.SetActive(false);
        flogros.SetActive(false);
        fshop.SetActive(false);
        fjuegos.SetActive(true);
        fmundos.SetActive(false);
        upPanel.SetActive(true);
        fetapas.SetActive(false);
    }
    public void fmundosOn()
    {
        fpanel.SetActive(false);
        fprofile.SetActive(false);
        flogros.SetActive(false);
        fshop.SetActive(false);
        fjuegos.SetActive(false);
        fmundos.SetActive(true);
        upPanel.SetActive(false);
        fetapas.SetActive(false);
    }

    public void fetapasOn()
    {
        fpanel.SetActive(false);
        fprofile.SetActive(false);
        flogros.SetActive(false);
        fshop.SetActive(false);
        fjuegos.SetActive(false);
        fmundos.SetActive(false);
        upPanel.SetActive(true);
        fetapas.SetActive(true);
    }
}
