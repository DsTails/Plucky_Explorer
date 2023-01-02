using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReset : MonoBehaviour
{
    private Vector2 startMinPosition;
    private Vector2 startMaxPosition;
    private DontDestroyOnLoad _gameManager;
    void Start()
    {
        _gameManager = FindObjectOfType<DontDestroyOnLoad>();
        startMinPosition = GetComponent<camFollow>().minPosition;
        startMaxPosition = GetComponent<camFollow>().maxPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
