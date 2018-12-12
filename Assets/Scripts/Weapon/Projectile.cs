using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [Header("General Settings")]
    [SerializeField] protected float damage;
    [SerializeField] protected float moveSpeed;

    [Header("Lifetime Settings")]
    [SerializeField] protected float lifetime;
    [SerializeField] protected GameObject lifeOverParticle;

    [Header("Impact Settings")]
    [SerializeField] protected GameObject impactParticle;
    [SerializeField] protected AudioClip impactSound;

    protected float lifeCount;
    protected LayerMask damageLayer;

    public void Init(LayerMask damageLayer)
    {
        this.damageLayer = damageLayer;
    }

    protected virtual void Update()
    {
        UpdateLifetime();
    }

    protected virtual void UpdateLifetime()
    {
        if (lifetime == 0)
            return;

        if(lifeCount < lifetime)
        {
            lifeCount += Time.deltaTime;
            return;
        }

        SimplePool.Spawn(lifeOverParticle, transform.position, Quaternion.identity);
        Remove();
    }

    protected virtual void Remove()
    {
        SimplePool.Despawn(gameObject);
    }

    protected virtual void HitTarget()
    {
        // Do damage stuff here
        AudioSource.PlayClipAtPoint(impactSound, transform.position);
        SimplePool.Spawn(impactParticle, transform.position, Quaternion.identity);
        Remove();
    }
}
