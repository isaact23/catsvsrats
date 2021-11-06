using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] private PlacingManager pMaster;
    [SerializeField] private int sellingCost;

    // Start is called before the first frame update
    void Start()
    {
        if (pMaster == null)
        {
            pMaster = FindObjectOfType<PlacingManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setup(PlacingManager givenMaster)
    {
        pMaster = givenMaster;
    }
    void OnMouseDown()
    {
        if (pMaster.IsSelling())
        {
            // CAT SOLD
            pMaster.money = pMaster.money + sellingCost;
            pMaster.Deselect();
            Destroy(gameObject);
        }
    }
}
