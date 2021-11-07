using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatSelect : MonoBehaviour
{
    [SerializeField] private PlacingManager pManager;
    [SerializeField] private GameObject catPrefab;
    [SerializeField] private Sprite changedCursor;
    [SerializeField] private int cost;

    [SerializeField] private TextMeshPro priceText;

    void Start()
    {
        priceText.text = "$" + cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        pManager.GiveCat(catPrefab, cost, changedCursor);    
    }
}
