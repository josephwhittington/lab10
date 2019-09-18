using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRoomOnDoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Room = null;
    // Start is called before the first frame update
    void Start()
    {
        Room.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Room.SetActive(true);
        }
    }
}
