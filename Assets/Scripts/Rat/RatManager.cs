using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Rat
{
    public class RatManager : MonoBehaviour
    {
        public List<RatObject> allRats;
        public Animator roundAnimator;
        public RatObject ratPrefab;
        public Vector3[] cheesePositions;
        public List<RatTypeScriptableObject> ratTypeIndex;
        public List<PathScriptableObject> pathIndex;
        
        private int lastCheesePosition = 0;
        
        // Start is called before the first frame update
        private void Awake()
        {
            allRats = new List<RatObject>();
        }

        public void StartRound(int round)
        {
            
        }

        // Digit 1 (LSD) - path ID
        // Digits 2 and 3 - rat type
        // e.g. 105 is rat type 10, path ID 5
        public void SpawnRat(int typePath)
        {
            RatObject newRat = Instantiate(ratPrefab);
            int path = typePath % 10;
            int ratType = typePath / 10;
            if (ratType >= ratTypeIndex.Count) {
                Debug.LogError("Rat type " + ratType + " out of bounds.");
            } else if (path >= pathIndex.Count) {
                Debug.LogError("Path " + path + " out of bounds.");
            } else {
                newRat.ratType = ratTypeIndex[ratType];
                newRat.path = pathIndex[path];
            }

            newRat.ratManager = this;
            allRats.Add(newRat);
        }

        public Vector3 GetCheesePosition()
        {
            lastCheesePosition++;
            if (lastCheesePosition >= cheesePositions.Length) {
                lastCheesePosition = 0;
            }

            return cheesePositions[lastCheesePosition];
        }

        public void RemoveRat(RatObject rat)
        {
            allRats.Remove(rat);
        }
    }
}
