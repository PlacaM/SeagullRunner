using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caca : MonoBehaviour
{
    public Rigidbody caca;

    //pa referenciar al player
    private GameObject playerReference = GameObject.FindGameObjectWithTag("Player");

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Suelo") || (other.CompareTag("Obstaculo")))
        {
            Destroy(gameObject);
        }
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Instantiate(caca, transform.position, Quaternion.identity);
        }
    }

}
