using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float difficulty = 0.1f;

    public GameObject enemyGameObject;
    public BoxCollider enemySpawnArea;
    public Transform playerTransform, enemyParent;
    public Player playerScript;

    void FixedUpdate()
    {
        if(Random.value < difficulty) {
            Vector3 boxCenter = enemySpawnArea.transform.position;
            Vector3 boxExtent = enemySpawnArea.bounds.size / 2;

            Vector3 spawnPoint = new Vector3(
                boxCenter.x + Random.Range(-boxExtent.x, boxExtent.x),
                boxCenter.y + Random.Range(-boxExtent.y, boxExtent.y),
                boxCenter.z + Random.Range(-boxExtent.z, boxExtent.z));

            GameObject newEnemy = Instantiate(enemyGameObject, spawnPoint, Quaternion.identity, enemyParent);

            EnemyAI enemyAI = newEnemy.GetComponent<EnemyAI>();
            enemyAI.playerTransform = playerTransform;
            enemyAI.playerScript = playerScript;
        }
    }
}
