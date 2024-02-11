using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShoot : MonoBehaviour
{
    [SerializeField]private CinemachineVirtualCamera aimVirtualCamera;
    private Player player;

    // Start is called before the first frame update
    private void Awake()
    {
        player= GetComponent<Player>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
        }
    }
}
