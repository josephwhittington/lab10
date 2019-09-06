using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    // Start is called before the first frame update
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            PlayerStats.Coins += 1;
            Destroy(gameObject);
        }
    }

}
