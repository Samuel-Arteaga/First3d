using System.Collections;
using UnityEngine;
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

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction runAction;
    private InputAction jumpAction;
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
        //jumpAction = playerInput.actions.FindAction("Jump");
        cameraPosition = Camera.main.transform;
    }

    void Start()
    {
   
    }

    void Update()
    {
        Movement();
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

}
