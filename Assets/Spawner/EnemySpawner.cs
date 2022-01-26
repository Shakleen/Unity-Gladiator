using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static event Action WaveEnd;
    
    #region Serialize Field properties
    [Tooltip("Enemy prefab to spawn")] [SerializeField] private Enemy _enemyPrefab;
    [Tooltip("Maximum number of clones to create and reuse for spawning")] [SerializeField] private int _maxClones = 10;
    [Tooltip("Time gap between successive sapwns")] [SerializeField] private float _spawnDelay = 0.5f;
    [Tooltip("Places where enemies will be spawned")] [SerializeField] private Transform[] _spawnLocations;
    [Tooltip("Object that will keep the enemies")] [SerializeField] private Transform _cloneParent;
    #endregion

    [SerializeField] private int _enemiesSpawned;

    public int EnemiesALive { get => _enemiesSpawned; set => _enemiesSpawned = value; }

    private Enemy[] _enemyClones;

    private void Awake() => CreateClones();

    private void DecrementEnemiesAlive(int score)
    {
        _enemiesSpawned -= 1;

        if (_enemiesSpawned == 0)
            WaveEnd?.Invoke();
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

    private void Spawn(int waveNo) => StartCoroutine(SpawnEnemies(waveNo));

    private IEnumerator SpawnEnemies(int waveNo)
    {
        _enemiesSpawned = waveNo + 3;

        for (int i = 0; i < _enemiesSpawned; ++i)
        {
            if (!_enemyClones[i].gameObject.activeInHierarchy)
            {
                _enemyClones[i].transform.position = _spawnLocations[GetRandomSpawnPoint()].position;
                _enemyClones[i].transform.rotation = Quaternion.identity;
                _enemyClones[i].gameObject.SetActive(true);
                _enemyClones[i].Init();
                yield return new WaitForSeconds(_spawnDelay);
            }
        }
    }

    private int GetRandomSpawnPoint() => UnityEngine.Random.Range(0, _spawnLocations.Length-1);
}
