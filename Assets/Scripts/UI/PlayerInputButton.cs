using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action<PlayerMoveDirection> OnButtonDown;

    private PlayerMoveDirection _moveDirection;
    private Player _player;
    private bool _pressed;

    public void Init(PlayerMoveDirection moveDirection, Player player)
    {
        _moveDirection = moveDirection;
        _player = player;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;
        OnButtonDown?.Invoke(_moveDirection);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;
    }

    private void FixedUpdate()
    {
        if( _pressed ) 
        {
            _player.MovePlayer(_moveDirection);
        }
    }

    private void OnDestroy()
    {
        OnButtonDown = null;
    }
}
