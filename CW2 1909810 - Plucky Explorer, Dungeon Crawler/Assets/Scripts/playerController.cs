using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    //Move speed is set to 5, used to determine how quickly the player moves left, right, up or down
    public float moveSpd = 5f;
    //Private float that is affected by the calculation between normal move speed and a diagonal modifier
    private float diagMovSpd;
    //Diagonal move modifier used to reduce the movement speed when moving diagonally
    public float diagonalMoveModifier = 0.75f;
    //Set player damage to 10, though this has yet to be used
    public int damage = 10;

    //Makes reference to the animator and rigidbody2d
    private Rigidbody2D rb;
    private Animator animator;
    //used to determine player movement
    private Vector2 movement;
    void Start()
    {
        //Gains access to the rigidbody and animator componenets for the purpose of altering position and animation states
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //diagonal move speed si calculated
        diagMovSpd = moveSpd * diagonalMoveModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement.x and movement.y receive a value depending on the axis input and the direction of the input
        //(1 for right and down, and -1 for left and up respectively)
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        animator.speed = 0.3f;
    }
    private void FixedUpdate()
    {
        //Detects if the player is pressing a horizontal and vertical movement key. Moves the player diagonally if these conditions are true
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) != 0f && Mathf.Abs(Input.GetAxisRaw("Vertical")) != 0f)
        {
            //The position of the player is determined by movement multiplied by the diagonal movespeed, and is dependent on how long the keys are held down for
            rb.MovePosition(rb.position + movement * diagMovSpd * Time.deltaTime);
        }
        else
        {
            //Position is affected by movement multiplied by move speed, and is dependent on the duration of the key being pressed.
            rb.MovePosition(rb.position + movement * moveSpd * Time.deltaTime);
        }
    }
}
