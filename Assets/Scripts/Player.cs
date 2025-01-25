using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeedBackup = 3f; // Default move speed
    [SerializeField] private GameInput gameInput; // Input handler
    [SerializeField] private Rigidbody rb; // Rigidbody for physics
    [SerializeField] private Animator animator; // Animator for animations

    private float moveSpeed;
    private bool isWalking;
    private bool isRunning;
    private bool isJumping;

    private void Awake()
    {
        moveSpeed = moveSpeedBackup;
    }

    private void Start()
    {
        gameInput = FindObjectOfType<GameInput>();
    }

    private void Update()
    {
        if (animator.GetBool("IsEmoting"))
        {
            ResetAllAnimatorFlags();
            return;
        }

        HandleMovement();
        HandleActions();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized(this.gameObject);
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (gameInput.GetSprintInput(this.gameObject) && isWalking)
        {
            isRunning = true;
            moveSpeed = moveSpeedBackup + 4;
        }
        else
        {
            isRunning = false;
            moveSpeed = moveSpeedBackup;
        }

        transform.position += moveDir * moveSpeed * Time.deltaTime;
        isWalking = moveDir != Vector3.zero;

        // Rotate character to face movement direction
        if (isWalking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    // Handle player actions like jumping, emoting, and shooting
    private void HandleActions()
    {
        if (gameInput.GetJumpInput(this.gameObject))
        {
            Jump();
        }

        if (gameInput.GetEmoteInput(this.gameObject) && !animator.GetBool("IsEmoting"))
        {
            animator.SetBool("IsEmoting", true);
        }

        if (gameInput.GetShootInput(this.gameObject) && !animator.GetBool("IsShooting") && !isWalking && !isRunning)
        {
            animator.SetBool("IsShooting", true);
        }
    }

    // Jump logic
    private void Jump()
    {
        isJumping = true;
        animator.SetBool("IsJumping", isJumping);
    }

    // Reset all animator flags
    private void ResetAllAnimatorFlags()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsShooting", false);
    }

    // Public helper methods for external access
    public bool IsWalking() => isWalking;
    public bool IsRunning() => isRunning;
    public bool IsJumping() => isJumping;

    // Animation Event handlers
    public void OnJumpAnimationEnd()
    {
        isJumping = false;
        animator.SetBool("IsJumping", false);
    }

    public void OnEmoteAnimationStart()
    {
        ResetAllAnimatorFlags();
    }

    public void OnEmoteAnimationEnd()
    {
        animator.SetBool("IsEmoting", false);
    }

    public void OnShootAnimationStart()
    {
        ResetAllAnimatorFlags();
    }

    public void OnShootAnimationEnd()
    {
        animator.SetBool("IsShooting", false);
    }
}
