using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleFireGun : BaseWeapon
{
    [Min(0.1f)]
    [SerializeField]private float fireInterval = 0.5f;
    
    [SerializeField]private PoolObjectData bulletData;
    [SerializeField]private Transform[] firePoints;
    private float _nextTimeToFire;
    private Pool _bulletPool;

    private void Awake() {
        _bulletPool = new Pool(bulletData);
    }
    private void Start() {
        _nextTimeToFire = 0.0f;
    }
    public override void Fire()
    {
         if(_nextTimeToFire < Time.time)
        {
            for(int i = 0; i < firePoints.Length; i++)
            {
                Bullet bullet = (Bullet)_bulletPool.ReuseObject(firePoints[i].position,Quaternion.identity);
                if(bullet != null)
                {
                    bullet.transform.right = firePoints[i].right;
                }   
            }
            SoundManager.Instance.PlayInstantly(SoundType.BulletShootSound,base.soundSource);
            _nextTimeToFire = Time.time + fireInterval;
        }       
    }

    public override WeaponType GetWeaponType()
    {
        return WeaponType.TripleFire;
    }
}
