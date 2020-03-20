using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] float timeUnitlHide = 2f;
    Health health;
    Slider healthBar;
    Canvas canvas;

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

        if (timeSinceHealthChange > timeUnitlHide || health.IsDead) HideDisplay(true);
    }

    void UpdateDisplay()
    {
        timeSinceHealthChange = 0;
        HideDisplay(false);

        healthBar.value = health.HealthPoints;
    }

    void HideDisplay(bool isHidden)
    {
        canvas.enabled = !isHidden;
    }
}
