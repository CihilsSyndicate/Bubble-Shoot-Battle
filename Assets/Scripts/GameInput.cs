using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    private PlayerInputAction playerInputAction;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Enable();

    }

    public void EnablePlayerInput(int playerIndex)
    {
        switch (playerIndex)
        {
            case 0:
                playerInputAction.Player.Enable();
                break;
            case 1:
                playerInputAction.Player2.Enable();
                break;
            case 2:
                playerInputAction.Player3.Enable();
                break;
            case 3:
                playerInputAction.Player4.Enable();
                break;
        }
    }

    public Vector2 GetMovementVectorNormalized(GameObject GO)
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();

        // If this is a specific player's input script
        switch (GO.name)
        {
            case "Player2":
                inputVector = playerInputAction.Player2.Move.ReadValue<Vector2>();
                break;
            case "Player3":
                inputVector = playerInputAction.Player3.Move.ReadValue<Vector2>();
                break;
            case "Player4":
                inputVector = playerInputAction.Player4.Move.ReadValue<Vector2>();
                break;
        }

        return inputVector.normalized;
    }

    public bool GetJumpInput(GameObject GO)
    {
        switch (GO.name)
        {
            case "Player1":
                return playerInputAction.Player.Jump.triggered;
            case "Player2":
                return playerInputAction.Player2.Jump.triggered;
            case "Player3":
                return playerInputAction.Player3.Jump.triggered;
            case "Player4":
                return playerInputAction.Player4.Jump.triggered;
            default:
                return false;
        }
    }

    public bool GetShootInput(GameObject GO)
    {
        switch (GO.name)
        {
            case "Player1":
                return playerInputAction.Player.Shoot.triggered;
            case "Player2":
                return playerInputAction.Player2.Shoot.triggered;
            case "Player3":
                return playerInputAction.Player3.Shoot.triggered;
            case "Player4":
                return playerInputAction.Player4.Shoot.triggered;
            default:
                return false;
        }
    }

    public bool GetSprintInput(GameObject GO)
    {
        switch (GO.name)
        {
            case "Player1":
                return playerInputAction.Player.Sprint.IsPressed();
            case "Player2":
                return playerInputAction.Player2.Sprint.IsPressed();
            case "Player3":
                return playerInputAction.Player3.Sprint.IsPressed();
            case "Player4":
                return playerInputAction.Player4.Sprint.IsPressed();
            default:
                return false;
        }
    }

    public bool GetEmoteInput(GameObject GO)
    {
        switch (GO.name)
        {
            case "Player1":
                return playerInputAction.Player.Emote.triggered;
            case "Player2":
                return playerInputAction.Player2.Emote.triggered;
            case "Player3":
                return playerInputAction.Player3.Emote.triggered;
            case "Player4":
                return playerInputAction.Player4.Emote.triggered;
            default:
                return false;
        }
    }

    public bool GetSpecialShootInput(GameObject GO)
    {
        switch (GO.name)
        {
            case "Player1":
                return playerInputAction.Player.SShoot.IsPressed();
            case "Player2":
                return playerInputAction.Player2.SShoot.IsPressed();
            case "Player3":
                return playerInputAction.Player3.SShoot.IsPressed();
            case "Player4":
                return playerInputAction.Player4.SShoot.IsPressed();
            default:
                return false;
        }
    }
}
