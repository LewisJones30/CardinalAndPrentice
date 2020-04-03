using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitlesUI : MonoBehaviour
{
    //Ensure that these two are the same length
    public Sprite[] subtitleSprites;
    public float[] subtitleLengths;
    public AudioClip[] subtitleClips;
    //Rest of variables
    Canvas SubtitlesCanvas;
    Image currentSubtitle;
    public int currentSubtitleNumber = -1;
    float subtitleDuration = 0f;
    public AudioSource audio;



    void Start()
    {
        SubtitlesCanvas = this.gameObject.GetComponent<Canvas>();
        currentSubtitle = SubtitlesCanvas.GetComponentInChildren<Image>();
        UpdateImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowSubtitle(float durationOfSubtitle)
    {
    }
    public void UpdateImage()
    {
        currentSubtitleNumber = currentSubtitleNumber + 1;
        currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
        subtitleDuration = subtitleLengths[currentSubtitleNumber];
        audio.PlayOneShot(subtitleClips[currentSubtitleNumber], 1f); //Plays audio once.
        StartCoroutine("DisplaySubtitle");
    }
    IEnumerator DisplaySubtitle()
    {
        //First, enable the canvas
        SubtitlesCanvas.enabled = true;
        yield return new WaitForSeconds(subtitleDuration);
        //Secondly, remove the canvas after waiting a certain amount of time.
        SubtitlesCanvas.enabled = false;
        yield return false;
    }
}
