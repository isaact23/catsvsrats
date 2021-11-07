using System.Collections;
using System.Collections.Generic;
using Rat;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HealthManager : MonoBehaviour
{
    public Sprite[] cheeseSprites;
    public GameObject cheese;
    public TextMeshPro text;
    public int startHealth = 100;

    private SpriteRenderer cheeseSpriteRenderer;
    private int health;
    
    private void Start()
    {
        health = startHealth;
        cheeseSpriteRenderer = cheese.GetComponent<SpriteRenderer>();
        UpdateCheeseSprite();
        UpdateHealthText();
    }

    private void UpdateCheeseSprite()
    {
        int cheeseSprite = (int) Mathf.Floor(
            (cheeseSprites.Length - 1) - ((health * (cheeseSprites.Length - 1)) / startHealth));
        cheeseSpriteRenderer.sprite = cheeseSprites[cheeseSprite];
    }

    private void UpdateHealthText()
    {
        text.text = health.ToString();
    }

    public int GetHealth()
    {
        return health;
    }

    public void SubtractHealth(int damage)
    {
        health -= damage;
        if (health < 0) {
            health = 0;
        }
        UpdateCheeseSprite();
        UpdateHealthText();
    }
}
