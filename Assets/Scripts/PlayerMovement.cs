using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : NetworkBehaviour
{
    [Header("Movement params")]
    [SerializeField] private float moveSpeed = 75f;
    [SerializeField] private float dashForce = 150f;
    [SerializeField] private float dashCooldown = 3f;
    [SerializeField] private float dashWorkingTime = 1f;

    [SerializeField] private KeyCode dash = KeyCode.Mouse0;

    [SerializeField] private Transform playerOrientation;
    [SerializeField] private Transform model;

    private PlayerBrain playerBrain;

    private Rigidbody rb;

    private Vector3 moveDirection;

    private float horizontalInput;
    private float verticalInput;

    private bool canDash = true;
    public bool inDash { get; private set; }

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (!isOwned)
            return;

        //moving
        UpdateMovement();
        if (TryDesh())
        {
            StartCoroutine(DashReseting());
            StartCoroutine(DashTimer());
        }
    }

    private void FixedUpdate()
    {
        if (!isOwned)
            return;

        Move();
    }

    public void UpdateMovement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        LimitingSpeed();
    }

    public bool TryDesh()
    {
        if (Input.GetKey(dash) && canDash)
        {
            Dash();
            return true;
        }
        return false;
    }

    public void Move()
    {
        moveDirection = playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }

    private void Dash()
    {
        rb.AddForce(model.forward * dashForce, ForceMode.Impulse);
    }

    public IEnumerator DashTimer()
    {
        playerBrain = GetComponentInParent<PlayerBrain>();
        inDash = true;
        playerBrain.isInDash = true;
        yield return new WaitForSeconds(dashWorkingTime);
        inDash = false;
        playerBrain.isInDash = false;
    }

    public IEnumerator DashReseting()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
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
