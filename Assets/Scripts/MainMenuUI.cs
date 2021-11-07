using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject credits;
    public GameObject tutorial;
    private bool loading;
    void Start()
    {
        loading = false;
    }

    public void OnStartButtonPress()
    {
        // LOAD OPENING SCENE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!?
        loading = true;
    }

    public void OnCreditsButtonPress()
    {
        if (loading)
        {
            return;
        }
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void OnTutorialButtonPress()
    {
        if (loading)
        {
            return;
        }
        mainMenu.SetActive(false);
        tutorial.SetActive(true);
    }
    
    public void OnBackButtonPress()
    {
        if (loading)
        {
            return;
        }
        mainMenu.SetActive(true);
        credits.SetActive(false);
        tutorial.SetActive(false);
    }
}
