using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovePersonas : MonoBehaviour
{
    public float velocidad;
    
    void Move()
    {
        transform.position += Vector3.back * velocidad * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Caca"))
        {
            velocidad=30;
        }
    }

    void Update()
    {
        Move();
    }
}
