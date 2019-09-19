using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine;

public class BossManController : IPausable
{
    enum BossState
    {
        PHASE1Vulnerable,
        PHASE1Invulnerable
    }

    private BossState State = BossState.PHASE1Vulnerable;

    [SerializeField] private GameObject MeshThing = null;

    [SerializeField] private GameObject EnvObjects = null;
    [SerializeField] private GameObject WinScreen = null;

    [SerializeField] private GameObject[] BulletSpawnPoints = null;
    [SerializeField] private GameObject BulletPrefab = null;
    [SerializeField] private GameObject SlowBulletPrefab = null;
    [SerializeField] private GameObject RichochetBulletPrefab = null;
    [SerializeField] private GameObject[] Waypoints = null;
    [SerializeField] private GameObject CenterWaypoint = null;

    [SerializeField] private float ShotInterval = 0.1f;

    [SerializeField] private int Health = 100000;
    [SerializeField] private GameObject DestroyEffect = null;

    // Health
    private int maxheath = 0;
    [SerializeField] private Image BossManHealth = null;
    // Health

    // AI
    private NavMeshAgent agent = null;
    private int WaypointIndex = 0;

    private MeshRenderer mesh = null;

    private bool phase2 = false;

    private int shotcount = 0;
    // AI

    // Indicator Lights
    [SerializeField] private GameObject[] IndicatorLights = null;
    // Indicator Lights

    private float TimeElapsedSinceLastShot = 0.0f;
    private Quaternion target = Quaternion.identity;
    private Quaternion last_position = Quaternion.identity;

    private float timeinstate = 10.0f;

    void Start()
    {
        // Disable the win screen canvas
        WinScreen.gameObject.SetActive(false);
        // Disable the win screen canvas
        
        maxheath = Health;

        target = Quaternion.AngleAxis(180.0f, Vector3.up);
        agent = GetComponent<NavMeshAgent>();

        mesh = MeshThing.GetComponent<MeshRenderer>();
        mesh.enabled = false;
    }

    void Update()
    {
        if (!GamePaused)
        {
            BossManHealth.fillAmount = (float)((float)Health / (float)maxheath);

            if (Health <= 0)
                Suicide();
            if ((Health <= ((float)maxheath * 0.6f)) && !phase2)
            {
                phase2 = true;

                EnvObjects.gameObject.GetComponent<LightningWallsController>().EnableEnvEffects();
            }

            if (Health <= (maxheath * 0.3f))
            {
                EnvObjects.gameObject.GetComponent<LightningWallsController>().StartPhase3();
            }

            TimeElapsedSinceLastShot += Time.deltaTime;

            if (TimeElapsedSinceLastShot >= ShotInterval)
            {
                shotcount++;
                for (int i = 0; i < BulletSpawnPoints.Length; i++)
                {
                    Shoot(BulletSpawnPoints[i]);
                }

                if (shotcount == 4) shotcount = 0;

                TimeElapsedSinceLastShot = 0.0f;
            }
            Rotate();
            SetInterctionLightColors();
        }
    }

    void SetInterctionLightColors()
    {
        if (State == BossState.PHASE1Vulnerable)
        {
            for (int i = 0; i < IndicatorLights.Length; i++)
            {
                IndicatorLights[i].GetComponent<Light>().color = Color.green;
            }
        } else if (State == BossState.PHASE1Invulnerable)
        {
            for (int i = 0; i < IndicatorLights.Length; i++)
            {
                IndicatorLights[i].GetComponent<Light>().color = Color.red;
            }
        }
    }

    void Rotate()
    {
        timeinstate -= Time.deltaTime;

        if (State == BossState.PHASE1Vulnerable && timeinstate > 0)
        {
            if (target != Quaternion.identity)
            {
                last_position = transform.rotation;
                transform.rotation = Quaternion.Lerp(transform.rotation, target, 0.1f);
            }

            if (transform.rotation == last_position)
            {
                if (target == Quaternion.AngleAxis(180.0f, Vector3.up))
                    target = Quaternion.AngleAxis(360, Vector3.up);
                else target = Quaternion.AngleAxis(180.0f, Vector3.up);
            }
        }
        else if (State == BossState.PHASE1Invulnerable)
        {
            // Rotation
            if (target != Quaternion.identity)
            {
                last_position = transform.rotation;
                transform.rotation = Quaternion.Lerp(transform.rotation, target, 0.01f);
            }

            if (transform.rotation == last_position)
            {
                if (target == Quaternion.AngleAxis(180.0f, Vector3.up))
                    target = Quaternion.AngleAxis(360, Vector3.up);
                else target = Quaternion.AngleAxis(180.0f, Vector3.up);
            }
            // Rotation

            if ((timeinstate % 10) < 0.1f)
            {
                System.Random rand = new System.Random();
                int randindex = rand.Next(Waypoints.Length);
                if (WaypointIndex == randindex)
                {
                    if (randindex == Waypoints.Length - 1)
                        WaypointIndex -= 1;
                    else WaypointIndex += 1;
                }
                else WaypointIndex = randindex;
            }

            if (timeinstate <= 0)
            {
                State = BossState.PHASE1Vulnerable;
                agent.SetDestination(CenterWaypoint.transform.position);
                timeinstate = 10.0f;
            }

            Vector3 position = Waypoints[WaypointIndex].gameObject.transform.position;
            agent.SetDestination(position);
        }

        // State changes
        if (State == BossState.PHASE1Vulnerable && timeinstate <= 0)
        {
            State = BossState.PHASE1Invulnerable;
            timeinstate = 30;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().DealDamageToPlayer(5);
        }

        if (collision.gameObject.CompareTag("Projectile") && State == BossState.PHASE1Invulnerable)
        {
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlayBossDeflect();
        }
    }

    void Suicide()
    {
        WinScreen.SetActive(true);
        Time.timeScale = 0;

        Instantiate<GameObject>(DestroyEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void Shoot(GameObject p_spawnPoint)
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlayEnemyPew();

        if (BulletPrefab && shotcount < 4)
        {
            Instantiate<GameObject>(BulletPrefab, p_spawnPoint.transform.position, p_spawnPoint.transform.rotation);
        }
        else
        {
            if(SlowBulletPrefab)
                Instantiate<GameObject>(SlowBulletPrefab, p_spawnPoint.transform.position, p_spawnPoint.transform.rotation);
        }
    }

    public void DealDamage(uint p_damage)
    {
        Health = (Health - p_damage >= 0) ? Health - (int)p_damage : 0;
    }

    public bool IsInvulnerable()
    {
        if (State == BossState.PHASE1Invulnerable) return true;
        return false;
    }

    public void InstantiateBulletReflection(Vector3 p_position, Quaternion p_orientation)
    {
        Instantiate<GameObject>(RichochetBulletPrefab, p_position, p_orientation);
    }

    public void MakeVisible()
    {
        mesh.enabled = true;
        Invoke("MakeInvisible", 0.25f);
    }

    public void MakeInvisible()
    {
        mesh.enabled = false;
    }
}
