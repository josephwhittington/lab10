using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntered : MonoBehaviour
{
    private bool triggered = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !triggered)
        {
            triggered = true;
            PlayerPrefs.SetInt("RoomsEntered", PlayerPrefs.GetInt("RoomsEntered") + 1);
        }
    }
}
