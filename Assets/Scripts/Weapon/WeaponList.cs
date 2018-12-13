using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon Lis", menuName = "Weapon System/Weapon")]
public class WeaponList : ScriptableObject {
    public List<WeaponInfo> weaponInfo;
}
