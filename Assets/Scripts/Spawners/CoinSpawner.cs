using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [Header("Coins Settings")]
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private float _screenHeight = 5f;

    public List<GameObject> _activeCoins;
    private Vector2 _spawnPosition;
    private float _spawnPos = 0;

    private bool _canSpawn;
    private void Start()
    {
        _spawnPos = _spawnDistance;
        SpawnCoin();
        _spawnPos = 0;
    }

    private void Update()
    {
        if(_activeCoins.Count < 3)
        {
            _canSpawn = true;
        }

        if (_player.position.x - 40 > _spawnPos && _canSpawn)
        {
            SpawnCoin();
            
        }

        if(_activeCoins.Count >= 3)
        {
            DeleteCoin();
        }
    }

    private void SpawnCoin()
    {
        var horSpavnPoint = _player.position.x + _spawnDistance;
        var vertSpavnPoint = Random.Range(-_screenHeight, _screenHeight);
        _spawnPosition = new Vector2(horSpavnPoint, vertSpavnPoint);
        GameObject nextCoin = Instantiate(_coinPrefab, _spawnPosition, transform.rotation);
        _activeCoins.Add(nextCoin);
        _spawnPos += _spawnDistance;
    }
    private void DeleteCoin()
    {
        Destroy(_activeCoins[0]);
        _activeCoins.RemoveAt(0);
    }


}
