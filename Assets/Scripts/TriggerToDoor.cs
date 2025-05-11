using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToDoor : MonoBehaviour
{
    public Transform Door;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Rota el padre lentamente
            StartCoroutine(CloseDoor());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Rota el padre lentamente
            StartCoroutine(OpenDoor());
        }
    }

    public IEnumerator CloseDoor()
    {
        //Rota el padre lentamente
        float rotationSpeed = 100f;
        float targetRotation = 180f;
        float currentRotation = 90f;
        while (currentRotation < targetRotation)
        {
            currentRotation += rotationSpeed * Time.deltaTime;
            Door.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator OpenDoor()
    {
        //Rota el padre lentamente
        float rotationSpeed = 100f;
        float targetRotation = 90f;
        float currentRotation = 180f;
        while (currentRotation > targetRotation)
        {
            currentRotation -= rotationSpeed * Time.deltaTime;
            Door.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
