using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [Header("Enemy Settings")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private float _screenHeight = 5f;

    public List<GameObject> _activeEnemies;
    private Vector2 _spawnPosition;
    private float _spawnPos = 0;

    private bool _canSpawn;
    private void Start()
    {
        _spawnPos = _spawnDistance;
        SpawnEnemy();
        _spawnPos = 0;
    }

    private void Update()
    {
        if (_activeEnemies.Count < 4)
        {
            _canSpawn = true;
        }

        if (_player.position.x - 20 > _spawnPos && _canSpawn)
        {
            SpawnEnemy();

        }

        if (_activeEnemies.Count >= 4)
        {
            DeleteEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var horSpavnPoint = _player.position.x + Random.Range(_spawnDistance - 5, _spawnDistance + 5);
        var vertSpavnPoint = Random.Range(-_screenHeight, _screenHeight);
        _spawnPosition = new Vector2(horSpavnPoint, vertSpavnPoint);
        GameObject nextEnemy = Instantiate(_enemyPrefab, _spawnPosition, transform.rotation);
        _activeEnemies.Add(nextEnemy);
        _spawnPos += _spawnDistance;
    }
    private void DeleteEnemy()
    {
        Destroy(_activeEnemies[0]);
        _activeEnemies.RemoveAt(0);
    }


}
