using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    //Sets the speed and damage of the arrow, as well as properties for knockback
    public float speed = 20.0f;
    private Rigidbody2D rb;
    public int damage = 25;
    public GameObject arrow;
    private GameObject arrowTrap;
    private GameObject player;
    public float knockThrust;
    public float knockDur;
    void Start()
    {
        //Finds the object tagged with "player"
        player = GameObject.FindWithTag("player");
        arrow = GetComponent<GameObject>();
        rb = GetComponentInParent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //sets the velocity of the arrow, which is affected by the speed value
        rb.velocity = transform.right * speed;
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("player"))
        {
            
            other.attachedRigidbody.velocity = Vector2.zero;
            //Gets the rigidbody component from the player
            
            //accesses the UpdateHealth method in the basicPlayer script, which reduces player health
            player.GetComponent<basicPlayer>().UpdateHealth(damage);
            //If the player hasn't been destroyed, knockback is applied
            
           
            
        }
        //Destroys the instance of the object
        new WaitForSeconds(0.1f);
        Object.Destroy(transform.parent.gameObject);
    }
    

}
