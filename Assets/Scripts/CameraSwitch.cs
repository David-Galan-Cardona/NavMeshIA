using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject globalCamera;
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;
    public GameObject FPGun;
    public bool hasGun = false;
    public Player player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && player.isGrounded)
        {
            globalCamera.SetActive(false);
            firstPersonCamera.SetActive(true);
            thirdPersonCamera.SetActive(false);
            StartCoroutine(ActivateGun());
        }
        if (Input.GetMouseButtonUp(1))
        {
            globalCamera.SetActive(false);
            firstPersonCamera.SetActive(false);
            thirdPersonCamera.SetActive(true);
            FPGun.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            globalCamera.SetActive(!globalCamera.activeSelf);
            firstPersonCamera.SetActive(false);
            thirdPersonCamera.SetActive(!globalCamera.activeSelf);
        }
    }
    public IEnumerator ActivateGun()
    {
        yield return new WaitForSeconds(0.18f);
        if (firstPersonCamera.activeSelf && hasGun)
        {
            FPGun.SetActive(true);
        }
        else
        {
            FPGun.SetActive(false);
        }
    }
}
