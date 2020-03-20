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
        SetColour();

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.material = entityColour.colourMaterial;
        }
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
    Blue
}
