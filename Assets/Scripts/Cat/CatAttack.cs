using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rat;

public class CatAttack : MonoBehaviour
{
    [SerializeField] private RatObject target;
    [SerializeField] private Projectile projectileAttack;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackDamage = 1f;


    // Start is called before the first frame update
    void Start()
    {
        SpawnProjectile();
    }

    // Update is called once per frame
    void Update()
    {
        // if cat is activated
        if (target != null && Vector2.Distance(target.transform.position, transform.position) > attackRange)
        {
            target = null;
        }
        if (target == null)
        {
            // Find new target
            RatObject locatedTarget = default;

            if (locatedTarget != null)
            {
                target = locatedTarget;
            }
            

            
        }
    }

    public void SpawnProjectile()
    {
        Projectile spawnedProjectile = Instantiate(projectileAttack) as Projectile;
        spawnedProjectile.SetDamage(attackDamage);
        spawnedProjectile.SetTarget(target);
    }
    public void InstantAttack()
    {
        if (target != null)
        {
            target.TakeDamage(attackDamage);
        }
    }
}
