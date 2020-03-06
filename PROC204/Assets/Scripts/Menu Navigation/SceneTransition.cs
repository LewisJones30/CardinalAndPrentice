using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Level1()
    {

    }
    public void Level2()
    {

    }
    public void GameOver()
    {
    }
    public void EndScreen()
    {
        SceneManager.LoadScene("GameOver"); //Load the GameOver scene.
    }
    public void MoveFromEndScreen()
    {
        if (PlayerPrefs.GetInt("Level1Status") == 1)
        {
            SceneManager.LoadScene("Level1");
        }
        else if (PlayerPrefs.GetInt("Level2Status") == 1)
        {
            SceneManager.LoadScene("Level2");
        }

    }
    public void MainMenu()
    {
        SceneManager.LoadScene("StartupProject");
    }
}
