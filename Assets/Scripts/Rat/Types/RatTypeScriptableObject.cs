using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rat
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RatTypeScriptableObject", order = 2)]
    public class RatTypeScriptableObject : ScriptableObject
    {
        // Appearance
        public Sprite[] sprites;
        public Sprite[] eatingSprites;
        public Color color = Color.white;
        public bool flip;
        
        // Attributes
        public float startHp = 5;
        public float speed = 1;
        public float spritesPerSecond = 2;
        public float standardDefense = 0;
        public float bombDefense = 0;
        public float magicDefense = 0;
        public float bitesPerSecond = 1f;
        public int biteDamage = 1;
        public int cheesePosition = -1;
        
        // Sounds
        public AudioClip walk;
        public AudioClip eat;
        public float soundsPerSec = 0.15f;
        
        // Cash
        public int cashDrop;
    }
}
