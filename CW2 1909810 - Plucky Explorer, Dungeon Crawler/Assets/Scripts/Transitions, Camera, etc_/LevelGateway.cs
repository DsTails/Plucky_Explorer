using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GatewayState { 
    inactive,
    active
}
public class LevelGateway : MonoBehaviour
{
    private int currentLevel;
    private Animator animator;
    public GatewayState currentState;
    private DontDestroyOnLoad _gameManager;

   
    void Start()
    {
        _gameManager = FindObjectOfType<DontDestroyOnLoad>();
        //Gets the index of the current scene
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        //Gets the current state of the gateway
        currentState = GatewayState.inactive;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GatewayState.active) {
            animator.SetBool("ConditionFulfilled", true);
        }
        
    }
    private void FixedUpdate()
    {
        if (_gameManager.relicPieces == _gameManager.relicRequirements) {
            currentState = GatewayState.active;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player") && currentState == GatewayState.active) {
            SceneManager.LoadScene(currentLevel + 1);
            _gameManager.relicPieces = 0;
            _gameManager.relicRequirements = 4;
            _gameManager.timeRemaining = 60;
            
            
        }
    }
    
    
}
