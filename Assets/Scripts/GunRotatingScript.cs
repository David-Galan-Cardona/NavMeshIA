using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotatingScript : MonoBehaviour
{
    private void Update()
    {
        //haz que el objeto este girando
        transform.Rotate(Vector3.back * Time.deltaTime * 100);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //destruye el objeto
            Destroy(gameObject);
            //añade el objeto al jugador
            other.GetComponent<Player>().hasGun = true;
            other.GetComponent<Player>().gun.SetActive(true);
            other.GetComponent<Player>().cameraSwitch.hasGun = true;
            if (other.GetComponent<Player>().cameraSwitch.firstPersonCamera.activeSelf)
            {
                other.GetComponent<Player>().gun.SetActive(true);
            }
        }
    }
}
