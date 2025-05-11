using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Player : HPInterface, PlayerControls.IPlayerActions
{
    private PlayerControls playerControls;
    public Animator animator;
    public int speed = 10;
    public int baseSpeed = 10;
    public bool isGrounded = false;
    public float ySpeed;
    public bool hasGun = false;
    public bool isDancing = false;
    public GameObject gun;
    public CameraSwitch cameraSwitch;
    public float FPCameraOffset = 1f;
    CharacterController controller;

    [Header("Bullets")]
    public GameObject prefabBullet;
    public Transform bulletSpawn;
    public float bulletSpeed = 10f;

    public void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.Player.SetCallbacks(this);
        }
        playerControls.Enable();
    }
    public void OnDisable()
    {
        playerControls.Disable();
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed && !isDancing)
        {
            speed = baseSpeed / 2;
            animator.SetBool("Crouch", true);
            cameraSwitch.firstPersonCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset += new Vector3(0, -FPCameraOffset, 0);
        }
        else if (context.canceled)
        {
            speed = baseSpeed;
            animator.SetBool("Crouch", false);
            cameraSwitch.firstPersonCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset += new Vector3(0, FPCameraOffset, 0);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && (speed == baseSpeed || speed == baseSpeed * 2) && !cameraSwitch.firstPersonCamera.activeSelf && !isDancing)
        {
            if (isGrounded)
            {
                ySpeed = 5;
            }
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed && speed == baseSpeed && !isDancing)
        {
            speed = baseSpeed * 2;
            animator.SetBool("Run", true);
        }
        else if (context.canceled || cameraSwitch.firstPersonCamera.activeSelf)
        {
            speed = baseSpeed;
            animator.SetBool("Run", false);
        }
    }

    public void OnDance(InputAction.CallbackContext context)
    {
        if (context.performed && !isDancing)
        {
            isDancing = true;
            animator.SetBool("Dancing", true);
        }
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed && hasGun && !isDancing && cameraSwitch.firstPersonCamera.activeSelf)
        {
            Pool.Instance.Get(bulletSpawn).transform.position = bulletSpawn.position + new Vector3(0, 1, 0);
        }
        
    }

    public override void TakeDamage(int damage)
    {
        HP -= damage;
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (cameraSwitch.firstPersonCamera.activeSelf)
        {
            speed = baseSpeed;
            animator.SetBool("Run", false);
        }
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            animator.SetBool("Jump", false);
        }
        else
        {
            animator.SetBool("Jump", true);
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical != 0 && !isDancing)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
        
        
        if (!isGrounded)
        {
            ySpeed -= 9.8f * Time.deltaTime;
            Vector3 move = new Vector3(horizontal * speed, ySpeed, vertical * speed);
            //mueve segun la rotacion del jugador
            move = transform.TransformDirection(move);
            controller.Move(move * Time.deltaTime);
        }
        else if (!isDancing)
        {
            Vector3 move = new Vector3(horizontal * speed, ySpeed, vertical * speed);
            //mueve segun la rotacion del jugador
            move = transform.TransformDirection(move);
            controller.Move(move * Time.deltaTime);
        }
    }
}
