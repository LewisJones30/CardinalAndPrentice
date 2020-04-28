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
    public int currentAudioClipNumber = -1;
    public AudioSource audio;
    void Start()
    {
        SubtitlesCanvas = this.gameObject.GetComponent<Canvas>();
        currentSubtitle = SubtitlesCanvas.GetComponentInChildren<Image>();
        UpdateImage();
        SubtitlesCanvas.enabled = false;
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
        

        //currentSubtitleNumber = currentSubtitleNumber + 1;
        //currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        //currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
        //subtitleDuration = subtitleLengths[currentSubtitleNumber];
        //audio.PlayOneShot(subtitleClips[currentSubtitleNumber], 1f); //Plays audio once.
        //StartCoroutine("DisplaySubtitle");
    }
    public void GameStart()
    {
        SubtitlesCanvas.enabled = true;
        StartCoroutine("GameStartSubtitle");

    }
    public void TargetTutorial()
    {
        SubtitlesCanvas.enabled = true;
        StartCoroutine("TargetTutorialSubtitles");
    }
    public void EnemyTutorial()
    {
        SubtitlesCanvas.enabled = true;
        StartCoroutine("EnemyTutorialSubtitles");
    }
    public void BlueEnemies()
    {
        SubtitlesCanvas.enabled = true;
        StartCoroutine("BlueEnemiesSubtitles");
    }
    public void LevelOneComplete()
    {
        SubtitlesCanvas.enabled = true;
        StartCoroutine("EndOfLevelOneSubtitles");
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
    IEnumerator GameStartSubtitle()
    {

        currentSubtitleNumber = currentSubtitleNumber + 1;
        SubtitlesCanvas.enabled = true;

        //Element 0, first line of the starting game - "Cardinal and his wizard apprentice"
        audio.PlayOneShot(subtitleClips[0]);
        currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
        yield return new WaitForSeconds(3.6f);


        //Element 1, second line of the starting game - "Uh,"


        currentSubtitleNumber = currentSubtitleNumber + 1;
        currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
        yield return new WaitForSeconds(0.5f);


        //Element 2 - "Prentice"

        currentSubtitleNumber = currentSubtitleNumber + 1;
        currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
        yield return new WaitForSeconds(1f);

        //Element 3, third line of the start - "Walking to the right"


        currentSubtitleNumber = currentSubtitleNumber + 1;
        currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
        yield return new WaitForSeconds(3.2f);


        //Element 4, Prentice - "How original"

        audio.PlayOneShot(subtitleClips[1]);
        currentSubtitleNumber = currentSubtitleNumber + 1;
        currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
        yield return new WaitForSeconds(1.6f);
        //************************************WORKING ABOVE*********************************

        //Element 5 - Cardinal does this by moving the left stick

        audio.PlayOneShot(subtitleClips[2]);
        currentSubtitleNumber = currentSubtitleNumber + 1;
        currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
        yield return new WaitForSeconds(2.9f);


        //Element 6 - Prentice does this by.. oh wait.. oh.. Prentice can't actually move


        currentSubtitleNumber = currentSubtitleNumber + 1;
        currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
        yield return new WaitForSeconds(3.9f);


        //Element 7 - Butt... when he moves the left stick


        currentSubtitleNumber = currentSubtitleNumber + 1;
        currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
        yield return new WaitForSeconds(2.2f);


        currentSubtitleNumber = currentSubtitleNumber + 1;
        currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
        yield return new WaitForSeconds(2.2f);

        //Element 9 - Cardinal grunt


        currentSubtitleNumber = currentSubtitleNumber + 1;
        currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
        yield return new WaitForSeconds(0.4f);


        //Element 10 - Give it a try!


        currentSubtitleNumber = currentSubtitleNumber + 1;
        currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
        yield return new WaitForSeconds(1.2f);
        currentSubtitle.enabled = true;
        Destroy(GameObject.Find("DiaTriggerTargetBlock"));
    }

    IEnumerator TargetTutorialSubtitles()
    {
        //Element 11 and onwards required, gamestart uses 0-10. (Sprites)
        //For clips, start at element 3.
        SubtitlesCanvas.enabled = true;
        currentSubtitleNumber = 10;
        //Element 11 - Gate Locked sprite
        audio.PlayOneShot(subtitleClips[3]);
        currentSubtitleNumber = currentSubtitleNumber + 1;
        currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
        yield return new WaitForSeconds(5f);


        //Element 12 - Press B

        currentSubtitleNumber = currentSubtitleNumber + 1;
        currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
        yield return new WaitForSeconds(4.2f);

        //Element 13 - Hit the target
        currentSubtitleNumber = currentSubtitleNumber + 1;
        currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
        currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
        yield return new WaitForSeconds(4.2f);
        Destroy(GameObject.Find("DiaTriggerBlueBlock"));
        yield return new WaitForSeconds(5f);
        SubtitlesCanvas.enabled = false;

        IEnumerator EnemyTutorialSubtitles()
        {
            //Element 14 and onwards required. 0-13 used by TargetTutorial and gameStartSubtitles.
            //For clips, starts at element 4.
            SubtitlesCanvas.enabled = true;
            currentSubtitleNumber = 13;
            //Element 14 - Evil Knights

            audio.PlayOneShot(subtitleClips[4]);
            currentSubtitleNumber = currentSubtitleNumber + 1;
            currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
            currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
            yield return new WaitForSeconds(4f);

            //Element 15 - Whack

            currentSubtitleNumber = currentSubtitleNumber + 1;
            currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
            currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
            yield return new WaitForSeconds(2f);
        }

        IEnumerator BlueEnemiesSubtitles()
        {
            //Element 16 and onwards required.
            //Clips start at element 5.

            currentSubtitleNumber = 15;
            //Element 16
            audio.PlayOneShot(subtitleClips[5]);
            currentSubtitleNumber = currentSubtitleNumber + 1;
            currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
            currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
            yield return new WaitForSeconds(3f);


            //Element 17
            currentSubtitleNumber = currentSubtitleNumber + 1;
            currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
            currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
            yield return new WaitForSeconds(4f);

            //Element 18
            audio.PlayOneShot(subtitleClips[6]);
            currentSubtitleNumber = currentSubtitleNumber + 1;
            currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
            currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
            yield return new WaitForSeconds(2f);
            Destroy(GameObject.Find("DiaTriggerEndBlock"));
            yield return new WaitForSeconds(5f);
            SubtitlesCanvas.enabled = false;
        }

        IEnumerator EndOfLevelOneSubtitles()
        {
            //Starts at Element 19.
            //Clips start at element 7

            currentSubtitleNumber = 18;
            SubtitlesCanvas.enabled = true;
            //Element 19
            audio.PlayOneShot(subtitleClips[7]);
            currentSubtitleNumber = currentSubtitleNumber + 1;
            currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
            currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
            yield return new WaitForSeconds(3f);

            //Element 20
            audio.PlayOneShot(subtitleClips[8]);
            currentSubtitleNumber = currentSubtitleNumber + 1;
            currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
            currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
            yield return new WaitForSeconds(0.1f);

            //Element 21

            currentSubtitleNumber = currentSubtitleNumber + 1;
            currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
            currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
            yield return new WaitForSeconds(3.5f);

            //Element 22
            currentSubtitleNumber = currentSubtitleNumber + 1;
            currentSubtitle.overrideSprite = subtitleSprites[currentSubtitleNumber];
            currentSubtitle.sprite = subtitleSprites[currentSubtitleNumber];
            yield return new WaitForSeconds(2.75f);
            yield return new WaitForSeconds(5f);
            SubtitlesCanvas.enabled = false;
        }
    }

}
