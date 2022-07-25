using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private Queue<PoolObject> _pooledObjects;

    public Pool(PoolObjectData poolObjectData,Transform parentTransform = null)
    {
        _pooledObjects = new Queue<PoolObject>();
        GameObject poolObjectsContainer = new GameObject(poolObjectData.actualObject.name + " Pool");
        if(parentTransform != null)
        {
            poolObjectsContainer.transform.parent = parentTransform;
        }
        for(int i = 0; i < poolObjectData.count; i++)
        {
            PoolObject poolObject = GameObject.Instantiate<PoolObject>(poolObjectData.actualObject);
            poolObject.Pool = this;
            poolObject.transform.parent = poolObjectsContainer.transform;
            poolObject.gameObject.SetActive(false);
            _pooledObjects.Enqueue(poolObject);    
        }
    }
    

    public PoolObject ReuseObject(Vector3 position,Quaternion rotation)
    {
        if(_pooledObjects.Count == 0)
        {
            return null;
        }
        PoolObject poolObject = _pooledObjects.Dequeue();
        poolObject.transform.position = position;
        poolObject.transform.rotation = rotation;
        poolObject.gameObject.SetActive(true);
        poolObject.Reuse();
        return poolObject;
    }

    public void ReturnObjectToPool(PoolObject poolObject)
    {
        if(poolObject == null)
        {
            Debug.LogError("Null object cannot be returned to pool!!");
            return;
        }

        if(!poolObject.Pool.Equals(this))
        {
            Debug.LogError(poolObject.name + " Pool not found!!");
            return;
        }

        _pooledObjects.Enqueue(poolObject);
    }
}
