using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LGWeaponBlackHole : LGWeapon {
    
    [Header("References")] 
    public LineRenderer lr;
    
    [Header("Trajectory stuff")] 
    public int numPoints = 50;
    public float timeBetweenPoints = 0.1f;
    public LayerMask bulletMask;
    
    public override void Shoot() {
        LGBullet bullet = poolManager.GetPoolBullet(config.bulletType);
        bullet.transform.position = bulletSource.position;
        bullet.transform.rotation = Quaternion.Euler(bulletSource.eulerAngles);
        
        bullet.Initialize();
        
        bullet.rb.velocity = bullet.transform.forward * config.shootForce;
    }

    private void Update() {
        
        if (isDemoWeapon)
            return;
        
        var path = CalculateBulletPath(numPoints, timeBetweenPoints, 0, bulletSource, config.shootForce,
            bulletMask);
        lr.positionCount = path.Length;
        lr.SetPositions(path);
    }
}
