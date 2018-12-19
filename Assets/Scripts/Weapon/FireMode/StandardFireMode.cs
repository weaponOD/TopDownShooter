using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardFireMode : FireMode
{
    protected override void OnInputDown(WeaponSettings weaponSettings, Transform weaponBarrel, LayerMask damageLayer, LayerMask firedByLayer)
    {
        base.OnInputDown(weaponSettings, weaponBarrel, damageLayer, firedByLayer);
        if (canShoot == false)
            return;

        canShoot = false;

        if(weaponSettings.weaponInfo.projectile == null)
        {
            Debug.LogWarning("Trying to shoot a weapon with no projectile!");
            return;
        }

        SpawnProjectile(weaponSettings, weaponBarrel, damageLayer, firedByLayer);
    }
    
    protected override void OnInputHeld(WeaponSettings weaponSettings, Transform weaponBarrel, LayerMask damageLayer, LayerMask firedByLayer)
    {
        if (shouldReset == true)
            return;

        if (shootCount < weaponSettings.weaponInfo.fireRateAuto)
        {
            shootCount += Time.deltaTime;
            return;
        }

        shootCount = 0f;
        SpawnProjectile(weaponSettings, weaponBarrel, damageLayer, firedByLayer);
    }
}
