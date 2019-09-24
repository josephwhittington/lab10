using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDoorController : MonoBehaviour
{
    [SerializeField] private string Roomname = "";

    // Shit we need
    [SerializeField] GameObject SpawnPosition = null;
    // Shit we need

    private bool roomClear = false;

    private void OnEnable()
    {
        StartSpawner.PlayerEntered += RoomCleared;
    }

    void RoomCleared(string roomname)
    {
        if(roomname == Roomname)
            roomClear = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && roomClear)
        {
            other.gameObject.SetActive(false);
            other.gameObject.transform.position = SpawnPosition.transform.position;
            other.gameObject.SetActive(true);
        }
    }

    void OnDisable()
    {
        StartSpawner.PlayerEntered -= RoomCleared;
    }
}
