using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // BASIS FROM https://www.youtube.com/watch?v=f473C43s8nE&list=PLmo33sM32UcqXt29JkFr4KP4CPktmgi9o

    [Header("Movement")]
    public float moveSpeed;
    float preRunSpeed;
    bool running;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool readyToJump = true;

    [SerializeField] private ClimbingWallTrigger[] climbingWallTrigger; //REMEMBER TO ALWAYS ADD NEW CLIMBING WALLS TO ARRAY

    public float climbingSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Audio")] // Audio
    public AudioSource audioSource;
    public AudioClip[] footstepClips;
    public AudioClip landingClip;

    public float stepInterval = 0.5f;
    [Range(0f, 1f)]
    public float footstepVolume = 0.7f;
    [Range(0f, 1f)]
    public float landingVolume = 0.2f;
    private float stepTimer = 0f;

    private bool wasGroundedLastFrame = false;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround; //ALWAYS ADD WHATISGROUND LAYER TO PLATFORMS WHERE JUMPING SHOULD BE ENABLED
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        preRunSpeed = moveSpeed;
        running = false;
    }

    void Update()
    {
        //ground check
        //grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        //Debug.Log(grounded);

        if (GetComponent<StickyPlatformTracker>().isOnMovingPlatform
        || Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        // for Landing Sound 
        if (!wasGroundedLastFrame && grounded)
        {
            PlayLandingSound();
        }
        wasGroundedLastFrame = grounded; // 

        MyInput();
        SpeedControl();

        //handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        // for Footstep Sounds
        if (grounded && (Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs(rb.velocity.z) > 0.1f))
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                PlayRandomFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        } //

    }

    // Sound
    private void PlayRandomFootstep()
    {
        if (footstepClips.Length == 0 || audioSource == null) return;
        int index = Random.Range(0, footstepClips.Length);
        audioSource.PlayOneShot(footstepClips[index], footstepVolume);
    }

    private void PlayLandingSound()
    {
        if (landingClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(landingClip, landingVolume);
        }
    } 

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded && !IsTouchingWall())
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        else if (IsTouchingWall() && Input.GetKey("space"))
        {
            Climb();
        }
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 15f, ForceMode.Force);

        //in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 15f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {

        if (Input.GetKey("left shift") && !running && grounded)
        {
            moveSpeed = moveSpeed * 1.5f; //running
            running = true;
        }
        else if (!Input.GetKey("left shift") && running && grounded)
        {
            moveSpeed = preRunSpeed; //stop running
            running = false;
        }

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

    }

    private void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private bool IsTouchingWall()
    {
        for (int i = 0; i < climbingWallTrigger.Length; i++)
        {
            if (climbingWallTrigger[i].GetIsInDaWall())
            {
                return true;
            }
        }
        return false;
    }

    private void Climb()
    { 
        rb.velocity = new Vector3(rb.velocity.x, climbingSpeed, rb.velocity.z);
    }
}
