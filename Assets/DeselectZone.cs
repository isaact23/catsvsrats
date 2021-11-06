using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeselectZone : MonoBehaviour
{
    [SerializeField] private PlacingManager pManager;

    void OnMouseDown()
    {
        pManager.Deselect();
    }
}
