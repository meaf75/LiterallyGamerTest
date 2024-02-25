using UnityEngine;

public class LGBulletParabolic : LGBullet {

    [Header(nameof(LGBulletParabolic))]
    public float lifetime;
    public float postHitLifetime;
    
    public float currentLifetime;

    public override void Initialize() {
        base.Initialize();
        currentLifetime = lifetime;
        gameObject.SetActive(true);
    }

    private void Update() {
        currentLifetime -= Time.deltaTime;

        if (currentLifetime <= 0) {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other) {
        currentLifetime = postHitLifetime;
    }
}
