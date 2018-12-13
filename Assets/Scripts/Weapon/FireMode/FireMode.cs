using UnityEngine;

[System.Serializable]
public abstract class FireMode {
    protected bool canShoot;
    protected bool shouldReset;

    protected float shootCount;

    public FireMode()
    {
        canShoot = true;
    }

    public void Update(WeaponSettings weaponSettings)
    {
        UpdateShootReset(weaponSettings);
    }

    public void Shoot(WeaponSettings weaponSettings, Transform weaponBarrel, LayerMask damageLayer, KeyInputType inputType)
    {
        switch (inputType)
        {
            case KeyInputType.Down:
                OnInputDown(weaponSettings, weaponBarrel, damageLayer);
                break;
            case KeyInputType.Held:
                OnInputHeld(weaponSettings, weaponBarrel, damageLayer);
                break;
            case KeyInputType.Up:
                OnInputUp(weaponSettings);
                break;
            default:
                break;
        }
    }

    protected virtual void SpawnProjectile(WeaponSettings weaponSettings, Transform weaponBarrel, LayerMask damageLayer)
    {
        Projectile p = SimplePool.Spawn(weaponSettings.weaponInfo.projectile.gameObject, weaponBarrel.position, weaponBarrel.rotation).GetComponent<Projectile>();
        p.Init(damageLayer);
    }

    protected virtual void OnInputDown(WeaponSettings weaponSettings, Transform weaponBarrel, LayerMask damageLayer) { }

    protected virtual void OnInputHeld(WeaponSettings weaponSettings, Transform weaponBarrel, LayerMask damageLayer) { }

    protected virtual void OnInputUp(WeaponSettings weaponSettings) {
        if (shouldReset == true)
            return;

        shootCount = 0f;
        shouldReset = true;
    }

    protected virtual void UpdateShootReset(WeaponSettings weaponSettings)
    {
        if (canShoot == true || shouldReset == false)
            return;

        if(shootCount < weaponSettings.weaponInfo.fireRate)
        {
            shootCount += Time.deltaTime;
            return;
        }

        shootCount = 0f;
        canShoot = true;
        shouldReset = false;
    }
}
