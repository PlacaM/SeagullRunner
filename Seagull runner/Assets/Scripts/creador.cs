using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creador : MonoBehaviour
{
    public GameObject[] objetos;
    public Vector3 posicion1;
    public float cadencia;
    public bool escalado;
    private float tiempoCadencia;
    private float timer;
    public int score;
    private int fases = 0;

    void Start()
    {
        posicion1 = transform.position;
    }

    void gen()
    {
        Instantiate(objetos[Random.Range(0, objetos.Length)],posicion1, Quaternion.identity);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        tiempoCadencia += Time.deltaTime;

        score =+ Mathf.RoundToInt(tiempoCadencia*6);

        if (escalado == true)
        {
            if (score > 100 && fases == 0)
            {
                cadencia = 2f;
                fases++;
            }

            if (score > 200 && fases == 1)
            {
                cadencia = 1.8f;
                fases++;
            }

            if (score > 400 && fases == 2)
            {
                cadencia = 1.4f;
                fases++;
            }

            if (score > 600 && fases == 3)
            {
                cadencia = 1f;
                fases++;
            }

        }

        if (timer >= cadencia)
        {
            gen();
            timer = 0;
        }
    }
}

   