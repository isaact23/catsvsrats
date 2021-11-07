using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject credits;
    public GameObject tutorial;

    public void OnStartButtonPress()
    {
        // LOAD OPENING SCENE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!?
    }

    public void OnCreditsButtonPress()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void OnTutorialButtonPress()
    {
        mainMenu.SetActive(false);
        tutorial.SetActive(true);
    }
    
    public void OnBackButtonPress()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
        tutorial.SetActive(false);
    }
}
