using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponSettings
{
    public WeaponInfo weaponInfo;
    public int currentAmmo;
}

public class PlayerWeapon : MonoBehaviour
{
    private Transform weapon;
    private Transform weaponBarrel;
    private SpriteRenderer weaponSprite;
    private LayerMask damageLayer;

    private List<WeaponSettings> weapons;
    private WeaponSettings currentWeapon;

    private FireModeFactory fireModeFactory;
    private FireMode currentFireMode;

    private int weaponIndex;

    public void Init(Transform weapon, int startingWeaponId, LayerMask damageLayer)
    {
        this.weapon = weapon;
        weaponBarrel = weapon.GetChild(0);
        weaponSprite = weapon.GetComponent<SpriteRenderer>();
        this.damageLayer = damageLayer;

        fireModeFactory = new FireModeFactory();

        GiveNewWeaponById(startingWeaponId);
        UpdateCurrnetWeapon();
    }

    private void Update()
    {
        if (currentFireMode == null)
            return;

        currentFireMode.Update(currentWeapon);
    }

    public void GiveNewWeaponById(int weaponId)
    {
        if (weapons == null)
            weapons = new List<WeaponSettings>();

        WeaponInfo info = WeaponFactory.GetWeaponInfo(weaponId);

        if(info == null)
        {
            Debug.LogError(string.Format("Player was given a weapon with id {0}, this does not exist!", weaponId));
            return;
        }

        weapons.Add(new WeaponSettings
        {
            weaponInfo = info,
            currentAmmo = info.maxAmmo
        });
    }

    private void OnEnable()
    {
        InputController.ShootInput += ShootInput;
    }

    private void OnDisable()
    {
        InputController.ShootInput -= ShootInput;
    }

    private void ShootInput(KeyInputType inputType)
    {
        currentFireMode.Shoot(currentWeapon, weaponBarrel, damageLayer, gameObject.layer, inputType);
    }

    private void UpdateCurrnetWeapon()
    {
        currentWeapon = weapons[weaponIndex];
        currentFireMode = fireModeFactory.GetFireModeByType(currentWeapon.weaponInfo.fireType);

        if (currentWeapon.weaponInfo.weaponSprite != null)
            weaponSprite.sprite = currentWeapon.weaponInfo.weaponSprite;

        weaponIndex++;

        if (weaponIndex >= weapons.Count)
            weaponIndex = 0;
    }
}
