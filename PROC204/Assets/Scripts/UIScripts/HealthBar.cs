using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] float timeUnitlHide = 2f;

    //CACHE REFERENCES
    Health health;
    Slider healthBar;
    Canvas canvas;

    //STATES
    float timeSinceHealthChange = 0f;

    private void Awake()
    {
        health = GetComponentInParent<Health>();
        healthBar = GetComponent<Slider>();
        canvas = GetComponentInParent<Canvas>();

        healthBar.minValue = 0;
        healthBar.maxValue = health.HealthPoints;

        HideDisplay(true);
    }

    private void OnEnable()
    {
        health.onHealthChange += UpdateDisplay;
    }

    private void OnDisable()
    {
        health.onHealthChange -= UpdateDisplay;
    }

    private void Update()
    {
        timeSinceHealthChange += Time.deltaTime;

        //Hide health bar when time since damage passes set time
        if (timeSinceHealthChange > timeUnitlHide || health.IsDead) HideDisplay(true);
    }

    void UpdateDisplay()
    {
        timeSinceHealthChange = 0;
        HideDisplay(false);

        healthBar.value = health.HealthPoints;
    }

    //Health bar hidden after time of recieving damage is passed
    //or character is dead to avoid clutter of UI
    void HideDisplay(bool isHidden)
    {
        canvas.enabled = !isHidden;
    }
}
