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
    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = flightSpeed * Time.deltaTime;
        Vector3 moveDirection = target.transform.position - transform.position;
        transform.Translate(moveDirection.normalized * moveDistance);
        if (Vector2.Dot(moveDirection, target.transform.position - transform.position) < 0)
        {
            // Target Reached
            target.TakeDamage(damageType, attackDamage);
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
}
