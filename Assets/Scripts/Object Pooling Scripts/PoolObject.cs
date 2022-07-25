using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    private Pool _pool;

    public Pool Pool { get => _pool; set => _pool = value;}
    public abstract void Reuse();

    
    protected virtual void Destroy()
    {
        this.gameObject.SetActive(false);
        _pool.ReturnObjectToPool(this);
    }
}
