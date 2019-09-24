using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : IPausable
{
    [SerializeField] float Speed = 100.0f;
    [SerializeField] GameObject Effect = null;
    [SerializeField] uint Damage = 1;
    [SerializeField] private bool PlayerBullet = false;
    [SerializeField] private uint BounceCount = 0;
    [SerializeField] private bool IsBossBullet = false;

    // Richochet effect
    private uint TimesBounced = 0;
    private bool CanBounce = false;
    // Richochet effect

    bool IsColliding = false;

    Rigidbody _RigidBody = null;

    // Safety check
    Vector3 LastPosition = Vector3.zero;
    // Safety check

    [System.Obsolete]
    void Start()
    {
        _RigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, 10);

        _RigidBody.velocity = transform.forward.normalized * Speed;

        // Richochet
        if (PlayerStats.Trichochet)
        {
            System.Random rand = new System.Random();

            if (rand.Next(3 - PlayerPrefs.GetInt(GlobalConfigs.Bouncy)) == 0)
            {
                CanBounce = true;
            }
        }

        //_RigidBody.angularVelocity = Vector3.zero;
        _RigidBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        // Richochet

        // Set volumes
        if (GetComponent<AudioSource>())
            GetComponent<AudioSource>().volume =
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SFXVolume;
        // Set volumes
    }

    void update()
    {
        // Set volumes
        if(GetComponent<AudioSource>())
            GetComponent<AudioSource>().volume =
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SFXVolume;
        // Set volumes

        if (transform.position == LastPosition)
            Destroy(gameObject);
        else LastPosition = transform.position;
    }

    void FixedUpdate()
    {
        _RigidBody.velocity = _RigidBody.velocity.normalized * Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsColliding)
        {
            // Play sound
            //GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlayBulletRicochetSound();
            // Play sound

            Vector2 directionVector = Vector2.Reflect(new Vector2(_RigidBody.velocity.normalized.x, _RigidBody.velocity.normalized.z), new Vector2(collision.contacts[0].normal.normalized.x, collision.contacts[0].normal.normalized.z)).normalized;
            _RigidBody.velocity = (new Vector3(directionVector.x, 0, directionVector.y)).normalized * Speed;
            transform.forward = _RigidBody.velocity.normalized;

            IsColliding = true;

            bool spawnEffect = true;
            // Deal damage to player
            if (collision.gameObject.CompareTag("Player") && !PlayerBullet)
            {
                PlayerController Player = collision.gameObject.GetComponent<PlayerController>();
                if (Player && !Player.GodModeEnabled())
                    Player.DealDamageToPlayer(Damage);
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                if(collision.gameObject)
                    collision.gameObject.GetComponent<EnemyController>().DealDamage((int)(Damage + PlayerStats.DamageUpgrade));
            }
            else if (collision.gameObject.CompareTag("EnemyBoom"))
            {
                collision.gameObject.GetComponent<EnemySelfDestruct>().DealDamage(Damage + PlayerStats.DamageUpgrade);
            }
            else if (collision.gameObject.CompareTag("Boomer"))
            {
                collision.gameObject.GetComponent<BoomerEnemy>().DealDamage(Damage + PlayerStats.DamageUpgrade);
            }
            else if (collision.gameObject.CompareTag("CoinThief"))
            {
                collision.gameObject.GetComponent<EnemyCoinThief>().DealDamage(Damage + PlayerStats.DamageUpgrade);
            }


            else if (collision.gameObject.CompareTag("Bossman"))
            {
                if (!collision.gameObject.GetComponent<BossManController>().IsInvulnerable())
                {
                    collision.gameObject.GetComponent<BossManController>()
                        .DealDamage(Damage + PlayerStats.DamageUpgrade);
                    collision.gameObject.GetComponent<BossManController>()
                        .MakeVisible();
                }
            }
            else if (collision.gameObject.CompareTag("Sniper"))
            {
                collision.gameObject.GetComponent<SniperController>().DealDamage(Damage + PlayerStats.DamageUpgrade);
            }
            else if (collision.gameObject.CompareTag("Roomba"))
            {
                collision.gameObject.GetComponent<ObstacleEnemyController>().DealDamage((int)(Damage + PlayerStats.DamageUpgrade));
            }
            else if ((TimesBounced <= BounceCount) && PlayerBullet & CanBounce)
            {
                IsColliding = false;
                spawnEffect = false;
                Richochet();
            }
            else
            {
                if (!IsBossBullet)
                {
                    Instantiate<GameObject>(Effect, collision.GetContact(0).point, Quaternion.identity);
                    GetComponent<Light>().intensity = 0;
                }
                Destroy(gameObject);
            }


            if (spawnEffect)
            {
                if (!IsBossBullet)
                {
                    Instantiate<GameObject>(Effect, collision.GetContact(0).point, Quaternion.identity);
                    GetComponent<Light>().intensity = 0;
                }
                Destroy(gameObject);
            }

            IsColliding = false;
        }
    }

    private void Richochet()
    {
        TimesBounced += 1;
    }
}
