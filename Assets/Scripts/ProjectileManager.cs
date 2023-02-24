using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is for creating, pooling, and enqueueing projectiles after they have hit an enemy
//or been active for a certain amount of time, with each turret having its own queue
public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] Transform[] projectileSpawnPoints;
    private Queue<GameObject> pooledProjectiles = new Queue<GameObject>();
    private int poolSize = 5;

    //creates an amount of projectiles based on pool size, increasing as the amount needed goes up
    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            createNewProjectile();
        }
    }

    //returns a projectile if there is one available in queue, else it will create a new one to 
    //add to the queue and return
    public GameObject GetProjectile()
    {
        if (pooledProjectiles.Count > 0)
        {
            return pooledProjectiles.Dequeue();
        }
        else
        {
            createNewProjectile();
            return pooledProjectiles.Dequeue();
        }
    }

    //fires a projectile, currently on mouse click, but will change when enemy is in range
    //iterates through each firing point that is active, allows for more projectiles to be 
    //shot at once at a later time
    public void fireProjectile()
    {
        foreach (Transform firingPoint in projectileSpawnPoints)
        {
            if (firingPoint.gameObject.activeInHierarchy) 
            {
                GameObject projectile = GetProjectile();
                projectile.SetActive(true);
                projectile.transform.position = firingPoint.position;
                projectile.transform.rotation = firingPoint.rotation;
            }
        }
    }

    //Adds projectile back to the pool from projectile script
    public void EnqueueProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
        pooledProjectiles.Enqueue(projectile);
    }

    //creates a new projectile and adds it to a queue that holds all projectiles as inactive
    private void createNewProjectile() {
        GameObject newProjectile = Instantiate(projectilePrefab);
        newProjectile.GetComponent<Projectile>().SetProjectileManager(this);
        newProjectile.SetActive(false);
        pooledProjectiles.Enqueue(newProjectile);
    }
}
