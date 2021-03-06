﻿using UnityEngine;

[System.Serializable]
public abstract class FireMode
{
    protected bool canShoot;
    protected bool shouldReset;

    protected float shootCount;
    protected int maxAmmo;
    protected int currentAmmo;
    protected int currentClip;
    protected int ammoPerClip;

    private bool initialized;

    protected bool reloading;
    protected float reloadCount;

    public FireMode()
    {
        canShoot = true;
    }

    public void Update(WeaponSettings weaponSettings, AmmoUI ammoUI)
    {
        UpdateShootReset(weaponSettings);
        UpdateReload(weaponSettings, ammoUI);
    }

    public void Shoot(WeaponSettings weaponSettings, Transform weaponBarrel, LayerMask damageLayer, LayerMask firedByLayer, KeyInputType inputType, AmmoUI ammoUI)
    {
        switch (inputType)
        {
            case KeyInputType.Down:
                OnInputDown(weaponSettings, weaponBarrel, damageLayer, firedByLayer, ammoUI);
                break;
            case KeyInputType.Held:
                OnInputHeld(weaponSettings, weaponBarrel, damageLayer, firedByLayer, ammoUI);
                break;
            case KeyInputType.Up:
                OnInputUp(weaponSettings);
                break;
            default:
                break;
        }
    }

    protected virtual void SpawnProjectile(WeaponSettings weaponSettings, Transform weaponBarrel, LayerMask damageLayer, LayerMask firedByLayer)
    {
        Projectile p = SimplePool.Spawn(weaponSettings.weaponInfo.projectile.gameObject, weaponBarrel.position, weaponBarrel.rotation).GetComponent<Projectile>();
        p.Init(damageLayer, firedByLayer);
    }

    protected virtual void OnInputDown(WeaponSettings weaponSettings, Transform weaponBarrel, LayerMask damageLayer, LayerMask firedByLayer, AmmoUI ammoUI)
    {
        if (initialized == false)
            Init(weaponSettings);
    }

    protected virtual void OnInputHeld(WeaponSettings weaponSettings, Transform weaponBarrel, LayerMask damageLayer, LayerMask firedByLayer, AmmoUI ammoUI) { }

    protected virtual void OnInputUp(WeaponSettings weaponSettings)
    {
        if (shouldReset == true)
            return;

        shootCount = 0f;
        shouldReset = true;
    }

    protected virtual void UpdateShootReset(WeaponSettings weaponSettings)
    {
        if (canShoot == true || shouldReset == false || reloading)
            return;

        if (shootCount < weaponSettings.weaponInfo.fireRate)
        {
            shootCount += Time.deltaTime;
            return;
        }

        shootCount = 0f;
        canShoot = true;
        shouldReset = false;
    }

    public virtual void Reload()
    {
        if (currentClip == ammoPerClip)
            return;

        reloading = true;
        canShoot = false;
    }

    protected virtual void UpdateReload(WeaponSettings weaponSettings, AmmoUI ammoUI)
    {
        if (reloading == false)
            return;

        if(reloadCount < weaponSettings.weaponInfo.reloadTime)
        {
            reloadCount += Time.deltaTime;
            return;
        }

        reloading = false;
        reloadCount = 0f;
        canShoot = true;

        if (currentAmmo < ammoPerClip && maxAmmo != 0)
            currentClip = currentAmmo;
        else
            currentClip = ammoPerClip;

        ammoUI.Reloaded();
    }

    private void Init(WeaponSettings weaponSettings)
    {
        initialized = true;
        maxAmmo = weaponSettings.weaponInfo.maxAmmo;
        currentAmmo = weaponSettings.currentAmmo;
        ammoPerClip = weaponSettings.weaponInfo.ammoPerClip;

        if (currentAmmo < ammoPerClip && maxAmmo != 0)
            currentClip = currentAmmo;
        else
            currentClip = ammoPerClip;
    }
}
