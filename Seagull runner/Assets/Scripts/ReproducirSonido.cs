using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproducirSonido : MonoBehaviour
{
    public AudioSource audioSource;
    public void Sonido(AudioClip clip)
    {
        
        audioSource.PlayOneShot(clip);
    }
    

}
