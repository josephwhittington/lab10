using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTriggerInteractibleItem : MonoBehaviour
{
    [SerializeField] Text Message = null;
    [SerializeField] float waittime = 5;
    [SerializeField] bool openDoors = true;

    bool RoomClear = false;
    // Start is called before the first frame update
    void Start()
    {
        Message.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !RoomClear)
        {
            RoomClear = true;
            Message.gameObject.SetActive(true);

            if(openDoors)
            {
                // Open doors
                Invoke("OpenDoors", waittime);
            }
        }
    }

    void OpenDoors()
    {
        transform.parent.GetComponentInChildren<EventOnlyRoomController>().OpenDoors();
    }
}
