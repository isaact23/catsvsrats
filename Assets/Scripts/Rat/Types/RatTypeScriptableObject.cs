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
        public Color color;
        public Sprite sprite;
        
        // Attributes
        public float startHp;
        public float speed;
        public float defense;
        public float bombDefense;
        public float magicDefense;
    }
}
