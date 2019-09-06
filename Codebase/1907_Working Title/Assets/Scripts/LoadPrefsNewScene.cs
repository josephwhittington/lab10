using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPrefsNewScene : MonoBehaviour
{
    void Start()
    {
        PlayerStats.LoadPlayerPrefs();
    }

}
