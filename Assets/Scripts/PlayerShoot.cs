using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float shootCooldown = 0.5f;
    [SerializeField] private Vector3 specialBulletScale = new Vector3(0.5f, 0.5f, 0.5f); // Ukuran peluru special
    [SerializeField] private GameInput gameInput;
    [SerializeField] private ParticleSystem specialShoot; // [1
    [SerializeField] private Player player;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioSource sfxHold;

    private bool specialShootReady = true;

    private float lastShootTime;
    private GameObject currentSpecialBullet;

    private void Start()
    {
        gameInput = FindObjectOfType<GameInput>();
    }

    private void Update()
    {
        if (gameInput.GetShootInput(this.gameObject) && Time.time > lastShootTime + shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
        if (gameInput.GetSpecialShootInput(this.gameObject) && Time.time > lastShootTime + shootCooldown && specialShootReady)
        {
            SpecialShoot();
            lastShootTime = Time.time;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        sfx.Play();

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = shootPoint.forward * bulletSpeed;
        }

        Destroy(bullet, 5f);
    }

    private void SpecialShoot()
    {
        if (currentSpecialBullet != null)
        {
            return; // Jika peluru spesial sudah ada, hentikan eksekusi
        }

        if (player != null)
        {
            player.SetSpecialShooting(true);
        }

        specialShootReady = false;

        ParticleSystem effect = Instantiate(specialShoot, shootPoint.position, Quaternion.identity);
        sfxHold.Play();
        effect.transform.SetParent(shootPoint); // Set effect as a child of shootPoint
        effect.Play();
        Debug.Log(effect.main.duration);
        // Destroy the effect after it has finished playing
        Destroy(effect.gameObject, effect.main.duration);
        StartCoroutine(SpecialShootRoutine());
    }

    private IEnumerator SpecialShootRoutine()
    {
        // Menunggu selama 2 detik sebelum peluru 
        yield return new WaitForSeconds(2f);

        currentSpecialBullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        currentSpecialBullet.transform.localScale = specialBulletScale;

        Rigidbody bulletRb = currentSpecialBullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = shootPoint.forward * bulletSpeed;
        }

        Destroy(currentSpecialBullet, 5f);
        if (player != null)
        {
            player.SetSpecialShooting(false);
        }
        specialShootReady = true;
        currentSpecialBullet = null; // Reset setelah peluru dihancurkan
    }
}
    