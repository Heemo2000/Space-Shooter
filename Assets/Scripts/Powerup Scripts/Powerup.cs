using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    [Min(0f)]
    [SerializeField]private float rotatingSpeed = 10f;

    [Min(0f)]
    [SerializeField]private float movingSpeed = 20f;

    [Min(0f)]
    [SerializeField]private float destroyTime = 3f;

    private float _currentTime;

    private void Start() {
        _currentTime = 0f;
    }
    private void Update() 
    {
        if(_currentTime >= destroyTime)
        {
            Destroy(gameObject);
            return;
        }

        _currentTime += Time.deltaTime;
    }
    private void FixedUpdate() 
    {
        transform.Translate(Vector3.right * -movingSpeed * Time.fixedDeltaTime,Space.World);
        transform.Rotate(Vector3.up * rotatingSpeed * Time.fixedDeltaTime,Space.World);
    }

    public abstract void Pickup(GameObject target);
}
