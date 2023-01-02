using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    //Sets up basic properties, such as whether it has been opened
    public bool ChestOpened;
    public DontDestroyOnLoad _gameManager;
    private Animator animator;
    private AudioSource audioSource;
    public float timeIncrease;

    // Start is called before the first frame update
    void Start()
    {
        //Sets the ChestOpened to false, meaning it hasn't been interacted with
        ChestOpened = false;
        //allows access to the gameManager object, giving access to methods
        _gameManager = FindObjectOfType<DontDestroyOnLoad>();
        //gets the animator component
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player")) {
            //If the player collides with the chest, sets the "PlayerInteractWithChest"
            //animator parameter to true, allows animation to player
            if (!ChestOpened) {
                animator.SetBool("PlayerInteractWithChest", true);
                audioSource.Play();
                //Executes ChestOpen method
                ChestOpen();
            }
        }
    }
    void ChestOpen()
    {
        if (!ChestOpened)
        {
            //If the chest hasn't been opened, increases the number of relic pieces owned, and increases the amount of time
            //remaining
            _gameManager.relicPieces += 1;
            _gameManager.timeRemaining += timeIncrease;
            //Sets ChestOpened to true, which prevents repeated interactions
            ChestOpened = true;
        }
    }
}
