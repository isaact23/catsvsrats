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
        public HealthManager healthManager;
        public PlacingManager placingManager;
        public AudioClip mutateSound;
        public AudioClip deathSound;

        private AudioSource audioSource;
        private int lastCheesePosition = 0;
        

        private void Awake()
        {
            allRats = new List<RatObject>();
            audioSource = GetComponent<AudioSource>();
        }

        public void StartRound(int round)
        {
            //roundAnimator.Play();
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
            newRat.healthManager = healthManager;
            newRat.placingManager = placingManager;
            newRat.mutateSound = mutateSound;
            allRats.Add(newRat);
        }

        public Vector3 GetCheesePosition(int pos=-1)
        {
            // Optional parameter pos overrides which position to grab.
            if (pos >= 0 && pos < cheesePositions.Length) {
                return cheesePositions[pos];
            }
            
            lastCheesePosition++;
            if (lastCheesePosition >= cheesePositions.Length) {
                lastCheesePosition = 0;
            }

            return cheesePositions[lastCheesePosition];
        }

        public void RemoveRat(RatObject rat)
        {
            allRats.Remove(rat);
            audioSource.clip = deathSound;
            audioSource.Play();
        }
    }
}
