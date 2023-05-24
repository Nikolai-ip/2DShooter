using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrokeController : MonoBehaviour
{
    [SerializeField] private GameObject _stroke;
    private Enemy _instance;
    private ShootController _shotController;
    private void Start()
    {
        _instance = GetComponent<Enemy>();
        HideStroke();
        _shotController = FindObjectOfType<ShootController>();
        _shotController.OnTargeting += StrokeHandle;
    }
    private void OnDestroy()
    {
        _shotController.OnTargeting -= StrokeHandle;
    }
    private void StrokeHandle(Enemy enemy)
    {
        if (enemy == _instance && enemy != null)
        {
            ShowStroke();
        }
        else
        {
            HideStroke();
        }
    }
    public void ShowStroke()
    {
        _stroke?.SetActive(true);
    }
    public void HideStroke()
    {
        _stroke?.SetActive(false);
    }
}
