using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rat;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float attackRange = 3f;
    private float lifetime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        transform.localScale = Vector3.one * lifetime;
        if (lifetime < 0.4f)
        {
            Destroy(gameObject);
        }
    }
    public void Explode(float damage, DamageType damageType, RatManager rManager)
    {
        for (int i = 0; i < rManager.allRats.Count; i++)
        {
            if (Vector2.Distance(rManager.allRats[i].transform.position, transform.position) < attackRange)
            {
                rManager.allRats[i].TakeDamage(damageType, damage);
            }
        }
    }
}
