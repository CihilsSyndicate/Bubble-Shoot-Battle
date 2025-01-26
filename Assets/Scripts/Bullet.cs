using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitEffect;

    private void OnTriggerEnter(Collider other)
    {
        // Instantiate and play the hit effect at the collision point
        ParticleSystem effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        effect.Play();

        // Destroy the effect after it has finished playing
        Destroy(effect.gameObject, effect.main.duration);

        // Destroy the bullet
        Destroy(gameObject);
    }
}