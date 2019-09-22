using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEnemyEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string RoomName = null;
    [SerializeField] GameObject obstacleEnemy = null;
    private void OnEnable()
    {
        StartSpawner.PlayerEntered += StartObst;
    }
    void StartObst(string roomName)
    {
        Debug.Log("Entered");
        if (RoomName == roomName)
            obstacleEnemy.SetActive(true);

    }
    private void OnDisable()
    {
        StartSpawner.PlayerEntered -= StartObst;
    }
    // Update is called once per frame
}
