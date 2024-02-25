using TMPro;
using UnityEngine;

public class LGWeaponPillar : MonoBehaviour {
    
    [Header("References")]
    public int weaponId;
    public Transform weaponContainer;
    public LGScriptableObjectWeapons scriptableWeapons;
    public TextMeshPro txtWeaponName;

    private LGWeapon weapon;
    private LGGameMaster gm;

    public void Initialize(LGGameMaster gameMaster) {
        gm = gameMaster;

        LGScriptableObjectWeapons.WeaponDefinition weaponData = scriptableWeapons.weapons[weaponId];
        weapon = Instantiate(weaponData.prefab, weaponContainer);
        weapon.transform.localPosition = Vector3.zero;
        weapon.isDemoWeapon = true;
        weapon.Initialize(gm.poolManager);
        txtWeaponName.text = weaponData.name;
    }

    public void SetPlayerWeapon() {
        gm.playerController.AssignWeapon(weaponId);
    }
}
