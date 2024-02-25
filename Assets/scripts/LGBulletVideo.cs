using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
using Random = UnityEngine.Random;

public class LGBulletVideo : LGBullet {

    [Header(nameof(LGBulletVideo))] 
    public string videoFileName;
    public VideoPlayer videoPlayer;
    public Transform player;
    public Collider myCollider;
    public BulletType[] subEmitters;

    private LGPoolManager poolManager;

    [Header("config")]
    public float spawnHeight = 4;
    public float pushRadius;
    public float pushForce;
    public float emitterDispersion;

    private bool finished = false;
    
    public override void Initialize() {
        finished = false;
        
        videoPlayer.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag(LGConstants.TAG_NAME_PLAYER).transform;
        poolManager = FindObjectOfType<LGPoolManager>();
        base.Initialize();
        rb.constraints = RigidbodyConstraints.None;
        myCollider.enabled = true;
        
        gameObject.SetActive(true);

    }

    private void OnCollisionEnter(Collision other) {
        myCollider.enabled = false;
        videoPlayer.transform.position = new Vector3(transform.position.x, spawnHeight, transform.position.z);
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, videoFileName);
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update() {
        transform.LookAt(player, Vector3.up);

        if (videoPlayer.isPaused && !finished) {

            finished = true;
            
            // Explode and throw away close elements
            var collisions = Physics.SphereCastAll(transform.position, pushRadius,transform.up);

            foreach (var element in collisions) {
                
                if (!element.transform.CompareTag(LGConstants.TAG_NAME_ELEMENT) && !element.transform.CompareTag(LGConstants.TAG_NAME_PLAYER)) {
                    continue;
                }

                var elementRb = element.transform.GetComponent<Rigidbody>();
                elementRb.velocity = (element.transform.position - transform.position).normalized * pushForce;
            }

            StartCoroutine(LaunchEmitters());
        }
    }

    IEnumerator LaunchEmitters() {

        yield return null;

        if (subEmitters.Length > 0) {
            yield return new WaitForSeconds(1);
            
            // Generate emitters
            foreach (var emitter in subEmitters) {
                LGBullet bullet = poolManager.GetPoolBullet(emitter);
                bullet.transform.position = videoPlayer.transform.position;
                bullet.transform.rotation = Quaternion.identity;

                float x = Random.Range(-emitterDispersion, emitterDispersion);
                float z = Random.Range(-emitterDispersion, emitterDispersion);
                
                bullet.Initialize();
                bullet.rb.velocity = new Vector3(x,4,z);

                yield return new WaitForSeconds(1);
            }   
        }
        
        gameObject.SetActive(false);
    }
}
