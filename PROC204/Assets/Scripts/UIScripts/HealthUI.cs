using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    public Sprite threeHearts, twoHearts, OneHeart, twoHalfHearts, oneHalfHearts, zeroHalfHearts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeHealthUpdate()
    {
        Sprite currentSprite = GetComponent<Sprite>();
        var currentSpriteName = currentSprite.name;
        if (currentSpriteName.ToString() == "threeHearts")
        {
            //Instantiate 2.5 hearts sprite
        }
        else if (currentSpriteName.ToString() == "twoHalfHearts")
        {
            //Instantiate 2 hearts sprite
        }
        else if (currentSpriteName.ToString() == "twoHearts")
        {
            //Instantiate 1.5 hearts sprite
        }
        else if (currentSpriteName.ToString() == "oneHalfHearts")
        {
            //Instantiate 1 hearts sprite
        }
        else if (currentSpriteName.ToString() == "oneHeart")
        {
            //Instantiate 0.5 hearts sprite
        }
        else
        {
            //Default case
        }
    }
}
