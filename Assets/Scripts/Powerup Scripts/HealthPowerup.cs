using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : Powerup
{
    [Min(0f)]
    [SerializeField]private float healthFillAmount = 10f;
    public override void Pickup(GameObject target)
    {
        if(target.tag == StringHolder.PlayerTag)
        {
            Health health = target.transform.GetComponent<Health>();
            health.OnHealthHealed?.Invoke(healthFillAmount);
            Debug.Log("Health increased.");
            Destroy(gameObject);
        }
        
    }
}
