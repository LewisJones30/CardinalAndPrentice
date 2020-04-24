using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesObjectCaller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("DiaTriggerStartPlayed", 0);
        PlayerPrefs.SetInt("DiaTriggerTargetPlayed", 0);
        PlayerPrefs.SetInt("DiaTriggerBluePlayed", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.gameObject.name);
        GameObject UI = GameObject.Find("Subtitles Canvas");
        SubtitlesUI subtitles = UI.GetComponent<SubtitlesUI>();
        AudioSource audio = UI.GetComponent<AudioSource>();
        if (this.gameObject.name == "DiaTriggerStart")
        {
            if (PlayerPrefs.GetInt("DiaTriggerStartPlayed") == 1)
            {
                Debug.Log("Tutorial Start already triggered before!");
                Destroy(this.gameObject);
                Destroy(GameObject.Find("DiaTriggerTargetBlock"));
            }
            else
            {
                Destroy(this.gameObject);
                subtitles.StopAllCoroutines();
                audio.Stop();
                PlayerPrefs.SetInt("DiaTriggerStartPlayed", 1);
                subtitles.GameStart();

            }

        }
        else if (this.gameObject.name == "DiaTriggerTarget")
        {
            if (PlayerPrefs.GetInt("DiaTriggerTargetPlayed") == 1)
            {
                Debug.Log("Tutorial Target already triggered before!");
                Destroy(this.gameObject);
                Destroy(GameObject.Find("DiaTriggerBlueBlock"));
            }
            else
            {
                subtitles.StopAllCoroutines();
                audio.Stop();
                PlayerPrefs.SetInt("DiaTriggerTargetPlayed", 1);
                subtitles.TargetTutorial();
                Destroy(this.gameObject);
            }

        }
        else if (this.gameObject.name == "DiaTriggerBlue")
        {
            if (PlayerPrefs.GetInt("DiaTriggerBluePlayed") == 1)
            {
                Debug.Log("Tutorial Blue already triggered before!");
                Destroy(this.gameObject);
                Destroy(GameObject.Find("DiaTriggerEndBlock"));
            }
            else
            {
                subtitles.StopAllCoroutines();
                audio.Stop();
                PlayerPrefs.SetInt("DiaTriggerBluePlayed", 1);
                subtitles.BlueEnemies();
                Destroy(this.gameObject);
            }

        }
        else if (this.gameObject.name == "DiaTriggerEndL1" && other.gameObject.transform.parent.name == "Cardinal" )
        {
            subtitles.LevelOneComplete();
            Destroy(this.gameObject);
        }

    }
}
