using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rat
{
    public class RatManager : MonoBehaviour
    {
        public List<RatObject> allRats;
        
        // Start is called before the first frame update
        private void Awake()
        {
            allRats = new List<RatObject>();
        }

        // Update is called once per frame
        private void Update()
        {
            
        }

        public void StartRound(int round)
        {
            
        }
    }
}
