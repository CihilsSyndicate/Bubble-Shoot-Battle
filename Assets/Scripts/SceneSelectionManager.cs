using UnityEngine;
using UnityEngine.SceneManagement; // Pastikan menggunakan namespace ini untuk mengakses SceneManager

public class SceneChanger : MonoBehaviour
{
    // Fungsi untuk mengganti scene
    public void ChangeScene(string sceneName)
    {
        // Memuat scene berdasarkan nama
        SceneManager.LoadScene(sceneName);
    }

}
