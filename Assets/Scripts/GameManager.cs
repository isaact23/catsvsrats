using System.Collections;
using System.Collections.Generic;
using Rat;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RatManager ratManager;
    public int health = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        ratManager.StartRound(1);
    }
}
