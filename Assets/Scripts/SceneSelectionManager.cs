using UnityEngine;
using UnityEngine.UI; // Untuk Dropdown dan Button
using UnityEngine.SceneManagement; // Untuk SceneManager

public class SceneSelectionManager : MonoBehaviour
{
    public Dropdown sceneDropdown; // Dropdown untuk memilih scene
    public Button loadButton; // Tombol untuk memuat scene berdasarkan pilihan

    // Pastikan untuk mengisi dropdown dengan nama scene yang ada di Build Settings
    void Start()
    {
        // Tambahkan pilihan scene yang ada di Build Settings
        PopulateSceneDropdown();

        // Menambahkan listener untuk tombol load
        loadButton.onClick.AddListener(OnLoadSceneButtonClicked);
    }

    void PopulateSceneDropdown()
    {
        sceneDropdown.ClearOptions(); // Menghapus opsi lama (jika ada)

        // Menambahkan nama scene yang ada di Build Settings ke dropdown
        var scenes = new System.Collections.Generic.List<string>();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            scenes.Add(sceneName);
        }

        // Menambahkan opsi ke dropdown
        sceneDropdown.AddOptions(scenes);
    }

    // Fungsi untuk memuat scene berdasarkan pilihan dropdown
    void OnLoadSceneButtonClicked()
    {
        string selectedScene = sceneDropdown.options[sceneDropdown.value].text; // Ambil nama scene dari dropdown
        SceneManager.LoadScene(selectedScene); // Pindah ke scene yang dipilih
    }
}
