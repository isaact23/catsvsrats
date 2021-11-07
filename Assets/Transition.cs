using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField] private bool loadsAScene = false;
    [SerializeField] private string sceneLoadName;
    [SerializeField] private bool startOnScreen = false;
    [SerializeField] private Transform endPos;
    private Vector3 middlePos;
    private Vector3 startPos;
    private float loadTime = 0f;
    [SerializeField] private float totalLoadTime = 1f;
    public bool loading = false;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        loadTime = 0f;
        if (startOnScreen)
        {
            middlePos = Vector3.Lerp(startPos, endPos.position, 1f);
            loading = true;
        }
        else
        {
            middlePos = Vector3.Lerp(endPos.position, startPos, 1f);
            loading = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (loading)
        {
            loadTime += Time.deltaTime;
            float current = loadTime / totalLoadTime;
            transform.position = Vector3.Lerp(Vector3.Lerp(startPos, middlePos, current), Vector3.Lerp(middlePos, endPos.position, current), current);
            if (loadTime >= totalLoadTime)
            {
                if (loadsAScene && loadTime >= totalLoadTime)
                {
                    LoadScene(sceneLoadName);
                    loading = false;
                }
            }
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
