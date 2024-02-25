using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LGWeapons", menuName = "ScriptableObjects/ScriptableWeapons", order = 1)]
public class LGScriptableObjectWeapons : ScriptableObject {
    [Serializable]
    public class WeaponDefinition {
        public string name;
        [Disabled]
        public int id;
        public LGWeapon prefab;
    }

    public WeaponDefinition[] weapons;

#if UNITY_EDITOR
    private void OnValidate() {
        for (var i = 0; i < weapons.Length; i++) {
            var w = weapons[i];
            
            if (!w.prefab || (w.prefab.id == i && weapons[i].id == i)) 
                continue;
            
            weapons[i].id = w.prefab.id = i;
            UnityEditor.EditorUtility.SetDirty(w.prefab);
        }
    }
#endif
}
