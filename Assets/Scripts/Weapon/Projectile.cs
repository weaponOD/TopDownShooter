using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] protected int damage;
    [SerializeField] protected float moveSpeed;

    [Header("Lifetime Settings")]
    [SerializeField] protected float lifetime;
    [SerializeField] protected GameObject lifeOverParticle;

    [Header("Impact Settings")]
    [SerializeField] protected GameObject damageHitPartile;
    [SerializeField] protected AudioClip damageAudio;
    [SerializeField] protected GameObject impactParticle;
    [SerializeField] protected AudioClip impactAudio;

    protected float lifeCount;
    protected LayerMask damageLayer;
    protected LayerMask firedByLayer;

    public void Init(LayerMask damageLayer, LayerMask firedByLayer)
    {
        this.damageLayer = damageLayer;
        this.firedByLayer = firedByLayer;

        lifeCount = 0f;

        GetComponent<Rigidbody2D>().isKinematic = true;
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

    protected virtual void Impacted(bool applyDamage, GameObject hitObject)
    {
        if (applyDamage)
        {
            Entity hitEntity = hitObject.GetComponent<Entity>();
            hitEntity.Damaged(damage);
        }


        if (applyDamage && damageAudio || applyDamage == false && impactAudio)
            AudioSource.PlayClipAtPoint(applyDamage ? damageAudio : impactAudio, transform.position);

        if (applyDamage && damageHitPartile || applyDamage == false && impactParticle)
            SimplePool.Spawn(applyDamage ? damageHitPartile : impactParticle, transform.position, Quaternion.identity);

        Remove();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer != firedByLayer)
            Impacted(damageLayer == (damageLayer | (1 << other.gameObject.layer)), other.gameObject);
    }
}
