using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitlesUI : MonoBehaviour
{
    //Ensure that these two are the same length
    public Sprite[] subtitleSprites;
    public int[] subtitleLengths;

    //Rest of variables
    Canvas SubtitlesCanvas;
    Image currentSubtitle;
    public int currentSubtitleNumber = -1;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowSubtitle(int durationOfSubtitles, Sprite SubtitleToDisplay)
    {

    }
    public void UpdateImage()
    {
        currentSubtitleNumber = currentSubtitleNumber + 1;
        currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
    }
    IEnumerator DisplaySubtitle(int SubtitleLength)
    {
        //First, enable the canvas
        SubtitlesCanvas.enabled = true;
        yield return new WaitForSeconds(SubtitleLength);
        //Secondly, remove the canvas after waiting a certain amount of time.
        SubtitlesCanvas.enabled = false;
        yield return false;
    }
}
