using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public bool IsEnabled { get; set; } = true;
    abstract protected void FindPlayer();

    protected GameObject character;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
        FindPlayer();
    }

    protected virtual void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    protected virtual void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        FindPlayer();
    }

    protected bool CanControl()
    {
        if (IsEnabled && character != null) return true;
        return false;
    }
}
