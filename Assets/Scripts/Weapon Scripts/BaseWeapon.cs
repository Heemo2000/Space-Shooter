using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField]protected AudioSource soundSource;
    public abstract void Fire();

    public abstract WeaponType GetWeaponType();
}
