using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] private int pickupAmount = 1;
   
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(PlayerStats.CurrentHealth +pickupAmount <= PlayerStats.MaxHitPoints)
                PlayerStats.CurrentHealth += (uint)pickupAmount;
            else if (PlayerStats.CurrentHealth + pickupAmount > PlayerStats.MaxHitPoints)
                PlayerStats.CurrentHealth = PlayerStats.MaxHitPoints;

            Destroy(gameObject);

            // Play picup sound
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>()?.PlayHealthItemPickup();
        }
    }

}
