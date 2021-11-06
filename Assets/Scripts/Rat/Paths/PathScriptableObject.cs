using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rat
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PathScriptableObject", order = 1)]
    public class PathScriptableObject : ScriptableObject
    {
        public Vector2[] coordinates;
        public PathType pathType;
    }

    public enum PathType
    {
        Mutate,
        Cheese,
        Exit
    }
}

