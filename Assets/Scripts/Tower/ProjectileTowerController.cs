using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTowerController : MonoBehaviour
{

    [Header("Weapon Attributes")]

    [SerializeField] private int damage = 100;
    [SerializeField] private float fireRate = 1.5f;
    [SerializeField] private float fireCountDown = 0f;

    [SerializeField] private float rotationSpeed = 10;

    [SerializeField] private Transform towerTop;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    [Header("Enemy Target Attributes")]

    [SerializeField] private List<GameObject> enemiesInRangeList = new List<GameObject>();
    [SerializeField] private Transform enemyTarget;


    private bool noEnemiesInRange = true;
    private bool canFireProjectile = true;


    private void Start()
    {
        InvokeRepeating("FindEnemyTarget", 0f, 0.5f);
    }


    // Update is called once per frame
    void Update()
    {
        if (enemyTarget == null)
        {
            return;
        }

        RotateTowerTopTowardsEnemy();
        ShootAtEnemyTarget();
    }

    private void FindEnemyTarget()
    {
        CheckForNulledOutEnemiesWithinRange();

        float shortestDistanceToEnemy = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemiesInRangeList)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistanceToEnemy)
            {
                shortestDistanceToEnemy = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null) 
        {
            enemyTarget = nearestEnemy.transform;
        }
    }

    /*
    //FUNCTION:: Coroutine to Fire the Weapon's Projectile
    private IEnumerator FireProjectile()
    {
        canFireProjectile = false;

        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, towerTop.rotation);
        projectile.GetComponent<ProjectileController>().SetDamage(damage);
        yield return new WaitForSeconds(fireWait);
        canFireProjectile = true;
    }*/


    private void ShootAtEnemyTarget()
    {
        if (fireCountDown <= 0)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, towerTop.rotation);
            ProjectileController proj = projectile.GetComponent<ProjectileController>();
            proj.SetDamage(damage);
            proj.SetTargetTransform(enemyTarget);

            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }


    //FUNCTION:: Rotate the top of the tower towards the enemy Target 
    private void RotateTowerTopTowardsEnemy()
    {
        //Enemy Target Lock on
        Vector3 direction = enemyTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(towerTop.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;

        towerTop.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    //FUNCTION:: Check in an enemy within the list has been removed in another way
    private void CheckForNulledOutEnemiesWithinRange()
    {
        foreach (GameObject _enemy in enemiesInRangeList)
        {
            if (_enemy == null)
            {
                enemiesInRangeList.Remove(_enemy);
                break;
            }
        }
    }


    //FUNCTION:: Collision Functions to Handle in Range Enemies 
    private void OnTriggerEnter(Collider collision)
    {
       //Check if collision if an Enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Add to within range list 
            enemiesInRangeList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        //Check if exist collision is an enemy 
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Remove from list 
            enemiesInRangeList.Remove(collision.gameObject);
        }
    }
}
