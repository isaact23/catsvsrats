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
        public Color color = Color.white;
        
        // Attributes
        public float startHp = 100;
        public float speed = 1;
        public float spritesPerSecond = 2;
        public float defense = 0;
        public float bombDefense = 0;
        public float magicDefense = 0;
    }
}
