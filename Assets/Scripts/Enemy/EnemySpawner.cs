using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy _enemy;
    [SerializeField] private float _spawnDistanceX;
    [SerializeField] private float _spawnDistanceY;
    [SerializeField] private int _intialEnemyCount;
    private void Start()
    {
        for (int i = 0; i < _intialEnemyCount; i++)
        {
            Vector2 spawmPosition = new Vector2(Random.Range(-_spawnDistanceX / 2, _spawnDistanceX / 2), Random.Range(-_spawnDistanceY / 2, _spawnDistanceY / 2));
            Instantiate(_enemy, spawmPosition, new Quaternion());
        }

    }

}
