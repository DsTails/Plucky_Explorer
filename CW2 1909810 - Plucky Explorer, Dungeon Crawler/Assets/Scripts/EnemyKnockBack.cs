using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour
{
    public float knockThrust;
    public float KnockDur;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            Rigidbody2D playerRB = other.GetComponent<Rigidbody2D>();

            if (playerRB != null)
            {
                playerRB.isKinematic = true;
                playerRB.isKinematic = false;
                Vector2 Difference = playerRB.transform.position - transform.position;
                Difference = Difference.normalized * knockThrust;
                playerRB.AddForce(Difference, ForceMode2D.Impulse);
                StartCoroutine(EnemyKnockCo(playerRB));
                playerRB.isKinematic = true;
                playerRB.isKinematic = false;
                if (gameObject.CompareTag("projectile")) {
                    StartCoroutine(ProjectileDestroy());
                }
            }
        }
    }
    private IEnumerator EnemyKnockCo(Rigidbody2D playerRB)
    {
        if (playerRB != null)
        {
            yield return new WaitForSeconds(KnockDur);
            playerRB.velocity = Vector2.zero;
            

        }
    }
    private IEnumerator ProjectileDestroy()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(this.gameObject);
    }
}
