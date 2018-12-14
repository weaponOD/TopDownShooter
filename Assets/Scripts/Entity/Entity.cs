using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    [Header("Health Settings")]
    [SerializeField] private HealthSettings healthSettings;

    public Health Health { get { return health; } }

    protected Health health;

    public virtual void Init() {
        health = new Health(healthSettings.maxHealth, this);
    }

    public virtual void Damaged(int damage)
    {
        health.Damage(damage);

        // Do damage flash

        if (healthSettings.damageParticle)
            SimplePool.Spawn(healthSettings.damageParticle, transform.position, Quaternion.identity);
        if (healthSettings.damageSound)
            AudioSource.PlayClipAtPoint(healthSettings.damageSound, transform.position);
    }

    public virtual void Healed()
    {
        if (healthSettings.healParticle)
            SimplePool.Spawn(healthSettings.healParticle, transform.position, Quaternion.identity);
        if (healthSettings.healSound)
            AudioSource.PlayClipAtPoint(healthSettings.healSound, transform.position);
    }

    public virtual void Died()
    {
        if (healthSettings.deathParticle)
            SimplePool.Spawn(healthSettings.deathParticle, transform.position, Quaternion.identity);
        if (healthSettings.deathSound)
            AudioSource.PlayClipAtPoint(healthSettings.deathSound, transform.position);
    }
}
