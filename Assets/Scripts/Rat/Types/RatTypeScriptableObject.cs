using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rat
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RatTypeScriptableObject", order = 2)]
    public class RatTypeScriptableObject : ScriptableObject
    {
        public Sprite sprite;
        public float startHp;
        public float speed;
    }
}
