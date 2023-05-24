using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    private MoveController _moveController;
    private ShootController _shootController;
    private void Start()
    {
        _moveController = GetComponent<MoveController>();
        _shootController = GetComponent<ShootController>();
    }
    void FixedUpdate()
    {
        _moveController.Move(new Vector2(_joystick.Horizontal,_joystick.Vertical));
    }
    public void PointDownShootButton()
    {
        _shootController.EnableFireMode();
    }
    public void PointUpShootButton()
    {
        _shootController.DisableFireMode();
    }
}
