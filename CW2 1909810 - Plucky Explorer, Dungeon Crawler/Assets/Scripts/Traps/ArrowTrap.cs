using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public Transform target;
    public float fireRange;
    public int rateOfFire;
    public Transform firepoint;
    public GameObject Arrow;
    private Animator animator;
    private bool playerInRange;
    private bool arrowReady;
    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
        animator = GetComponent<Animator>();
        arrowReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        checkDistance();
    }
    void checkDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= fireRange)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
        if (playerInRange == true)
        {
            animator.SetBool("isPlayerInRange", true);
            if (arrowReady)
            {
                StartCoroutine(fireArrow());
            }
            
        }
    }
    IEnumerator fireArrow()
    {
        fire();
        arrowReady = false;
        yield return new WaitForSeconds(rateOfFire);
        arrowReady = true;
        
    }
    void fire()
    {
        Instantiate(Arrow, new Vector2(this.transform.position.x + 1, this.transform.position.y), this.transform.rotation);
    }
}