using UnityEngine;

[System.Serializable]
public class WeaponInfo
{
    [Header("General Information")]
    public string name;
    public string info;
    public int id;

    [Header("Fire Rates")]
    public float fireRate;
    public float fireRateAuto;

    [Header("Ammo")]
    [Tooltip("If 0 unlimited")]public int maxAmmo;
    [Tooltip("If 0 max ammo is clip size")]public int ammoPerClip;
    public float reloadTime;

    [Header("Projectile Settings")]
    public Projectile projectile;
    public int projectileNumber;
    public FireType fireType; // This might make more sense in projectile

    [Header("Other Settings")]
    public Sprite weaponSprite;
    public AudioClip fireSound;
    public GameObject fireParticle;
}
