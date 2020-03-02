using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void NextScene()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++currentBuildIndex);
    }
}
