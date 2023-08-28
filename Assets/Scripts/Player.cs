using UnityEngine;

public enum PlayerMoveDirection
{
    Up, 
    Down, 
    Left, 
    Right
}

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidbody2D;
    private Vector2 cameraMin;
    private Vector2 cameraMax;
    private Bounds _playerBounds;

    public float PlayerRadius => GetRadius();

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        var camera = Camera.main;
        cameraMin = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        cameraMax = camera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        _playerBounds = transform.GetComponent<SpriteRenderer>().bounds;
    }

    public void MovePlayer(PlayerMoveDirection moveDirection)
    {
        switch (moveDirection)
        {
            case PlayerMoveDirection.Left:
                _rigidbody2D
                    .MovePosition(MoveLimitByCameraBoundaries(new Vector2(transform.position.x - _speed, 
                        transform.position.y)));
                break; 
            case PlayerMoveDirection.Right:
                _rigidbody2D
                    .MovePosition(MoveLimitByCameraBoundaries(new Vector2(transform.position.x + _speed, 
                        transform.position.y)));
                break; 
            case PlayerMoveDirection.Up:
                _rigidbody2D
                    .MovePosition(MoveLimitByCameraBoundaries(new Vector2(transform.position.x, 
                        transform.position.y + _speed)));
                break; 
            case PlayerMoveDirection.Down:
                _rigidbody2D
                    .MovePosition(MoveLimitByCameraBoundaries(new Vector2(transform.position.x, 
                        transform.position.y - _speed)));
                break;
            default:
                break;
        }
    }

    private Vector2 MoveLimitByCameraBoundaries(Vector2 newPos)
    {
        newPos.x = Mathf.Clamp(newPos.x, 
            cameraMin.x + _playerBounds.extents.x, 
            cameraMax.x - _playerBounds.extents.x);
        newPos.y = Mathf.Clamp(newPos.y, 
            cameraMin.y + _playerBounds.extents.y, 
            cameraMax.y - _playerBounds.extents.y);
        return newPos;
    }

    private float GetRadius()
    {
        return Mathf.Max(_playerBounds.size.x,
            _playerBounds.size.y);
    }
}
