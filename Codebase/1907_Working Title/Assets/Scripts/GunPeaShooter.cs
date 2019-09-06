using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPeaShooter : IGunBase
{
    void Start()
    {
    }

    public override void Shoot()
    {
        Instantiate<GameObject>(BulletPrefab, transform.position, transform.rotation);
       // Instantiate<GameObject>(AmmoShell, transform.position, transform.rotation);
    }

    public override string GetName()
    {
        throw new System.NotImplementedException();
    }
}
