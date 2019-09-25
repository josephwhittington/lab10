using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : IPausable
{
    CharacterController characterController;

    [SerializeField] float speed = 6.0f;
    [SerializeField] ParticleSystem healEffect = null;

    // Shooting
    private float ShootSpeedTimer = 0;
    // Shooting

    // God mode
    bool GodMode = false;
    float DefaultYPosition;
    // God mode

    // Alex's Dash
    private Rigidbody rb = null;
    private bool dashing = false;
    private bool DashCoolDown = false;

    float DashSpeed = 18.0f;
    float coolDown = 1.0f;
    float TimerCoolDown = 0.0f;
    [SerializeField] GameObject Dash = null;
    [SerializeField] GameObject DashParticle = null;

    TrailRenderer trailRend = null;
    // Alex's Dash

    //Alex Hit effect
    SkinnedMeshRenderer meshRenderer = null;
    public GameObject model = null;
    public Texture normTexture = null;
    public Texture tempTexture = null;
  

    private float HitColorCoolDown = 0.25f;
    private float HitCoolDown = 0.25f;
    private bool HitEffectON = false;
    //Alex Hit effect

    //Alex BulletShells
    //[SerializeField] GameObject AmmoShell;
    //Alex BulletShells

    // Whittington params
    //[SerializeField] GameObject BulletPrefab = null;
    [SerializeField] GameObject SpawnPoint1 = null;
    [SerializeField] GameObject SpawnPoint2 = null;
    [SerializeField] GameObject SpawnPoint3 = null;
    // Whittington params

    // Current gun
    string CurrentGun = "";
    // Current gun

    //Wills Slow speed
    float SlowDuration = 1.0f;
    float SlowTimer = 0.0f;
    float tempSpeed = 0;
    float tempDash = 0;

    //Wills Slow Speed

    private Vector3 moveDirection = Vector3.zero;


    Animator anim;

    void Start()
    {
        tempDash = DashSpeed;
        tempSpeed = speed;
        // Player Y correction
        DefaultYPosition = transform.position.y;
        // Player Y correction

        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        trailRend = Dash.GetComponent<TrailRenderer>();

        // Grab material for hit effect
        meshRenderer = model.GetComponent<SkinnedMeshRenderer>();
        
        anim = GetComponent<Animator>();
        healEffect.Stop();

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check to determine i we're gonna apply the hit effect
        if (collision.gameObject.CompareTag("EnemyProjectile") && !GodMode)
        {
            if (!HitEffectON)
            {
                HitEffectON = true;
                HitColorCoolDown = HitCoolDown;
            }
        }

        //Will's Check for Slowing projectile
        if (collision.gameObject.CompareTag("SlowProjectile") && !GodMode)
        {
            if (!HitEffectON)
            {
                HitEffectON = true;
                HitColorCoolDown = HitCoolDown;
                SlowTimer = SlowDuration;
                SlowPlayer();
            }
        }
        //Will's Check for Slowing projectile
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            string name = other.gameObject.GetComponent<ItemPickupScript>()?.GetItemName();
            string location = other.gameObject.GetComponent<ItemPickupScript>()?.GetPrefabLocation();
            ChangeGun(location, name);
        }
        if (other.gameObject.CompareTag("FirstAid"))
        {
            if (!healEffect.isPlaying)
                healEffect.Play();
        }

    }

    public void ChangeGun(string p_gunlocation, string p_name)
    {
        CurrentGun = p_name;
        GameObject thing = Resources.Load<GameObject>(p_gunlocation);
        // If resource loads
        if (thing)
        {
            GameObject newGun = Instantiate<GameObject>(thing, SpawnPoint1.transform.position, SpawnPoint1.transform.rotation);
            newGun.transform.parent = gameObject.transform;
        }
    }

    void Update()
    {
        if (!GamePaused)
        {
            float vSpeed = characterController.velocity.magnitude;
            float hSpeed = characterController.velocity.magnitude;

            anim.SetFloat("Hspeed", hSpeed);

            if (PlayerStats.CurrentHealth <= 0 || PlayerStats.CurrentHealth > PlayerStats.MaxHitPoints)
            {
                PlayerStats.PlayerDead = true;
            }
            if (!PlayerStats.PlayerDead)
            {
                TimerCheckOnHitColorEffect();
                SlowPlayer();
                DashFunction();

                Shoot();

                // God mode
#if DEBUG
                if (Input.GetKeyDown(KeyCode.G))
                    ToggleGodMode();

                if (Input.GetKeyDown(KeyCode.Return))
                    PlayerStats.Coins += 5;
#endif
                // God mode
            }

            // Player Y correction
            transform.position = new Vector3(transform.position.x, DefaultYPosition, transform.position.z);
            // Player Y correction
        }
    }

    void ToggleGodMode()
    {
        GodMode = !GodMode;
#if DEBUG
        Debug.Log(GodMode ? "Godmode enabled" : "Godmode Disabled");
#endif
    }

    void DashFunction()
    {
        // Dash
        if (Input.GetButtonDown("Dash") && !dashing)
        {
            //float thing = (PlayerPrefs.GetInt(GlobalConfigs.DashCooldownUpgrade) * 0.2f);
            dashing = true;
            TimerCoolDown = coolDown - ((float)(PlayerPrefs.GetInt(GlobalConfigs.DashCooldownUpgrade, 0) * 0.2f));
        }

        // Alex's Dash
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        if (!dashing)
        {
            trailRend.time = Mathf.Lerp(trailRend.time, 0, Time.deltaTime);
            characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
        else if (dashing)
        {
            Instantiate<GameObject>(DashParticle, transform.position, Quaternion.identity);
            trailRend.time = Mathf.Lerp(trailRend.time, 0.25f, Time.deltaTime);
            characterController.Move(moveDirection.normalized * DashSpeed * Time.deltaTime);
        }

        TimerCheckForCoolDown();
        // Alex's Dash
    }

    // TIMER CHECK
    void TimerCheckForCoolDown()
    {
        if (TimerCoolDown > 0)
        {
            TimerCoolDown -= Time.deltaTime;
        }

        if (TimerCoolDown <= 0)
        {
            //TimerCoolDown = coolDown - (float)(PlayerPrefs.GetInt(GlobalConfigs.DashCooldownUpgrade, 0) * 0.2f);
            TimerCoolDown = 0;
            dashing = false;
        }
    }

    private void FixedUpdate()
    {
        if (!GamePaused)
            LookAtMouse();
    }

    void LookAtMouse()
    {
        // Rotate towards mouse
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    public void DealDamageToPlayer(uint p_damage)
    {
        PlayerStats.DealDamageToPlayer(p_damage);
    }

    // Shoot
    void Shoot()
    {
        if (Input.GetMouseButton(0) && !string.IsNullOrEmpty(CurrentGun))
        {
            ShootSpeedTimer += Time.deltaTime;

            if (ShootSpeedTimer >= (3.3f - PlayerStats.FireRate) * 0.1f)
            {
                ShootSpeedTimer = 0;

                gameObject.GetComponentInChildren<IGunBase>().Shoot(SpawnPoint1, SpawnPoint2, SpawnPoint3);
                gameObject.GetComponentInChildren<IGunBase>().GetComponent<AudioSource>().volume = GameObject
                    .FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SFXVolume;
                gameObject.GetComponentInChildren<IGunBase>().GetComponent<AudioSource>().Play();
            }
        }
    }

    //TimerCheckOnHitColorEffect
    void TimerCheckOnHitColorEffect()
    {
        meshRenderer.material.mainTexture = normTexture;

        if (HitEffectON)
        {
            if (HitColorCoolDown > 0)
            {
               
                meshRenderer.material.mainTexture = tempTexture;
                HitColorCoolDown -= Time.deltaTime;
            }

            else if (HitColorCoolDown < 0)
            {
                HitColorCoolDown = 0;
                HitEffectON = false;
            }
        }
    }

    public bool GodModeEnabled()
    {
        return GodMode;
    }

    //Wills Timer for when hit by a slow projectile
    void SlowPlayer()
    {
        if (SlowTimer > 0)
        {
            SlowTimer -= Time.deltaTime;
            if (speed == tempSpeed)
                speed = tempSpeed / 2;
            if (DashSpeed == tempDash)
                DashSpeed = tempDash / 2;
        }
        if (SlowTimer < 0)
        {
            SlowTimer = 0;
            if (speed == tempSpeed / 2)
                speed = tempSpeed;
            if (DashSpeed == tempDash / 2)
                DashSpeed = tempDash;
        }
        //Wills Timer for when hit by a slow projectile
    }

    public bool PlayerCanDash()
    {
        return !dashing && !DashCoolDown;
    }
}