using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{


    public Animator logoMove;

    public Animator buttonMove;

    public Animator levelScreen;

    public Button logo;

    public GameObject menuButtons;

    public GameObject levelSelect;

    bool logoPressed = false;

    bool mainMenuChoice = false;

    bool levelSelectBool = false;

    bool buttonPressed = false;


    void Start()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        menuButtons.SetActive(false);
        levelSelect.SetActive(false);
    }
    void Update()
    {
        if (!Gamepad.all[0].buttonSouth.isPressed == true && !Gamepad.all[0].buttonNorth.isPressed == true && !Gamepad.all[0].buttonWest.isPressed == true && !Gamepad.all[0].buttonNorth.isPressed == true)
        {
            buttonPressed = false;
        }
        //Each state of the game
        if (Gamepad.all[0].buttonSouth.isPressed == true && logoPressed == false && buttonPressed == false) //Front page
        {
            LogoClicked();
            logoPressed = true;
            buttonPressed = true;
            
        }
        if (Gamepad.all[0].buttonEast.isPressed == true && buttonPressed == false && mainMenuChoice == false && levelSelectBool == false) //Front page, pressing B 
        {
            quitClicked(); //Closes the game entirely
        }
        if (Gamepad.all[0].buttonSouth.isPressed == true && logoPressed == true && mainMenuChoice == false && buttonPressed == false)  //Front page, pressing A
        {
            tutorial();
            LogoClicked();
            logoPressed = true;
            buttonPressed = true;
        }
        else if (Gamepad.all[0].buttonWest.isPressed == true && logoPressed == true && mainMenuChoice == false && buttonPressed == false) //Front page, pressing X
        {
            levelSelectClicked();
            LogoClicked();
            mainMenuChoice = true;
            levelSelectBool = true; //Boolean to activate level select page
        }
        else if (Gamepad.all[0].leftShoulder.isPressed == true && logoPressed == true && levelSelectBool == true) //Level Select, pressing LB
        {
            tutorial();
        }
        else if (Gamepad.all[0].rightShoulder.isPressed == true && logoPressed == true && levelSelectBool == true) //Level Select, pressing RB
        {
            level1();
        }
        else if (Gamepad.all[0].buttonEast.isPressed == true && logoPressed == true && levelSelectBool == true) //Level Select, pressing B
        {
            levelSelectBool = false;
            mainMenuChoice = false;
            backClicked();
            buttonPressed = true;

        }
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
        Invoke("deactivateLevelSelect", 5f); 

    }

    public void levelSelectClicked()
    {

        levelSelect.SetActive(true);
        levelScreen.SetTrigger("Level Screen Click");

        Invoke("deactivateMenu", 5f);

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
