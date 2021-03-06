using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public Transition transition;
    public GameObject mainMenu;
    public GameObject credits;
    public GameObject tutorial;
    private bool loading;
    
    private void Start()
    {
        loading = false;
        Time.timeScale = 1f;
    }

    public void OnStartButtonPress()
    {
        loading = true;
        transition.loading = true;
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
