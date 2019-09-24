using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBehavior : MonoBehaviour
{
    [SerializeField] GameObject StoreUI = null;

    private void Start()
    {
        StoreUI.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerStats.StoreActive = true;
            StoreUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats.StoreActive = false;
            StoreUI.SetActive(false);
        }
    }
}
