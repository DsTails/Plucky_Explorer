using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Guard : basicEnemy
{
    //Sets up the properties of the object, such as the target, chase range, etc.
    public Transform target;
    //Chase ranges determines the range at which it may pursue the player
    public float chaseRange;
    //Cattack radius determines the distance at which it attacks the player
    public float attackRadius;
    //Determines the origin point of the enemy
    public Transform homeposition;
    private Animator animator;
    private Rigidbody2D rb;
    public float startSpeed;
    
    
    private void Awake()
    {
        //Sets the homeposition before the game starts
        homeposition.position = transform.position;
    }
    void Start()
    {
        startSpeed = moveSpeed;
        //health is set to defined max health
        health = maxHealth;
        //gets the rigidbody2d and animator components respectively
        rb = GetComponent<Rigidbody2D>();
        //sets the default state of the enemy to idle
        currentState = EnemyFSM.idle;
        animator = GetComponent<Animator>();
        ///target variable is set to objects with the 'player' tag
        target = GameObject.FindWithTag("player").transform;
    }

    
    void Update()
    {
      
        //Runs the checkDistance method every frame
        checkDistance();
        
        
    }
    void checkDistance() {
        //If the enemy's distance is within the chase range and is not too close to the player to attack, then it will pursue the player
        if (Vector3.Distance(target.position, transform.position) <= chaseRange && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            //Utilises the variable 'Temp' to move the enemy towards the player
            Vector3 Temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            rb.MovePosition(Temp);
            //Sets the enemy state to walk
            currentState = EnemyFSM.walk;
            //Calculates the direction of the enemy's movement
            changeAnim(Temp - transform.position);


        }
        //If the player is in range of the enemy's attack radius, then the enemy will attack
        else if (Vector3.Distance(target.position, transform.position) <= chaseRange && Vector3.Distance(target.position, transform.position) <= attackRadius && currentState != EnemyFSM.attack)
        {
            //Sets move speed to 0
            moveSpeed = 0;
            //Sets animator booleans to false to ensure animation can play
            animator.SetBool("isPlayerAbove", false);
            animator.SetBool("isPlayerBelow", false);
            animator.SetBool("isPlayerRightOrLeft", false);
            //Executes CoRoutine that allows enemy to attack
            StartCoroutine(EnemyAttackCo());
        }
    }

    public IEnumerator EnemyAttackCo() {
        //Retains default direct of the sprite, which prevents interference with hitbox positions
        GetComponent<SpriteRenderer>().flipX = false;
        //Sets the boolean "isEnemyAttacking" to true
        animator.SetBool("isEnemyAttacking", true);
        //Waits 1.25 seconds before setting "isEnemyAttacking" to false
        yield return new WaitForSeconds(1.25f);
        animator.SetBool("isEnemyAttacking", false);
        //Waits .1 seconds before returning the enemy to the walk state
        yield return new WaitForSeconds(.1f);
        currentState = EnemyFSM.walk;
        moveSpeed = startSpeed;
    }

    private void changeAnim(Vector2 direction) {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            animator.SetBool("isPlayerRightOrLeft", true);
            if (direction.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;

            }
            else if (direction.x < 0) {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        } else if(Mathf.Abs(direction.x ) < Mathf.Abs(direction.y)){
            animator.SetBool("isPlayerRightOrLeft", false);
            if (direction.y > 0)
            {
                animator.SetBool("isPlayerAbove", true);
                animator.SetBool("isPlayerBelow", false);
            }
            else if (direction.y < 0) {
                animator.SetBool("isPlayerBelow", true);
                animator.SetBool("isPlayerAbove", false);
            }
        }
    }

    void FixedUpdate()
    {
        //Update Animations based on direction
        /*if (currentState == EnemyFSM.walk)
        {
            if (target.position.y > transform.position.y)
            {
                animator.SetBool("isPlayerAbove", true);
                animator.SetBool("isPlayerBelow", false);
                animator.SetBool("isPlayerRightOrLeft", false);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (target.position.y < transform.position.y)
            {
                animator.SetBool("isPlayerAbove", false);
                animator.SetBool("isPlayerBelow", true);
                animator.SetBool("isPlayerRightOrLeft", false);
                GetComponent<SpriteRenderer>().flipX = false;

            }
            else
            {
                animator.SetBool("isPlayerAbove", false);
                animator.SetBool("isPlayerBelow", false);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (target.position.x != transform.position.x)
            {
                animator.SetBool("isPlayerRightOrLeft", true);
                animator.SetBool("isPlayerAbove", false);
                animator.SetBool("isPlayerBelow", false);

                if (target.position.x > transform.position.x)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (target.position.x < transform.position.x)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    animator.SetBool("isPlayerRightOrLeft", false);
                }
            }7
        }*/
    }
   
    
}
