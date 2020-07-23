using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using Taller;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    
    
    private Rigidbody rb;
    public int currentRail = 1;
    public float moveDistance = 2;
    public float moveTime = .25f;
    public GameObject GaviBase;
    public GameObject GaviVolando;
    private bool salto;
    public bool Aire=false;
    public AudioSource audioSource;


    [Header("Jump settings")]
    public Vector3 endValue;

    public float jumpTime = 0.5f;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        EventTouch.OnSwipeFinished += OnSwipeFinished;

        GaviBase.SetActive(true);
        GaviVolando.SetActive(false);
        salto=true;

        if(Aire==true)
        {
            GaviBase.SetActive(false);
            GaviVolando.SetActive(true);
        }

    }

    public void OnSwipeFinished(TouchResult touchEventResult)
    {
        if(salto==true)
        {
            switch (touchEventResult.direction4Way)
            {
                case EDirections.LEFT:
                    MoveCharacter(-1);
                    break;

                case EDirections.RIGHT:
                    MoveCharacter(1);
                    break;

                case EDirections.UP:
                    Jump();
                    break;
            }
        }
        
    }

    private void MoveCharacter(float xDir)
    {
        if (xDir > 0 && currentRail == 2) return;
        if (xDir < 0 && currentRail == 0) return;

        Vector3 finalPos = transform.position + Vector3.right * xDir * moveDistance;
        rb.DOMove(finalPos, moveTime);
        if (xDir > 0) currentRail++;
        if (xDir < 0) currentRail--;

        DontDestroyOnLoad(transform.gameObject);
    }

    

    private void Jump()
    {
        if(salto==true)
        {
            Vector3 finalPos = transform.position + endValue;

            rb.DOMove(finalPos, jumpTime).SetLoops(2, LoopType.Yoyo);

            GaviBase.SetActive(false);
            GaviVolando.SetActive(true);
            salto = false;
            
            void Sonido(AudioClip clip)
            {
                audioSource.PlayOneShot(clip);
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            GameManager.Instance.AddCoinScore(1);
            Destroy(other.gameObject);

        }

        if (other.CompareTag("Obstaculo"))
        {
            GameManager.Instance.SetGameOver();
           
            print("muerto");
            DontDestroyOnLoad(transform.gameObject);
        }

        if (other.CompareTag("Suelo") && Aire == false)
        {
            salto = true;
            GaviBase.SetActive(true);
            GaviVolando.SetActive(false);
        }

        else
        {
            salto = true;
            GaviVolando.SetActive(true);
        }
        
    }

    private void Update()
    {
        if (GameManager.Instance.gameOver == true)
        {
            Time.timeScale = 0;
            
        }
    }

}
