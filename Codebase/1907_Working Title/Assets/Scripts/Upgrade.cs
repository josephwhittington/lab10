using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Upgrade : MonoBehaviour
{
    [SerializeField] Slider upgradeSlider = null;
    
    public void OnPurchase()
    {
        upgradeSlider.value += 1;
    }

    


}
