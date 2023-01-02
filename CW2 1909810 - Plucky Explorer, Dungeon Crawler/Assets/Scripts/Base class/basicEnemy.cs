using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyFSM {
    idle,
    walk,
    attack,
    stagger,
}
public class basicEnemy : MonoBehaviour
{
    //Establishes basic properties, such as the current state of the enemy, Health, name, attack, speed and so on
    public EnemyFSM currentState;
    public float maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    //Allows access to the _gameManager's methods
    public DontDestroyOnLoad _gameManager;
    public AudioSource audioSource;
    public float timeIncrease;

    void start()
    {
        _gameManager = FindObjectOfType<DontDestroyOnLoad>();
        audioSource = gameObject.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HitBox")) {
            audioSource.Play();
            health -= other.gameObject.GetComponentInParent<basicPlayer>().attack;
            if (health <= 0)
            {
                //If the player defeats the enemy, increases the amount of time they have remaining
                other.gameObject.GetComponentInParent<basicPlayer>().TimeUpdate(timeIncrease);
                //Destroys the enemy
                Destroy(this.gameObject);
            }
        }
        
    }
}
