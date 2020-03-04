using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // TODO make movement smooth
    [SerializeField] ParticleSystem finishParticlePrefab = null;
    ParticleSystem vfx = null;
    [SerializeField] float movementPeriod = 0.5f;

    PlayerHealth playerHealth;

    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movementPeriod);
        }
        SelfDestruct(); // at the end of path
    }

    private void SelfDestruct()
    {
        var parentObject = GameObject.FindGameObjectsWithTag("Parent");
        vfx = Instantiate(finishParticlePrefab, transform.position, Quaternion.identity, parentObject[0].transform);
        vfx.Play();
        Destroy(vfx.gameObject, vfx.main.duration);
        Destroy(gameObject);

        playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.PlayerHit();
    }
}
