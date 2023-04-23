using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    [SerializeField] private GameObject impact_ParticleEffect;


    private int damage;
    private float fireSpeed = 10.0f;

    private Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        FindDirection();

    }


    private void FindDirection()
    {
        Vector3 dir = targetTransform.position - transform.position;
        //float distanceThisFrame 

        transform.Translate(dir.normalized * fireSpeed * Time.deltaTime, Space.World);
    }

    private void MoveProjectile()
    {
        transform.position += transform.forward * fireSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);

            GameObject particleEff = Instantiate(impact_ParticleEffect, transform.position, transform.rotation);


            Destroy(particleEff, 2f);
            Destroy(gameObject);
        }
    }

    //Damage Getters and Setters
    public void SetDamage(int _damage)
    {
        damage = _damage;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void SetTargetTransform(Transform _transform)
    {
        targetTransform = _transform;
    }

}
