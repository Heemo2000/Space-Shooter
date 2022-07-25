using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour,IDamageable
{
    [Range(5f,20f)]
    [SerializeField]private float moveSpeed = 10f;

    [Range(5f,30f)]
    [SerializeField]private float rotateAngle = 15f;
    
    [Range(5f,30f)]
    [SerializeField]private float rotationSpeed = 30f;

    private Rigidbody _playerRB;
    private Vector2 _moveInput;
    private bool _firePressed;
    private WeaponManager _weaponManager;

    private Health _health;
    private Shield _shield;

    public Health HealthComp { get => _health; }

    public Shield ShieldComp { get => _shield; }

    private void Awake() 
    {
        _playerRB = GetComponent<Rigidbody>();
        _weaponManager = GetComponent<WeaponManager>();
        _health = GetComponent<Health>();
        _shield = GetComponent<Shield>();    
    }

    private void Start() 
    {
        GameManager.Instance.OnGameOver.AddListener(DisablePlayer);    
    }
    private void Update() 
    {
        if(!GameManager.Instance.IsGamePlaying)
        {
            return;
        }
        if(_firePressed)
        {
            _weaponManager?.UseCurrentWeapon();
        }
    }

    private void FixedUpdate() 
    {
        if(!GameManager.Instance.IsGamePlaying)
        {
            return;
        }
        Move(_moveInput);   
    }

    public void Move(Vector2 moveInput)
    {
        _playerRB.AddForce(moveInput * moveSpeed,ForceMode.Impulse);
        Quaternion targetRotation = Quaternion.AngleAxis(moveInput.y * rotateAngle,Vector3.right);
        _playerRB.MoveRotation(Quaternion.Slerp(_playerRB.rotation,targetRotation,rotationSpeed * Time.fixedDeltaTime));    
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            _firePressed = !_firePressed;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>(); 
    }

    public void TakeDamage(float amount)
    {
        if(_shield.CurrentAmount > 0)
        {
            _shield.OnShieldDamaged?.Invoke(amount);
            return;
        }

        _health.OnHealthDamaged?.Invoke(amount);

        if(_health.CurrentAmount <= 0)
        {
            ExplosionManager.Instance?.GenerateExplosionEffect(transform.position,Quaternion.identity);
            SoundManager.Instance.PlayAtPoint(SoundType.ExplosionSound,transform.position);
            gameObject.SetActive(false);
            GameManager.Instance.OnGameOver?.Invoke();
        }
    }


    private void OnCollisionEnter(Collision other) {
        Powerup powerup = other.transform.GetComponent<Powerup>();
        powerup?.Pickup(gameObject);    
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameOver.RemoveListener(DisablePlayer);
    }

    private void DisablePlayer()
    {
        gameObject.SetActive(false);
    }
}
