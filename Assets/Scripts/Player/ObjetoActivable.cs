using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoActivable : MonoBehaviour, IActivable
{
    public GameObject objetoMap;
    public GameObject objetoPlayer;
    public GameObject image;

    private void Start()
    {
        objetoMap.SetActive(true);
        objetoPlayer.SetActive(false);
        image.SetActive(false);
    }
    public void ActivateObject()
    {
        objetoMap.SetActive(false);
        image.SetActive(true);
        objetoPlayer.SetActive(true);
    }
}
