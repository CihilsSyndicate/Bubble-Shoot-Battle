using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeedBackup = 3f;
    private float moveSpeed;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;

    private bool isWalking;
    private bool isRunning;
    private bool isJumping;
    //public bool isGrounded = true;

    private void Awake()
    {
        moveSpeed = moveSpeedBackup;
    }

    private void Update()
    {
        if (animator.GetBool("IsEmoting") == true)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsShooting", false);
            return;
        }

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Periksa apakah sprint dan walking ditekan bersamaan
        if (gameInput.GetSprintInput() && isWalking)
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

        // Perbaiki rotasi agar karakter selalu menghadap ke arah pergerakan
        if (isWalking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        if (gameInput.GetJumpInput())
        {
            Jump();
        }

        if (gameInput.GetEmoteInput() && animator.GetBool("IsEmoting") == false)
        {
            animator.SetBool("IsEmoting", true);
        }

        if (gameInput.GetShootInput() && animator.GetBool("IsShooting") == false)
        {
            animator.SetBool("IsShooting", true);
        }
    }

    private void Jump()
    {
        isJumping = true;
        animator.SetBool("IsJumping", isJumping);
    }
     
    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public bool IsJumping()
    {
        return isJumping;
    }

    public void OnJumpAnimationEnd()
    {
        isJumping = false;
        animator.SetBool("IsJumping", isJumping);
    }

    public void OnEmoteAnimationStart()
    {
        animator.SetBool("IsJumping", false);
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
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsEmoting", false);
    }

    public void OnShootAnimationEnd()
    {
        animator.SetBool("IsShooting", false);
    }

}
