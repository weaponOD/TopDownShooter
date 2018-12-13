using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireModeFactory {
    public FireMode GetFireModeByType(FireType fireType)
    {
        switch (fireType)
        {
            default:
                return new StandardFireMode();
        }
    }
}
