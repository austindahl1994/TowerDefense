using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Will have to change projectile script to a more basic one for all turrets
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 2.0f;
    [SerializeField] private ParticleSystem particleEffect;
    private TrailRenderer tr;
    public float timer;
    public ProjectileManager projectileManager;

    //The reason to have a timer for the life of projectile is after it is put back into pool
    //it will reset timer using the onEnable() function
    private void Awake()
    {
        timer = lifetime;
    }

    //Trail renderer was causing visual issues, tr for trail renderer to change states
    void Start()
    {
        tr = GetComponent<TrailRenderer>();
        tr.material = new Material(Shader.Find("Sprites/Default"));
    }

    //On projectile creation from ProjectileManager script, makes it so the tower that created
    //The projectile is set as the turret for projectiles to go back into the queue for pooling
    public void SetProjectileManager(ProjectileManager manager)
    {
        projectileManager = manager;
    }

    //so far update just moves projectile and upon end of life will add projectile back to pool
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            tr.Clear();
            projectileManager.EnqueueProjectile(gameObject);
        }
    }

    //No effect yet, when enemies are added will add objects back into pool on collision with enemy
    //and play some sort of particle effect
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            particleEffect.Play();
            projectileManager.EnqueueProjectile(gameObject);
        }
    }

    //Everytime the projectile is setActive when it will be "fired", timer is reset for a
    //life cycle
    private void OnEnable()
    {
        timer = lifetime;
    }
}
