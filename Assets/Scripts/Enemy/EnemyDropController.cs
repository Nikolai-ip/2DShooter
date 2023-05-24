using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropController : MonoBehaviour
{
    [SerializeField] private GameObject[] _availableLoots;

    public void DropLoot()
    {
        Instantiate(_availableLoots[Random.Range(0, _availableLoots.Length)], transform.position, new Quaternion());
    }
}
