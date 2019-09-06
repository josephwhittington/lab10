﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{

    //alex killstreak
    public static uint KillStreak = 0;
    //alex killstreak
    public enum UPGRADE { HEALTH, DASH, DMG, BOUNCY };
    // Stats
    public static uint MaxHitPoints = 10;
    public static uint CurrentHealth = 10;
    public static uint Coins = 0;
    public static bool PlayerDead = false;
    // Stats

    //Maxes
    public static uint MAXHP = 15;
    public static uint MAXDASH = 3;
    public static uint MAXDMG = 2;
    public static uint MAXBOUNCEY = 4;
    //Maxes


    //Upgrades
    public static uint HealthUpgrade = 0; //5 per level
    public static uint DashCDUpgrade = 0; //.2 per level
    public static uint DamageUpgrade = 0; //1 per level, max 3 dmg
    public static uint Bouncy = 0;
    //public static uint DamageSizeUpgrade = 0;

    //WHITTINGTON REMOVE YOU HOE \/\/\/\/
    public static bool Trichochet = true; // 25% chance of richochet
    //Upgrades

    public static void DealDamageToPlayer(uint p_damage)
    {
        if (CurrentHealth - p_damage <= 0)
        {
            CurrentHealth = 0;
            PlayerDead = true;
        }
        else CurrentHealth -= p_damage;
    }

    //Call on scene load
    public static void LoadPlayerPrefs()
    {
        HealthUpgrade = (uint)PlayerPrefs.GetInt(GlobalConfigs.HealthUpgrade, 0);
        DashCDUpgrade = (uint)PlayerPrefs.GetInt(GlobalConfigs.DashCooldownUpgrade, 0);
        DamageUpgrade = (uint)PlayerPrefs.GetInt(GlobalConfigs.DamageUpgrade, 0);
        Bouncy = (uint)PlayerPrefs.GetInt(GlobalConfigs.Bouncy, 0);

        MaxHitPoints = (uint)PlayerPrefs.GetInt("MaxHealth", 10);
        CurrentHealth = (uint)PlayerPrefs.GetInt("CurrentHealth", 10);
        Coins = (uint)PlayerPrefs.GetInt("Coins", 0);
        PlayerDead = false;
    }
    //Call on scene load

    public static bool ApplyUpgrade(UPGRADE up)
    {
        bool Sucess = false;
        switch (up)
        {
            case UPGRADE.HEALTH:
                Sucess = ApplyHealth();
                break;
            case UPGRADE.DASH:
                Sucess = ApplyDash();
                break;
            case UPGRADE.DMG:
                Sucess = ApplyDMG();
                break;
            case UPGRADE.BOUNCY:
                Sucess = ApplyBounce();
                break;
            default:
                Sucess = false;
                break;
        }

        PlayerPrefs.Save();
        return Sucess;
    }

    private static bool ApplyHealth()
    {
        if (HealthUpgrade < MAXHP)
        {
            // Whittington
            PlayerPrefs.SetInt(GlobalConfigs.HealthUpgrade, PlayerPrefs.GetInt(GlobalConfigs.HealthUpgrade) + 1);
            // Whittington

            HealthUpgrade += 5;
            MaxHitPoints = MaxHitPoints + 5;
            CurrentHealth += 5;
            CurrentHealth = CurrentHealth > MaxHitPoints ? MaxHitPoints : CurrentHealth;
            return true;
        }
        return false;
    }

    private static bool ApplyDash()
    {
        if (DashCDUpgrade < MAXDASH)
        {
            // Whittington
            PlayerPrefs.SetInt(GlobalConfigs.DashCooldownUpgrade, PlayerPrefs.GetInt(GlobalConfigs.DashCooldownUpgrade) + 1);
            // Whittington

            DashCDUpgrade += 1;
            return true;
        }
        return false;
    }
    private static bool ApplyDMG()
    {
        if (DamageUpgrade < MAXDMG)
        {
            // Whittington
            PlayerPrefs.SetInt(GlobalConfigs.DamageUpgrade, PlayerPrefs.GetInt(GlobalConfigs.DamageUpgrade) + 1);
            // Whittington

            DamageUpgrade += 1;
            return true;
        }
        return false;
    }

    public static bool ApplyBounce()
    {
        if (Bouncy < MAXBOUNCEY)
        {
            // Whittington
            PlayerPrefs.SetInt(GlobalConfigs.Bouncy, PlayerPrefs.GetInt(GlobalConfigs.Bouncy) + 1);
            // Whittington
            Bouncy += 1;
            return true;
        }
        return false;
    }

    //Call on scene exit
    public static void PlayerPrefSave()
    {
        PlayerPrefs.SetInt("MaxHealth", (int)MaxHitPoints);
        PlayerPrefs.SetInt("CurrentHealth", (int)CurrentHealth);
        PlayerPrefs.SetInt("Coins", (int)Coins);

        PlayerPrefs.SetInt(GlobalConfigs.Bouncy, (int)Bouncy);
        PlayerPrefs.SetInt(GlobalConfigs.HealthUpgrade, (int)HealthUpgrade);
        PlayerPrefs.SetInt(GlobalConfigs.DashCooldownUpgrade, (int)DashCDUpgrade);
        PlayerPrefs.SetInt(GlobalConfigs.DamageUpgrade, (int)DamageUpgrade);

        PlayerPrefs.SetInt("StoreVisited", 0);

        //PlayerPrefs.SetInt("Damage Size Upgrade", (int)DamageSizeUpgrade);
    }

    public static int GetUpgradeCost(int p_upgrade)
    {
        UPGRADE upgrade = (UPGRADE) p_upgrade;
        int multiplier = 0;

        switch (upgrade)
        {
            case UPGRADE.HEALTH:
                multiplier = PlayerPrefs.GetInt(GlobalConfigs.HealthUpgrade);
                break;
            case UPGRADE.DASH:
                multiplier = PlayerPrefs.GetInt(GlobalConfigs.DashCooldownUpgrade);
                break;
            case UPGRADE.DMG:
                multiplier = PlayerPrefs.GetInt(GlobalConfigs.DamageUpgrade);
                break;
            case UPGRADE.BOUNCY:
                multiplier = PlayerPrefs.GetInt(GlobalConfigs.Bouncy);
                break;
            default:
                break;
        }

        return 5 + (multiplier * 5);
    }
    //Call on scene exit
}