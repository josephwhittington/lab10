using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePricing : MonoBehaviour
{
    [SerializeField] uint UpgradeCost = 0;
    [SerializeField] Button UpgradeButton = null;

    private void Start()
    {
        if(UpgradeButton)
            UpgradeButton.onClick.AddListener(PurchaseUpgrade);
    }

    private void PurchaseUpgrade()
    {
        if(PlayerStats.Coins >= UpgradeCost)
        {
            PlayerStats.Coins -= UpgradeCost;
        }
    }

}
