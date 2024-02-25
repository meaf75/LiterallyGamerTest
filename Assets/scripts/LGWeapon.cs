using System.Collections.Generic;
using UnityEngine;

public class LGWeapon : MonoBehaviour {
    [Header("References")] 
    [Space,Disabled] 
    public int id;
    
    public Transform bulletSource;
    public MeshRenderer mrPrimary;
    public MeshRenderer mrSecondary;

    internal LGPoolManager poolManager;
    
    [Header("Extra")]
    public LGScriptableObjectWeapon config;
    public bool isDemoWeapon;

    public void Initialize(LGPoolManager _poolManager) {
        poolManager = _poolManager;
        ReplaceMeshColors(mrPrimary, config.primaryColor);
        ReplaceMeshColors(mrSecondary, config.secondaryColor);
    }
    
    internal void ReplaceMeshColors(MeshRenderer mr, Color color) {
        Material[] materials = new Material[mr.materials.Length];

        for (int i = 0; i < mr.materials.Length; i++) {
            materials[i] = new Material(mr.materials[i]) {
                color = color
            };
        }

        mr.materials = materials;
    }

    public Vector3[] CalculateBulletPath(int numPoints, float timeBetweenPoints, float gravity, Transform source, float shootForce, LayerMask layerMask) {
        // y = y0 + v0y * t - 1/2 gt^2
        // y = original_position + velocity_in_the_direction * time - 1/2 gravity * time * time
        
        List<Vector3> points = new List<Vector3>();
        Vector3 origin = source.position;
        Vector3 startingVelocity = source.forward * shootForce;
        for (float t = 0; t < numPoints; t += timeBetweenPoints)
        {
            Vector3 newPoint = origin + t * startingVelocity;
            newPoint.y = origin.y + startingVelocity.y * t + gravity/2f * t * t;
            points.Add(newPoint);

            if(Physics.OverlapSphere(newPoint, .5f, layerMask).Length > 0)
            {
                break;
            }
        }

        return points.ToArray();
    }

    public virtual void Shoot() { }
}
