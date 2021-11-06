using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellSelect : MonoBehaviour
{
    [SerializeField] private PlacingManager pManager;
    
    void OnMouseDown()
    {
        pManager.SelectSell();
    }
}
