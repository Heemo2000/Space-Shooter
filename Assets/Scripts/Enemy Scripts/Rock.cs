using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : BaseEnemy
{
    [Min(0f)]
    [SerializeField]private float minRotationSpeed = 10f;
    
    [Min(0f)]
    [SerializeField]private float maxRotationSpeed = 30f;

    private float _targetRotationSpeed;

    private float _currentZRotation;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _targetRotationSpeed = Random.Range(minRotationSpeed,maxRotationSpeed);
    }

    protected override void FixedUpdate() 
    {
        base.FixedUpdate();
        _currentZRotation += _targetRotationSpeed * Time.fixedDeltaTime;
        base.enemyRB.MoveRotation(Quaternion.AngleAxis(_currentZRotation,Vector3.forward));
    }
}
