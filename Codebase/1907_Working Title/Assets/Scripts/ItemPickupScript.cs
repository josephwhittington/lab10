using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupScript : MonoBehaviour
{
    [SerializeField] string ItemName = "";
    [SerializeField] string PrefabLocation = "";

    bool Triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !Triggered)
        {
            Triggered = true;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<AudioSource>().volume = GameObject.FindGameObjectWithTag("AudioManager")
                .GetComponent<AudioManager>().SFXVolume;
            GetComponent<AudioSource>().Play();
            Destroy(gameObject, 4);
        }
    }

    public string GetItemName()
    {
        return ItemName;
    }

    public string GetPrefabLocation()
    {
        return PrefabLocation;
    }
}
