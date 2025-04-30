using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Player : HPInterface
{
    private Rigidbody _rb;
    public Animator animator;
    public int speed = 10;
    public int baseSpeed = 10;
    public bool isGrounded = false;
    public float ySpeed;
    public bool hasGun = false;
    public GameObject gun;
    public CameraSwitch cameraSwitch;
    public float FPCameraOffset = 1f;
    CharacterController controller;

    public override void TakeDamage(int damage)
    {
        HP -= damage;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        isGrounded = controller.isGrounded;
        animator.SetBool("Jump", false);

        // mueve al jugador sin utilizar moveposition y que cambie segun su rotacion
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Vector3 move = transform.right * horizontal + transform.forward * vertical;
        /*_rb.velocity = new Vector3(move.x * speed, _rb.velocity.y, move.z * speed);*/
        

        //mira si se esta moviendo
        if (horizontal != 0 || vertical != 0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
        if (Input.GetKey(KeyCode.LeftControl) && speed==baseSpeed)
        {
            speed = baseSpeed*2;
            animator.SetBool("Run", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) || cameraSwitch.firstPersonCamera.activeSelf)
        {
            speed = baseSpeed;
            animator.SetBool("Run", false);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = baseSpeed / 2;
            animator.SetBool("Crouch", true);
            cameraSwitch.firstPersonCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset += new Vector3(0, -FPCameraOffset, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = baseSpeed;
            animator.SetBool("Crouch", false);
            cameraSwitch.firstPersonCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset += new Vector3(0, FPCameraOffset, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && (speed==baseSpeed || speed==baseSpeed*2) && !cameraSwitch.firstPersonCamera.activeSelf)
        {
            if (isGrounded)
            {
                animator.SetTrigger("Jump");
                ySpeed = 5;
            }
        }
        if (!isGrounded)
        {
            //no va

            ySpeed -= 9.8f * Time.deltaTime;
            Vector3 move = new Vector3(horizontal * speed, ySpeed, vertical * speed);
            //mueve segun la rotacion del jugador
            move = transform.TransformDirection(move);
            controller.Move(move * Time.deltaTime);
        }
        else
        {
            Vector3 move = new Vector3(horizontal * speed, ySpeed, vertical * speed);
            //mueve segun la rotacion del jugador
            move = transform.TransformDirection(move);
            controller.Move(move * Time.deltaTime);
        }
    }
}
