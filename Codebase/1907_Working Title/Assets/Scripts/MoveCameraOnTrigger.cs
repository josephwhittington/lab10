using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject CameraPosition = null;

    private bool Moving = false; 
    void OnTriggerEnter(Collider other)
    {
        //Camera.main.transform.position = CameraPosition.transform.position;
        //if (other.gameObject.CompareTag("Player") && CameraPosition)
        //    Moving = true;

        // Whittington
        if(other.gameObject.CompareTag("Player") && CameraPosition)
            Camera.main.GetComponent<CameraController>().MoveCameraToPosition(CameraPosition.transform.position);
        // Whittington
    }

    void OnTriggerExit(Collider other)
    {
        //if(other.gameObject.CompareTag("Player"))
        //    Moving = false;
    }

    void FixedUpdate()
    {
        if(Moving)
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, CameraPosition.gameObject.transform.position, 1f * Time.deltaTime);
    }
}
