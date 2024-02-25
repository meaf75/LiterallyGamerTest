using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LGInteractableElement : MonoBehaviour {
    public UnityEvent onInteract;

    public void Interact() {
        onInteract.Invoke();
    }
}
