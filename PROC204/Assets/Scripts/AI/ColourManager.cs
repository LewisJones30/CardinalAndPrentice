using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourManager : MonoBehaviour
{
    [SerializeField] ColourValue presetColour = ColourValue.None;
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
        if (presetColour != ColourValue.None) //Colour chosen by user is set
        {
            foreach (var container in entityColours)
            {
                if (container.colour == presetColour)
                {
                    entityColour = container;
                    break;
                }
            }
        }
        else //Otherwise by default choose random colour
        {
            int randomIndex = UnityEngine.Random.Range(0, entityColours.Length);
            entityColour = entityColours[randomIndex];
        }        
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
