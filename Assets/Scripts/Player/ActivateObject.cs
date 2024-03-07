using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    [Header("BATE DE PINCHOS")]
    public GameObject bateMap;
    public GameObject batePlayer;

    [Header("MASCARA DE SAMU")]
    public GameObject mascaraMap;
    public GameObject mascaraPlayer;

    [Header("CHALECO DE SAMU")]
    public GameObject chalecoMap;
    public GameObject chalecoPlayer;

    void Start()
    {
        batePlayer.SetActive(false);
        chalecoPlayer.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bate"))
        {
            bateMap.SetActive(false);
            batePlayer.SetActive(true);
        }
        else if (other.gameObject.CompareTag("mascara"))
        {
            mascaraMap.SetActive(false);
            mascaraPlayer.SetActive(true);
        }
        else if (other.gameObject.CompareTag("chaleco"))
        {
            chalecoMap.SetActive(false);
            chalecoPlayer.SetActive(true);
        }
    }
}
