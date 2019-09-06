using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StoreCostTextUpdater : MonoBehaviour
{
    // Text
    [SerializeField] private Text CooldownCostText = null;
    [SerializeField] private Text HealthCostText = null;
    [SerializeField] private Text DamageCostText = null;
    [SerializeField] private Text BounceCostText = null;
    // Text

    // Sliders
    [SerializeField] private Slider CooldownSlider = null;
    [SerializeField] private Slider HealthSlider = null;
    [SerializeField] private Slider DamageSlider = null;
    [SerializeField] private Slider BounceSlider = null;
    // Sliders

    // Upgrade Levels
    private int DashCoolDown = 0;
    private int Health = 0;
    private int Damage = 0;
    private int Bounce = 0;
    // Upgrade Levels

    // Update is called once per frame
    void Update()
    {
        // Check for value updates
        if (PlayerPrefs.HasKey(GlobalConfigs.DashCooldownUpgrade))
            DashCoolDown = PlayerPrefs.GetInt(GlobalConfigs.DashCooldownUpgrade);
        if (PlayerPrefs.HasKey(GlobalConfigs.HealthUpgrade))
            Health = PlayerPrefs.GetInt(GlobalConfigs.HealthUpgrade);
        if (PlayerPrefs.HasKey(GlobalConfigs.DamageUpgrade))
            Damage = PlayerPrefs.GetInt(GlobalConfigs.DamageUpgrade);
        if (PlayerPrefs.HasKey(GlobalConfigs.Bouncy))
            Bounce = PlayerPrefs.GetInt(GlobalConfigs.Bouncy);
        // Check for value updates

        // Set sliders
        CooldownSlider.value = DashCoolDown;
        HealthSlider.value = Health;
        DamageSlider.value = Damage;
        BounceSlider.value = Bounce;
        // Set sliders
        
        // Update Cost Text
        CooldownCostText.text = PlayerStats.GetUpgradeCost(1).ToString();
        HealthCostText.text = PlayerStats.GetUpgradeCost(0).ToString();
        DamageCostText.text = PlayerStats.GetUpgradeCost(2).ToString();
        BounceCostText.text = PlayerStats.GetUpgradeCost(3).ToString();
        // Update Cost Text
    }
}
