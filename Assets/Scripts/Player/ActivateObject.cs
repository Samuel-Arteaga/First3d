using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public GameObject bateMap;
    public GameObject batePlayer;
    // Start is called before the first frame update
    void Start()
    {
        batePlayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bate"))
        {
            Debug.Log("Lo ha tomado");
            bateMap.SetActive(false);
            batePlayer.SetActive(true);
        }
    }
}
