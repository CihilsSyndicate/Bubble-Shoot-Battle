using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private GameObject healthbarP1GO;
    [SerializeField] private GameObject healthbarP2GO;
    [SerializeField] private GameObject healthbarP3GO;
    [SerializeField] private GameObject healthbarP4GO;

    [SerializeField] private GameObject indicatorP1;
    [SerializeField] private GameObject indicatorP2;
    [SerializeField] private GameObject indicatorP3;
    [SerializeField] private GameObject indicatorP4;

    [SerializeField] private Image healthbarP1;
    [SerializeField] private Image healthbarP2;
    [SerializeField] private Image healthbarP3;
    [SerializeField] private Image healthbarP4;
    [SerializeField] private GameObject parent;
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [SerializeField] private Player player;
    [SerializeField] private ParticleSystem death;

    private void Start()
    {
        currentHealth = maxHealth;

        CheckAndDisplayHealthbars();

        UpdateHealthbar();
    }

    private void CheckAndDisplayHealthbars()
    {
        if (gameObject.name == "Player1")
        {
            healthbarP1GO.gameObject.SetActive(true);
            indicatorP1.SetActive(true);
        }
        if (gameObject.name == "Player2")
        {
            healthbarP2GO.gameObject.SetActive(true);
            indicatorP2.SetActive(true);
        }
        if (gameObject.name == "Player3")
        {
            healthbarP3GO.gameObject.SetActive(true);
            indicatorP3.SetActive(true);
        }
        if (gameObject.name == "Player4")
        {
            healthbarP4GO.gameObject.SetActive(true);
            indicatorP4.SetActive(true);
        }
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
        switch (parent.name)
        {
            case "Player1":
                healthbarP1.fillAmount = currentHealth / maxHealth;
                break;
            case "Player2":
                healthbarP2.fillAmount = currentHealth / maxHealth;
                break;
            case "Player3":
                healthbarP3.fillAmount = currentHealth / maxHealth;
                break;
            case "Player4":
                healthbarP4.fillAmount = currentHealth / maxHealth;
                break;
            default:
                return;
        }
    }

    // Metode yang dipanggil saat player mati
    public void OnDeath()
    {
        player.OnDeaths();
        death.Play();
        Destroy(death.gameObject, death.main.duration);
        Destroy(gameObject, 2f);
    }
}
