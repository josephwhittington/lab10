using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCoolDownUIUpdater : MonoBehaviour
{
    [SerializeField] RawImage DashCooldownUI = null;

    // Update is called once per frame
    void Update()
    {
        PlayerController Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        float alpha = 1;

        if (Player && !Player.PlayerCanDash())
        {
            alpha = 0.3f;
        }

        DashCooldownUI.color = new Vector4(DashCooldownUI.color.r, DashCooldownUI.color.g, DashCooldownUI.color.b, alpha);
    }
}
