using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Serialize Field properties
    [SerializeField] private GameEvent _onWaveEnd;
    [SerializeField] private SessionData _session;
    [Tooltip("Enemy prefab to spawn")] [SerializeField] private Enemy _enemyPrefab;
    [Tooltip("Maximum number of clones to create and reuse for spawning")] [SerializeField] private int _maxClones = 10;
    [Tooltip("Time gap between successive sapwns")] [SerializeField] private float _spawnDelay = 0.5f;
    [Tooltip("Places where enemies will be spawned")] [SerializeField] private Transform[] _spawnLocations;
    [Tooltip("Object that will keep the enemies")] [SerializeField] private Transform _cloneParent;
    #endregion

    private int _enemiesSpawned;
    private Enemy[] _enemyClones;
    private WaitForSeconds _spawnDelayWFS;

    private void Awake()
    {
        CreateClones();
        _spawnDelayWFS = new WaitForSeconds(_spawnDelay);
    }
        

    public void DecrementEnemiesAlive()
    {
        _enemiesSpawned -= 1;

        if (_enemiesSpawned == 0)
            _onWaveEnd.Raise();
    }

    private void CreateClones()
    {
        _enemyClones = new Enemy[_maxClones];

        for (int i = 0; i < _maxClones; ++i)
        {
            _enemyClones[i] = Instantiate(_enemyPrefab, _cloneParent.position, Quaternion.identity, _cloneParent);
            _enemyClones[i].gameObject.SetActive(false);
        }
    }

    public void Spawn() => StartCoroutine(SpawnEnemies());

    private IEnumerator SpawnEnemies()
    {
        _enemiesSpawned = _session.Wave + 3;

        for (int i = 0; i < _enemiesSpawned; ++i)
        {
            if (!_enemyClones[i].gameObject.activeInHierarchy)
            {
                _enemyClones[i].transform.position = _spawnLocations[GetRandomSpawnPoint()].position;
                _enemyClones[i].transform.rotation = Quaternion.identity;
                _enemyClones[i].gameObject.SetActive(true);
                yield return _spawnDelayWFS;
            }
        }
    }

    private int GetRandomSpawnPoint() => UnityEngine.Random.Range(0, _spawnLocations.Length-1);
}
