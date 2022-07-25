using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]private BaseWeapon[] weapons;
    [SerializeField]private BaseWeapon initialWeapon;
    private BaseWeapon _currentWeapon;

    private Coroutine _limitedWeaponCoroutine;

    private void Start() {
        for(int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }

        _currentWeapon = initialWeapon;
        _currentWeapon.gameObject.SetActive(true);
    }

    public void UseCurrentWeapon()
    {
        _currentWeapon?.Fire();
    }
    
    public void ChangeToWeapon(WeaponType type,float timeToUse)
    {
        for(int i = 0; i < weapons.Length; i++)
        {
            if(weapons[i].GetWeaponType() == type)
            {
                if(_limitedWeaponCoroutine != null)
                {
                    StopCoroutine(_limitedWeaponCoroutine);
                }
                _limitedWeaponCoroutine = StartCoroutine(LimitedWeaponCoroutine(weapons[i],timeToUse));
                break;        
            }
        }   
    }

    private IEnumerator LimitedWeaponCoroutine(BaseWeapon toWeapon,float timeToUse)
    {
        ChangeToWeapon(toWeapon);
        yield return new WaitForSeconds(timeToUse);
        ChangeToWeapon(initialWeapon);
    }
    private void ChangeToWeapon(BaseWeapon weapon)
    {
        _currentWeapon.gameObject.SetActive(false);
        _currentWeapon = weapon;
        _currentWeapon.gameObject.SetActive(true);
    }
}
