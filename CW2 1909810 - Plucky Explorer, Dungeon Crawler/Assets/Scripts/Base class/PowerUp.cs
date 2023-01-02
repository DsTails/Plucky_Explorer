using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : basicPickup
{
    public float healthIncrease;
    private DontDestroyOnLoad _gameManager;
    public AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<DontDestroyOnLoad>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player")) {
            if (isAttackPickup == true)
            {
                IncreaseDamage();
            }
            else if (isHealthPickup == true)
            {
                HealPlayer();

            }
            else if (isSpeedPickup == true) {
                IncreaseSpeed();
            }
        }
        Destroy(this.gameObject);
    }
    void HealPlayer()
    {
        _gameManager.GetComponent<DontDestroyOnLoad>().HealPlayer(healthIncrease);
        _audioSource.Play();
    }
    void IncreaseSpeed()
    {


    }
    void IncreaseDamage()
    {
        
    }
}
