using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rat;

public class PlacingManager : MonoBehaviour
{
    [SerializeField] private RatManager rManager;
    [SerializeField] public int money = 0;
    [SerializeField] private MouseOnWorld mouseW;
    public GameObject catToPlace;
    private int currentCost = 0;
    private bool sell = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool IsPlacing()
    {
        return (catToPlace != null);
    }
    public bool IsSelling()
    {
        return sell;
    }
    public void SelectSell()
    {
        Deselect();
        sell = true;
        
    }
    public void PlaceCat()
    {
        GameObject createdCat = Instantiate(catToPlace, mouseW.GetPlacerPosition(), Quaternion.identity);
        createdCat.GetComponent<CatAttack>().Setup(rManager);
        createdCat.GetComponent<Placeable>().Setup(this);
        money -= currentCost;
        catToPlace = null;
    }
    public void GiveCat(GameObject givenPrefab, int cost)
    {
        Deselect();
        if (money >= cost)
        {
            currentCost = cost;
            catToPlace = givenPrefab;
        }
    }
    public void Deselect()
    {
        sell = false;
        catToPlace = null;
    }


}
