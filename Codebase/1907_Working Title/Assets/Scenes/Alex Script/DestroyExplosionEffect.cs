using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosionEffect : MonoBehaviour
{
    //public float time = 1f;
    //public float CubeSize = 0.2f;
    //public float CubesInRow = 5;

    //public float explosionRadius;
    //public float explosionUpwards;
    //public float explosionForce;
    //float cubesPivotDistance;
    //Vector3 cubePivot;


    // Start is called before the first frame update
    void Start()
    {
        //cubesPivotDistance = CubeSize * CubesInRow / 2;
        //cubePivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
        //Destroy(gameObject, time);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Floor"))
        //{
        //    explode();
        //

        if (other.gameObject.CompareTag("Player") && !other.GetComponent<PlayerController>().GodModeEnabled())
        {
            PlayerStats.DealDamageToPlayer(2);
        }
    }

    //public void explode()
    //{
    //    for (int  x = 0;  x <CubesInRow;  x++)
    //    {
    //        for (int y = 0; y < CubesInRow; y++)
    //        {
    //            for (int z = 0; z < CubesInRow; z++)
    //            {
    //                creator(x, y, z);
    //            }
    //        }
    //    }


    //    Vector3 explosionPos = transform.position;

    //    Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

    //    foreach (Collider item in colliders)
    //    {
    //        Rigidbody rb = item.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            rb.AddExplosionForce(explosionForce ,transform.position, explosionRadius, explosionUpwards);
    //        }

    //    }
    //}


    //public void creator(int x, int y, int z)
    //{
    //    GameObject piece;

    //    piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

    //    piece.transform.position = transform.position + new Vector3(CubeSize * x, CubeSize * y, CubeSize * z) - cubePivot ;
    //    piece.transform.localScale = new Vector3(CubeSize, CubeSize, CubeSize);

      
    //    piece.AddComponent<Rigidbody>();
    //    piece.GetComponent<Rigidbody>().mass = CubeSize;

    //    Destroy(piece, 3);


    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
