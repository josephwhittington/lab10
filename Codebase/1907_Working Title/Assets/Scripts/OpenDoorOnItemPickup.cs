using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorOnItemPickup : MonoBehaviour
{
    [SerializeField] GameObject[] Doors = null;
    [SerializeField] private GameObject[] DoorLights = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlayDoorOpenSound();

            for(uint i = 0; i < Doors.Length; i++)
            {
                Doors[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < DoorLights.Length; i++)
            {
                DoorLights[i].gameObject.SetActive(true);
            }
        }
    }
}
