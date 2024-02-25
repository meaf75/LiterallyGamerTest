using System;
using UnityEngine;

public class LGGameMaster : MonoBehaviour {
    public LGCharacterPuppet puppet;
    public LGPlayerController playerController;
    public LGPoolManager poolManager;
    public UIGame uiGame;
    public LGWeaponPillar[] weaponPillars;

    private static int puppetAnimation = LGConstants.DANCE_HOUSE;

    public static void SetupStartup(int animationIdx) {
        puppetAnimation = animationIdx;
    }

    private void Start() {
        uiGame.Initialize(this);
        puppet.PlayAnimation(puppetAnimation);

        foreach (var pillar in weaponPillars) {
            pillar.Initialize(this);
        }
    }
}
