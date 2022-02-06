using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("STATES")]
    public bool isGrounded;
    public bool isSprinting;
    [Header("MOVEMENT")]
    public float gravity;
    public float speed;
    public float jumpHeight = 2f;
    public float sprintMultiplier;
    public Vector3 crouchScale;
    [Header("CHECKS")]
    public float groundDistance = 0.4f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    [Header("REFERENCES")]
    public CharacterController controller;

    float x;
    float z;
    public Vector3 velocity;
    Vector3 originalScale;
    Vector3 inputVector;
    float defaultSlopeHeight;
    float defaultStepHeight;
    Abilities abilityScript;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        abilityScript = GetComponent<Abilities>();

        originalScale = transform.localScale;
        defaultSlopeHeight = controller.slopeLimit;
        defaultStepHeight = controller.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        //INPUT
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.z = Input.GetAxis("Vertical");
        Vector3.Normalize(inputVector);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer); 
        GetInput();
        Move();
        ApplyGravity();
    }

    void GetInput()
    {   //Basic Movement
        if (Input.GetButton("Jump") && isGrounded)
            Jump();
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
            StartCrouch();
        if (Input.GetKeyUp(KeyCode.LeftControl))
            EndCrouch();
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        //Abilities
        if (!isGrounded && Input.GetKeyDown(KeyCode.S) && !abilityScript.isGroundPounding)
            StartCoroutine(abilityScript.GroundPound());
        if (Input.GetKeyDown(KeyCode.F))
            StartCoroutine(abilityScript.Dash());

    }


    void EndCrouch(){
        transform.localScale = originalScale;
    }

    void StartCrouch(){
        transform.localScale = crouchScale;
    }

    void Jump(){
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);//Real 
         
        //change character controller
        controller.stepOffset = 0;
        controller.slopeLimit = 0;
    }

    void Move(){

        if (isGrounded)
        {// Reset character 
            controller.stepOffset = defaultStepHeight;
            controller.slopeLimit = defaultSlopeHeight;
            if (abilityScript.isGroundPounding)
                abilityScript.ResetGroundPound();
        }

        Vector3 move = transform.right * inputVector.x + transform.forward * inputVector.z;
        move = Vector3.ClampMagnitude(move, 1f);
        if(!isSprinting)
            controller.Move(speed * Time.deltaTime * move);//Player Movement
        else if(isSprinting)
            controller.Move(speed * Time.deltaTime * move * sprintMultiplier);//Sprinting
    }

    void ApplyGravity(){

        velocity.y += gravity * Time.deltaTime;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        controller.Move(velocity * Time.deltaTime);
        
    }



}
