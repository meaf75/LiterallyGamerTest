using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LGCharacterPuppet : MonoBehaviour {

    public Animator animator;
    
    private readonly int hashHouseDance = Animator.StringToHash("House Dancing");
    private readonly int hashWaveHipHop = Animator.StringToHash("Wave Hip Hop Dance");
    private readonly int hashMacarena = Animator.StringToHash("Macarena Dance");

    public void PlayAnimation(int id) {
        if (id == LGConstants.DANCE_HOUSE) {
            animator.Play(hashHouseDance,-1, 0);
        }else if (id == LGConstants.DANCE_WAVE_HIP_HIP) {
            animator.Play(hashWaveHipHop,-1, 0);
        } else if (id == LGConstants.DANCE_MACARENA) {
            animator.Play(hashMacarena,-1, 0);
        } else {
            Debug.LogError($"[{id}] animation not found");
        }
    }
}
