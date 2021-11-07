using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rat;

public class BeginButton : MonoBehaviour
{
    [SerializeField] private RatManager rManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        rManager.StartRound();
    }
}
