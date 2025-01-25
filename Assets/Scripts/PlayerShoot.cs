using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float shootCooldown = 0.5f;
    [SerializeField] private GameInput gameInput;

    private float lastShootTime;

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
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = shootPoint.forward * bulletSpeed;
        }

        Destroy(bullet, 5f);
    }
}
