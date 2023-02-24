using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will take a scriptable object for values, type, firing radius, etc.
//controlls turret behavior, which is looking at the mouse currently, eventually 
//will target enemies that enter a circle collider
public class Turret : MonoBehaviour
{
    [SerializeField] private bool lookAtTarget; //some towers might not have to look at target
    [SerializeField] private float radius = 10.0f; //will hold the radius for finding enemies
    private ProjectileManager projectileManager;

    //projectileManager is attached to turret as well as this script to help with projectile pooling
    private void Start()
    {
        projectileManager = GetComponent<ProjectileManager>();
    }

    private void Update()
    {
        //Test code to have the turret look at the mouse, update so it looks at enemy
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
        transform.up = mousePosition - transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            projectileManager.fireProjectile();
        }
    }
}
