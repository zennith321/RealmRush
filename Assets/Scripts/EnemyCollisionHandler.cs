using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyCollisionHandler : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab = null;
    [SerializeField] ParticleSystem deathParticlePrefab = null;
    ParticleSystem vfx = null;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;

    AudioSource audioSource;

    void Start()
    {
        AddNonTriggerBoxCollider();
        audioSource = GetComponent<AudioSource>();
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
        GetComponent<AudioSource>().PlayOneShot(enemyHitSFX);
        hitPoints--;
        hitParticlePrefab.Play();
    }

    private void KillEnemy()
    {
        var parentObject = GameObject.FindGameObjectsWithTag("Parent");
        vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity, parentObject[0].transform);
        vfx.Play();
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position); //TODO move under a parent
        
        Destroy(vfx.gameObject, vfx.main.duration);
        Destroy(gameObject);
    }
}
