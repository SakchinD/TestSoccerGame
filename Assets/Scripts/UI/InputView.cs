using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputView : MonoBehaviour
{
    [SerializeField] PlayerInputButton _leftInputButton;
    [SerializeField] PlayerInputButton _rightInputButton;
    [SerializeField] PlayerInputButton _upInputButton;
    [SerializeField] PlayerInputButton _downInputButton;

    private Player _player;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _leftInputButton.Init(PlayerMoveDirection.Left, _player);
        _rightInputButton.Init(PlayerMoveDirection.Right, _player);
        _upInputButton.Init(PlayerMoveDirection.Up, _player);
        _downInputButton.Init(PlayerMoveDirection.Down, _player);
    }
}
