using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Serialize Field properties
    [Tooltip("Enemy prefab to spawn")] [SerializeField] private GameObject _enemyPrefab;
    [Tooltip("Maximum number of clones to create and reuse for spawning")] [SerializeField] private int _maxClones = 10;
    [Tooltip("Time gap between successive sapwns")] [SerializeField] private float _spawnDelay = 0.5f;
    [Tooltip("Places where enemies will be spawned")] [SerializeField] private Transform[] _spawnLocations;
    [Tooltip("Object that will keep the enemies")] [SerializeField] private Transform _cloneParent;
    [Tooltip("Game manager object")] [SerializeField] private GameManager _gameManager;
    #endregion

    [SerializeField] private int _enemiesSpawned;

    public int EnemiesALive { get => _enemiesSpawned; set => _enemiesSpawned = value; }

    private GameObject[] _enemyClones;

    private void Awake() => CreateClones();

    private void OnEnable()
    {
        GameManager.OnWaveNoChange += Spawn;
        GameManager.OnScoreChange += DecrementEnemiesAlive;
    }

    private void OnDisable()
    {
        GameManager.OnWaveNoChange -= Spawn;
        GameManager.OnScoreChange -= DecrementEnemiesAlive;
    }

    private void DecrementEnemiesAlive()
    {
        _enemiesSpawned -= 1;

        if (_enemiesSpawned == 0)
            _gameManager.EndWave();
    }

    private void CreateClones()
    {
        _enemyClones = new GameObject[_maxClones];

        for (int i = 0; i < _maxClones; ++i)
        {
            _enemyClones[i] = Instantiate(_enemyPrefab, _cloneParent.position, Quaternion.identity, _cloneParent);
            _enemyClones[i].gameObject.SetActive(false);
        }
    }

    private void Spawn() => StartCoroutine(SpawnEnemies());

    private IEnumerator SpawnEnemies()
    {
        _enemiesSpawned = _gameManager.WaveNo + 3;

        for (int i = 0; i < _enemiesSpawned; ++i)
        {
            if (!_enemyClones[i].activeInHierarchy)
            {
                _enemyClones[i].transform.position = _spawnLocations[GetRandomSpawnPoint()].position;
                _enemyClones[i].transform.rotation = Quaternion.identity;
                _enemyClones[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(_spawnDelay);
            }
        }
    }

    private int GetRandomSpawnPoint() => UnityEngine.Random.Range(0, _spawnLocations.Length-1);
}
