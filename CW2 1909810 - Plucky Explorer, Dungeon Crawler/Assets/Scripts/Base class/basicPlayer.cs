using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum PlayerState {
    walk,
    attack,
    interact
}
public class basicPlayer : MonoBehaviour
{
    //Establishes variables for player properties
    //Such as currentState, movement speed, etc.
    
    public float attack;
    public PlayerState currentState;
    public float movSpd;
    private float diagMovSpd;
    public float diagMovModifier;
    [SerializeField]
    private int livesNum;
    private Rigidbody2D rb;
    private Vector3 move;
    private Animator animator;
    private DontDestroyOnLoad _gameManager;
    private MusicManager _musicManager;
    private LivesManager _livesManager;
    private int SceneID;
    // Start is called before the first frame update
    
    void Start()
    {
        livesNum = 3;
        //The default player state is set to walk
        currentState = PlayerState.walk;
        //animator and rb get the respective components from the gameObject
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        diagMovSpd = movSpd * diagMovModifier;
        //Make reference to the gameManager object, allows access to methods
        _gameManager = FindObjectOfType<DontDestroyOnLoad>();
        _musicManager = FindObjectOfType<MusicManager>();
        _livesManager = FindObjectOfType<LivesManager>();
        SceneID = SceneManager.GetActiveScene().buildIndex;

        
    }

    public void UpdateHealth(float damage) {
        //Calls the method in the gameManager object, Updating the player's health
        _gameManager.ChangeHealth(damage);
        

    }

    // Update is called once per frame
    void Update()
    {
        //sets move to 0
        //move.x and y are equivalent to inputs on the Horizontal and Vertical Axis respectively
        move = Vector3.zero;
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        //Executes coroutine that allows player to attack
        if (Input.GetButton("attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
        //Updates the player animation and allows them to move
        else if (currentState == PlayerState.walk) {
            UpdateAnimationAndMove();
        }
        checkHealth();


    }
    void checkHealth() {
        //Assesses whether or not the player's health is 0
        if (_gameManager.playerHP <= 0)
        {
           
            LifeDecreaseAndResetScene();
            

        }
    }
    void checkTime()
    {
        if (_gameManager.timeRemaining <= 0)
        {
            LifeDecreaseAndResetScene();

        }
    }
    void LifeDecreaseAndResetScene()
    {
        //Reduces lives number by 1
        LivesManager.livesNum = LivesManager.livesNum - 1;

        if (LivesManager.livesNum > 0)
        {
            //Resets the scene
            if (SceneManager.GetActiveScene().name == "Level 1")
            {
                //Destroys the gameManager and Music manager object to prevent duplicates
                Destroy(_gameManager.gameObject);
                Destroy(_musicManager.gameObject);
                //Reloads the current scene, which will create new instances of the gameManager and musicManager
                SceneManager.LoadScene(SceneID);
            }
            else
            {
                //Reset the scene normally (As gamemanager and musicManager both already exist)
                _gameManager.playerHP = _gameManager.playerMaxHP;
                if (_gameManager.PlayerHPBar)
                {

                    _gameManager.PlayerHPBar.updateHealth();
                }
                _gameManager.timeRemaining = _gameManager.startTime;
                SceneManager.LoadScene(SceneID);

            }
        }
        else
        {
            Destroy(_gameManager.gameObject);
            Destroy(_musicManager.gameObject);
            Destroy(_livesManager.gameObject);
            SceneManager.LoadScene("Game Over");
        }
    }
   
    void MoveCharacter()
    {
        //normalizes movement for diagonal directions
        move.Normalize();
        rb.velocity = new Vector2(move.x * movSpd, move.y * movSpd);
        //rb.MovePosition(transform.position + move * movSpd * Time.deltaTime);

    }
    private IEnumerator AttackCo() {
        //Prevents the animation from being flipped, which would interfere with the hitbox placement
        GetComponent<SpriteRenderer>().flipX = false;
        //Sets the state of the "Attacking" condition to true
        animator.SetBool("Attacking", true);
        //Sets the player state to attack
        currentState = PlayerState.attack;
        //Waits 0.75f seconds to set the "Attacking" animator parameter to false
        yield return new WaitForSeconds(0.75f);
        animator.SetBool("Attacking", false);
        //Suspends the coroutine for .05f seconds, before changing the player state back to walk
        yield return new WaitForSeconds(.05f);
        currentState = PlayerState.walk;
    }
    //Knockback coroutine, knocks the player back after being hit by a trap
    public IEnumerator knockback(float duration, float knockPower, Vector3 knockDirection) {
        float timer = 0;
        while (duration > timer) {
            timer += Time.deltaTime;
            rb.AddForce(new Vector3(knockDirection.x * 4, knockDirection.y * knockPower, transform.position.z));
        }
        yield return 0;
    }
    void FixedUpdate()
    {
        if (currentState == PlayerState.walk) { 
            //Allows for animator states to be changed during gameplay
            if (move.y < 0f)
            {
                //If movement.y is less than 0, the player is moving down
                animator.SetBool("isWalkingDownwards", true);
                animator.SetBool("isWalkingUpwards", false);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (move.y > 0f)
            {
                //if movement.y is greater than 0, the player is moving true
                animator.SetBool("isWalkingDownwards", false);
                animator.SetBool("isWalkingUpwards", true);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                //The player isn't moving vertically at all
                animator.SetBool("isWalkingDownwards", false);
                animator.SetBool("isWalkingUpwards", false);
                GetComponent<SpriteRenderer>().flipX = false;
            }
        if (move.x != 0f)
        {
            animator.SetBool("isWalkingHorizontal", true);
            if (move.x < 0f)
            {
                //If the player is moving left, flip sprite to face direction
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                //Retains the default direction of the sprite
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else {
            animator.SetBool("isWalkingHorizontal", false);
        }
    }
}
    //Method is executed when the player defeats an enemy
    public void TimeUpdate(float timeIncrease) {
        //Increases the amount of time the player has remaining
        _gameManager.timeRemaining += timeIncrease;
    }
    void UpdateAnimationAndMove()
    {
        MoveCharacter();
        
        
    }
}
