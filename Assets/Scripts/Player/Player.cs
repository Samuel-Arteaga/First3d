using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 0.5f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float rotationSpeed = 0.5f;
    private bool _jumpPressed; 
    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;

    //ACTIONS
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction runAction;
    private InputAction jumpAction;
    private InputAction hipHopAction;
    private InputAction dieAction;
    private InputAction bendAction;
    private InputAction shootAction;
    private InputAction apuntarAction;
    private Animator anim;
    public bool Apuntar;
    private CharacterController controller;

    //ARMA
    public Transform positionArma;
    public Transform rotationArma;
    public float velocidadBala = 4.0f;
    private float fuerzaBala = 1000f;


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
        dieAction = playerInput.actions.FindAction("Die");
        bendAction = playerInput.actions.FindAction("Bend");
        apuntarAction = playerInput.actions.FindAction("Apuntar");
        shootAction = playerInput.actions.FindAction("Disparar");
        StartCoroutine(ShootCoroutine());
        cameraPosition = Camera.main.transform;   
    }

    void Update()
    {
        Movement();
        ActivateAnimations();
    }
    public void OnApuntar(InputValue value)
    {
        ApuntarInput(value.isPressed);
    }
    public void ApuntarInput(bool newAimState) 
    {
        Apuntar = newAimState;
    }
    private void Movement()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = cameraPosition.forward * direction.y + cameraPosition.right * direction.x;
        moveDirection.y = 0.5f;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.Euler(0, cameraPosition.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        controller.Move(moveDirection * playerSpeed * Time.deltaTime);
    }

    private void ActivateAnimations()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        float runInput = runAction.ReadValue<float>();
        float danceInput = hipHopAction.ReadValue<float>();
        float jumpInput = jumpAction.ReadValue<float>();
        float dieInput = dieAction.ReadValue<float>();
        float bendWalkInput = bendAction.ReadValue<float>();
        float shootInput = shootAction.ReadValue<float>();
        float apuntarInput = apuntarAction.ReadValue<float>();

        Vector3 moveDirection = cameraPosition.forward * direction.y + cameraPosition.right * direction.x;
        moveDirection.y = 0f;

        float movementMagnitude = moveDirection.magnitude;

        // Activar animaciones según las condiciones
        anim.SetBool("Caminar", movementMagnitude > 0);
        anim.SetBool("idle", movementMagnitude == 0);
        anim.SetBool("Run", runInput == 1);
        anim.SetBool("HipHop", danceInput == 1);
        anim.SetBool("Jump", jumpInput == 1);
        anim.SetBool("Die", dieInput == 1);
        anim.SetBool("caminarAbajo", bendWalkInput == 1);
        anim.SetBool("Disparar", shootInput == 1);
        anim.SetBool("Apuntar", apuntarInput == 1);

        //IDLE
        if (direction.x == 0 && direction.y == 0)
        {
            anim.SetBool("idle", true);
            anim.SetBool("Run", false);

        }
        else anim.SetBool("idle", false);

        //CAMINAR ATRAS
        if (direction.y < 0f)
        {
            anim.SetBool("Caminar", false);
            anim.SetBool("CaminarAtras", true);
            
        }
        else
        {
            anim.SetBool("CaminarAtras", false);
        }

        //CAMBIAR DE CAMINAR A RUN
        if (direction.y > 0f) 
        {
            anim.SetBool("Caminar", true);
            if(runInput == 1)
            {
                anim.SetBool("Run", true);
                anim.SetBool("Caminar", false);

            }
        }
        //AGACHARSE CAMINANDO
        if (bendWalkInput == 1)
        {
            anim.SetBool("idle", false);
        }

        //BAILE
        if (danceInput == 1)
        {
            anim.SetBool("idle", false);
        }

        //SALTO
        if (jumpInput == 1 && !_jumpPressed)
        {
            anim.SetBool("idle", false);
            anim.SetBool("Caminar", false);
            anim.SetBool("Run", false);
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
            _jumpPressed = true;
        }

        // Si el jugador no está presionando la tecla de salto, se restablece
        if (jumpInput == 0)
        {
            _jumpPressed = false;
        }
        // Aplico gravedad
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //MORIR
        if(dieInput == 1)
        {
            anim.SetBool("idle", false);
            anim.SetBool("Caminar", false);
            anim.SetBool("Run", false);
        }
        //DISPARAR
        if (shootInput == 1)
        {
            Shoot();
        }

    }
    private IEnumerator ShootCoroutine()
    {

        while (true)
        {
            
            Shoot();
            yield return new WaitForSeconds(4f);
        }
    }
    private void Shoot()
    {
        GameObject bullet = BulletPool.Instance.GetBullet(); 
      
        if (bullet != null)
        {
            bullet.transform.position = positionArma.position;
            bullet.transform.rotation = transform.rotation;
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                // Aplicar velocidad inicial en la dirección del cañón del arma
                bulletRigidbody.AddForce(rotationArma.forward * fuerzaBala, ForceMode.VelocityChange);
            }

            bullet.SetActive(true);
            StartCoroutine(DisableBullet(bullet));
        }
    }
    private IEnumerator DisableBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(8f);
        bullet.SetActive(false);
    }


}
