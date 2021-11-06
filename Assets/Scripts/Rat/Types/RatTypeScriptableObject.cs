using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rat
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RatTypeScriptableObject", order = 2)]
    public class RatTypeScriptableObject : ScriptableObject
    {
        // Evolution
        public RatTypeScriptableObject[] nextGeneration;
        
        // Appearance
        public Sprite[] sprites;
        public Sprite[] eatingSprites;
        public Color color = Color.white;
        public bool flip;
        
        // Attributes
        public float startHp = 100;
        public float speed = 1;
        public float spritesPerSecond = 2;
        public float standardDefense = 0;
        public float bombDefense = 0;
        public float magicDefense = 0;
    }
}
