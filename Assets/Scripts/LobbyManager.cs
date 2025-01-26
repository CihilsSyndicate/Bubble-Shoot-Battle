using UnityEngine;
using UnityEngine.UI;
using Photon.Pun; // Photon untuk multiplayer
using TMPro;
public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("UI Elements")]
    public Button createRoomButton; // Tombol untuk membuat room
    public InputField enterRoomCodeInput; // Input untuk kode room
    public Button joinRoomButton; // Tombol untuk join room
    public Text statusText; // Teks status (opsional)
    public InputField inputField;

    void Start()
    {
        // Hubungkan event tombol
        createRoomButton.onClick.AddListener(CreateRoom);
        joinRoomButton.onClick.AddListener(JoinRoom);
        inputField.onValueChanged.AddListener(OnInputValueChanged);
        // Inisialisasi koneksi Photon
        PhotonNetwork.ConnectUsingSettings();
        UpdateStatus("Menghubungkan ke server...");
    }

    public void CreateRoom()
    {
        // Cek apakah sudah terkoneksi ke Master Server
        if (PhotonNetwork.IsConnectedAndReady)
        {
            // Generate kode room acak (angka)
            string roomCode = Random.Range(1000, 9999).ToString();
            PhotonNetwork.CreateRoom(roomCode); // Buat room di Photon
            UpdateStatus($"Membuat room dengan kode: {roomCode}");
        }
        else
        {
            UpdateStatus("Tidak terhubung ke Master Server. Tunggu beberapa saat dan coba lagi.");
        }
    }

    public override void OnConnectedToMaster()
    {
        UpdateStatus("Berhasil terhubung ke Master Server!");
        PhotonNetwork.JoinLobby(); // Bergabung ke lobby
    }

    public override void OnJoinedLobby()
    {
        UpdateStatus("Berhasil masuk ke lobby. Siap untuk membuat atau bergabung ke room.");
    }


    public void JoinRoom()
    {
        if (PhotonNetwork.InLobby)
        {
            string roomCode = enterRoomCodeInput.text;
            if (!string.IsNullOrEmpty(roomCode))
            {
                PhotonNetwork.JoinRoom(roomCode); // Bergabung ke room dengan kode
                UpdateStatus($"Bergabung ke room: {roomCode}");
            }
            else
            {
                UpdateStatus("Kode room tidak boleh kosong!");
            }
        }
        else
        {
            UpdateStatus("Belum masuk ke lobby. Tunggu beberapa saat...");
        }
    }

    void OnInputValueChanged(string text)
    {
        Debug.Log("Input text: " + text); // Periksa input yang diterima
    }

    public override void OnJoinedRoom()
    {
        UpdateStatus($"Berhasil bergabung ke room: {PhotonNetwork.CurrentRoom.Name}");
        // Pindahkan ke scene game, jika diperlukan
        PhotonNetwork.LoadLevel("DesertMap");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        UpdateStatus($"Gagal membuat room: {message}");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        UpdateStatus($"Gagal bergabung: {message}");
    }

    void UpdateStatus(string message)
    {
        Debug.Log(message); // Tampilkan di console log
        if (statusText != null)
        {
            statusText.text = message; // Tampilkan di UI (opsional)
        }
    }

}
