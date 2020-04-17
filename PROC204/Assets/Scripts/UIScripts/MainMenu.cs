using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public Animator logoMove;

    public Animator buttonMove;

    public Animator levelScreen;

    public Button logo;

    public GameObject menuButtons;

    public GameObject levelSelect;


    void Start()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        menuButtons.SetActive(false);
        levelSelect.SetActive(false);
    }


    public void tutorial()
    {

        SceneManager.LoadScene(1);

    }


    public void level1()
    {

        SceneManager.LoadScene(2);

    }

    
    public void playClicked()
    {

        SceneManager.LoadScene(1);

    }

    
    public void backClicked()
    {


        menuButtons.SetActive(true);
        levelScreen.SetTrigger("Back Clicked");

        Invoke("deactivateLevelSelect", 1.1f); 

    }

    public void levelSelectClicked()
    {

        levelSelect.SetActive(true);
        levelScreen.SetTrigger("Level Screen Click");

        Invoke("deactivateMenu", 1.3f);

    }


    public void deactivateMenu()
    {
        menuButtons.SetActive(false);
    }

    public void deactivateLevelSelect()
    {
        levelSelect.SetActive(false);
    }




        public void quitClicked()
    {

        Application.Quit();

    }


    public void LogoClicked()
    {
        menuButtons.SetActive(true);

        logoMove.SetTrigger("Logo Click");

        buttonMove.SetTrigger("Logo Click");
        

        logo.interactable = false;


    }

    
}
