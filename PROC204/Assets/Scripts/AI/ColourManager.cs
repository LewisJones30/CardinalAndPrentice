using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourManager : MonoBehaviour
{
    [SerializeField] ColourContainer[] entityColours;

    ColourContainer entityColour;

    public ColourValue Colour { get => entityColour.colour; }

    private void Awake()
    {
        if (entityColours == null || entityColours.Length < 1) return;
        SetColour();

        SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        renderer.material = entityColour.colourMaterial;

        GetComponent<CombatTarget>().ColourWeakness = entityColour.colour;
    }

    private void SetColour()
    {

        int randomIndex = UnityEngine.Random.Range(0, entityColours.Length);
        entityColour = entityColours[randomIndex];
    }

    [System.Serializable]
    class ColourContainer
    {
        public ColourValue colour;
        public Material colourMaterial;
    }
}

public enum ColourValue
{
    Yellow,
    Red,
    Green,
    Blue,
    None
}
