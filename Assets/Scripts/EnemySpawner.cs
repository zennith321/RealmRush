using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnTimer = 1f;
    [SerializeField] EnemyMovement enemyPrefab = null;
    [SerializeField] Transform parent = null;
    [SerializeField] AudioClip spawnedEnemySFX;
    void Start()
    {
        StartCoroutine(SpawnEnemy());   
    }
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);
            Instantiate(enemyPrefab, transform.position, Quaternion.identity, parent);
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
