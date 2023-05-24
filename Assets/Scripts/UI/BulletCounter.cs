using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    int _maxBulletCount;
    Player _player;
    Weapon _weapon;
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _weapon = _player.GetComponent<ShootController>().CurrentWeapon;
        _weapon.OnBulletCountChanged += BulletCountTextHandler;
        BulletCountTextHandler(_weapon.MaxBulletCount);
    }
    private void BulletCountTextHandler(int bulletCount)
    {
        _text.text= $"{bulletCount}/{_weapon.MaxBulletCount}";
    }
}
