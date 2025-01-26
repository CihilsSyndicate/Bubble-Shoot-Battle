using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] playerSpawnPoints;

    private const int MAX_PLAYERS = 4;
    private GameObject[] activePlayers = new GameObject[MAX_PLAYERS];
    private int selectedPlayerCount = 2; // Jumlah pemain yang dipilih

    private void Start()
    {
        // Pilih jumlah pemain di awal game
        SelectPlayerCount();
    }

    private void SelectPlayerCount()
    {
        Debug.Log("Pilih jumlah pemain (1 hingga " + MAX_PLAYERS + "):");

        // Simulasi jumlah pemain (bisa diganti dengan UI input)
        selectedPlayerCount = 2; // Ubah sesuai kebutuhan (contoh 2 pemain)

        // Validasi jumlah pemain
        if (selectedPlayerCount < 1 || selectedPlayerCount > MAX_PLAYERS)
        {
            Debug.LogError("Jumlah pemain tidak valid. Mengatur ke jumlah minimal (1).");
            selectedPlayerCount = 1;
        }

        // Spawn pemain berdasarkan jumlah yang dipilih
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        // Buat daftar posisi spawn yang diacak
        List<Transform> shuffledSpawnPoints = new List<Transform>(playerSpawnPoints);
        ShuffleList(shuffledSpawnPoints);

        for (int i = 0; i < selectedPlayerCount; i++)
        {
            if (i < shuffledSpawnPoints.Count)
            {
                // Spawn player di posisi yang sudah diacak
                GameObject newPlayer = Instantiate(playerPrefab, shuffledSpawnPoints[i].position, Quaternion.identity);

                // Update nama player
                newPlayer.name = $"Player{i + 1}";

                // Assign player ke array aktif
                activePlayers[i] = newPlayer;

                // Setup input untuk player
                //SetupPlayerInput(newPlayer, i);
            }
            else
            {
                Debug.LogWarning("Tidak cukup posisi spawn untuk semua pemain.");
                break;
            }
        }
    }

    private void SetupPlayerInput(GameObject playerObject, int playerIndex)
    {
        GameInput gameInput = playerObject.GetComponent<GameInput>();
        if (gameInput != null)
        {
            gameInput.EnablePlayerInput(playerIndex);
        }
        else
        {
            Debug.LogWarning($"GameInput is missing on the player prefab. Make sure the player prefab has a GameInput component.");
        }
    }

    private void ShuffleList(List<Transform> list)
    {
        // Algoritma Fisher-Yates Shuffle
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
