using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rat
{
    public class RatManager : MonoBehaviour
    {
        public List<RatObject> allRats;
        public Animator roundAnimator;
        
        // Start is called before the first frame update
        private void Awake()
        {
            allRats = new List<RatObject>();
        }

        public void StartRound(int round)
        {
            
        }

        private void SpawnRat()
        {
            
        }
    }
}
