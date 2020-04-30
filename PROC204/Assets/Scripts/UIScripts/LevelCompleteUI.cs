using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelCompleteUI : MonoBehaviour
{
    // Start is called before the first frame update

    bool levelComplete = false;
    [SerializeField] Canvas fadeCanvas;
    CanvasGroup fadecanvas;
    Canvas mainCanvas;
    Canvas LevelCompleteCanvas;
    bool endUnlocked;
    bool level1 = false;
    void Start()
    {
        endUnlocked = false;
        fadecanvas = fadeCanvas.GetComponent<CanvasGroup>();
        GameObject canvas = GameObject.Find("Main Canvas");
        mainCanvas = canvas.GetComponent<Canvas>(); //Get the canvas from the Main Canvas (contains the game UI).
        GameObject LevelCompleteCanvasObj = GameObject.Find("LevelCompUI Canvas");
        LevelCompleteCanvas = LevelCompleteCanvasObj.GetComponent<Canvas>();
        LevelCompleteCanvas.enabled = false;
        PlayerPrefs.SetInt("Level2EndUnlocked", 0);
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            level1 = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (levelComplete == true)
        {
            LevelCompleteCanvas.enabled = true;
            if (Gamepad.all[0].buttonSouth.isPressed == true && level1 == true)
            {
                StartCoroutine("Fade"); //Starts coroutine for the fading animation
                PlayerPrefs.SetInt("Level1Completed", 1); //1 for level complete, 0 is for when they have not started level
                SceneManager.LoadScene("Level 2");
            }
            else if (Gamepad.all[0].startButton.isPressed == true)
            {
                //Transport to the main menu, currently not available as this is not a scene.
                StartCoroutine("FadeMainMenu"); //Fade to the main menu
                SceneManager.LoadScene("Main Menu"); //Temporary, change text when main menu is created
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cardinal" || other.gameObject.name == "Head")
        {
            if (this.gameObject.name == "LevelCompleteObject")
            {
                CheckCoins();
            }
            else if (this.gameObject.name == "Level2CompleteObject")
            {
                CheckCoinsL2();
            }
            else if (this.gameObject.name == "Level2TriggerObject")
            {
                Level2Complete();
            }
            else
            {
                LevelComplete();
            }
        }
    }
    public void LevelComplete()
    {
        {
            PlayerPrefs.SetInt("Level1Completed", 1);
            GameObject levelCompleteCanvas = GameObject.Find("LevelComplete Canvas");
            mainCanvas.enabled = false;
            if (levelCompleteCanvas != null)
            {
                levelCompleteCanvas.GetComponent<Canvas>().enabled = true;
                levelComplete = true;
                //Disable the controller inputs to "freeze" the player as such
                CardinalController cardinalController = (CardinalController)GameObject.FindObjectOfType(typeof(CardinalController));
                cardinalController.BlockInput = true;
                PrenticeController prenticeController = (PrenticeController)GameObject.FindObjectOfType(typeof(PrenticeController));
                prenticeController.BlockInput = true;
            }

        }
    }
    public void Level2Complete()
    {
        {
            PlayerPrefs.SetInt("Level2Completed", 1);
            GameObject levelCompleteCanvas = GameObject.Find("LevelComplete Canvas");
            mainCanvas.enabled = false;
            if (levelCompleteCanvas != null)
            {
                levelCompleteCanvas.GetComponent<Canvas>().enabled = true;
                levelComplete = true;
                //Disable the controller inputs to "freeze" the player as such
                CardinalController cardinalController = (CardinalController)GameObject.FindObjectOfType(typeof(CardinalController));
                cardinalController.BlockInput = true;
                PrenticeController prenticeController = (PrenticeController)GameObject.FindObjectOfType(typeof(PrenticeController));
                prenticeController.BlockInput = true;
            }

        }
    }
    void CheckCoins()
    {
        if (PlayerPrefs.GetInt("LevelEndUnlocked") == 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GameObject NotEnoughCoins = GameObject.Find("NotEnoughCoins");
            Text text = NotEnoughCoins.GetComponent<Text>();
            text.enabled = true;
        }
    }
    void CheckCoinsL2()
    {
        if (PlayerPrefs.GetInt("Level2EndUnlocked") == 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GameObject NotEnoughCoins = GameObject.Find("NotEnoughCoins");
            Text text = NotEnoughCoins.GetComponent<Text>();
            text.enabled = true;
        }
    }
    IEnumerator Fade()
        {

            while (fadecanvas.alpha < 1)
            {
                fadecanvas.alpha += Time.fixedDeltaTime / 50;
                yield return null;
            }
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Level 2");
        }
        IEnumerator FadeMainMenu()
        {

            while (fadecanvas.alpha < 1)
            {
                fadecanvas.alpha += Time.fixedDeltaTime / 12;
                yield return null;
            }
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Level 2");
        }

      
    }

