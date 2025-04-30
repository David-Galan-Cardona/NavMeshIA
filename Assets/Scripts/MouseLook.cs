using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensibilidadX = 1f;
    private float rotacionX = 0f;

    void Update()
    {
        rotacionX += Input.GetAxis("Mouse X") * sensibilidadX;
        transform.rotation = Quaternion.Euler(0, rotacionX, 0);
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
