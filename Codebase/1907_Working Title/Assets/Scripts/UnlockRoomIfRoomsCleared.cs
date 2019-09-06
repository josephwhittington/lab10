using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockRoomIfRoomsCleared : MonoBehaviour
{
    [SerializeField] private int Index = 0;
    [SerializeField] private GameObject DoorGuard = null;
    [SerializeField] private GameObject DoorLight = null;

    void Start()
    {
        PlayerPrefs.SetInt("RoomsEntered", 0);

        if (DoorGuard)
            DoorGuard.gameObject.SetActive(true);

        if(DoorLight)
            DoorLight.gameObject.SetActive(false);
    }

    void Update()
    {
        if (PlayerPrefs.HasKey("RoomsEntered") && PlayerPrefs.GetInt("RoomsEntered") >= Index)
        {
            DoorGuard.gameObject.SetActive(false);
            DoorLight.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlayDoorOpenSound();
        }
    }
}
