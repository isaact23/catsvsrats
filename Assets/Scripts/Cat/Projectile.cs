using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rat;

public class Projectile : MonoBehaviour
{
    private RatObject target;
    [SerializeField] private float flightSpeed = 1f;
    [SerializeField] private float attackDamage = 1f;
    [SerializeField] private DamageType damageType;
    [SerializeField] private Explosion explode;
    private RatManager rManager;
    [SerializeField] private bool rotatesForward = false;
    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        float moveDistance = flightSpeed * Time.deltaTime;
        Vector3 moveDirection = target.transform.position - transform.position;
        transform.position = transform.position + moveDirection.normalized * moveDistance;
        if (rotatesForward)
        {
            var newSet = new Quaternion();
            newSet.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg);
            transform.rotation = newSet;
        }

        if (Vector2.Dot(moveDirection, target.transform.position - transform.position) < 0)
        {
            // Target Reached
            if (explode != null)
            {
                Explosion created = Instantiate(explode, target.transform.position, Quaternion.identity) as Explosion;
                created.Explode(attackDamage, damageType, rManager);
            }
            else
            {
                target.TakeDamage(damageType, attackDamage);
            }
            
            Destroy(gameObject);
        }
        
    }

    public void SetDamage(DamageType damageType, float damage)
    {
        this.damageType = damageType;
        this.attackDamage = damage;
    }
    public void SetTarget(RatObject target)
    {
        this.target = target;
    }
    public void SetManager(RatManager givenManager)
    {
        rManager = givenManager;
    }
}
