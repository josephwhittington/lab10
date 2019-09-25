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
    [SerializeField] private Text FireRateText = null;
    [SerializeField] private Text MultishotText = null;
    // Text

    // Sliders
    [SerializeField] private Slider CooldownSlider = null;
    [SerializeField] private Slider HealthSlider = null;
    [SerializeField] private Slider DamageSlider = null;
    [SerializeField] private Slider BounceSlider = null;
    [SerializeField] private Slider FireRateSlider = null;
    [SerializeField] private Slider MultishotSlider = null;
    // Sliders
    [SerializeField] GameObject store = null;
    // Upgrade Levels
    private int DashCoolDown = 0;
    private int Health = 0;
    private int Damage = 0;
    private int Bounce = 0;
    private int Multishot = 0;
    private float FireRate = 0.0f;
    // Upgrade Levels

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (store.activeSelf)
                store.SetActive(false);
        }

        // Check for value updates
        if (PlayerPrefs.HasKey(GlobalConfigs.DashCooldownUpgrade))
            DashCoolDown = PlayerPrefs.GetInt(GlobalConfigs.DashCooldownUpgrade);
        if (PlayerPrefs.HasKey(GlobalConfigs.HealthUpgrade))
            Health = PlayerPrefs.GetInt(GlobalConfigs.HealthUpgrade);
        if (PlayerPrefs.HasKey(GlobalConfigs.DamageUpgrade))
            Damage = PlayerPrefs.GetInt(GlobalConfigs.DamageUpgrade);
        if (PlayerPrefs.HasKey(GlobalConfigs.Bouncy))
            Bounce = PlayerPrefs.GetInt(GlobalConfigs.Bouncy);

        if (PlayerPrefs.HasKey(GlobalConfigs.FireRate))
            FireRate = PlayerPrefs.GetFloat(GlobalConfigs.FireRate);

        if (PlayerPrefs.HasKey(GlobalConfigs.MultiShot))
            Multishot = PlayerPrefs.GetInt(GlobalConfigs.MultiShot);
        // Check for value updates

        // Set sliders
        CooldownSlider.value = DashCoolDown;
        HealthSlider.value = Health;
        DamageSlider.value = Damage;
        BounceSlider.value = Bounce;
        FireRateSlider.value = FireRate;
        MultishotSlider.value = Multishot;
        // Set sliders
        
        // Update Cost Text
        CooldownCostText.text = PlayerStats.GetUpgradeCost(1).ToString();
        HealthCostText.text = PlayerStats.GetUpgradeCost(0).ToString();
        if(Damage == PlayerStats.MAXDMG - 1 || Damage == PlayerStats.MAXDMG)
        {
            DamageCostText.text = "500";
        }
        else
            DamageCostText.text = PlayerStats.GetUpgradeCost(2).ToString();

        BounceCostText.text = PlayerStats.GetUpgradeCost(3).ToString();
        FireRateText.text = PlayerStats.GetUpgradeCost(4).ToString();
        MultishotText.text = PlayerStats.GetUpgradeCost(5).ToString();
        // Update Cost Text
    }
}
