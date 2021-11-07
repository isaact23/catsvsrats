using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rat;
using TMPro;


public class PlacingManager : MonoBehaviour
{
    [SerializeField] private RatManager rManager;
    [SerializeField] public int money = 0;
    [SerializeField] private MouseOnWorld mouseW;
    [SerializeField] private SpriteRenderer cursorRenderer;
    public GameObject catToPlace;
    private int currentCost = 0;
    private bool sell = false;
    private Sprite cursorChanged;
    [SerializeField] Sprite defaultCursor;
    [SerializeField] Sprite sellCursor;
    [SerializeField] TextMeshPro moneyText;

    // Start is called before the first frame update
    void Start()
    {
        Deselect();
    }
    void Update()
    {
        moneyText.text = money.ToString();
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
        SetCursor(sellCursor);
        
    }
    public void PlaceCat()
    {
        GameObject createdCat = Instantiate(catToPlace, mouseW.GetPlacerPosition(), Quaternion.identity);
        createdCat.GetComponent<CatAttack>().Setup(rManager);
        createdCat.GetComponent<Placeable>().Setup(this);
        money -= currentCost;
        Deselect();
    }
    public void GiveCat(GameObject givenPrefab, int cost, Sprite givenCursor)
    {
        Deselect();
        if (money >= cost)
        {
            currentCost = cost;
            catToPlace = givenPrefab;
            SetCursor(givenCursor);
        }
    }
    public void Deselect()
    {
        sell = false;
        catToPlace = null;
        SetCursor(defaultCursor);
        
    }

    public void SetCursor(Sprite toSet)
    {
        cursorRenderer.sprite = toSet;
    }


}
