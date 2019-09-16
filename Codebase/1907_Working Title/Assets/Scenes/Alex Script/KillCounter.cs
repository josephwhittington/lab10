using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class KillCounter : MonoBehaviour
{
    public static KillCounter instance;
    [SerializeField] TextMeshProUGUI killcounter = null;
    [SerializeField] GameObject SpecialMove;
    [SerializeField] Transform player;
    public int count;

    void Start()
    {
        player = GetComponent<Transform>();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

    }

    public void UpdateCounter()
    {
        killcounter.text = count.ToString();

        if (count == 2)
        {
            Debug.Log("SpeCialMOVESSSSSS BITCHHHH");
            //Instantiate<GameObject>(SpecialMove, player.transform.position, player.transform.rotation);

            //inside the effect make a check, that has a nav mesh agent and for all tags found with enemy
            //destroy those game objects
            //sick moves bro
        }
    }

    //void CheckKillStreak()
    //{
    //    if (KillCounter.instance.count == 2)
    //    {
    //        foreach (GameObject i in enemies)
    //        {
    //            Destroy(i);
    //        }
    //    }
    //}

}
