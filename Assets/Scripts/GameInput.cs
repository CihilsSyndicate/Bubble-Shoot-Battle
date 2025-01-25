using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputAction playerInputAction;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }

    public bool GetJumpInput()
    {
        return playerInputAction.Player.Jump.triggered;
    }

    public bool GetShootInput()
    {
        return playerInputAction.Player.Shoot.triggered;
    }

    public bool GetSprintInput()
    {
        return playerInputAction.Player.Sprint.IsPressed();
    }

    public bool GetEmoteInput()
    {
        return playerInputAction.Player.Emote.triggered;
    }
}
