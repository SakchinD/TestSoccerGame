using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class TopbarView : BaseUI
{
    [SerializeField] private Button _backButton;
    [SerializeField] TMP_Text _topbarHeaderText;

    private GameController _gameController;
    private UiType _currentUiType;

    [Inject]
    private void Construct(UiController uiController,
        GameController gameController)
    {
        _uiController = uiController;
        _gameController = gameController;
    }

    public void SetCurrentUiType(UiType uiType)
    {
        _currentUiType = uiType;
    }

    public void SetTopbarHeaderText(string text)
    {
        _topbarHeaderText.text = text;
    }

    protected override void Awake()
    {
        base.Awake();
        _backButton.onClick.AddListener(OnBackClick);
    }

    private void OnDestroy()
    {
        _backButton.onClick.RemoveAllListeners();
    }

    private void OnBackClick()
    {
        SetCanvasGroupInteractable(false);

        if(_currentUiType == UiType.Game)
        {
            _gameController.ResetGame();
        }
        _uiController.ToPreviosUI();

        SetCanvasGroupInteractable(true);
    }
}
