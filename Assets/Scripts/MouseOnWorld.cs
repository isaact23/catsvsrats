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
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        testObject.position = mousePos;

    }
}
