using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    [Header("BATE DE PINCHOS")]
    public GameObject bateMap;
    public GameObject batePlayer;

    //[Header("MASCARA DE SAMU")]
    //public GameObject mascaraMap;
    //public GameObject mascaraPlayer;

    [Header("CASCO DE SAMU")]
    public GameObject cascoMap;
    public GameObject cascoPlayer;

    void Start()
    {
        batePlayer.SetActive(false);
        cascoPlayer.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bate"))
        {
            bateMap.SetActive(false);
            batePlayer.SetActive(true);
        }
        //else if (other.gameObject.CompareTag("mascara"))
        //{
        //    mascaraMap.SetActive(false);
        //    mascaraPlayer.SetActive(true);
        //}
        else if (other.gameObject.CompareTag("casco"))
        {
            cascoMap.SetActive(false);
            cascoPlayer.SetActive(true);
        }
    }
}
