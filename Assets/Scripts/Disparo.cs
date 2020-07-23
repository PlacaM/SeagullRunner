using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public GameObject Caca;
    public float cacaSpeed ;
    public Transform posCaca;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
          GameObject bullet = Instantiate(Caca, transform.position, Quaternion.identity);
            
        }
    }
}
