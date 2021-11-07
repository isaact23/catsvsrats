using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineIdle : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] float offset = 1f;
    float magnitude = 30f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + new Vector3(Mathf.Cos(Time.time + offset)*magnitude, Mathf.Sin(Time.time + offset)*magnitude, 0f);
    }
}
