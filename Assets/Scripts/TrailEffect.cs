using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;   
    [SerializeField] private float _startSpawnTime;
    [SerializeField] private float _trailDestroyTime;

    private float _spawnTime;

    private void FixedUpdate()
    {
        CreateTrail();
    }

    private void CreateTrail()
    {
        if (_spawnTime <= 0)
        {
            GameObject instance = (GameObject)Instantiate(_prefab, transform.position, Quaternion.identity);
            Destroy(instance, _trailDestroyTime);
            _spawnTime = _startSpawnTime;
        }
        else
        {
            _spawnTime -= Time.deltaTime;
        }
    }

}
