using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOnWorld : MonoBehaviour
{
    public Vector3 mousePos = Vector3.zero;
    Camera cam;
    public Transform testObject;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        testObject.position = GetPlacerPosition();
    }
    public Vector3 GetPlacerPosition()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), 0f);
    }
}
