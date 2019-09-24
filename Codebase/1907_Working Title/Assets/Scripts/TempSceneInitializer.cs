using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSceneInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>()?.ChangeGun("GunPeaShooter", "GunPeaShooter");

        PlayerStats.PlayerCoinCountOnLevelLoad = PlayerStats.Coins;

        PlayerStats.LoadPlayerPrefs();
    }
}
