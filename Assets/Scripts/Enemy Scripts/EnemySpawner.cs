using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField]private PoolObjectData[] enemyPrefabsData;

    [Min(0.5f)]
    [SerializeField]private float minInterval = 2f;
    [Min(1.0f)]
    [SerializeField]private float maxInterval = 5f;

    [Min(0f)]
    [SerializeField]private float maxSpawnRange = 4f;
    private Coroutine _spawnCoroutine;

    private Pool[] _pools;
    
    private void Awake() 
    {
        _pools = new Pool[enemyPrefabsData.Length];
        for(int i = 0; i < enemyPrefabsData.Length; i++)
        {
            _pools[i] = new Pool(enemyPrefabsData[i]);
        }
    }
    void Start()
    {
        GameManager.Instance.OnGameStart.AddListener(StartSpawning);
    }

    public void StartSpawning()
    {
        if(_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }
        _spawnCoroutine = StartCoroutine(SpawnEnemies());
    }
    IEnumerator SpawnEnemies()
    {
        
        while(GameManager.Instance.IsGamePlaying)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * Random.Range(0,maxSpawnRange);
            int randomIndex = Random.Range(0,enemyPrefabsData.Length);
            _pools[randomIndex].ReuseObject(spawnPosition,Quaternion.identity);
            float timeInterval = Random.Range(minInterval,maxInterval);
            yield return new WaitForSeconds(timeInterval);
        }
        yield break;
    }
    private void OnDestroy() 
    {
        GameManager.Instance.OnGameStart.RemoveListener(StartSpawning);
    }
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position , transform.position + Vector3.up * maxSpawnRange);    
    }
}
