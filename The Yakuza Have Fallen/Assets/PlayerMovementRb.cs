using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRb : MonoBehaviour
{
    [Header("MOVEMENT")]
    public float moveSpeed = 0f;

    float horizontalMovement;
    float verticalMovement;

    Vector3 moveDirection;
    public float rbDrag;
    public float speedMultiplier;
    Rigidbody rb;
    public Transform fpsCam;
    public bool isGrounded;
    public float playerHeight = 1;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundLayer;
    public float jumpHeight;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
       
        GetInput();
        ControlDrag();
    }

    void GetInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Jump") && isGrounded)
            Jump();


        moveDirection = fpsCam.forward * verticalMovement + transform.right * horizontalMovement;
    }

    void Jump()
    {
        float jumpForce = Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed * speedMultiplier, ForceMode.Acceleration);
    }

    void ControlDrag()
    {
        rb.drag = rbDrag;
    }


}
