using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Player : HPInterface
{
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
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            animator.SetBool("Jump", false);
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
        if (Input.GetKey(KeyCode.C) && !cameraSwitch.firstPersonCamera.activeSelf && isGrounded && speed != speed/2 && !isDancing)
        {
            isDancing = true;
            speed = baseSpeed;
            animator.SetBool("Dancing", true);
            //llama a una corrutina para que el jugador deje de bailar cuando la animacion termine
            //StartCoroutine(StopDancing());
        }
        if (Input.GetKey(KeyCode.LeftControl) && speed==baseSpeed && !isDancing)
        {
            speed = baseSpeed*2;
            animator.SetBool("Run", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) || cameraSwitch.firstPersonCamera.activeSelf)
        {
            speed = baseSpeed;
            animator.SetBool("Run", false);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDancing)
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
        if (Input.GetKeyDown(KeyCode.Space) && (speed==baseSpeed || speed==baseSpeed*2) && !cameraSwitch.firstPersonCamera.activeSelf && !isDancing)
        {
            if (isGrounded)
            {
                animator.SetTrigger("Jump");
                ySpeed = 5;
            }
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
    /*private IEnumerator StopDancing()
    {
        // Espera a que la animación de baile termine
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("Dancing", false);
        isDancing = false;
    }*/
}
