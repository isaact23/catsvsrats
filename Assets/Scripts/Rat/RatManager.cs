using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rat
{
    public class RatManager : MonoBehaviour
    {
        public List<RatObject> allRats;
        // Start is called before the first frame update
        void Awake()
        {
            allRats = new List<RatObject>();
        }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
