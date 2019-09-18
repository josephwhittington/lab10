using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Documentation
    Door needs the following to not break:
        SpawnPosition
        CameraPosition
        NextRoom
    Things to consider:
        The player spawn position needs to make sense in terms of where the player actually goes.
*/

public class DoorController : MonoBehaviour
{
    // Shit we need
    [SerializeField] GameObject SpawnPosition = null;
    //[SerializeField] GameObject CameraPos;
    //[SerializeField] GameObject NextRoom;
    // Shit we need

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.transform.position = SpawnPosition.transform.position;
            other.gameObject.SetActive(true);
        }
    }

    void MoveCamera()
    {
        //Camera.main.transform.position = CameraPos.transform.position;
    }
}
