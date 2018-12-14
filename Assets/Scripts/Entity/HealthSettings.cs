using UnityEngine;

[CreateAssetMenu(fileName = "New Health Setting", menuName = "Entity/Health Settings")]
public class HealthSettings : ScriptableObject {
    public int maxHealth;

    [Header("Damage")]
    public GameObject damageParticle;
    public AudioClip damageSound;
    public Color damageFlashColour;

    [Header("Healing")]
    public GameObject healParticle;
    public AudioClip healSound;

    [Header("Death")]
    public GameObject deathParticle;
    public AudioClip deathSound;
}
