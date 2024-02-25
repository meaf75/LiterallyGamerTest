using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour {

    private readonly int hashSelectedButton = Animator.StringToHash("selected");

    public LGCharacterPuppet characterPuppet;
    public Animator[] danceButtons;

    private int selectedAnimation;

    private void Start() {
        OnSelectButton(LGConstants.DANCE_HOUSE);
    }

    public void GoToPlayScene() {
        LGGameMaster.SetupStartup(selectedAnimation);
        SceneManager.LoadScene("Game");
    }

    public void OnSelectButton(int idx) {
        selectedAnimation = idx;
        characterPuppet.PlayAnimation(idx);
        for (var i = 0; i < danceButtons.Length; i++) {
            danceButtons[i].SetBool(hashSelectedButton, i == idx);
        }
    }
    
}
