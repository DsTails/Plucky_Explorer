using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DontDestroyOnLoad : MonoBehaviour
{
    
    [SerializeField]
    //Sets up player properties, level conditions, etc.
    public float playerHP;
    //Conditions for levels
    public int relicPieces;
    public int relicRequirements;
    public int livesNum;
    //Max Player HP is used to set starting HP
    public float playerMaxHP;
    public float timeRemaining;
    public float startTime;
    //Used to create a timer for the player
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI relicText;
    public TextMeshProUGUI livesText;
    private basicPlayer PlayerChar;
    private Image HealthBarImage;
    public HealthBar PlayerHPBar;
    private void Start()
    {
        
        
        
    }
    public void ChangeHealth(float damage) {
        //Player Health is reduced by the damage received from object
        playerHP -= damage;
        //Updates the HP Bar on the UI
        if (PlayerHPBar)
        {
            PlayerHPBar.updateHealth();
        }
       
    }
    public void HealPlayer(float healthIncrease) {
        
        playerHP += healthIncrease;
        if (playerHP > playerMaxHP) {
            playerHP = playerMaxHP;
        }
        if (PlayerHPBar) {
            PlayerHPBar.updateHealth();
        }
    }
    private void Awake()
    {
        
        PlayerChar = FindObjectOfType<basicPlayer>();
        //Sets player HP to match max HP
        playerHP = playerMaxHP;
        timeRemaining = startTime;
        DontDestroyOnLoad(this);

    }
    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        relicText.text = "" + relicPieces + " / " + relicRequirements;
        timeText.text = "" + Mathf.Round(timeRemaining);
        livesText.text = "x " + LivesManager.livesNum;
    }
}
