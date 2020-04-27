using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesObjectCaller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("DiaTriggerStartPlayed") == 1)
        {
            Debug.Log("Tutorial Start already triggered before!");
            Destroy(GameObject.Find("DiaTriggerStart"));
            Destroy(GameObject.Find("DiaTriggerTargetBlock"));
        }
        if (PlayerPrefs.GetInt("DiaTriggerTargetPlayed") == 1)
        {
            Destroy(GameObject.Find("DiaTriggerTarget"));
            Destroy(GameObject.Find("DiaTriggerTargetBlock"));
        }
        if (PlayerPrefs.GetInt("DiaTriggerBluePlayed") == 1)
        {
            Destroy(GameObject.Find("DiaTriggerBlue"));
            Destroy(GameObject.Find("DiaTriggerBlueBlock"));
        }
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
            
                subtitles.StopAllCoroutines();
                audio.Stop();
                PlayerPrefs.SetInt("DiaTriggerTargetPlayed", 1);
                subtitles.TargetTutorial();
                Destroy(this.gameObject);
            

        }
        else if (this.gameObject.name == "DiaTriggerBlue")
        {
        subtitles.StopAllCoroutines();
        audio.Stop();
        PlayerPrefs.SetInt("DiaTriggerBluePlayed", 1);
        subtitles.BlueEnemies();
        Destroy(this.gameObject);

        }
        else if (this.gameObject.name == "DiaTriggerEndL1" && other.gameObject.transform.parent.name == "Cardinal" )
        {
            subtitles.LevelOneComplete();
            Destroy(this.gameObject);
        }

    }
}
