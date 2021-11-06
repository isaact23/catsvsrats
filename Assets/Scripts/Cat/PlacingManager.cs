using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] public int money = 0;
    [SerializeField] private MouseOnWorld mouseW;
    private GameObject catToPlace;

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
        return true;
    }
    public bool IsSelling()
    {
        return true;
    }
    public void PlaceCat()
    {
        GameObject createdCat = Instantiate(catToPlace, mouseW.mousePos, Quaternion.identity);
        createdCat.GetComponent<CatAttack>().Setup(gameManager.ratManager);
        createdCat.GetComponent<Placeable>().Setup(this);
    }


}
