using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 150f;

    float xRotation = 0f;
    Transform playerBody;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerBody = transform.parent;

        Debug.Log("=== MouseLook Started ===");
        Debug.Log("PlayerBody = " + playerBody);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Debug.Log("Window Focused");
        }
    }

    void Update()
    {
        if (playerBody == null)
        {
            Debug.LogError("PlayerBody is NULL!");
            return;
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (mouseX != 0 || mouseY != 0)
        {
            Debug.Log("Mouse Input: " + mouseX + " / " + mouseY);
        }

        mouseX *= mouseSensitivity * Time.deltaTime;
        mouseY *= mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}