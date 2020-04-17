using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesObjectCaller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject UI = GameObject.Find("Subtitles Canvas");
        SubtitlesUI subtitles = UI.GetComponent<SubtitlesUI>();
        subtitles.GameStart();
        Destroy(this.gameObject);
    }
}
