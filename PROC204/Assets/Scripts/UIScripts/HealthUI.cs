using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    public Sprite threeHearts, twoHearts, oneHeart, twoHalfHearts, oneHalfHearts, zeroHalfHearts;
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
        Image currentSprite = GetComponent<Image>();
        var currentSpriteName = currentSprite.sprite.name;
        if (currentSpriteName.ToString() == "threeHearts")
        {
            currentSprite.overrideSprite = twoHalfHearts;
            currentSprite.sprite = twoHalfHearts;
        }
        else if (currentSpriteName.ToString() == "twoHalfHearts")
        {
            currentSprite.overrideSprite = twoHearts;
            currentSprite.sprite = twoHearts;
        }
        else if (currentSpriteName.ToString() == "twoHearts")
        {
            currentSprite.overrideSprite = oneHalfHearts;
            currentSprite.sprite = oneHalfHearts;
        }
        else if (currentSpriteName.ToString() == "oneHalfHearts")
        {
            currentSprite.overrideSprite = oneHeart;
            currentSprite.sprite = oneHeart;
        }
        else if (currentSpriteName.ToString() == "oneHeart")
        {
            currentSprite.overrideSprite = zeroHalfHearts;
            currentSprite.sprite = zeroHalfHearts;
        }
        else
        {
            //Default case
            currentSprite.enabled = false;
        }
    }
}
