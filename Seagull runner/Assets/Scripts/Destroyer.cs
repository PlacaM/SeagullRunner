using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Obstaculo"))
        {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Deco"))
        {
            Destroy(other.gameObject);
        }
    }

}
