using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class StartSpawner : MonoBehaviour
{
    public delegate void PlayerEnter(string RoomName);
    public static event PlayerEnter PlayerEntered;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerEntered?.Invoke(gameObject.transform.parent.gameObject.name);
        }
    }
}
