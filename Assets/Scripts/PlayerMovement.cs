using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player movement")]
    /// <summary>
    /// player speed
    /// </summary>
    [SerializeField] private float moveSpeed = 0.1f;

    [Header("Dash parametrs")]
    [SerializeField] private float dashForce = 5;
    [SerializeField] private float dashCooldown = 3;
    [SerializeField] private float dashWorkingTime = 1;

    [Header("Keys")]
    [SerializeField] private KeyCode dash = KeyCode.Mouse0;

    [Header("Player orientation")]
    [SerializeField] private Transform orientation;

    private Rigidbody rb;

    private Vector3 moveDirection;

    private float horizontalInput;
    private float verticalInput;

    private bool canDash;
    public bool inDash { get; private set; }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canDash = true;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(dash) && canDash)
        {
            canDash = false;
            Dash();

            Invoke(nameof(DashReseting), dashCooldown);
        }

        LimitingSpeed();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }

    private void Dash()
    {
        Debug.Log("Dash");
        StartCoroutine(dashTimer());
        rb.AddForce(orientation.forward * dashForce, ForceMode.Impulse);
    }

    private IEnumerator dashTimer()
    {
        inDash = true;
        yield return new WaitForSeconds(dashWorkingTime);
        inDash = false;
    }

    private void DashReseting()
    {
        canDash = true;
    }

    private void LimitingSpeed()
    {
        Vector3 velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (velocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = velocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

}
