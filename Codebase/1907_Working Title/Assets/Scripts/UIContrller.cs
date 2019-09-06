using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContrller : MonoBehaviour
{
    [SerializeField] Text CoinText = null;
    [SerializeField] Image HealthBar = null;
    [SerializeField] Image Store = null;

    void Start()
    {
        GetComponent<Canvas>().enabled = true;

        // Disable store
        Store.gameObject.SetActive(false);
    }

    void Update()
    {
        CoinText.text = PlayerStats.Coins.ToString();

        if (!PlayerStats.PlayerDead)
            HealthBar.fillAmount = (float) ((float) PlayerStats.CurrentHealth / (float) PlayerStats.MaxHitPoints);
        else HealthBar.fillAmount = 0;
    }
}
