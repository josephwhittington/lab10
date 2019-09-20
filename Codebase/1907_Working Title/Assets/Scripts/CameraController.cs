using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject StartingCameraPosition = null;

    Vector3 next_position = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = StartingCameraPosition.transform.position;
        PlayerStats.LoadPlayerPrefs();
    }

    public void MoveCameraToPosition(Vector3 p_position)
    {
        next_position = p_position;
    }

    void FixedUpdate()
    {
        if(next_position != Vector3.zero)
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, next_position, 0.9f * Time.deltaTime);
    }
}
