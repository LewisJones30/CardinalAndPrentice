using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Heart UI container that stores both heart state sprites
public class HeartUI : MonoBehaviour
{
    [SerializeField] Sprite fleshHeart;
    [SerializeField] Sprite metalHeart;

    bool isFleshHeart;

    //Switches heart sprite to represent 1 or 2 lives
    public bool IsFleshHeart { get => isFleshHeart;
        set
        {
            isFleshHeart = value;

            if (isFleshHeart) image.sprite = fleshHeart;
            else image.sprite = metalHeart;
        }
        }

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
}
