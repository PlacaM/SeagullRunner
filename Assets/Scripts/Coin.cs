using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public int coinScore = 1;
    public TextMeshProUGUI coinText;


    private void Start()
    {
        coinScore= PlayerPrefs.GetInt("Coin");
    }

    private void Update()
    {
        
    }

    public void SetCoin(int value)
    {
        coinScore += value;
        coinText.SetText(value.ToString());
        
    }
    public int getCoin()
    {
        return coinScore;
    }
    void OnGUI()
    {
        GUILayout.Label("Coin" + coinScore.ToString());
    }
}
