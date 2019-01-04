using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardFireMode : FireMode
{
    protected override void OnInputDown(WeaponSettings weaponSettings, Transform weaponBarrel, LayerMask damageLayer, LayerMask firedByLayer, AmmoUI ammoUI)
    {
        base.OnInputDown(weaponSettings, weaponBarrel, damageLayer, firedByLayer, ammoUI);
        if (canShoot == false)
            return;

        if (weaponSettings.weaponInfo.projectile == null)
        {
            Debug.LogWarning("Trying to shoot a weapon with no projectile!");
            return;
        }

        // If we have ammo or if we are using a weapon with unlimited ammo
        if (currentAmmo > 0 || currentAmmo >= 0 && maxAmmo == 0)
        {
            if(currentClip > 0)
            {
                canShoot = false;

                currentClip--;

                if (currentAmmo > 0)
                    currentAmmo--;

                ammoUI.UpdateCurrentAmmo(currentClip);
                SpawnProjectile(weaponSettings, weaponBarrel, damageLayer, firedByLayer);
            }
            else
            {
                Reload();
            }
        }
    }

    protected override void OnInputHeld(WeaponSettings weaponSettings, Transform weaponBarrel, LayerMask damageLayer, LayerMask firedByLayer, AmmoUI ammoUI)
    {
        if (shouldReset == true)
            return;

        if (shootCount < weaponSettings.weaponInfo.fireRateAuto)
        {
            shootCount += Time.deltaTime;
            return;
        }

        shootCount = 0f;

        // If we have ammo or if we are using a weapon with unlimited ammo
        if (currentAmmo > 0 || currentAmmo >= 0 && maxAmmo == 0)
        {
            if (currentClip > 0)
            {
                canShoot = false;

                currentClip--;

                if (currentAmmo > 0)
                    currentAmmo--;

                ammoUI.UpdateCurrentAmmo(currentClip);
                SpawnProjectile(weaponSettings, weaponBarrel, damageLayer, firedByLayer);
            }
        }
    }
}
