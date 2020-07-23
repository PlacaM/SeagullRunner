//no estamos reuitilizando el mismo script de primero xD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCarril : MonoBehaviour
{
    public GameObject gaviota;
    public float distanciaMov;
    public List<Transform> carriles;
    public int carrilInicial;
    private int carrilActual;


    private bool lastX;

    private void Start()
    {
        carrilActual = carrilInicial;
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            lastX = false;
        }

        if (!lastX)
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                MoverIzquierda();
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                MoverDerecha();
            }
        }
        //Disparar con boton B
        if (Input.GetButtonDown("Fire1"))
        {

        }
    }

    public void MoverDerecha()
    {

        carrilActual += 1;

        if (carrilActual >= carriles.Count)
        {
            carrilActual = carriles.Count - 1;
        }

        // mover a la deracha 
        print("derecha, carril: " + carrilActual);

        MoverGaviota();


    }

    public void MoverIzquierda()
    {

        carrilActual -= 1;

        if (carrilActual < 0)
        {
            carrilActual = 0;
        }

        // mover a la deracha 
        print("izquierda, carril: " + carrilActual);
        MoverGaviota();

    }

    public void MoverGaviota()
    {


        gaviota.transform.position = carriles[carrilActual].position;

        lastX = true;
    }


}

