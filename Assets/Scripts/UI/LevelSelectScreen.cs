using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class LevelSelectScreen : BaseUI
{
    [SerializeField] private Button _mup1Button;
    [SerializeField] private Button _mup2Button;
    [SerializeField] private Button _mup3Button;
    private GameController _gameController;

    [Inject]
    private void Construct(UiController uiController, 
        GameController gameController)
    {
        _uiController = uiController;
        _gameController = gameController;
    }

    protected override void Awake()
    {
        base.Awake();
        _mup1Button.onClick.AddListener(OnMap1ButtonClick);
        _mup2Button.onClick.AddListener(OnMap2ButtonClick);
        _mup3Button.onClick.AddListener(OnMap3ButtonClick);
    }

    private void OnDestroy()
    {
        _mup1Button.onClick.RemoveAllListeners();
        _mup2Button.onClick.RemoveAllListeners();
        _mup3Button.onClick.RemoveAllListeners();
    }

    private void OnMap1ButtonClick()
    {
        OnButtonClick(GameMap.Map1);
    }

    private void OnMap2ButtonClick()
    {
        OnButtonClick(GameMap.Map2);
    }

    private void OnMap3ButtonClick()
    {
        OnButtonClick(GameMap.Map3);
    }

    private void OnButtonClick(GameMap gameMap)
    {
        SetCanvasGroupInteractable(false);

        _gameController.CreateGame(gameMap);
        _uiController.ToUi(UiType.Game);

        SetCanvasGroupInteractable(true);
    }
}
