using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeedBackup = 3f; // Default move speed
    [SerializeField] private GameInput gameInput; // Input handler
    [SerializeField] private Rigidbody rb; // Rigidbody for physics
    [SerializeField] private Animator animator; // Animator for 
    [SerializeField] private float slowSpeed = 1f; // Kecepatan saat specialShoot


    private float moveSpeed;
    private bool isSpecialShooting = false;
    private bool isWalking;
    private bool isRunning;

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
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsShooting", false);
            return;
        }

        if (animator.GetBool("IsShooting"))
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsEmoting", false);
            return;
        }

        HandleMovement();
        HandleActions();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized(this.gameObject);
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (gameInput.GetSprintInput(this.gameObject) && isWalking && !isSpecialShooting)
        {
            isRunning = true;
            moveSpeed = moveSpeedBackup + 4;
        }
        else
        {
            isRunning = false;
            moveSpeed = isSpecialShooting ? slowSpeed : moveSpeedBackup;
            animator.speed = isSpecialShooting ? 0.5f : 1f;
        }

        transform.position += moveDir * moveSpeed * Time.deltaTime;
        isWalking = moveDir != Vector3.zero;

        if (isWalking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    public void SetSpecialShooting(bool isShooting)
    {
        isSpecialShooting = isShooting;
    }

    // Handle player actions like jumping, emoting, and shooting
    private void HandleActions()
    {
        if (gameInput.GetEmoteInput(this.gameObject) && !animator.GetBool("IsEmoting"))
        {
            animator.SetBool("IsEmoting", true);
            Debug.Log("Emote");
        }

        if (gameInput.GetShootInput(this.gameObject) && !animator.GetBool("IsShooting"))
        {
            if (!isWalking && !isRunning)
            {
                animator.SetBool("IsShooting", true);
            }
        }

        if(gameInput.GetSpecialShootInput(this.gameObject) && !animator.GetBool("IsShooting"))
        {
            if (!isWalking && !isRunning)
            {
                animator.SetBool("IsShooting", true);
            }
        }
    }

    // Public helper methods for external access
    public bool IsWalking() => isWalking;
    public bool IsRunning() => isRunning;

    public void OnEmoteAnimationStart()
    {
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsShooting", false);
    }

    public void OnEmoteAnimationEnd()
    {
        animator.SetBool("IsEmoting", false);
    }

    public void OnShootAnimationStart()
    {
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsEmoting", false);
    }

    public void OnShootAnimationEnd()
    {
        animator.SetBool("IsShooting", false);
    }
}
