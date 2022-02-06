using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    Transform player;
    public GameObject shopGroup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            player = collision.transform;
            StartShop();
        }
    }

    void StartShop()
    {
        shopGroup.SetActive(true);


    }
}
