using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private ProjectileUI projectileUIPrefab;

    private TextMeshProUGUI ammoText;
    private int maxAmmo;

    private List<ProjectileUI> projectileImages;
    private GameObject infiniteSymbol;
    private Transform imageParent;
    private Transform unusedImageParent;

    public void Init()
    {
        ammoText = GetComponentInChildren<TextMeshProUGUI>();

        imageParent = transform.GetChild(0);

        infiniteSymbol = ammoText.transform.GetChild(0).gameObject;
        infiniteSymbol.SetActive(false);

        unusedImageParent = new GameObject().transform;
        unusedImageParent.SetParent(transform);

        projectileImages = new List<ProjectileUI>();
    }

    public void UpdateCurrentAmmo(int currentAmmo)
    {
        for (int i = 0; i < projectileImages.Count; i++)
        {
            if (projectileImages[i].Active)
            {
                projectileImages[i].ProjectileUsed();
                break;
            }
        }

        if (maxAmmo == 0)
        {
            if (infiniteSymbol.activeInHierarchy == false)
            {
                infiniteSymbol.SetActive(true);
                ammoText.text = "";
            }
            return;
        }

        if (infiniteSymbol.activeInHierarchy)
        {
            infiniteSymbol.SetActive(false);
            ammoText.gameObject.SetActive(true);
        }

        ammoText.text = string.Format("{0} / {1}", currentAmmo, maxAmmo);
    }

    public void UpdateWeapon(WeaponSettings weaponSettings)
    {
        maxAmmo = weaponSettings.weaponInfo.maxAmmo;

        UpdateCurrentAmmo(weaponSettings.currentAmmo);
        CleanupProjectileImages();

        for (int i = 0; i < weaponSettings.weaponInfo.ammoPerClip; i++)
        {
            ProjectileUI projectile = SimplePool.Spawn(projectileUIPrefab.gameObject, Vector3.zero, Quaternion.identity).GetComponent<ProjectileUI>();
            projectile.Init(weaponSettings.weaponInfo.projectileSprite, weaponSettings.weaponInfo.usedProjectileSprite);
            projectile.transform.SetParent(imageParent, false);
            projectileImages.Add(projectile);
        }
    }

    private void CleanupProjectileImages()
    {
        for (int i = projectileImages.Count - 1; i >= 0; i--)
        {
            projectileImages[i].transform.SetParent(unusedImageParent);
            SimplePool.Despawn(projectileImages[i].gameObject);
            projectileImages.RemoveAt(i);
        }
    }
}
