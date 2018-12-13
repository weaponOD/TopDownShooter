using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
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
        lifeCount = 0f;
    }

    protected virtual void Update()
    {
        UpdateLifetime();
        UpdatePosition();
    }

    protected virtual void UpdateLifetime()
    {
        if (lifetime == 0)
            return;

        if (lifeCount < lifetime)
        {
            lifeCount += Time.deltaTime;
            return;
        }

        if (lifeOverParticle)
            SimplePool.Spawn(lifeOverParticle, transform.position, Quaternion.identity);

        Remove();
    }

    protected virtual void UpdatePosition()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;
    }

    protected virtual void Remove()
    {
        SimplePool.Despawn(gameObject);
    }

    protected virtual void HitTarget()
    {
        // Do damage stuff here
        if (impactSound)
            AudioSource.PlayClipAtPoint(impactSound, transform.position);

        if (impactParticle)
            SimplePool.Spawn(impactParticle, transform.position, Quaternion.identity);
        Remove();
    }
}
