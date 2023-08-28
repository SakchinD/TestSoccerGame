using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class MainMenuScreen : BaseUI
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _resultButton;
    [SerializeField] private Button _policyButton;
    [SerializeField] private Button _quitButton;

    [Inject]
    private void Construct(UiController uiController)
    {
        _uiController = uiController;
    }

    protected override void Awake()
    {
        base.Awake();
        _startButton.onClick.AddListener(OnStartButtonClick);
        _resultButton.onClick.AddListener(OnResultButtonClick);
        _policyButton.onClick.AddListener(OnPolicyButtonClick);
        _quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    private void OnDestroy()
    {
        _startButton.onClick.RemoveAllListeners();
        _resultButton.onClick.RemoveAllListeners();
        _policyButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }

    private void OnStartButtonClick()
    {
        OnButtonClick(UiType.LevelSelect);
    }

    private void OnResultButtonClick()
    {
        OnButtonClick(UiType.Results);
    }

    private void OnPolicyButtonClick()
    {
        OnButtonClick(UiType.Policy);
    }

    private void OnQuitButtonClick()
    {
        Application.Quit();
    }

    private void OnButtonClick(UiType uiType)
    {
        SetCanvasGroupInteractable(false);

        _uiController.ToUi(uiType);

        SetCanvasGroupInteractable(true);
    }
}
