using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] HeartUI heartPrefab;

    //CACHE REFERENCES
    Health cardinalHealth;

    //STATES

    //Health that is currently shown by the UI
    int displayedHealth = 0;

    //List of all hearts displayed in correct order
    List<HeartUI> hearts = new List<HeartUI>();

    private void Awake()
    {
        cardinalHealth = GameObject.FindGameObjectWithTag("Player 1").GetComponent<Health>();
    }

    private void Start()
    {
        //Display start health 
        UpdateDisplay();
    }

    private void OnEnable()
    {
        cardinalHealth.onHealthChange += UpdateDisplay;
    }

    private void OnDisable()
    {
        cardinalHealth.onHealthChange -= UpdateDisplay;
    }

    private void UpdateDisplay()
    {
        int newHealth = cardinalHealth.HealthPoints;
        if (newHealth < 0) newHealth = 0; //Negative health not valid

        int change = newHealth - displayedHealth; //Get change and apply changes only

        if (change < 0) DecreaseHearts(Mathf.Abs(change));
        else if (change > 0) IncreaseHearts(change);

        displayedHealth = newHealth; //Displayed health as now new health

        if (displayedHealth < 1) GameOver(); //Display gameover canvas
    }

    private void DecreaseHearts(int removeCount)
    {
        while (removeCount != 0) //Changes left?
        {
            HeartUI lastHeart = hearts[hearts.Count - 1];

            if (removeCount > 1) //Remove heart that has max value 2
            {
                if (lastHeart.IsFleshHeart) removeCount -= 1;
                else removeCount -= 2;

                DestroyHeart(lastHeart);
            }
            else if (removeCount == 1) //Destroy flesh heart or change metal heart when one life left to remove
            {
                if (lastHeart.IsFleshHeart) DestroyHeart(lastHeart);
                else lastHeart.IsFleshHeart = true;

                removeCount = 0;
            }
        }
    }

    private void IncreaseHearts(int addCount)
    {
        while (addCount != 0) //Changes left?
        {
            if (hearts.Count < 1) //Prevents null error when there is no last heart at the start
            {
                AddHeart(true);
                addCount -= 1;
            }

            HeartUI lastHeart = hearts[hearts.Count - 1];

            if (addCount > 1)
            {
                if (lastHeart.IsFleshHeart) //Make health displayed even 
                {
                    lastHeart.IsFleshHeart = false;
                    addCount -= 1;
                }
                else //Add 2 hearts
                {
                    AddHeart(false);
                    addCount -= 2;
                }
            }
            else if (addCount == 1) 
            {
                if (lastHeart.IsFleshHeart) lastHeart.IsFleshHeart = false; //Change heart to iron to increase by 1
                else AddHeart(true); //Add one flesh heart with value of 1

                addCount = 0;
            }

        }
    }

    //Removes heart from list and destroys sprite
    private void DestroyHeart(HeartUI heart)
    {
        hearts.Remove(heart);
        Destroy(heart.gameObject);
    }

    //Instnatiates heart sprite and adds to list
    private void AddHeart(bool isFleshHeart)
    {
        HeartUI newHeart = Instantiate(heartPrefab, transform);
        hearts.Add(newHeart);
        newHeart.IsFleshHeart = isFleshHeart;
    }    

    private void GameOver() //Called by health if cardinal dies
    {
        gameOverCanvas.enabled = true; //Set the gameover canvas to be true
    }
}
