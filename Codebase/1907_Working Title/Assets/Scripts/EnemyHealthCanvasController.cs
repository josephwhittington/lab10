using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthCanvasController : MonoBehaviour
{
    float lockposition = 0;
    // Start is called before the first frame update
    void Start()
    {
        //lockposition = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lockposition, lockposition);
    }
}
