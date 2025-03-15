using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private Camera cam;
    private float xRotation = 0f;

    private float xSensitivity = 30f;
    private float ySensitivity = 30f;

    private void Start()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ProcessLook(Vector2 input)
    {
        var mouseX = input.x;
        var mouseY = input.y;

        xRotation -= (mouseY * Time.smoothDeltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * (mouseX * Time.smoothDeltaTime) * xSensitivity);
    }
}
