using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 3.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float rotationSpeed = 0.5f;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private Animator anim;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Transform cameraPosition;

    // Start is called before the first frame update

    private void Awake()
    {
       
    }
    void Start()
    {
        playerInput=GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y) * playerSpeed * Time.deltaTime;
    }
}
