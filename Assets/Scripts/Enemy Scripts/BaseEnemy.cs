using UnityEngine;
public abstract class BaseEnemy : PoolObject,IDamageable
{
    [Min(0f)]
    [SerializeField]private float moveSpeed = 20f;

    [Min(0f)]
    [SerializeField]private float destroyTime = 10f;
    [Min(0f)]
    [SerializeField]private float damage = 10f;
    [SerializeField]private Powerup powerup;

    [Range(0f,1f)]
    [SerializeField]private float powerupSpawnProbability = 0.8f;

    [SerializeField]private ScoreTracker playerScoreData;
    private float _currentTime = 0f;
    private Health _health;
    protected Rigidbody enemyRB;

    protected virtual void Awake() 
    {
        enemyRB = GetComponent<Rigidbody>();    
        _health = GetComponent<Health>();
    }
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Reuse();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
       if(_currentTime >= destroyTime)
       {
            base.Destroy();
            return;
       }
       _currentTime += Time.deltaTime;
    }

    protected virtual void FixedUpdate() 
    {
        enemyRB.MovePosition(enemyRB.position + Vector3.right * -moveSpeed * Time.fixedDeltaTime);
    }

    protected virtual void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == gameObject.layer)
        {
            return;
        }
        Player damageable = other.GetComponent<Player>();
        if(damageable != null)
        {
            damageable.TakeDamage(damage);
            SoundManager.Instance.PlayAtPoint(SoundType.ExplosionSound,transform.position);
            base.Destroy();
        }    
    }

    public override void Reuse()
    {
        _currentTime = 0f;
    }
    public virtual void TakeDamage(float amount)
    {
        _health.OnHealthDamaged?.Invoke(amount);
        if(_health.CurrentAmount <= 0)
        {
            playerScoreData.IncreaseScore(amount);
            float probability = Random.Range(0f,1f);
            if(probability >= powerupSpawnProbability && powerup != null)
            {
                Instantiate(powerup,transform.position,Quaternion.identity);
            }
            SoundManager.Instance.PlayAtPoint(SoundType.ExplosionSound,transform.position);
            ExplosionManager.Instance?.GenerateExplosionEffect(transform.position,Quaternion.identity);
            base.Destroy();
        }
    }
}
