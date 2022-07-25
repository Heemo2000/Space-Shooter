using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolObject
{
    [Min(0f)]
    [SerializeField]private float moveSpeed = 10f;

    [Min(0f)]
    [SerializeField]private float destroyTime = 5f;

    [SerializeField]private float damage = 10f;
    private Rigidbody _bulletRB;
    private float _currentTime; 
    private void Awake() {
        _bulletRB = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        _currentTime = 0f;
    }

    private void Update() 
    {
        if(_currentTime >= destroyTime)
        {
            base.Destroy();
            return;
        }    
        _currentTime += Time.deltaTime;
    }
    private void FixedUpdate()
    {
      _bulletRB.MovePosition(_bulletRB.position + transform.right * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == gameObject.layer)
        {
            return;
        }

        IDamageable damageable = other.GetComponent<IDamageable>();
        damageable?.TakeDamage(damage);
        base.Destroy();
    }

    public override void Reuse()
    {
        _currentTime = 0f;
    }
}
