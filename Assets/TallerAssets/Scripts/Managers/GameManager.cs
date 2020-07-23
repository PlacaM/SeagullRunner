using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Taller;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int coinScore;
    public TextMeshProUGUI coinScoreText;
    public bool gameOver;
    public GameObject canvasGameOver;
    public GameObject canvasStart;
    public bool start; 
    public int score;
    public int highScore;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI deadScoreText;
    public TextMeshProUGUI scoreText;
    public float timer = 0.1f;

    [Header("Level Config")]
    public int fases=0;
    public float playerSpeed=25f;

    [Header("Pasar de nivel")]
    public int SiguienteNivel;
    public bool Nopasa;

    [Header("Player lvl1")]
    public GameObject player;


    public void SetGameOver()
    {
        gameOver = true;
        canvasGameOver.SetActive(true);
    }

    public void DisableCanvas()
    {
        start = false;
        canvasStart.SetActive(false);
    }
    

    private Action updateCallback;


    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
        AddUpdateCallback(EventTouch.ExternalUpdate);

    }

    public void AddCoinScore(int NewCoinScore)
    {
        coinScore += NewCoinScore;
        coinScoreText.SetText(coinScore.ToString());
        PlayerPrefs.SetInt("Coin", coinScore);
    }

    public void AddUpdateCallback(Action updateMethod)
    {
        updateCallback += updateMethod;
    }


    public void NotifyPlayerDead()
    {
        print("player is dead");

    }

    //cambio de velocidad, eso crack
    public void CheckCHangeSpeed()
    {
        if(score>100 && fases == 0)
        {
            playerSpeed=35;
            fases++;
        }

        if (score > 200 && fases == 1)
        {
            playerSpeed = 55;
            fases++;
        }

        if (score > 400 && fases == 2)
        {
            playerSpeed = 75;
            fases++;
        }

        if (score > 600 && fases == 3)
        {
            playerSpeed = 85;
            fases++;
        }

    }

    
    private void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("highScore", 0).ToString();
        //sacar comentario de la linea de abajo para borrar highScore paso 1
        //PlayerPrefs.DeleteKey("highScore");
    }


    private void Update()
    {
        timer += Time.deltaTime;
        score= Mathf.RoundToInt            (timer * 6);
        score= Mathf.RoundToInt            (timer * 6);
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();

        CheckCHangeSpeed();

        //Score de Canvas Game Over

        deadScoreText.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt
            (timer * 6).ToString();

        if(score >= 800 && Nopasa==false)
        {
            player.SetActive(false);
            coinScore = PlayerPrefs.GetInt("Coin");
            SceneManager.LoadScene(SiguienteNivel);
        }
        else
        {
            if (Nopasa==true && coinScore <= 30)
            {
                coinScoreText.text = "Coin" + coinScore;
                SceneManager.LoadScene(SiguienteNivel);
            }
        }

        if (score > PlayerPrefs.GetInt("highScore", 0))
        {
            //Cambiar 2do highScore por score para que se guarde el highScore
            PlayerPrefs.SetInt("highScore", score);


        }

        if (GameManager.Instance.gameOver == true)
        {
            Time.timeScale = 0;
            DisableCanvas(); 
        }
        

        if (updateCallback != null)
            updateCallback();
    }


    public static class EventManager
    {
   // public static Dictionary<string, Action<object>> eventList;
    public static Dictionary<string, Delegate> delegateList;

     
    public static void AddEvent(string eventName, Delegate Param)
    {
        if (delegateList == null)
        {
            //Debug.LogError("eventList==null");
            delegateList = new Dictionary<string, Delegate>();

        }


        delegateList.Add(eventName, Param);

    }
     public static void AddEvent<T>(string eventName, Action<T> Param)
    {
        if (delegateList == null)
        {
            //Debug.LogError("eventList==null");
            delegateList = new Dictionary<string, Delegate>();

        }


        delegateList.Add(eventName, Param); 

    } 
    /*public static void AddEvent<T>(string eventName, Action<T> Param)
    {
        if(eventList==null)
        {
            //Debug.LogError("eventList==null");
            eventList = new Dictionary<string, Action<object>>();

        }

         
        eventList.Add(eventName, o => Param((T)o));

    }
    public static void TriggerEvent<T>(string eventName, T parameter1)
    {

        if (eventList.ContainsKey(eventName))
        {
            Action<object> eventTocall;

            if (eventList.TryGetValue(eventName, out eventTocall))
            {
                if (eventTocall == null) return;
                eventTocall(parameter1);

            }
        }

    }*/
    public static void TriggerEvent<T>(string eventName, T parameter1)
    {
        if(delegateList.ContainsKey(eventName))
        {
            Delegate eventTocall;

            if(delegateList.TryGetValue(eventName,out eventTocall))
            {
                if (eventTocall == null) return;

                eventTocall?.DynamicInvoke(parameter1);

            }
        }

    }


    }
}
