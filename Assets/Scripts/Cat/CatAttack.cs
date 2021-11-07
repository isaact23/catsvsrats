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
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip hitSound;
    private AudioSource audioSource;
    private Animator anim;
    private RatManager rManager;
    public bool hasTarget;


    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rManager = FindObjectOfType<RatManager>();
        anim = GetComponent<Animator>();
    }

    public void Setup(RatManager givenManager)
    {
        rManager = givenManager;
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
        anim.SetBool("IsAttacking", (target != null));
    }

    public void SpawnProjectile()
    {
        if (shootSound != null) {
            audioSource.clip = shootSound;
            audioSource.Play();
        }
        Projectile spawnedProjectile = Instantiate(projectileAttack) as Projectile;
        spawnedProjectile.transform.position = projectileSpawnPlace.position;
        spawnedProjectile.SetDamage(damageType, attackDamage);
        spawnedProjectile.SetTarget(target);
        spawnedProjectile.SetManager(rManager);
        spawnedProjectile.SetHitSound(hitSound);
    }
    public void InstantAttack()
    {
        if (target != null)
        {
            target.TakeDamage(damageType, attackDamage);
            if (hitSound != null) {
                audioSource.clip = hitSound;
                audioSource.Play();
            }
        }
    }
    private RatObject LocateRat()
    {
        RatObject closest = null;
        for (int i = 0; i < rManager.allRats.Count; i++) {
            if (Vector2.Distance(rManager.allRats[i].transform.position, transform.position) < attackRange)
            {
                if (closest == null || closest.DistFromExit() < rManager.allRats[i].DistFromExit())
                {
                    closest = rManager.allRats[i];
                }
            }
   
        }
        return closest;
    }
}
