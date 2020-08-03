using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Disparo : MonoBehaviour
{
    public static int remainingCoin;
    public TextMeshProUGUI remainCoinText;
    public GameObject Caca;
    public float cacaSpeed ;
    public Transform posCaca;


    

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
          remainingCoin -=1; 
          remainCoinText.SetText(remainingCoin.ToString());
          GameObject bullet = Instantiate(Caca, transform.position, Quaternion.identity);
            
        }
    }
}
