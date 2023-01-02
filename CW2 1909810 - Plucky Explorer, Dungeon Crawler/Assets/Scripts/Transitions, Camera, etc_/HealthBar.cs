using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image HealthBarFill;
    public DontDestroyOnLoad _gameManager;
    public float health, maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        //Access the values and methods of the gameManager
        _gameManager = FindObjectOfType<DontDestroyOnLoad>();
        //Sets the maxHealth value of the health bar to the max health of the player stored in the game manager
        maxHealth = _gameManager.GetComponent<DontDestroyOnLoad>().playerMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void updateHealth() {
        //health value is set to the player's current HP when this method is called
        health = _gameManager.GetComponent<DontDestroyOnLoad>().playerHP;
        //Changes the fill amount to be equal to the current health divided by the max health
        HealthBarFill.fillAmount = health / maxHealth;
    }
}
