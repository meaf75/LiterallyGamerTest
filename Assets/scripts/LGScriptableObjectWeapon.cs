using UnityEngine;

public enum BulletType {
    PARABOLIC = 0,
    BLACK_HOLE = 1,
    FREE = 2,
    FREE_CAT = 3,
    FREE_DUCK = 4,
    FREE_NOBODY = 5,
    FREE_ME = 6,
    FREE_DRAGON = 7,
}

[CreateAssetMenu(fileName = "LGWeapon", menuName = "ScriptableObjects/LGScriptableObjectWeapon", order = 1)]
public class LGScriptableObjectWeapon : ScriptableObject {
    public BulletType bulletType;
    public Color primaryColor;
    public Color secondaryColor;
    public float shootForce;
}
