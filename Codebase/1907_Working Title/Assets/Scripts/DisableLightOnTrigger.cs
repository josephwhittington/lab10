using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLightOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] Lights = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < Lights.Length; i++)
            {
                Lights[i].SetActive(false);
            }
        }
    }
}
