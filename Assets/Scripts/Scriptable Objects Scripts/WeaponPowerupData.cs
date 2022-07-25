using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Weapon Powerup Data", fileName = "Weapon Powerup Data")]
public class WeaponPowerupData : ScriptableObject
{
    [Min(0f)]
    public float PowerUpActiveTime = 4.0f;

    public WeaponType WeaponType;
}
