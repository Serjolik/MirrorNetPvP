using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : NetworkBehaviour
{
    [Header("Camera prefab")]
    [SerializeField] GameObject ourCameraObj;

    [Header("Player transforms")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform playerObj;
    [SerializeField] private Transform ourCamera;

    [Header("Camera rotation speed")]
    /// <summary>
    /// determines the camera rotation speed
    /// </summary>
    [SerializeField] private float rotationSpeed;




    private void Start()
    {
        LockCursor();
    }

    public override void OnStartLocalPlayer()
    {
        if (isOwned)
            ourCameraObj.SetActive(true);
    }


    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!isOwned) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
        }
        else if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Mouse0))
        {
            LockCursor();
        }

        Vector3 viewDir = playerTransform.position - new Vector3(ourCamera.transform.position.x, playerTransform.position.y, ourCamera.transform.position.z);
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
