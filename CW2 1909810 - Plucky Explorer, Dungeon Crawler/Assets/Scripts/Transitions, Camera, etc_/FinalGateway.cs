using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalGateway : LevelGateway
{
    private int CurrentLevel;
    private Animator Animator;
    private DontDestroyOnLoad _GameManager;
    private MusicManager _MusicManager;
    private LivesManager _LivesManager;
    private basicPlayer playerCharacter;
    void Start()
    {
        playerCharacter = FindObjectOfType<basicPlayer>();
        _GameManager = FindObjectOfType<DontDestroyOnLoad>();
        _MusicManager = FindObjectOfType<MusicManager>();
        _LivesManager = FindObjectOfType<LivesManager>();
        CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        currentState = GatewayState.inactive;
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GatewayState.active) {
            Animator.SetBool("isConditionFulfilled", true);
        }
    }
    void FixedUpdate()
    {
        if (_GameManager.relicPieces == _GameManager.relicRequirements) {
            currentState = GatewayState.active;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("player") && currentState == GatewayState.active)
        {
            Destroy(_GameManager.gameObject);
            Destroy(_MusicManager.gameObject);
            Destroy(_LivesManager.gameObject);
            SceneManager.LoadScene(CurrentLevel + 1);
            
            
        }
    }
}
