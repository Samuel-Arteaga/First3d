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
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        cameraPosition = Camera.main.transform; // Asigna la transformación de la cámara a cameraPosition
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

        // Calcula la dirección en la que se mueve el jugador en relación con la cámara.
        Vector3 moveDirection = cameraPosition.forward * direction.y + cameraPosition.right * direction.x;
        moveDirection.y = 0f; // Asegura que no haya movimiento vertical.

        // Rota al jugador hacia la dirección de movimiento.
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Mueve al jugador.
        controller.Move(moveDirection * playerSpeed * Time.deltaTime);

        // Activa la animación de movimiento.
        float movementMagnitude = moveDirection.magnitude;
        anim.SetBool("Caminar", true);
    }
}
