using UnityEngine;

public class LGPlayerController : MonoBehaviour {

    [Header("References")] 
    public Rigidbody rb;
    public Transform weaponContainer;
    public LGPoolManager poolManager;
    public Camera mainCamera;
    public LGScriptableObjectWeapons scriptableWeapons;
    
    private LGWeapon weapon;
    
    [Header("Config")]
    public Vector2 cameraSensitive;
    public float movementSensitive;
    public bool processInput;

    [Space] 
    public LayerMask interactiveMask; 
    public float interactiveRange;

    public int interactiveHits;
    public RaycastHit[] interactiveElements;
    
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;

        if (!weapon) {
            weapon = GetComponentInChildren<LGWeapon>();
        }
        
        processInput = true;
        interactiveElements = new RaycastHit[1];
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            processInput = false;
            Cursor.lockState = CursorLockMode.None;
        }

        if (!processInput) {
            return;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && weapon) {
            weapon.Shoot();
        }
        
        HandleViewInput();
        HandleInteractive();
    }

    private void FixedUpdate() {
        HandleMovement();
    }

    private void HandleInteractive() {
        interactiveHits = Physics.RaycastNonAlloc(mainCamera.transform.position, transform.forward, interactiveElements, interactiveRange, interactiveMask);

        if (interactiveHits == 0) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            LGInteractableElement interactable;
            foreach (var element in interactiveElements) {
                if (element.transform && element.transform.TryGetComponent(out interactable)) {
                    interactable.Interact();
                }
            }
        }
    }

    private void HandleViewInput() {

        if (!Application.isFocused) {
            return;
        }
        
        float h = Input.GetAxisRaw("Mouse X");
        float v = Input.GetAxisRaw("Mouse Y");

        if (h + v == 0)
            return;
        
        var nextRotationAngles = transform.rotation.eulerAngles;
        nextRotationAngles.y += h * cameraSensitive.x * Time.deltaTime;  // Move horizontal
        nextRotationAngles.x += -v * cameraSensitive.y * Time.deltaTime;  // Move vertical
        
        transform.rotation = Quaternion.Euler(nextRotationAngles.x, nextRotationAngles.y, nextRotationAngles.z);
    }

    private void HandleMovement() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h == 0 && v == 0) {
            return;
        }

        Vector3 nextDirection = transform.forward * v + transform.right * h;
        rb.velocity += nextDirection * (Time.deltaTime * movementSensitive);
    }

    public void AssignWeapon(int weaponId) {
        if (weapon) {
            if (weapon.id == weaponId) {
                return;
            }
            
            Destroy(weapon.gameObject);
        }

        weapon = Instantiate(scriptableWeapons.weapons[weaponId].prefab, weaponContainer);
        weapon.transform.localPosition = Vector3.zero;
        weapon.Initialize(poolManager);
    }
    
    private void OnApplicationFocus(bool hasFocus) {
        processInput = hasFocus;
        Cursor.lockState = hasFocus ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(mainCamera.transform.position, (mainCamera.transform.forward * interactiveRange) + mainCamera.transform.position);
    }
}
