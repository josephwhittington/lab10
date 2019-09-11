using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWall : MonoBehaviour
{
    [SerializeField] private bool DestructibleItem = false;
    [SerializeField] float tickDamageCooldown = 2.0f;
    private float tickDamageTimer = 0.0f;
    public void OnTriggerStay(Collider other)
    {
        tickDamageTimer += Time.deltaTime;
        if(other.CompareTag("Player") && !other.GetComponent<PlayerController>().GodModeEnabled())
        {
            if(tickDamageTimer >= tickDamageCooldown)
            {
                tickDamageTimer = 0;
                PlayerStats.DealDamageToPlayer(1);
            }
        }
    }
}
