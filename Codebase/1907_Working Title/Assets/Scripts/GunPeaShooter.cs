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
    public override void Shoot(GameObject sp1, GameObject sp2, GameObject sp3)
    {
        switch (PlayerStats.MultiShot)
        {
            case 0:
                Instantiate<GameObject>(BulletPrefab, sp1.transform.position, sp1.transform.rotation);
                break;
            case 1:
                Instantiate<GameObject>(BulletPrefab, sp1.transform.position, sp1.transform.rotation);
                Instantiate<GameObject>(BulletPrefab, sp2.transform.position, sp2.transform.rotation);
                Instantiate<GameObject>(BulletPrefab, sp3.transform.position, sp3.transform.rotation);
                break;
            default:
                break;
        }
    }

    public override string GetName()
    {
        throw new System.NotImplementedException();
    }
}
