using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedownSpace : MonoBehaviour
{
    [SerializeField] private PlacingMaster pMaster;
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
        if (pMaster.IsPlacing())
        {
            //if ()
        }
    }
}
