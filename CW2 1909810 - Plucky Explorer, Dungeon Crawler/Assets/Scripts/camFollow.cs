using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerCharacter;
    public float smooth;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    
    private DontDestroyOnLoad _gameManager;
    // Start is called before the first frame update
    void Start()
    {
       
        _gameManager = FindObjectOfType<DontDestroyOnLoad>();
    }

    void Update()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != playerCharacter.position)
        {
            Vector3 playerPosition = new Vector3(playerCharacter.position.x, playerCharacter.position.y, transform.position.z);
            playerPosition.x = Mathf.Clamp(playerPosition.x, minPosition.x, maxPosition.x);
            playerPosition.y = Mathf.Clamp(playerPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, playerPosition, smooth);
        }
        
    }
    
}
