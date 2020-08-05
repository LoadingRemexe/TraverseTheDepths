using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPocket : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerMove>().RefillAirSupply();
        }
    }
}
