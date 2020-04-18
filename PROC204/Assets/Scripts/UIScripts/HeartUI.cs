using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [SerializeField] Sprite fleshHeart;
    [SerializeField] Sprite metalHeart;

    bool isFleshHeart;

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
