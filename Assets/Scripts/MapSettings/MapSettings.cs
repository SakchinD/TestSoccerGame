using UnityEngine;

[CreateAssetMenu]
public class MapSettings : ScriptableObject
{
    [SerializeField] private GameMap _gameMap;
    [SerializeField] private float _gameTime;
    [SerializeField] private int _ballsInGameCount;

    public GameMap GameMap => _gameMap;
    public float GameTime => _gameTime;
    public int BallsInGameCount => _ballsInGameCount;

}
