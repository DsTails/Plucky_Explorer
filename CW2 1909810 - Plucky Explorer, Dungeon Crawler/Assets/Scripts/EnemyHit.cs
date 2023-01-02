using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public float knockThrust;
    public float KnockDur;
    public AudioSource audioSource;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {

            GetComponentInParent<Guard>();
            other.gameObject.GetComponent<basicPlayer>().UpdateHealth(GetComponentInParent<Guard>().baseAttack);
            
        }

    }
    
}
