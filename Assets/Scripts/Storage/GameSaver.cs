using Assets.Scripts.Storage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    private Storage _storage;
    private GameData _gameData;
    [SerializeField] private Player _player;
    private void Start()
    {
        _storage = new Storage();
        Load();
    }   
    private void Save()
    {
        _storage.Save(_gameData);
    }
    private void OnApplicationQuit()
    {
        _gameData.playerPosition = _player.transform.position;
        Save();
    }
    private void Load()
    {
        _gameData = (GameData)_storage.Load(new GameData());
        _player.transform.position = _gameData.playerPosition;
    }
}
