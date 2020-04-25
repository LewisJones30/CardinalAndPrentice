using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourManager : MonoBehaviour
{
    [SerializeField] ColourValue presetColour = ColourValue.None;
    [SerializeField] ColourContainer[] entityColours;

    //Stores material associated with colour
    ColourContainer entityColour;

    public ColourValue Colour { get => entityColour.colour; }

    private void Awake()
    {
        if (entityColours == null || entityColours.Length < 1) return;
        SetColour();

        //Set colour material
        SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        renderer.material = entityColour.colourMaterial;

        //Set weakness so that Prentice does bonus damage to 
        //enemies of the same colour as fired projectile
        GetComponent<CombatTarget>().ColourWeakness = entityColour.colour;
    }

    //Sets colour of enemy at the start
    //Either random by default or manually set
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

    //Groups colour and associated material
    [System.Serializable]
    class ColourContainer
    {
        public ColourValue colour;
        public Material colourMaterial;
    }
}

//All available colours
public enum ColourValue
{
    Yellow,
    Red,
    Green,
    Blue,
    None
}
