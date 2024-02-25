using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGame : MonoBehaviour {

    public LGGameMaster gm;
    public GameObject txtPressToInteract;

    public void Initialize(LGGameMaster _gm) {
        gm = _gm;
    }

    private void Update() {
        if (!gm) {
            return;
        }

        txtPressToInteract.SetActive(gm.playerController.interactiveHits > 0);
    }
}
