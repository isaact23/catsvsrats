using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOnWorld : MonoBehaviour
{
    public Vector3 mousePos = Vector3.zero;
    Camera cam;
    public Transform testObject;
    public Placeable toPlace;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        testObject.position = mousePos;
        if (true || toPlace != null) 
        {
            Vector3 placePos = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), 0f);
            testObject.position = placePos;
        }


    }

    private bool CanSelectCat()
    {
        return true;
    }
}
