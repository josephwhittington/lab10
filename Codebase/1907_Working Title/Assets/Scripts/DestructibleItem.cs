using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleItem : MonoBehaviour
{
    [SerializeField] private int HP = 25;
    [SerializeField] private bool DestroyOtherObjectOnCollision = false;
    [SerializeField] private GameObject thingtpdestroy = null;

    private int maxhp = 0;

    // Start is called before the first frame update
    void Start()
    {
        maxhp = HP;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            if (HP > 0)
                HP -= 1;
        }

        if (HP <= 0)
        {
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlayDoorDestructionSound();

            if (DestroyOtherObjectOnCollision)
            {
                Debug.Log("Hit ");
                thingtpdestroy.GetComponent<LightningWall>().Disable();
                HP = maxhp;
            }
            else Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
