using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPowerUp : Powerup
{
    [SerializeField]private WeaponPowerupData weaponPowerupData;

    public override void Pickup(GameObject target)
    {
        if(target.tag == StringHolder.PlayerTag)
        {
            WeaponManager weaponManager = target.transform.GetComponent<WeaponManager>();
            weaponManager?.ChangeToWeapon(weaponPowerupData.WeaponType,weaponPowerupData.PowerUpActiveTime);
            Destroy(gameObject);
        }
        
    }
}
