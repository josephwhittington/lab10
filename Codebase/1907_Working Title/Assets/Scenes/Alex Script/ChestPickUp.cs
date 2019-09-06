using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestPickUp : MonoBehaviour
{
    //This is the text that will pop up in UI after collision
    [SerializeField]
    private Text OpenChest = null;

    // bool to see if open is allowed
    private bool check;

    // Start is called before the first frame update
    void Start()
    {
        OpenChest.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (check && Input.GetButtonDown("Enteract"))
            Open();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            OpenChest.gameObject.SetActive(true);
            check = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            OpenChest.gameObject.SetActive(false);
            check = false;
        }
    }

    void Open()
    {
        Destroy(gameObject);
        OpenChest.gameObject.SetActive(false);
    }
}
