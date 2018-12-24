using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] protected HealthSettings healthSettings;

    [Header("Sprites")]
    [SerializeField] protected Transform sprites;

    public Health Health { get { return health; } }

    protected Health health;

    private bool flashing;
    private List<SpriteRenderer> spriteRenderers;

    public virtual void Init()
    {
        health = new Health(healthSettings.maxHealth, this);

        spriteRenderers = new List<SpriteRenderer>();
        GetComponentsInChildren(spriteRenderers);
    }

    public virtual void Damaged(int amount)
    {
        health.Damage(amount);

        if (flashing == false)
            StartCoroutine(DamageFlash());

        if (healthSettings.damageParticle)
            SimplePool.Spawn(healthSettings.damageParticle, transform.position, Quaternion.identity);
        if (healthSettings.damageSound)
            AudioSource.PlayClipAtPoint(healthSettings.damageSound, transform.position);
    }

    public virtual void Healed(int amount)
    {
        health.Heal(amount);

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

    protected IEnumerator DamageFlash()
    {
        flashing = true;

        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            Color colour = new Color(healthSettings.damageFlashColour.r, healthSettings.damageFlashColour.g,
                healthSettings.damageFlashColour.a, spriteRenderers[i].color.a);
            spriteRenderers[i].color = colour;
        }

        yield return new WaitForSeconds(healthSettings.damageFlashTime);

        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            Color colour = new Color(1, 1, 1, spriteRenderers[i].color.a);
            spriteRenderers[i].color = colour;
        }

        flashing = false;
    }
}
