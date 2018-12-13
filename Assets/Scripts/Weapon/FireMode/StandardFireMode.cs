using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardFireMode : FireMode
{
    protected override void OnInputDown(WeaponSettings weaponSettings, Transform weaponBarrel, LayerMask damageLayer)
    {
        if (canShoot == false)
            return;

        canShoot = false;

        if(weaponSettings.weaponInfo.projectile == null)
        {
            Debug.LogWarning("Trying to shoot a weapon with no projectile!");
            return;
        }

        SpawnProjectile(weaponSettings, weaponBarrel, damageLayer);
    }
    
    protected override void OnInputHeld(WeaponSettings weaponSettings, Transform weaponBarrel, LayerMask damageLayer)
    {
        if (shouldReset == true)
            return;

        if (shootCount < weaponSettings.weaponInfo.fireRateAuto)
        {
            shootCount += Time.deltaTime;
            return;
        }

        shootCount = 0f;
        SpawnProjectile(weaponSettings, weaponBarrel, damageLayer);
    }
}
