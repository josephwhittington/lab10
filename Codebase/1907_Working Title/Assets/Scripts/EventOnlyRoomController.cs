using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOnlyRoomController : MonoBehaviour
{
    [SerializeField] private GameObject[] Doors = null;
    [SerializeField] private GameObject[] DoorLights = null;

    // Start is called before the first frame update
    void Start()
    {
        CloseDoors();
    }

    void CloseDoors()
    {
        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i].SetActive(true);
        }

        for (int i = 0; i < DoorLights.Length; i++)
        {
            DoorLights[i].SetActive(false);
        }
    }

    public void OpenDoors()
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlayDoorOpenSound();

        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i].SetActive(false);
        }

        for (int i = 0; i < DoorLights.Length; i++)
        {
            DoorLights[i].SetActive(true);
        }
    }

    public void ClearRoom()
    {
#if DEBUG
        Debug.Log("Start room cleared");
#endif
    }
}
