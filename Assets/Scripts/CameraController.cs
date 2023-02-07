using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Player transforms")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform playerObj;

    [Header("Camera rotation speed")]
    /// <summary>
    /// determines the camera rotation speed
    /// </summary>
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 viewDir = playerTransform.position - new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 InputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (InputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, InputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
