using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWall : MonoBehaviour
{
    [SerializeField] float tickDamageCooldown = 2.0f;
    [SerializeField] private GameObject[] HealthSpawnPoints = null;
    [SerializeField] private GameObject HealthPickup = null;
    private float tickDamageTimer = 0.0f;

    public void OnTriggerStay(Collider other)
    {
        tickDamageTimer += Time.deltaTime;
        if(other.CompareTag("Player") && !other.GetComponent<PlayerController>().GodModeEnabled())
        {
            if(tickDamageTimer >= tickDamageCooldown)
            {
                tickDamageTimer = 0;
                PlayerStats.DealDamageToPlayer(1);
            }
        }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        SpawnHealthPickup();
    }

    private void SpawnHealthPickup()
    {
        System.Random rand = new System.Random();
        int thing = rand.Next(HealthSpawnPoints.Length);
        Instantiate<GameObject>(HealthPickup, HealthSpawnPoints[thing].transform.position,
            HealthPickup.transform.rotation);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        Invoke("Enable", 10);
    }

    public void Suicide()
    {
        Destroy(gameObject);
    }
}
