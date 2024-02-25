using System;
using System.Collections.Generic;
using UnityEngine;



public class LGPoolManager : MonoBehaviour {
    
    [Serializable]
    public class BulletPool {
        public BulletType type;
        public LGBullet prefab;

        public List<LGBullet> generatedBullets;
    }

    public BulletPool[] bullets;
    
    public LGBullet GetPoolBullet(BulletType type) {
        int poolIdx = Array.FindIndex(bullets, b => b.type == type);

        if (poolIdx == -1) {
            Debug.LogError($"Bullet [{type}] not found");
            return null;
        }

        var pool = bullets[poolIdx];
        LGBullet freeBullet = pool.generatedBullets.Find(b => !b.gameObject.activeSelf);

        if (!freeBullet) {
            freeBullet = Instantiate(pool.prefab, transform);
            pool.generatedBullets.Add(freeBullet);
        }

        return freeBullet;
    }
}
