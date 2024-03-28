using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [Header("------------ Audio Source --------------")]
    private AudioSource audioSource;
    public List<AudioClip> generals;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        Instance = this;
    }
    void Start()
    {
        audioSource.clip = generals[3]; 
        audioSource.loop = true; 
        audioSource.volume = 0.1f;
        audioSource.Play();
    }
    public void collectObjects()
    {
        audioSource.PlayOneShot(generals[0]);
    }
    public void Disparo()
    {
        audioSource.PlayOneShot(generals[1]);
    }
    public void Guardado()
    {
        audioSource.PlayOneShot(generals[2]);
    }
    public void Morir()
    {
        audioSource.PlayOneShot(generals[4]);
    }
}
