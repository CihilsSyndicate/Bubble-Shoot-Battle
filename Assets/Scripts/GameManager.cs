using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] playerSpawnPoints;
    [SerializeField] private GameObject[] player;

    private const int MAX_PLAYERS = 4;
    private GameObject[] activePlayers = new GameObject[MAX_PLAYERS];
    [SerializeField] private int selectedPlayerCount = 1; // Jumlah pemain yang dipilih
    private static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        //SelectPlayerCount();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && selectedPlayerCount < 4 && SceneManager.GetActiveScene().name == "NumberPlayer")
        {
            selectedPlayerCount++;
            player[selectedPlayerCount - 1].SetActive(true);
        }
    }

    private void SelectPlayerCount()
    {
        Debug.Log("Pilih jumlah pemain (1 hingga " + MAX_PLAYERS + "):");

        // Simulasi jumlah pemain (bisa diganti dengan UI input)
        //selectedPlayerCount = 1; // Ubah sesuai kebutuhan (contoh 2 pemain)

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
            }
            else
            {
                Debug.LogWarning("Tidak cukup posisi spawn untuk semua pemain.");
                break;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "DesertMap")
        {
            AssignSpawnPoints();
            SelectPlayerCount();
        }
    }

    private void AssignSpawnPoints()
    {
        GameObject[] spawnObjects = GameObject.FindGameObjectsWithTag("Spawn");
        if (spawnObjects.Length == 0)
        {
            Debug.LogWarning("Tidak ada GameObject dengan tag 'Spawn' yang ditemukan di scene DesertMap.");
            return;
        }

        playerSpawnPoints = new Transform[spawnObjects.Length];

        for (int i = 0; i < spawnObjects.Length; i++)
        {
            playerSpawnPoints[i] = spawnObjects[i].transform;
        }

        Debug.Log($"{spawnObjects.Length} spawn points telah diassign.");
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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
