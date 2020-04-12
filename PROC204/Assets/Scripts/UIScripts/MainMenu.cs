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



    void Start()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }


    public void tutorial()
    {

        SceneManager.LoadScene(1);

    }


    public void level1()
    {

        SceneManager.LoadScene(2);

    }


    public void level2()
    {

        SceneManager.LoadScene(3);

    }
    
    public void level3()
    {

        SceneManager.LoadScene(4);

    }
    
    public void level4()
    {

        SceneManager.LoadScene(5);

    }

    
    public void playClicked()
    {

        SceneManager.LoadScene(1);

    }

    
    public void backClicked()
    {

        levelScreen.SetTrigger("Back Clicked");

    }

    public void levelSelectClicked()
    {

        levelScreen.SetTrigger("Level Screen Click");


    }

    
    public void quitClicked()
    {

        Application.Quit();

    }


    public void LogoClicked()
    {

        
        logoMove.SetTrigger("Logo Click");

        buttonMove.SetTrigger("Logo Click");
        

        logo.interactable = false;


    }

    
}
