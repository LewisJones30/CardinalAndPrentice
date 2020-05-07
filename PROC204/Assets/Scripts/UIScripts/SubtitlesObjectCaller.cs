using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesObjectCaller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // PlayerPrefs.SetInt("DiaTriggerStartPlayed", 0);
        //PlayerPrefs.SetInt("DiaTriggerTargetPlayed", 0);
        //PlayerPrefs.SetInt("DiaTriggerBluePlayed", 0);
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
        if (PlayerPrefs.GetInt("DiaTriggerLvl2Played") == 1)
        {
            Destroy(GameObject.Find("DiaTriggerLvl2"));
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
        //Structure:
        //Check the gameobject that this is applied to (For example, if you have a dialogue trigger for the beginning of a level, make an object called "DiaTriggerLevelExampleObject" and then check
        //Then, trigger the following five lines of code:
        /*
        Destroy(this.gameObject);
        subtitles.StopAllCoroutines();
        audio.Stop();
        PlayerPrefs.SetInt("DiaTriggerStartPlayed", 1);
        subtitles.GameStart();
        */
        //The playerprefs are only required if the audio should not be played more than once, otherwise ignore it.
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
    else if (this.gameObject.name == "DiaTriggerBlue" && other.gameObject.transform.parent.name == "Cardinal")
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
    else if (this.gameObject.name == "DiaTriggerLvl2" && other.gameObject.transform.parent.name == "Cardinal" || other.gameObject.name == "Cardinal")
{
            if (PlayerPrefs.GetInt("DiaTriggerLvl2Played") == 1)
            {
                return; //Don't play if heard once.
            }
    PlayerPrefs.SetInt("DiaTriggerLvl2Played", 1);
    subtitles.LevelTwoComplete();
    Destroy(this.gameObject);
}

}
}
