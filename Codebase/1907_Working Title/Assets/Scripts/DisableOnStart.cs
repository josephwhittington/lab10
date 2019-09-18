using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour
{
    [SerializeField] private GameObject ThingToDisable = null;
    void Start()
    {
        if(ThingToDisable)
            ThingToDisable.gameObject.SetActive(false);
    }
}
