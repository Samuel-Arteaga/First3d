using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 3.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float rotationSpeed = 5.0f;
    private bool _isGrounded;
    private bool _jumpPressed;
    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction runAction;
    private InputAction jumpAction;
    private InputAction hipHopAction;
    private Animator anim;
    private CharacterController controller;
    private Transform cameraPosition;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        runAction = playerInput.actions.FindAction("Run");
        jumpAction = playerInput.actions.FindAction("Jump");
        hipHopAction = playerInput.actions.FindAction("Dance");
        cameraPosition = Camera.main.transform;
    }

    void Start()
    {
   
        hipHop();
    }

    void Update()
    {
        Movement();
        Jump();
        ActivateAnimations();
    }

    private void Movement()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = cameraPosition.forward * direction.y + cameraPosition.right * direction.x;
        moveDirection.y = 0f;

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        controller.Move(moveDirection * playerSpeed * Time.deltaTime);
    }

    private void ActivateAnimations()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        float runInput = runAction.ReadValue<float>();

        Vector3 moveDirection = cameraPosition.forward * direction.y + cameraPosition.right * direction.x;
        moveDirection.y = 0f;

        float movementMagnitude = moveDirection.magnitude;

        // Activar animaciones según las condiciones
        anim.SetBool("Caminar", movementMagnitude > 0 && runInput <= 0.5f);
        anim.SetBool("idle", movementMagnitude == 0);
        anim.SetBool("Run", runInput > 0.5f);
    }

    private void Jump()
    {
        //_isGrounded = controller.isGrounded;
        float jumpInput = jumpAction.ReadValue<float>();

        // Si el jugador está en el suelo y se pulsa la tecla de salto
        if (jumpInput != 0 && !_jumpPressed)
        {
            // Aplicar fuerza vertical para el salto
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);

            // Activar la animación de salto
            anim.SetBool("Jump", true);

            // Indicar que el salto ha sido activado
            _jumpPressed = true;
        }

        // Si el jugador no está presionando la tecla de salto, se restablece _jumpPressed
        if (jumpInput == 0)
        {
            _jumpPressed = false;
        }

        // Aplicar gravedad
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void hipHop()
    {
        float danceInput = hipHopAction.ReadValue<float>();

        anim.SetFloat("HipHop", danceInput);
        //Debug.Log(danceInput);


    }



}
