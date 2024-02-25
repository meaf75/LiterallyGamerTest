using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LGBulletBlackHole : LGBullet
{

    [Serializable]
    public struct AbsorbedElement {
        public Rigidbody element;
        public Vector3 startPosition;
        public Vector3 endLocalPosition;
    }
    
    [Header(nameof(LGBulletBlackHole))]
    public float lifetime;
    public float currentLifetime;
    public float absorbRadius;
    public float moveElementsDuration;
    public float rotationDuration;
    public float pushForceOnDead;

    [Header("references")] 
    public Animator animator;
    public Collider myCollider;
    public GameObject explosionRadiusContainer;

    private bool hitSomething = false;
    private int animatorLayer = 0;
    private readonly int animatorHashBulletBlackHoleExpand = Animator.StringToHash("BulletBlackHole-expand");
    private readonly int animatorHashBulletBlackHoleEnd = Animator.StringToHash("BulletBlackHole-end");

    public override void Initialize() {
        base.Initialize();
        currentLifetime = lifetime;
        
        rb.isKinematic = false;
        
        hitSomething = false;
        myCollider.enabled = true;
        explosionRadiusContainer.SetActive(false);
        gameObject.SetActive(true);
    }

    private void Update() {
        currentLifetime -= Time.deltaTime;
        
        if (currentLifetime <= 0) {
            gameObject.SetActive(false);
        }        
    }

    private void OnCollisionEnter(Collision other) {

        if (hitSomething) {
            return;
        }

        hitSomething = true;
        
        currentLifetime = Mathf.Infinity;
        myCollider.enabled = false;
        rb.velocity = rb.angularVelocity = Vector3.zero;
        StartCoroutine(ExplodeRoutine());
    }

    public List<AbsorbedElement> obstaclesTargets;
    
    IEnumerator ExplodeRoutine() {
        explosionRadiusContainer.SetActive(true);
        animator.Play(animatorHashBulletBlackHoleExpand, animatorLayer, 0f);
        Vector3 origin = transform.position;

        yield return new WaitForSeconds(.2f);

        var collisions = Physics.SphereCastAll(origin, absorbRadius,transform.up);

        
        obstaclesTargets = new List<AbsorbedElement>();

        // ##### Phase 1, Define random positions inside the explosion container for each collided element #####
        float minY = origin.y + -(absorbRadius / 2);

        if (minY < 0) {
            minY = 0; // Make the minimum position to the ground
        }

        float range = 2;

        Vector2 xRange = new Vector2(origin.x - absorbRadius / range, origin.x + absorbRadius / range);
        Vector2 yRange = new Vector2(minY, origin.y + absorbRadius / range);
        Vector2 zRange = new Vector2(origin.z - absorbRadius / range, origin.z + absorbRadius / range);
        
        foreach (var element in collisions) {
            if (!element.transform.CompareTag(LGConstants.TAG_NAME_ELEMENT) && !element.transform.CompareTag(LGConstants.TAG_NAME_PLAYER)) {
                continue;
            }
            
            Vector3 targetPosition = Vector3.zero;

            targetPosition.x = Random.Range(xRange.x, xRange.y);
            targetPosition.y = Random.Range(yRange.x, yRange.y);
            targetPosition.z = Random.Range(zRange.x, zRange.y);
            
            obstaclesTargets.Add(new AbsorbedElement() {
                element = element.transform.GetComponent<Rigidbody>(),
                startPosition = element.transform.position,
                endLocalPosition = explosionRadiusContainer.transform.InverseTransformPoint(targetPosition)
            });
        }

        // ##### Phase 2, Move the elements to the random position set #####
        float t = 0;

        while (t  <= moveElementsDuration) {
            
            t += Time.deltaTime;
            yield return null;

            foreach (var target in obstaclesTargets) {
                target.element.transform.position = Vector3.Lerp(
                    target.startPosition,
                    explosionRadiusContainer.transform.TransformPoint(target.endLocalPosition),
                    t / moveElementsDuration
                );
            }
            
        }

        // ##### Phase 3, The elements are now in place, rotate for a while #####
        t = 0;

        while (t <= rotationDuration) {
            
            foreach (var target in obstaclesTargets) {
                target.element.transform.position = explosionRadiusContainer.transform.TransformPoint(target.endLocalPosition);
            }
            
            yield return null;
            t += Time.deltaTime;
        }
        
        // ##### Phase 4, Explode!! #####
        t = 0;
        
        animator.Play(animatorHashBulletBlackHoleEnd);
        
        while (t <= .55f) {
            
            foreach (var target in obstaclesTargets) {
                target.element.transform.position = explosionRadiusContainer.transform.TransformPoint(target.endLocalPosition);
            }
            
            yield return null;
            t += Time.deltaTime;
        }
        
        foreach (var target in obstaclesTargets) {
            target.element.velocity = (target.element.position - transform.position).normalized * pushForceOnDead;
        }

        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, absorbRadius);

        foreach (var el in obstaclesTargets) {
            Gizmos.DrawCube(explosionRadiusContainer.transform.TransformPoint(el.endLocalPosition),Vector3.one);
        }
    }
}
