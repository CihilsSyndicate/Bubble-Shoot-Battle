using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] playerSpawnPoints;

    private const int MAX_PLAYERS = 4;
    private GameObject[] activePlayers = new GameObject[MAX_PLAYERS];

    private void Update()
    {
        // Spawn new player with Space key if not at max players
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnAdditionalPlayer();
        }
    }

    private void SpawnAdditionalPlayer()
    {
        // Find first empty player slot
        int emptySlot = FindEmptyPlayerSlot();

        if (emptySlot != -1 && playerPrefab != null && emptySlot < playerSpawnPoints.Length)
        {
            // Instantiate player at the spawn point
            GameObject newPlayer = Instantiate(playerPrefab, playerSpawnPoints[emptySlot].position, Quaternion.identity);

            // Update the name of the player object
            if (emptySlot > 0) // Skip naming for the first player (index 0)
            {
                newPlayer.name = $"Player{emptySlot + 1}(Clone)";
            }

            // Ensure the player is assigned properly
            activePlayers[emptySlot] = newPlayer;

            // Setup input for the new player
            SetupPlayerInput(newPlayer, emptySlot);
        }
        else
        {
            Debug.LogWarning("Cannot spawn player: Either max players reached or spawn points exceeded.");
        }
    }


    private void SetupPlayerInput(GameObject playerObject, int playerIndex)
    {
        // Try to find GameInput and assign player input
        GameInput gameInput = playerObject.GetComponent<GameInput>();
        if (gameInput != null)
        {
            gameInput.EnablePlayerInput(playerIndex);
        }
        else
        {
            Debug.Log($"GameInput is missing on the player prefab. Make sure the player prefab has a GameInput component.");
        }
    }

    private int FindEmptyPlayerSlot()
    {
        for (int i = 0; i < activePlayers.Length; i++)
        {
            if (activePlayers[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
}
