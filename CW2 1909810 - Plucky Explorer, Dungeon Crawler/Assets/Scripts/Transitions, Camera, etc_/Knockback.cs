using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    //Knockthrust determines the distance the enemy/object is knocked back to
    public float Knockthrust;
    //KnockDur determines the duration of the knockback
    public float KnockDur;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Detects if the object is tagged with "enemy"

        if (other.gameObject.CompareTag("enemy"))
        {
            //Gets the rigidbody component of the enemy object
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                //Disables the kinematic property of the rigidbody, turning it into a dynamic rigidbody
                enemy.isKinematic = false;
                //Calculates the difference between the enemy transform and the player's transform
                Vector2 difference = enemy.transform.position - transform.position;
                //Normalizes the vector to a magnitude of 1, and multiplies it by knockthrust
                difference = difference.normalized * Knockthrust;
                //Applies the difference with the ForceMode Impulse
                enemy.AddForce(difference, ForceMode2D.Impulse);
                //Executes the KnockCo CoRoutine
                StartCoroutine(KnockCo(enemy));
            }
        }
        else if (other.gameObject.CompareTag("player")) { 
        
        }
    }
    private IEnumerator KnockCo(Rigidbody2D enemy) {
        if (enemy != null) {
            //If enemy still exists, then the Coroutine waits for the duration of the knockback to expire
            yield return new WaitForSeconds(KnockDur);
            //enemy velocity is set to 0, resulting in 0 movement
            enemy.velocity = Vector2.zero;
            //Enemy is converted back into a kinematic rigidbody
            enemy.isKinematic = true;
        }
    }
}
