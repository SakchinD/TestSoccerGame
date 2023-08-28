using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WinPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text _ballCountText;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _acceptButton;

    private IPlayersScoresSaveManager _playersScoresSaveManager;
    private UiController _uiController;
    private int _playerScore;

    [Inject]
    private void Consctruct(UiController uiController,
        IPlayersScoresSaveManager playersScoresSaveManager)
    {
        _uiController = uiController;
        _playersScoresSaveManager = playersScoresSaveManager;
    }

    private void Awake()
    {
        _acceptButton.onClick.AddListener(OnAcceptButtonClick);
    }

    private void OnDestroy()
    {
        _acceptButton.onClick.RemoveAllListeners();
    }

    public void SetPlayerScore(int score)
    {
        _playerScore = score;
        _ballCountText.text = $"{score} balls";
    }

    private void OnAcceptButtonClick()
    {
        _playersScoresSaveManager.SaveScore(_inputField.text, _playerScore);
        _uiController.ToUi(UiType.MainMenu);
        gameObject.SetActive(false);
    }
}
