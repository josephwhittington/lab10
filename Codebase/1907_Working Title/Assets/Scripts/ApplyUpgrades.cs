using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyUpgrades : MonoBehaviour
{
    private uint UpgradeCost = 1;

    [SerializeField] PlayerStats.UPGRADE UpgradeName = 0;
    [SerializeField] Button UpgradeButton = null;

    private void Start()
    {
        UpgradeButton.onClick.AddListener(PurchaseUpgrade);
    }

    public void PurchaseUpgrade()
    {
        UpgradeCost = (uint) PlayerStats.GetUpgradeCost((int)UpgradeName);
        if (UpgradeName == PlayerStats.UPGRADE.DMG && PlayerPrefs.GetInt(GlobalConfigs.DamageUpgrade) == PlayerStats.MAXDMG - 1)
            UpgradeCost = 500;
        if (PlayerStats.Coins >= UpgradeCost)
        {
            if (PlayerStats.ApplyUpgrade(UpgradeName))
            {
                if((int)UpgradeName == 1)
#if DEBUG
                    Debug.Log("Pu works");
#endif
                PlayerPrefs.Save();
                PlayerStats.Coins -= UpgradeCost;
            }
        }
    }
}
