using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] sprites;
    public float spritesPerSec;

    private Image image;
    private float timeElapsed = 0f;
    private int currentSprite;


    void Awake()
    {
        image = GetComponent<Image>();
    }
    
    void Update()
    {
        if (sprites.Length > 0) {
            timeElapsed += Time.unscaledDeltaTime;
            if (timeElapsed > 1 / spritesPerSec) {
                timeElapsed = 0f;
                currentSprite++;
                if (currentSprite >= sprites.Length) {
                    currentSprite = 0;
                }
            }

            image.sprite = sprites[currentSprite];
        }
    }
}
