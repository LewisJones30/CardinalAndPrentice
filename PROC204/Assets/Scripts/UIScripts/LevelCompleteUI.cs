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
    void Start()
    {

        fadecanvas = fadeCanvas.GetComponent<CanvasGroup>();
        GameObject canvas = GameObject.Find("Main Canvas");
        mainCanvas = canvas.GetComponent<Canvas>(); //Get the canvas from the Main Canvas (contains the game UI).
    }

    // Update is called once per frame
    void Update()
    {
        if (levelComplete == true)
        {
            if (Gamepad.all[0].buttonSouth.isPressed == true)
            {
                StartCoroutine("Fade"); //Starts coroutine for the fading animation
                PlayerPrefs.SetInt("Level1Completed", 1); //1 for level complete, 0 is for when they have not started level
                //SceneManager.LoadScene("Level 2");
            }
            else if (Gamepad.all[0].startButton.isPressed == true)
            {
                //Transport to the main menu, currently not available as this is not a scene.
                StartCoroutine("FadeMainMenu"); //Fade to the main menu
                SceneManager.LoadScene("Level 1"); //Temporary, change text when main menu is created
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cardinal")
        {
            LevelComplete();
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

    public void LevelComplete()
    {
        
        GameObject levelCompleteCanvas = GameObject.Find("LevelComplete Canvas");
        mainCanvas.enabled = false;
        if (levelCompleteCanvas != null)
        {
            levelCompleteCanvas.GetComponent<Canvas>().enabled = true;
            levelComplete = true;

        }
    }

}
