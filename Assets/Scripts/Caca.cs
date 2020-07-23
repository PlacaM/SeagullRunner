using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caca : MonoBehaviour
{
    public Rigidbody caca;
    // Start is called before the first frame update
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
