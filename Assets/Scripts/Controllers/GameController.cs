using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public enum GameMap
{
    Map1,
    Map2, 
    Map3
}

public class GameController : MonoBehaviour
{
    [SerializeField] GameScreen gameScreen;
    [SerializeField] private Map[] _maps;
    [SerializeField] private MapSettings[] _mapSettings;

    private Player _player;
    private ObjectPoolController _poolController;

    private List<Ball> _balls = new();

    private Map _currentMap;
    private float _cameraHeight;
    private float _cameraWidth;
    private float _gameTimeSeconds;
    private float _currentTimeSeconds;
    private int _ballsInGameCount;
    private int _playerBallsCount;
    private bool InGame;

    [Inject]
    private void Construct(ObjectPoolController poolController,
        Player player)
    {
        _poolController = poolController;
        _player = player;
    }

    private void Awake()
    {
        _cameraHeight = 2f * Camera.main.orthographicSize;
        _cameraWidth = _cameraHeight * Camera.main.aspect;
    }

    public void ResetGame()
    {
        InGame = false;
        _playerBallsCount = 0;
        _ballsInGameCount = 0;
        if (_currentMap != null)
        {
            Destroy(_currentMap.gameObject);
            _currentMap = null;
        }

        _balls.ForEach(ball =>
        {
            ball.ResetBall();
        });

        _balls.Clear();
    }

    public void CreateGame(GameMap gameMap)
    {
        ResetGame();
        var mapSetting = _mapSettings.First(x => x.GameMap == gameMap);
        _gameTimeSeconds = mapSetting.GameTime * 60;
        _ballsInGameCount = mapSetting.BallsInGameCount;
        _player.transform.position = GetSpawnPosition(_player.PlayerRadius);
        _currentMap = Instantiate(_maps.First(x => x.GameMap == gameMap),Vector2.zero, Quaternion.identity, transform);
        CreateBalls();
        _currentTimeSeconds = _gameTimeSeconds;
        InGame = true;
    }

    private void Update()
    {
        if (InGame)
        {
            if (_currentTimeSeconds > 0)
            {
                _currentTimeSeconds -= Time.deltaTime;
                UpdateTime();
            }
            else
            {
                InGame = false;
                _currentTimeSeconds = 0;
                UpdateTime();
                SetWinPopup();
            }
        }
    }

    private void SetWinPopup()
    {
        gameScreen.SetWipPopap(_playerBallsCount);
    }

    private void UpdateTime()
    {
        gameScreen.UpdateTimeOnSlider(_currentTimeSeconds, _gameTimeSeconds);

        float minutes = Mathf.FloorToInt(_currentTimeSeconds / 60);
        float seconds = Mathf.FloorToInt(_currentTimeSeconds % 60);
        gameScreen.UpdateTimeOnText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    private void CreateBalls()
    {
        _balls.Clear();
        for (int i = 0; i < _ballsInGameCount; i++)
        {
            var ball = _poolController.GetPooledObject("Ball");
            ball.gameObject.SetActive(true);
            ball.transform.position = GetSpawnPosition(ball.BallColladerRadius);
            _balls.Add(ball);
            ball.OnPlayerCollided += OnPlayerCollectBall;
        }
    }

    private void OnPlayerCollectBall(Ball ball)
    {
        ball.OnPlayerCollided -= OnPlayerCollectBall;
        _balls.Remove(ball);
        _playerBallsCount++;
        if(_balls.Count == 0)
        {
            CreateBalls();
        }
    }

    private Vector2 GetSpawnPosition(float radius)
    {
        Vector2 spawnPosition = Vector2.zero;
        do
        {
            spawnPosition = GetRandomPos(radius);
        }
        while (Physics2D.OverlapCircle(spawnPosition, radius) != null);

        return spawnPosition;
    }

    private Vector2 GetRandomPos(float radius)
    {
        float randomX = Random.Range(-_cameraWidth / 2 + radius, _cameraWidth / 2 - radius);
        float randomY = Random.Range(-_cameraHeight / 2 + radius, _cameraHeight / 2 - radius);

        return new Vector2(randomX, randomY);
    }
}
