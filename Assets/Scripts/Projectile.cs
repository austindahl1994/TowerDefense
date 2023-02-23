using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 2.0f;
    [SerializeField] private ParticleSystem particleEffect;

    private float lifeTimer;

    void Start()
    {
        lifeTimer = lifetime;
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            particleEffect.Play();
            gameObject.SetActive(false);
        }
    }

    
}
