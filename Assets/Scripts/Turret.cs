using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public static Turret Instance;


    [SerializeField] private bool lookAtTarget;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private float radius = 10.0f;
    [SerializeField] private Transform[] projectileSpawnPoints;

    private List<GameObject> pooledProjectiles = new List<GameObject>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    //creates a certain amount of projectiles to be added to pool
    private void Start()
    {

        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            pooledProjectiles.Add(projectile);
        }
    }

    private void Update()
    {
        
    }

    //Returns projectile from pooled projectiles, if no more in there will create more as needed
    public GameObject GetProjectile()
    {
        for (int i = 0; i < pooledProjectiles.Count; i++)
        {
            if (!pooledProjectiles[i].activeInHierarchy)
            {
                return pooledProjectiles[i];
            }
        }

        GameObject projectile = Instantiate(projectilePrefab);
        pooledProjectiles.Add(projectile);
        return projectile;
    }


}
