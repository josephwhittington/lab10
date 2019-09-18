using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject ThingToSpawn = null;
    [SerializeField] private GameObject location = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (ThingToSpawn && location)
                Instantiate<GameObject>(ThingToSpawn, location.transform.position, location.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
