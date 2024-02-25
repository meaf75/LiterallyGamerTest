using UnityEngine;

public class LGBullet : MonoBehaviour {
    
    [Header(nameof(LGBullet))]
    public Rigidbody rb;
    public TrailRenderer tr;

    public virtual void Initialize() {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        tr.Clear();
    }
    
    private void OnValidate() {
        if (!rb) {
            rb = GetComponent<Rigidbody>();
        }
        
        if (!tr) {
            tr = GetComponent<TrailRenderer>();
        }
    }
}
