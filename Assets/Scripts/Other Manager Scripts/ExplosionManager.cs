using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : GenericSingleton<ExplosionManager>
{
    [SerializeField]private PoolObjectData explosionEffectData;


    private Pool _pool;

    private void Start() {
        _pool = new Pool(explosionEffectData);
    }    

    public void GenerateExplosionEffect(Vector3 position,Quaternion rotation)
    {
        _pool.ReuseObject(position,Quaternion.identity);
    }
}
