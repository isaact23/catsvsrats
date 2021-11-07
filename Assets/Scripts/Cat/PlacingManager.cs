using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rat;
using TMPro;


public class PlacingManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
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
    private bool paused = false;
    private float defaultTimeScale = 1f;

    // Start is called before the first frame update
    public void PauseGame(bool setToPause)
    {
        paused = setToPause;
        if (setToPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = defaultTimeScale;
        }
        Deselect();
    }
    void Start()
    {
        Deselect();
        defaultTimeScale = Time.timeScale;
    }
    void Update()
    {
        moneyText.text = money.ToString();
        if (Input.GetKeyDown("escape"))
        {
            PauseScreen(!paused);
        }
    }

    public bool IsPlacing()
    {
        if (paused)
        {
            return false;
        }
        return (catToPlace != null);
    }
    public bool IsSelling()
    {
        if (paused)
        {
            return false;
        }
        return sell;
    }
    public void SelectSell()
    {
        if (paused)
        {
            return;
        }
        Deselect();
        sell = true;
        SetCursor(sellCursor);
        
    }
    public void PlaceCat()
    {
        if (paused)
        {
            return;
        }
        GameObject createdCat = Instantiate(catToPlace, mouseW.GetPlacerPosition(), Quaternion.identity);
        createdCat.GetComponent<CatAttack>().Setup(rManager);
        createdCat.GetComponent<Placeable>().Setup(this);
        money -= currentCost;
        Deselect();
    }
    public void GiveCat(GameObject givenPrefab, int cost, Sprite givenCursor)
    {
        if (paused)
        {
            return;
        }
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
    public void PauseScreen(bool toSet)
    {
        PauseGame(toSet);
        pauseUI.SetActive(toSet);
    }


}
