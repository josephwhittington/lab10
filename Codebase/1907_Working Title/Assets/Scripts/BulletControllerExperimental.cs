using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerExperimental : MonoBehaviour
{
    private Rigidbody rigidbody = null;

    private float Speed = 25;

    //private int BounceCount = 5;
    //private int TmesBounced = 0;


    private bool colliding = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward.normalized * Speed;

        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (!colliding)
        {
            Vector2 directionVector = Vector2.Reflect(new Vector2(rigidbody.velocity.x, rigidbody.velocity.z), new Vector2(collision.contacts[0].normal.x, collision.contacts[0].normal.z));
            rigidbody.velocity = (new Vector3(directionVector.x, 0, directionVector.y)).normalized * Speed;
        }
    }
}
