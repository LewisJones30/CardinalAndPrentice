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

    public GameObject title;

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
            buttonPressed = true;
            LogoClicked();
            logoPressed = true;
            return;

        }

        if (Gamepad.all[0].buttonSouth.isPressed == true && logoPressed == true && mainMenuChoice == false && buttonPressed == false)  //Front page, pressing A
        {
            buttonPressed = true;
            tutorial();
            LogoClicked();
            logoPressed = true;
            buttonPressed = true;
        }
        if (Gamepad.all[0].buttonWest.isPressed == true && logoPressed == true && mainMenuChoice == false && buttonPressed == false) //Front page, pressing X
        {
            buttonPressed = true;
            levelSelectClicked();
            LogoClicked();
            mainMenuChoice = true;
            levelSelectBool = true; //Boolean to activate level select page
            return;
        }
        if (Gamepad.all[0].leftShoulder.isPressed == true && logoPressed == true && levelSelectBool == true) //Level Select, pressing LB
        {
            buttonPressed = true;
            tutorial();
        }
        if (Gamepad.all[0].rightShoulder.isPressed == true && logoPressed == true && levelSelectBool == true) //Level Select, pressing RB
        {
            buttonPressed = true;
            level1();
        }
        if (Gamepad.all[0].buttonEast.isPressed == true && logoPressed == true && levelSelectBool == true) //Level Select, pressing B
        {
            levelSelectBool = false;
            mainMenuChoice = false;
            buttonPressed = true;
            backClicked();
            return;



        }
        if (Gamepad.all[0].startButton.isPressed == true && buttonPressed == false && mainMenuChoice == false && levelSelectBool == false) //Front page, pressing B 
        {
            buttonPressed = true;
            quitClicked(); //Closes the game entirely
        }
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            quitClicked();
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

        menuButtons.transform.position = new Vector3(Screen.width / 2, -405, 409);
        menuButtons.SetActive(true);
        buttonMove.SetTrigger("Logo Click");
        levelScreen.SetTrigger("Back Clicked");
        Invoke("deactivateLevelSelect", 2f); 

    }

    public void levelSelectClicked()
    {
        levelSelect.transform.position = new Vector3((Screen.width * 1.5f) + 1061.5f, -6.5f, 0);
        levelSelect.SetActive(true);
        levelScreen.SetTrigger("Level Screen Click");

        Invoke("deactivateMenu", 0.5f);

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

        logoMove.SetTrigger("Logo Disappear");

        buttonMove.SetTrigger("Logo Click");
        

        logo.interactable = false;



    }

    
}
