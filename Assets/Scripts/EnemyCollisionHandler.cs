using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab = null;
    [SerializeField] ParticleSystem deathParticlePrefab = null;
    ParticleSystem vfx = null;

    void Start()
    {
        AddNonTriggerBoxCollider();
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider nonTriggerBoxCollider = gameObject.AddComponent<BoxCollider>();
        nonTriggerBoxCollider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints <= 0)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        hitPoints--;
        hitParticlePrefab.Play();
    }

    private void KillEnemy()
    {
        var parentObject = GameObject.FindGameObjectsWithTag("Parent");
        vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity, parentObject[0].transform);
        vfx.Play();
        
        Destroy(vfx.gameObject, vfx.main.duration);
        Destroy(gameObject);
    }
}
