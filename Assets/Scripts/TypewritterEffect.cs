using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public Text uiText; // Referensi ke Text UI
    public string baseText = "Loading"; // Teks dasar
    public string dots = "..."; // Teks untuk typewriter effect
    public float typeSpeed = 0.5f; // Kecepatan efek typewriter

    private int currentDotCount = 0;

    void Start()
    {
        if (uiText != null)
        {
            StartCoroutine(ShowDots());
        }
    }

    IEnumerator ShowDots()
    {
        while (true)
        {
            // Tambahkan teks dasar + jumlah titik
            uiText.text = baseText + dots.Substring(0, currentDotCount);
            currentDotCount = (currentDotCount + 1) % (dots.Length + 1);

            // Tunggu sebelum menambahkan titik berikutnya
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
