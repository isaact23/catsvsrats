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
    [SerializeField] private Transform projectileSpawnPlace;
    [SerializeField] private DamageType damageType;
    private RatManager rManager;
    public bool hasTarget;


    // Start is called before the first frame update
    void Awake()
    {
        rManager = FindObjectOfType<RatManager>();
    }
    public void Setup(RatManager givenManager)
    {
        rManager = givenManager;
    }
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
            RatObject locatedTarget = LocateRat();

            if (locatedTarget != null)
            {
                target = locatedTarget;

            }
            

            
        }
    }

    public void SpawnProjectile()
    {
        Projectile spawnedProjectile = Instantiate(projectileAttack) as Projectile;
        spawnedProjectile.transform.position = projectileSpawnPlace.position;
        spawnedProjectile.SetDamage(damageType, attackDamage);
        spawnedProjectile.SetTarget(target);
    }
    public void InstantAttack()
    {
        if (target != null)
        {
            target.TakeDamage(damageType, attackDamage);
        }
    }
    private RatObject LocateRat()
    {
        RatObject closest = null;
        for (int i = 0; i < rManager.allRats.Count; i++) {
            if( closest == null || closest.DistFromExit() < rManager.allRats[i].DistFromExit())
            {
                closest = rManager.allRats[i];
            }
        }
        return closest;
    }
}
