using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 20f;
    public float verticalRange = 20f;

    public float fireRate = 1f;
    public float bigDamage = 2f;
    public float smallDamage = 1f;

    public float gunShotRadius = 20f;

    public float nextTimeToFire;

    private BoxCollider gunTrigger;

    public LayerMask raycastLayerMask;
    public LayerMask enemyLayerMask;
    public EnemyManager enemyManager;



    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, (float)(range * 0.5));
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire)
        {
            Fire();
        }
    }

    void Fire()
    {
        //gunshot radius

        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);

        foreach(var enemyCollider in enemyColliders)
        {
            enemyCollider.GetComponent<EnemyAwareness>().isAggro = true;
        }


        //test audio
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();

        //damges the enemy 
        foreach(var enemy in enemyManager.eneimesInTrigger)
        {
            //gets direction of the point of view of the character of the enemy
            var dir = enemy.transform.position - transform.position;

            RaycastHit hit;
            if(Physics.Raycast(transform.position, dir, out hit, range * 1.5f, raycastLayerMask))
            {
                if (hit.transform == enemy.transform ) 
                {
                    //range check
                    float dist = Vector3.Distance(enemy.transform.position, transform.position);

                    if(dist > range * 0.5)
                    {
                        enemy.TakeDamage(smallDamage);
                    }
                    else 
                    {
                        enemy.TakeDamage(bigDamage);
                    }

                }
            }
        }

        //reset time
        nextTimeToFire = Time.time + fireRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        //adds enemy
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            enemyManager.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //removes enemy
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
           enemyManager.RemoveEnemy(enemy);
        }
    }
}
