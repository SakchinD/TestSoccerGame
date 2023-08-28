using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private UiController _uiController;
    [SerializeField] private ObjectPoolController _poolController;
    [SerializeField] private GameController _gameController;

    public override void InstallBindings()
    {  
        Container
            .Bind<Player>()
            .FromInstance(_player)
            .AsSingle();

        Container
            .BindInterfacesTo<PlayersScoresSaveManager>()
            .AsSingle();

        Container
            .BindInterfacesTo<PrivacyPolicyController>()
            .AsSingle();

        Container
            .Bind<UiController>()
            .FromInstance(_uiController)
            .AsSingle();

        Container
            .Bind<ObjectPoolController>()
            .FromInstance(_poolController)
            .AsSingle();

        Container
            .Bind<GameController>()
            .FromInstance(_gameController)
            .AsSingle();
    }
}