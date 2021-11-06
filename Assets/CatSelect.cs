using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSelect : MonoBehaviour
{
    [SerializeField] private PlacingManager pManager;
    [SerializeField] private GameObject catPrefab;
    [SerializeField] private int cost;
    // Start is called before the first frame update
    void Awake()
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        pManager.GiveCat(catPrefab, cost);    
    }
}
