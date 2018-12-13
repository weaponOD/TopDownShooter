using System.Collections.Generic;

public static class WeaponFactory {
    private static Dictionary<int, WeaponInfo> weaponObjectById;
    private static bool IsInitialized { get { return weaponObjectById != null; } }

    private static void InitalizeFactory()
    {
        if (IsInitialized)
            return;

        WeaponList weaponList = ResourcesIndex.GetAsset<WeaponList>(1);

        weaponObjectById = new Dictionary<int, WeaponInfo>();
        for (int i = 0; i < weaponList.weaponInfo.Count; i++)
        {
            weaponObjectById.Add(weaponList.weaponInfo[i].id, weaponList.weaponInfo[i]);
        }
    }

    public static WeaponInfo GetWeaponInfo(int id)
    {
        InitalizeFactory();

        if (weaponObjectById.ContainsKey(id))
            return weaponObjectById[id];

        return null;
    }
}
