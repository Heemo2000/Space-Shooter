using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringEnemy : BaseEnemy
{
    [SerializeField]private SingleFireGun gun;

    [Min(0f)]
    [SerializeField]private float frontCheckDistance = 50f;
    [SerializeField]private Transform frontCheckPoint;
    // Update is called once per frame
    protected override void Update()
    {
        
       CheckInFront();
       base.Update();
    }

    private void CheckInFront()
    {
        if(!Physics.Raycast(frontCheckPoint.position,frontCheckPoint.right,frontCheckDistance,1 << gameObject.layer))
        {
            gun?.Fire();
        }
    }

    
    
}
