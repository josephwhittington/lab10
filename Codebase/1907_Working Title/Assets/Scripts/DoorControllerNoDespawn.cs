using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerNoDespawn : MonoBehaviour
{
    // Shit we need
    [SerializeField] GameObject SpawnPosition = null;
    // Shit we need
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.transform.position = SpawnPosition.transform.position;
            other.gameObject.SetActive(true);
        }
    }
}
