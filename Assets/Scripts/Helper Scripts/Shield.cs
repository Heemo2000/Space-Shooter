using System;
using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Min(0f)]
    [SerializeField]private float maxShieldAmount = 100f;

    [Min(0f)]
    [SerializeField]private float beforeRechargeInterval = 4f;
    
    [Min(0f)]
    [SerializeField]private float rechargingRate = 5f;

    private float _currentAmount;
    

    private bool _isRecharging;

    public float MaxShieldAmount { get => maxShieldAmount; }

    public float CurrentAmount { get => _currentAmount;}

    private Coroutine _beforeRechargeCoroutine;

    public Action<float> OnShieldDamaged;

    public Action<float,float> OnShieldAmountSet;

    // Start is called before the first frame update
    void Start()
    {
        OnShieldDamaged += ReduceCurrentShieldAmount;
        OnShieldAmountSet += SetCurrentShieldAmount;
        
        _currentAmount = maxShieldAmount;
        _isRecharging = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShieldAmount();
    }

    private void ReduceCurrentShieldAmount(float amount)
    {
        OnShieldAmountSet?.Invoke(_currentAmount - amount , maxShieldAmount);
        if(_beforeRechargeCoroutine != null)
        {
            StopCoroutine(_beforeRechargeCoroutine);
        }
        _beforeRechargeCoroutine = StartCoroutine(BeforeRecharge());
    }

    private void SetCurrentShieldAmount(float amount,float maxAmount)
    {
        _currentAmount = Mathf.Clamp(amount,0f,maxAmount);
    }
    
    private IEnumerator BeforeRecharge()
    {
        _isRecharging = false;
        yield return new WaitForSeconds(beforeRechargeInterval);
        _isRecharging = true;
    }

    private void UpdateShieldAmount()
    {
        if(_isRecharging)
        {
            if(_currentAmount >= maxShieldAmount)
            {
                _isRecharging = false;
                return;
            }
            OnShieldAmountSet?.Invoke(_currentAmount + rechargingRate * Time.deltaTime,maxShieldAmount);
        }
    }
}
