using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);    
    }
    public static int livesNum;
    private DontDestroyOnLoad _gameManager;
    void Start()
    {
        livesNum = 3;
        _gameManager = FindObjectOfType<DontDestroyOnLoad>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(livesNum);
    }
}
