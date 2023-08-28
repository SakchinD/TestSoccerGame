using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private GameMap _gameMap;

    public GameMap GameMap => _gameMap;
}
