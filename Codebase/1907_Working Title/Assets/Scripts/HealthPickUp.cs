using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(PlayerStats.CurrentHealth +1 <= PlayerStats.MaxHitPoints)
                PlayerStats.CurrentHealth += 1;
            Debug.Log(PlayerStats.CurrentHealth);
            Destroy(gameObject);

            // Play picup sound
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>()?.PlayHealthItemPickup();
        }
    }

}
