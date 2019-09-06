using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoShellDeath : MonoBehaviour
{
    public float DestroyTime = 5f;
  
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestroyTime);

        // Set the renderer to work with the new thing
        //if (GameObject.FindGameObjectWithTag("Weapon"))
            //GetComponent<ParticleSystem>().startColor = GameObject.FindGameObjectWithTag("Weapon").GetComponent<MeshRenderer>().material.color;
    }

}
