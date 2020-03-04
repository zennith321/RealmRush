using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerHitPoints = 10;

    public void PlayerHit()
    {
        print("enemy reached end");
        playerHitPoints--;
    }
}
