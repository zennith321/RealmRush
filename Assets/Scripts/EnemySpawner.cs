using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnTimer = 1f;
    [SerializeField] EnemyMovement enemyPrefab = null;
    [SerializeField] Transform parent = null;
    // Start is called before the first frame update

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity, parent);
            yield return new WaitForSeconds(spawnTimer);
        }
    }
    void Start()
    {
        StartCoroutine(SpawnEnemy());   
    }
}
