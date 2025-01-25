using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthbar;
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthbar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Pastikan health tidak kurang dari 0
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // Perbarui tampilan health bar
        UpdateHealthbar();

        // Jika health habis, panggil fungsi untuk menangani kematian
        if (currentHealth <= 0f)
        {
            OnDeath();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Bullet"))
        {
            Debug.Log("Hit by bullet!");
            TakeDamage(20f);
        }
    }

    private void UpdateHealthbar()
    {
        healthbar.fillAmount = currentHealth / maxHealth;
    }

    // Metode yang dipanggil saat player mati
    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
