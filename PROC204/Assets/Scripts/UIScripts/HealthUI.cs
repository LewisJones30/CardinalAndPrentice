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

    Health cardinalHealth;

    int displayedHealth = 0;

    List<HeartUI> hearts = new List<HeartUI>();

    private void Awake()
    {
        cardinalHealth = GameObject.FindGameObjectWithTag("Player 1").GetComponent<Health>();
    }

    private void Start()
    {
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
        if (newHealth < 0) newHealth = 0;

        int change = newHealth - displayedHealth;

        if (change < 0) DecreaseHearts(Mathf.Abs(change));
        else if (change > 0) IncreaseHearts(change);

        displayedHealth = newHealth;

        if (displayedHealth < 1) GameOver();
    }

    private void DecreaseHearts(int removeCount)
    {
        while (removeCount != 0)
        {
            HeartUI lastHeart = hearts[hearts.Count - 1];

            if (removeCount > 1)
            {
                if (lastHeart.IsFleshHeart) removeCount -= 1;
                else removeCount -= 2;

                DestroyHeart(lastHeart);
            }
            else if (removeCount == 1)
            {
                if (lastHeart.IsFleshHeart) DestroyHeart(lastHeart);
                else lastHeart.IsFleshHeart = true;

                removeCount = 0;
            }
        }
    }

    private void IncreaseHearts(int addCount)
    {
        while (addCount != 0)
        {
            if (hearts.Count < 1)
            {
                AddHeart(true);
                addCount -= 1;
            }

            HeartUI lastHeart = hearts[hearts.Count - 1];

            if (addCount > 1)
            {
                if (lastHeart.IsFleshHeart)
                {
                    lastHeart.IsFleshHeart = false;
                    addCount -= 1;
                }
                else
                {
                    AddHeart(false);
                    addCount -= 2;
                }
            }
            else if (addCount == 1)
            {
                if (lastHeart.IsFleshHeart) lastHeart.IsFleshHeart = false;
                else AddHeart(true);

                addCount = 0;
            }

        }
    }

    private void DestroyHeart(HeartUI heart)
    {
        hearts.Remove(heart);
        Destroy(heart.gameObject);
    }

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
