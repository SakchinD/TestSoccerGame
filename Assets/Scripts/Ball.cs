using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Ball : MonoBehaviour
{
    public event Action<Ball> OnPlayerCollided;

    private CircleCollider2D _ballCollider;

    public float BallColladerRadius => _ballCollider.radius * 2;

    public void ResetBall()
    {
        OnPlayerCollided = null;
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        _ballCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerCollided?.Invoke(this);
            ResetBall();
        }
    }
}
