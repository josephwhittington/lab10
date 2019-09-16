using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGunBase : MonoBehaviour
{
    protected string Name;
    protected Sprite ItemImage;
    protected Vector3 Posiion, Rotation;
    [SerializeField] protected GameObject Prefab;
    [SerializeField] protected GameObject BulletPrefab;
    [SerializeField] protected GameObject AmmoShell;

    public abstract string GetName();

    public abstract void Shoot();

    public abstract void Shoot(GameObject spawnPoint1, GameObject spawnPoint2, GameObject spawnPoint3);
}
