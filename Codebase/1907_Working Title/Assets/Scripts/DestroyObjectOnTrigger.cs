using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject ThingToDestroy = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(ThingToDestroy)
                Destroy(ThingToDestroy);
        }
    }
}
