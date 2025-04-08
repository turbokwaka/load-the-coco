using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Mouse Settings")]
    [SerializeField] private float mouseSensitivity = 100f;
    
    [Header("Vertical Rotation Limits")]
    [SerializeField] private float minY = -60f;
    [SerializeField] private float maxY = 60f;

    private float _xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, minY, maxY);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        transform.parent.Rotate(Vector3.up * mouseX);
    }
}