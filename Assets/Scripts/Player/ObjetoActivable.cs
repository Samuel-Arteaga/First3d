using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoActivable : MonoBehaviour, IActivable
{
    public GameObject objetoMap;
    public GameObject objetoPlayer;

    private void Start()
    {
        objetoMap.SetActive(true);
        objetoPlayer.SetActive(false);
    }
    public void ActivateObject()
    {
        objetoMap.SetActive(false);
        objetoPlayer.SetActive(true);
    }
}
