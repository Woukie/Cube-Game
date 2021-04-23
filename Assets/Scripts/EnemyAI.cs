using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform;
    public Player playerScript;

    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(playerTransform.position - transform.position);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            playerScript.DealDamage(10f);
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
    }
}
