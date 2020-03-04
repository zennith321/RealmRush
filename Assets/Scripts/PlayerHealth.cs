using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerHitPoints = 10;
    [SerializeField] Text healthText;
    [SerializeField] AudioClip playerSFX;

    void Start()
    {
        healthText.text = playerHitPoints.ToString();
    }

    public void PlayerHit()
    {
        GetComponent<AudioSource>().PlayOneShot(playerSFX);
        playerHitPoints--;
        healthText.text = playerHitPoints.ToString();
    }
}
