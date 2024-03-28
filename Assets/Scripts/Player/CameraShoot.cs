using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraShoot : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float rotationSpeed = 0.25f;
    private Player player;
    private Transform cameraPosition;
    public GameObject mira;

    // Start is called before the first frame update
    private void Awake()
    {
        player= GetComponent<Player>();
        cameraPosition = Camera.main.transform;

    }
    void Start()
    {
        mira.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        if (player.Apuntar)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            mira.SetActive(true);


            //Rotacion de la camara primera persona
            float mouseX = Mouse.current.delta.x.ReadValue() * rotationSpeed;
            float mouseY = Mouse.current.delta.y.ReadValue() * rotationSpeed;
            Debug.Log("X"+mouseX+" "+mouseY);
            transform.Rotate(Vector3.up, mouseX);
            cameraPosition.Rotate(Vector3.left, mouseY);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
        }
    }
}
