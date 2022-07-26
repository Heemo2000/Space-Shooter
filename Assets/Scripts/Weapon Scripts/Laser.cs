using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : BaseWeapon
{
    [Min(0f)]
    [SerializeField]private float maxLaserLength = 10f;
    [SerializeField]private Transform firePoint;
    [SerializeField]private bool showLaserDebug = true;
    [SerializeField]private LayerMask damageMask;
    [SerializeField]private LayerMask boundaryMask;

    [Min(50f)]
    [SerializeField]private float damage = 60f;
    private LineRenderer _laserIndicator;

    private float _previousTime;
    private float _currentTime;
    private bool _enableLaser;

    
    private void Awake() 
    {
        _laserIndicator = GetComponent<LineRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _previousTime = _currentTime = 0.0f;
        _enableLaser = false;
        _laserIndicator.enabled = _enableLaser;
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentTime > _previousTime)
        {
            _enableLaser = true;
            _previousTime = _currentTime;
        }
        else
        {
            _enableLaser = false;
        }

        HandleLaser();
    }

    public override void Fire()
    {
        _currentTime += Time.deltaTime;
    }

    public override WeaponType GetWeaponType()
    {
        return WeaponType.Laser;
    }

    private void HandleLaser()
    {
        if(_laserIndicator.enabled != _enableLaser)
        {
            _laserIndicator.enabled = _enableLaser;
        }
        
        if(_laserIndicator.enabled)
        {
            _laserIndicator.SetPosition(0,firePoint.position);
            _laserIndicator.SetPosition(1,firePoint.position + Vector3.right * maxLaserLength);
            
            
            SoundManager.Instance.PlayInstantly(SoundType.LaserSound,base.soundSource);
            if(Physics.BoxCast(firePoint.position,Vector3.one,firePoint.right,out RaycastHit hit,transform.rotation,maxLaserLength,damageMask))
            {
                Debug.Log("Laser hits " + hit.transform.name);
                IDamageable damageable = hit.transform.GetComponent<IDamageable>();
                damageable?.TakeDamage(damage);
            }
        }
    }


    private void OnDrawGizmos() 
    {
        if(firePoint == null || showLaserDebug == false)
        {
            return;
        }
        Gizmos.color = Color.green;
        Gizmos.DrawLine(firePoint.position,firePoint.position + Vector3.right * maxLaserLength);
        Gizmos.DrawLine(firePoint.position - Vector3.up * 0.5f,firePoint.position + Vector3.up * 0.5f);    
    }
}
